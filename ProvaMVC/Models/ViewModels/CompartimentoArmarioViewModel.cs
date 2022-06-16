using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Models.ViewModels {
	public class CompartimentoArmarioViewModel {

		public ICollection<Armario> Armarios { get; set; }
		public Compartimento Compartimento { get; set; }
	}
}
