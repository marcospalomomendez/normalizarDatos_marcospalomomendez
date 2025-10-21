
class Program
{
    static void Main()
    {
        List<string> datos = new List<string> {
                "Real Madrid;Barcelona;2-1;Liga;2025-10-12",
                "Atlético de Madrid;Sevilla;1-0;Liga;2025-10-13",
                "Barcelona;Valencia;3-2;Copa del Rey;2025-10-14",
                "Sevilla;Real Madrid;0-2;Liga;2025-10-15",
                "Valencia;Atlético de Madrid;1-1;Copa del Rey;2025-10-16",
                "Real Madrid;Atlético de Madrid;2-2;Liga;2025-10-17",
                "Barcelona;Sevilla;4-0;Liga;2025-10-18",
                "Valencia;Real Madrid;0-1;Copa del Rey;2025-10-19",
                "Atlético de Madrid;Barcelona;1-3;Liga;2025-10-20",
                "Sevilla;Valencia;2-2;Copa del Rey;2025-10-21"
            };

        var equipos = new List<Equipo>();
        var competiciones = new List<Competicion>();
        var partidos = new List<Partido>();

        int idEquipo = 1;
        int idCompeticion = 1;
        int idPartido = 1;

        foreach (var linea in datos)
        {
            var partes = linea.Split(';');
            var nombre1 = partes[0];
            var nombre2 = partes[1];
            var resultado = partes[2];
            var competicionNombre = partes[3];
            var fecha = DateOnly.Parse(partes[4]);

            // Equipo 1
            var eq1 = equipos.Find(e => e.Nombre == nombre1);
            if (eq1 == null)
            {
                eq1 = new Equipo { Id = idEquipo++, Nombre = nombre1 };
                equipos.Add(eq1);
            }

            // Equipo 2
            var eq2 = equipos.Find(e => e.Nombre == nombre2);
            if (eq2 == null)
            {
                eq2 = new Equipo { Id = idEquipo++, Nombre = nombre2};
                equipos.Add(eq2);
            }

            // Competición
            var comp = competiciones.Find(c => c.Nombre == competicionNombre);
            if (comp == null)
            {
                comp = new Competicion { Id = idCompeticion++, Nombre = competicionNombre };
                competiciones.Add(comp);
            }

            // Partido
            var partido = new Partido
            {
                Id = idPartido++,
                Equipo1Id = eq1.Id,
                Equipo2Id = eq2.Id,
                CompeticionId = comp.Id,
                Resultado = resultado,
                Fecha = fecha
            };
            partidos.Add(partido);
        }

        Console.WriteLine("Equipos:");
        foreach (var e in equipos)
        {
            Console.WriteLine($"{e.Id}: {e.Nombre}");
        }

        Console.WriteLine("\nCompeticiones:");
        foreach (var c in competiciones)
        {
            Console.WriteLine($"{c.Id}: {c.Nombre}");
        }

        Console.WriteLine("\nPartidos:");
        foreach (var p in partidos)
        {
            Console.WriteLine($"{p.Id}: Equipo1={p.Equipo1Id}, Equipo2={p.Equipo2Id}, Competicion={p.CompeticionId}, Resultado={p.Resultado}, Fecha={p.Fecha:dd/MM/yyyy}");
        }

    }
}
