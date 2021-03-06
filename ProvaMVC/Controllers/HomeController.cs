using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProvaMVC.Data;
using ProvaMVC.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;
		private readonly ProvaMVCContext _context;
		public HomeController(ILogger<HomeController> logger, ProvaMVCContext context) {
			_logger = logger;
			_context = context;
		}

		public IActionResult Index() {
			return View();
		}

		public IActionResult PageAdm() {
			return View();
		}

		public async Task<IActionResult> Create(Usuario usuario) {
			if (ModelState.IsValid) {
				var usuarioExiste = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == usuario.Email && x.Cpf == usuario.Cpf && x.Nome == usuario.Nome);
				if (usuarioExiste == null) {
					_context.Add(usuario);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index), "Armario", usuario);
				}

				return RedirectToAction(nameof(Index), "Armario", usuarioExiste);
			}
			return View();
		}
		public IActionResult Privacy() {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
