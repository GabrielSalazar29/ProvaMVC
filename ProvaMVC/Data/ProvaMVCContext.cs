using Microsoft.EntityFrameworkCore;
using ProvaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Data {
	public class ProvaMVCContext : DbContext{

		public ProvaMVCContext (DbContextOptions<ProvaMVCContext> options) : base(options) {

		}

		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Armario> Armarios { get; set; }
		public DbSet<Compartimento> Compartimentos { get; set; }

	}
}
