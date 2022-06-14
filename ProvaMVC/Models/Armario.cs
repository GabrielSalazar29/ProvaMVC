using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Models {
	public class Armario {
		public int Id { get; set; }
		public string Nome { get; set; }
		public double PontoX { get; set; }
		public double PontoY { get; set; }
		public ICollection<Compartimento> Compartimentos { get; set; } = new List<Compartimento>();
		public int Livres;

		public Armario() {

		}

		public Armario(int id, string nome, double pontoX, double pontoY) {
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
