using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Models.ViewModels {
	public class ArmarioFormViewModel {

		public ICollection<Armario> Armarios { get; set; }
		public Usuario Usuario { get; set; }

	}
}
