using System;
using System.Linq;

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

        var registros = datos.Select(d =>
        {
            var p = d.Split(';');
            return new
            {
                Equipo1 = p[0],
                Equipo2 = p[1],
                Resultado = p[2],
                Competicion = p[3],
                Fecha = DateOnly.Parse(p[4])
            };
        }).ToList();

        // Equipos
        var equipos = new List<Equipo>();
        int idEquipo = 1;
        foreach (var r in registros)
        {
            if (!equipos.Any(e => e.Nombre == r.Equipo1))
                equipos.Add(new Equipo { Id = idEquipo++, Nombre = r.Equipo1 });

            if (!equipos.Any(e => e.Nombre == r.Equipo2))
                equipos.Add(new Equipo { Id = idEquipo++, Nombre = r.Equipo2 });
        }

        // Competiciones
        var competiciones = new List<Competicion>();
        int idComp = 1;
        foreach (var r in registros)
        {
            if (!competiciones.Any(c => c.Nombre == r.Competicion))
                competiciones.Add(new Competicion { Id = idComp++, Nombre = r.Competicion });
        }

        // Partidos
        var partidos = registros.Select((r, i) => new Partido
        {
            Id = i + 1,
            Equipo1Id = equipos.First(e => e.Nombre == r.Equipo1).Id,
            Equipo2Id = equipos.First(e => e.Nombre == r.Equipo2).Id,
            CompeticionId = competiciones.First(c => c.Nombre == r.Competicion).Id,
            Resultado = r.Resultado,
            Fecha = r.Fecha
        }).ToList();


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
