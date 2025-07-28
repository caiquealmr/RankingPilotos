using System.ComponentModel.DataAnnotations;

namespace RankingPilotos.Models
{
    public class Pilot
    {
        public int Id { get; set; }
        
        [Required]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        public string Equipe { get; set; } = string.Empty;
        
        public TimeSpan MelhorVolta { get; set; }
        
        public string MelhorVoltaFormatada => $"{MelhorVolta.Minutes:D2}:{MelhorVolta.Seconds:D2}:{MelhorVolta.Milliseconds:D3}";
        
        [Required]
        public Categoria Categoria { get; set; }
        
        public double Peso { get; set; }
        
        public string ClassePeso => Peso switch
        {
            < 70 => "Leve",
            >= 70 and < 85 => "Médio",
            _ => "Pesado"
        };
        
        [Required]
        public Sexo Sexo { get; set; }
        
        [Required]
        public string Nacionalidade { get; set; } = string.Empty;
        
        [Required]
        public string Tracado { get; set; } = string.Empty;
        
    }

    public enum Categoria
    {
        Formula1,
        Endurance,
        Kart,
        StockCar,
        Rally,
        IndyCar
    }

    public enum Sexo
    {
        Masculino,
        Feminino
    }
}