using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Models.ViewModels {
	public class CompartimentoViewModel {

		public ICollection<Compartimento> Compartimentos { get; set; }
		public Usuario Usuario { get; set; }

	}
}
