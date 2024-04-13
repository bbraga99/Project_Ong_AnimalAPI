using System.ComponentModel.DataAnnotations;

namespace Ong_AnimalAPI.DTOs
{
    public class AnimalDTO
    {
        public int AnimalID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
