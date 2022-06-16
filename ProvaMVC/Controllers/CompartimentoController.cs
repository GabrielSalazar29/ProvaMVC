using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaMVC.Data;
using ProvaMVC.Models;
using ProvaMVC.Models.Enums;
using ProvaMVC.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Controllers {
	public class CompartimentoController : Controller {
		private readonly ProvaMVCContext _context;

		public CompartimentoController(ProvaMVCContext context) {
			_context = context;
		}

		public async Task<IActionResult> Index(int armarioId, int usuarioId) {
			var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
			var compartimentos = await _context.Compartimentos.Where(x => x.ArmarioId == armarioId).ToListAsync();

			var vw = new CompartimentoViewModel { Usuario = usuario, Compartimentos = compartimentos };

			return View(vw);
		}

		public async Task<IActionResult> CompartimentosAdm() {

			return View(await _context.Compartimentos.ToListAsync());
		}

		public async Task<IActionResult> Reserva(int compartimentoId, int usuarioId) {
			var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
			if (usuario.ArmarioId == null) {

				var compartimento = await _context.Compartimentos.FirstOrDefaultAsync(x => x.Id == compartimentoId);
				usuario.ArmarioId = compartimento.ArmarioId;
				usuario.CompartimentoId = compartimento.Id;
				compartimento.Status = Status.Ocupado;
			
				_context.Update(compartimento);
				_context.Update(usuario);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index), "Armario", usuario);
		}


		public async Task<IActionResult> Liberar(int compartimentoId, int usuarioId) {
			var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
			var compartimento = await _context.Compartimentos.FirstOrDefaultAsync(x => x.Id == compartimentoId);
			usuario.ArmarioId = null;
			usuario.CompartimentoId = null;
			compartimento.Status = Models.Enums.Status.Disponivel;

			_context.Update(compartimento);
			_context.Update(usuario);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index), "Armario", usuario);
		}
		public async Task<IActionResult> Voltar(int usuarioId) {
			var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);

			return RedirectToAction(nameof(Index), "Armario", usuario);
		}


        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var compartimento = await _context.Compartimentos.FirstOrDefaultAsync(m => m.Id == id);
            var armario = await _context.Armarios.FirstOrDefaultAsync(m => m.Id == compartimento.ArmarioId);
            var viewModel = new CompartimentoEArmarioViewModel { Armarios =  armario , Compartimento = compartimento };
            if (compartimento == null) {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Usuarios/Create
        public async Task<IActionResult> Create() {


            var armarios = await _context.Armarios.OrderBy(x => x.Nome).ToListAsync();
            var viewModel = new CompartimentoArmarioViewModel { Armarios = armarios };
            return View(viewModel);

        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,Tamanho,ArmarioId")] Compartimento compartimento) {
            if (ModelState.IsValid) {
                _context.Add(compartimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CompartimentosAdm));
            }
            return View(compartimento);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var compartimento = await _context.Compartimentos.FindAsync(id);
            var armarios = await _context.Armarios.OrderBy(x => x.Nome).ToListAsync();
            var viewModel = new CompartimentoArmarioViewModel { Armarios = armarios, Compartimento = compartimento };
            if (compartimento == null) {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Compartimento compartimento) {
            if (id != compartimento.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    var compartimentoBanco = await _context.Compartimentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == compartimento.Id);
                    var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.ArmarioId == compartimentoBanco.ArmarioId);
                    if (usuario != null) {
                        usuario.ArmarioId = compartimento.ArmarioId;
                        _context.Usuarios.Update(usuario);
                    }
                    compartimento.Status = compartimentoBanco.Status;
                    _context.Compartimentos.Update(compartimento);
                    await _context.SaveChangesAsync();

                } catch (DbUpdateConcurrencyException) {
                    if (!CompartimentoExists(compartimento.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CompartimentosAdm));
            }
            return View(compartimento);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var compartimento = await _context.Compartimentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compartimento == null) {
                return NotFound();
            }

            return View(compartimento);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            var compartimento = await _context.Compartimentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (compartimento.Status == Status.Ocupado) {
                var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.CompartimentoId == id);
                usuario.ArmarioId = null;
                usuario.CompartimentoId = null;
                _context.Update(usuario);
            }

            _context.Compartimentos.Remove(compartimento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CompartimentosAdm));
        }

        private bool CompartimentoExists(int id) {
            return _context.Compartimentos.Any(e => e.Id == id);
        }
    }
}
