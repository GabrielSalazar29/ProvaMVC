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

		public Armario() {

		}

		public Armario(int id, string nome) {
			Id = id;
			Nome = nome;
		}

		public void AddCompartimentos() {
			for (int i = 0; i < 10; i++) {
				Compartimento compartimento = new Compartimento {Id = i+1, Status = (Enums.Status)1, Tamanho = "100x500cm" };
				Compartimentos.Add(compartimento);
			}
		}
	}
}
