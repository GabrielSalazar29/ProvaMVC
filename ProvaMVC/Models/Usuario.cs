using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Models {
	public class Usuario {

		public int Id { get; set; }
		[Required(ErrorMessage = "{0} Obrigatório")]
		public string Nome { get; set; }
		[Required(ErrorMessage = "{0} Obrigatório")]
		[RegularExpression(@"^\d{11}$", ErrorMessage = "Insira um Cpf valido (digite 11 numeros)")]
		public string Cpf { get; set; }
		[Required(ErrorMessage = "{0} Obrigatório")]
		[EmailAddress(ErrorMessage = "Insira um Email valido")]
		public string Email { get; set; }
		public int? ArmarioId { get; set; }
		public int? CompartimentoId { get; set; }


		public Usuario() {
		}

		public Usuario(int id, string nome, string cpf, string email) {
			Id = id;
			Nome = nome;
			Cpf = cpf;
			Email = email;
		}
	}

}
