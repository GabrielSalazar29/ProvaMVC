using Microsoft.AspNetCore.Mvc;
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
	}
}
