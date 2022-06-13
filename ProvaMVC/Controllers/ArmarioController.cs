using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Controllers {
	public class ArmarioController : Controller {
		public IActionResult Index() {

			return View();
		}
	}
}
