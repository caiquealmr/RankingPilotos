using RankingPilotos.Models;
using System.ComponentModel.DataAnnotations;

namespace RankingPilotos.Services
{
    public class PilotService
    {
        private List<Pilot> _pilots;

        public PilotService()
        {
            _pilots = GenerateSamplePilots();
        }

        public async Task<List<Pilot>> GetPilotsAsync()
        {
            await Task.Delay(100);
            return _pilots.OrderBy(p => p.MelhorVolta).ToList();
        }

        public async Task<List<Pilot>> GetFilteredPilotsAsync(
            Categoria? categoria = null,
            Sexo? sexo = null,
            string? classePeso = null,
            string? tracado = null,
            string? searchTerm = null)
        {
            await Task.Delay(50);

            var query = _pilots.AsQueryable();

            if (categoria.HasValue)
                query = query.Where(p => p.Categoria == categoria.Value);

            if (sexo.HasValue)
                query = query.Where(p => p.Sexo == sexo.Value);

            if (!string.IsNullOrEmpty(classePeso))
                query = query.Where(p => p.ClassePeso == classePeso);

            if (!string.IsNullOrEmpty(tracado))
                query = query.Where(p => p.Tracado == tracado);

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(p =>
                    p.Nome.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.Equipe.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            return query.OrderBy(p => p.MelhorVolta).ToList();
        }

        public async Task<Dictionary<string, List<Pilot>>> GetGroupedPilotsAsync(
            Categoria? categoria = null,
            Sexo? sexo = null,
            string? classePeso = null,
            string? tracado = null,
            string? searchTerm = null)
        {
            var pilots = await GetFilteredPilotsAsync(categoria, sexo, classePeso, tracado, searchTerm);

            return pilots
                .GroupBy(p => $"{GetCategoriaDisplay(p.Categoria)} - {GetSexoDisplay(p.Sexo)} - {p.Tracado}")
                .ToDictionary(g => g.Key, g => g.OrderBy(p => p.MelhorVolta).ToList());
        }

        public List<string> GetUniqueTracados()
        {
            return _pilots.Select(p => p.Tracado).Distinct().OrderBy(t => t).ToList();
        }

        public List<string> GetUniqueClassesPeso()
        {
            return new List<string> { "Leve", "Médio", "Pesado" };
        }

        private string GetCategoriaDisplay(Categoria categoria)
        {
            return categoria switch
            {
                Categoria.Formula1 => "Fórmula 1",
                Categoria.StockCar => "Stock Car",
                Categoria.IndyCar => "IndyCar",
                _ => categoria.ToString()
            };
        }

        private string GetSexoDisplay(Sexo sexo)
        {
            return sexo switch
            {
                Sexo.Masculino => "Masculino",
                Sexo.Feminino => "Feminino",
                _ => sexo.ToString()
            };
        }

        private List<Pilot> GenerateSamplePilots()
        {
            return new List<Pilot>
            {
                new Pilot { Id = 1, Nome = "Lewis Hamilton", Equipe = "Mercedes-AMG Petronas", MelhorVolta = TimeSpan.FromMilliseconds(78543), Categoria = Categoria.Formula1, Peso = 73.5, Sexo = Sexo.Masculino, Nacionalidade = "Reino Unido", Tracado = "Interlagos" },
                new Pilot { Id = 2, Nome = "Max Verstappen", Equipe = "Red Bull Racing", MelhorVolta = TimeSpan.FromMilliseconds(78234), Categoria = Categoria.Formula1, Peso = 72.0, Sexo = Sexo.Masculino, Nacionalidade = "Holanda", Tracado = "Interlagos" },
                new Pilot { Id = 3, Nome = "Charles Leclerc", Equipe = "Scuderia Ferrari", MelhorVolta = TimeSpan.FromMilliseconds(78789), Categoria = Categoria.Formula1, Peso = 68.0, Sexo = Sexo.Masculino, Nacionalidade = "Mônaco", Tracado = "Monza" },
                new Pilot { Id = 4, Nome = "Susie Wolff", Equipe = "Williams Racing", MelhorVolta = TimeSpan.FromMilliseconds(82156), Categoria = Categoria.Formula1, Peso = 58.0, Sexo = Sexo.Feminino, Nacionalidade = "Escócia", Tracado = "Spa-Francorchamps" },
                new Pilot { Id = 5, Nome = "Sébastien Loeb", Equipe = "Citroën Total", MelhorVolta = TimeSpan.FromMilliseconds(95432), Categoria = Categoria.Rally, Peso = 75.0, Sexo = Sexo.Masculino, Nacionalidade = "França", Tracado = "Monte Carlo" },
                new Pilot { Id = 6, Nome = "Danica Patrick", Equipe = "Stewart-Haas Racing", MelhorVolta = TimeSpan.FromMilliseconds(89765), Categoria = Categoria.IndyCar, Peso = 45.0, Sexo = Sexo.Feminino, Nacionalidade = "Estados Unidos", Tracado = "Indianapolis" },
                new Pilot { Id = 7, Nome = "Felipe Massa", Equipe = "Scuderia Ferrari", MelhorVolta = TimeSpan.FromMilliseconds(78901), Categoria = Categoria.Formula1, Peso = 69.0, Sexo = Sexo.Masculino, Nacionalidade = "Brasil", Tracado = "Interlagos" },
                new Pilot { Id = 8, Nome = "Rubens Barrichello", Equipe = "Brawn GP", MelhorVolta = TimeSpan.FromMilliseconds(79234), Categoria = Categoria.Formula1, Peso = 71.5, Sexo = Sexo.Masculino, Nacionalidade = "Brasil", Tracado = "Spa-Francorchamps" },
                new Pilot { Id = 9, Nome = "Ayrton Senna", Equipe = "McLaren Honda", MelhorVolta = TimeSpan.FromMilliseconds(77890), Categoria = Categoria.Formula1, Peso = 68.5, Sexo = Sexo.Masculino, Nacionalidade = "Brasil", Tracado = "Monza" },
                new Pilot { Id = 10, Nome = "Michael Schumacher", Equipe = "Scuderia Ferrari", MelhorVolta = TimeSpan.FromMilliseconds(78123), Categoria = Categoria.Formula1, Peso = 74.0, Sexo = Sexo.Masculino, Nacionalidade = "Alemanha", Tracado = "Monza" },
                new Pilot { Id = 11, Nome = "Kimi Räikkönen", Equipe = "Alfa Romeo Racing", MelhorVolta = TimeSpan.FromMilliseconds(78567), Categoria = Categoria.Formula1, Peso = 70.0, Sexo = Sexo.Masculino, Nacionalidade = "Finlândia", Tracado = "Spa-Francorchamps" },
                new Pilot { Id = 12, Nome = "Lando Norris", Equipe = "McLaren F1 Team", MelhorVolta = TimeSpan.FromMilliseconds(78890), Categoria = Categoria.Formula1, Peso = 68.0, Sexo = Sexo.Masculino, Nacionalidade = "Reino Unido", Tracado = "Silverstone" },
                new Pilot { Id = 13, Nome = "Tatiana Calderón", Equipe = "Alfa Romeo Racing", MelhorVolta = TimeSpan.FromMilliseconds(83245), Categoria = Categoria.Formula1, Peso = 52.0, Sexo = Sexo.Feminino, Nacionalidade = "Colômbia", Tracado = "Silverstone" },
                new Pilot { Id = 14, Nome = "Tony Kanaan", Equipe = "Chip Ganassi Racing", MelhorVolta = TimeSpan.FromMilliseconds(87654), Categoria = Categoria.IndyCar, Peso = 73.0, Sexo = Sexo.Masculino, Nacionalidade = "Brasil", Tracado = "Indianapolis" },
                new Pilot { Id = 15, Nome = "Bia Figueiredo", Equipe = "Stock Car Pro Series", MelhorVolta = TimeSpan.FromMilliseconds(92345), Categoria = Categoria.StockCar, Peso = 55.0, Sexo = Sexo.Feminino, Nacionalidade = "Brasil", Tracado = "Interlagos" },
                new Pilot { Id = 16, Nome = "Nelson Piquet Jr.", Equipe = "Piquet Sports", MelhorVolta = TimeSpan.FromMilliseconds(85500), Categoria = Categoria.StockCar, Peso = 78.0, Sexo = Sexo.Masculino, Nacionalidade = "Brasil", Tracado = "Interlagos" },
                new Pilot { Id = 17, Nome = "Maria de Villota", Equipe = "Marussia", MelhorVolta = TimeSpan.FromMilliseconds(88000), Categoria = Categoria.Formula1, Peso = 58.5, Sexo = Sexo.Feminino, Nacionalidade = "Espanha", Tracado = "Silverstone" },
                new Pilot { Id = 18, Nome = "Jenson Button", Equipe = "McLaren", MelhorVolta = TimeSpan.FromMilliseconds(79000), Categoria = Categoria.Formula1, Peso = 70.5, Sexo = Sexo.Masculino, Nacionalidade = "Reino Unido", Tracado = "Monza" },
                new Pilot { Id = 19, Nome = "Dan Wheldon", Equipe = "Andretti Autosport", MelhorVolta = TimeSpan.FromMilliseconds(86000), Categoria = Categoria.IndyCar, Peso = 74.0, Sexo = Sexo.Masculino, Nacionalidade = "Reino Unido", Tracado = "Indianapolis" },
                new Pilot { Id = 20, Nome = "Simona de Silvestro", Equipe = "HVM Racing", MelhorVolta = TimeSpan.FromMilliseconds(89000), Categoria = Categoria.IndyCar, Peso = 60.0, Sexo = Sexo.Feminino, Nacionalidade = "Suíça", Tracado = "Indianapolis" }
            };
        }
    }
}
