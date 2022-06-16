using ProvaMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Models {
	public class Compartimento {
		public int Id { get; set; }
		public Status Status { get; set; } = Status.Disponivel;
		public string Tamanho { get; set; }
		public int ArmarioId { get; set; }

		public Compartimento() {

		}

		public Compartimento(int id, Status status, string tamanho, int armarioId) {
			Id = id;
			Status = status;
			Tamanho = tamanho;
			ArmarioId = armarioId;
		}
	}
}
