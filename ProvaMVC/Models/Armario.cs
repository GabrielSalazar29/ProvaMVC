using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Models {
	public class Armario {
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} Obrigatório")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "{0} Obrigatório")]
		[RegularExpression(@"^-?\d+(\.\d{1,})?$", ErrorMessage = "Esse campo deve conter apenas numeros inteiros ou decimais separados por '.'")]
		[Display(Name = "Ponto X")]
		public string PontoX { get; set; }

		[Required(ErrorMessage = "{0} Obrigatório")]
		[RegularExpression(@"^-?\d+(\.\d{1,})?$", ErrorMessage = "Esse campo deve conter apenas numeros inteiros ou decimais separados por '.'")]
		[Display(Name = "Ponto Y")]
		public string PontoY { get; set; }
		public ICollection<Compartimento> Compartimentos { get; set; } = new List<Compartimento>();
		public int Livres;

		public Armario() {

		}

		public Armario(int id, string nome, string pontoX, string pontoY) {
			Id = id;
			Nome = nome;
			PontoX = pontoX;
			PontoY = pontoY;
		}

		public int CompartimentosLivres() {
			int soma = 0;
			foreach (var item in Compartimentos) {
				soma += (int)item.Status;
			}
			return soma;
		}
	}
}
