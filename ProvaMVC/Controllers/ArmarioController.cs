using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaMVC.Data;
using ProvaMVC.Models;
using ProvaMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Controllers {
	public class ArmarioController : Controller {

		private readonly ProvaMVCContext _context;

		public ArmarioController(ProvaMVCContext context) {
			_context = context;
		}

		public IActionResult Index(Usuario usuario) {
			var armarios = _context.Armarios.ToList();
			for (int i = 0; i < armarios.Count; i++) {
				armarios[i].Compartimentos = _context.Compartimentos.Where(x => x.ArmarioId == (i + 1)).ToList();
				armarios[i].Livres = armarios[i].CompartimentosLivres();
			}

			var vw = new ArmarioFormViewModel { Usuario = usuario, Armarios = armarios };
			return View(vw);
		}

		public async Task<IActionResult> ArmariosAdm() {

			return View(await _context.Armarios.ToListAsync());
		}


        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var armario = await _context.Armarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armario == null) {
                return NotFound();
            }

            return View(armario);
        }

        // GET: Usuarios/Create
        public IActionResult Create() {


            return View();

        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,PontoX,PontoY")] Armario armario) {
            if (ModelState.IsValid) {
                _context.Add(armario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ArmariosAdm));
            }
            return View(armario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var armario = await _context.Armarios.FindAsync(id);
            if (armario == null) {
                return NotFound();
            }

            return View(armario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,PontoX,PontoY")] Armario armario) {
            if (id != armario.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    
                    _context.Armarios.Update(armario);
                    _context.SaveChanges();

                } catch (DbUpdateConcurrencyException) {
                    if (!ArmarioExists(armario.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ArmariosAdm));
            }
            return View(armario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var armario = await _context.Armarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armario == null) {
                return NotFound();
            }

            return View(armario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            var armario = await _context.Armarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

           
            _context.Armarios.Remove(armario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ArmariosAdm));
        }

        private bool ArmarioExists(int id) {
            return _context.Armarios.Any(e => e.Id == id);
        }
    }
}
