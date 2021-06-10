using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =2)]
        public string Name { get; set; }
    }
}