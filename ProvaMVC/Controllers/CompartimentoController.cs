using Microsoft.AspNetCore.Mvc;
using ProvaMVC.Data;
using ProvaMVC.Models;
using ProvaMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Controllers {
	public class CompartimentoController : Controller {
		private readonly ProvaMVCContext _context;

		public CompartimentoController(ProvaMVCContext context) {
			_context = context;
		}

		public IActionResult Index(int armarioId, int usuarioId) {
			var usuario = _context.Usuarios.FirstOrDefault(x => x.Id == usuarioId);
			var compartimentos = _context.Compartimentos.Where(x => x.ArmarioId == armarioId).ToList();

			var vw = new CompartimentoViewModel { Usuario = usuario, Compartimentos = compartimentos };

			return View(vw);
		}

		public IActionResult Reserva(int compartimentoId, int usuarioId) {
			var usuario = _context.Usuarios.FirstOrDefault(x => x.Id == usuarioId);
			if (usuario.ArmarioId == null) {

				var compartimento = _context.Compartimentos.FirstOrDefault(x => x.Id == compartimentoId);
				usuario.ArmarioId = compartimento.ArmarioId;
				usuario.CompartimentoId = compartimento.Id;
				compartimento.Status = Models.Enums.Status.Ocupado;
			
				_context.Update(compartimento);
				_context.Update(usuario);
				_context.SaveChanges();
			}

			return RedirectToAction(nameof(Index), "Armario", usuario);
		}

		public IActionResult Liberar(int compartimentoId, int usuarioId) {
			var usuario = _context.Usuarios.FirstOrDefault(x => x.Id == usuarioId);
			var compartimento = _context.Compartimentos.FirstOrDefault(x => x.Id == compartimentoId);
			usuario.ArmarioId = null;
			usuario.CompartimentoId = null;
			compartimento.Status = Models.Enums.Status.Disponivel;

			_context.Update(compartimento);
			_context.Update(usuario);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index), "Armario", usuario);
		}
	}
}
