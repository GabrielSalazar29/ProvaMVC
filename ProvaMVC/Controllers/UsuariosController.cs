using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaMVC.Data;
using ProvaMVC.Models;
using ProvaMVC.Models.Enums;
using ProvaMVC.Models.ViewModels;

namespace ProvaMVC.Controllers {
	public class UsuariosController : Controller
    {
        private readonly ProvaMVCContext _context;

        public UsuariosController(ProvaMVCContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public async Task<IActionResult> Create()
        {
            var armarios = await _context.Armarios.OrderBy(x => x.Nome).ToListAsync();
            var viewModel = new ArmarioFormViewModel { Armarios = armarios };
            return View(viewModel);

        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cpf,Email,ArmarioId,CompartimentoId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExiste = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == usuario.Email && x.Cpf == usuario.Cpf && x.Nome == usuario.Nome);
                if (usuarioExiste != null) {
                    var armarios = await _context.Armarios.OrderBy(x => x.Nome).ToListAsync();
                    var viewModel = new ArmarioFormViewModel { Armarios = armarios, Usuario = usuario };
                    ViewData["Criado"] = "Usuario ja existe";
                    return View(viewModel);
                }
                    if (usuario.CompartimentoId != null) {
                    var compartimento = await _context.Compartimentos.FindAsync(usuario.CompartimentoId);
                    compartimento.Status = Status.Ocupado;
                    _context.Compartimentos.Update(compartimento);
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var armarios = await _context.Armarios.OrderBy(x => x.Nome).ToListAsync();
            var viewModel = new ArmarioFormViewModel { Armarios = armarios, Usuario = usuario };
            ViewBag.Comp = usuario.CompartimentoId;
            if (ViewBag.Comp == null) {

                ViewBag.Comp = -1;

            }
            return View(viewModel);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cpf,Email,ArmarioId,CompartimentoId")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == usuario.Id);
                    if (user.CompartimentoId != usuario.CompartimentoId) {
                        if (usuario.CompartimentoId != null && user.CompartimentoId == null) {
                            var compartimento = await _context.Compartimentos.FindAsync(usuario.CompartimentoId);
                            compartimento.Status = Status.Ocupado;
                            _context.Compartimentos.Update(compartimento);
						}else if(usuario.CompartimentoId != null && user.CompartimentoId != null) {
                            var compartimentoNovo = await _context.Compartimentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == usuario.CompartimentoId);
                            var compartimentoVelho = await _context.Compartimentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.CompartimentoId);
                            compartimentoNovo.Status = Status.Ocupado;
                            compartimentoVelho.Status = Status.Disponivel;
                            _context.Compartimentos.Update(compartimentoNovo);
                            _context.Compartimentos.Update(compartimentoVelho);
                        }                     
                        else {
                            var compartimento = await _context.Compartimentos.FindAsync(user.CompartimentoId);
                            compartimento.Status = Status.Disponivel;
                            _context.Compartimentos.Update(compartimento);
                        }
                    }
                    user = usuario;
                    _context.Usuarios.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (usuario.CompartimentoId != null) {

                var compartimento = await _context.Compartimentos.FindAsync(usuario.CompartimentoId);
                compartimento.Status = Status.Disponivel;
                _context.Compartimentos.Update(compartimento);

			}
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> LoadCompartimentos(int IdArmario) {
            var compartimentos = await _context.Compartimentos.Where(x => x.ArmarioId == IdArmario && x.Status == Status.Disponivel).ToListAsync();
 
            return Json(new { body = compartimentos });
        }

        [HttpPost]
        public async Task<IActionResult> LoadCompartimentosEdit(int IdArmario, int IdCompartimento) {
            var compartimentos = await _context.Compartimentos.Where(x => (x.ArmarioId == IdArmario && x.Status == Status.Disponivel) || (x.ArmarioId == IdArmario && x.Id == IdCompartimento)).ToListAsync();
            return Json(new { body = compartimentos });
        }
    }
}
