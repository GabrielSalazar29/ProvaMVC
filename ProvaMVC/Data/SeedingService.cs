using ProvaMVC.Models;
using ProvaMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaMVC.Data {
	public class SeedingService {

        private ProvaMVCContext _context;

        public SeedingService(ProvaMVCContext context) {

            _context = context;
        }

        public void Seed() {

            if (_context.Armarios.Any() || _context.Compartimentos.Any()) {
                return;
            }

            Armario a1 = new Armario(1, "Jaguaribe", 13.0103, 38.5328);
            Armario a2 = new Armario(2, "Piatã", 13.0103, 38.5328);
            Armario a3 = new Armario(3, "Pituba", 13.0103, 38.5328);
            Armario a4 = new Armario(4, "Barra", 13.0103, 38.5328);
            Armario a5 = new Armario(5, "Pituaçu", 13.0103, 38.5328);

            Compartimento c1 = new Compartimento(1, Status.Disponivel, "100x500cm", 1);
            Compartimento c2 = new Compartimento(2, Status.Disponivel, "100x500cm", 1);
            Compartimento c3 = new Compartimento(3, Status.Disponivel, "100x500cm", 1);
            Compartimento c4 = new Compartimento(4, Status.Disponivel, "100x500cm", 1);
            Compartimento c5 = new Compartimento(5, Status.Disponivel, "100x500cm", 1);
            Compartimento c6 = new Compartimento(6, Status.Disponivel, "100x500cm", 2);
            Compartimento c7 = new Compartimento(7, Status.Disponivel, "100x500cm", 2);
            Compartimento c8 = new Compartimento(8, Status.Disponivel, "100x500cm", 2);
            Compartimento c9 = new Compartimento(9, Status.Disponivel, "100x500cm", 2);
            Compartimento c10 = new Compartimento(10, Status.Disponivel, "100x500cm", 2);
            Compartimento c11 = new Compartimento(11, Status.Disponivel, "100x500cm", 3);
            Compartimento c12 = new Compartimento(12, Status.Disponivel, "100x500cm", 3);
            Compartimento c13 = new Compartimento(13, Status.Disponivel, "100x500cm", 3);
            Compartimento c14 = new Compartimento(14, Status.Disponivel, "100x500cm", 3);
            Compartimento c15 = new Compartimento(15, Status.Disponivel, "100x500cm", 3);
            Compartimento c16 = new Compartimento(16, Status.Disponivel, "100x500cm", 4);
            Compartimento c17 = new Compartimento(17, Status.Disponivel, "100x500cm", 4);
            Compartimento c18 = new Compartimento(18, Status.Disponivel, "100x500cm", 4);
            Compartimento c19 = new Compartimento(19, Status.Disponivel, "100x500cm", 4);
            Compartimento c20 = new Compartimento(20, Status.Disponivel, "100x500cm", 4);
            Compartimento c21 = new Compartimento(21, Status.Disponivel, "100x500cm", 5);
            Compartimento c22 = new Compartimento(22, Status.Disponivel, "100x500cm", 5);
            Compartimento c23 = new Compartimento(23, Status.Disponivel, "100x500cm", 5);
            Compartimento c24 = new Compartimento(24, Status.Disponivel, "100x500cm", 5);
            Compartimento c25 = new Compartimento(25, Status.Disponivel, "100x500cm", 5);


            _context.Armarios.AddRange(a1, a2, a3, a4, a5);

            _context.Compartimentos.AddRange(
                c1, c2, c3, c4, c5, c6, c7, c8, c9, c10,
                c11, c12, c13, c14, c15, c16, c17, c18, c19, c20,
                c21, c22, c23, c24, c25
            );
            _context.SaveChanges();
        }

    }
}
