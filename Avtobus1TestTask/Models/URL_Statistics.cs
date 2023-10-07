using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avtobus1TestTask.Models
{
    public class URL_Statistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required(ErrorMessage = "Enter the URL")]
        public string? LongURL { get; set; }
        public string? ShortURL { get; set; }
        public DateTime DateCreation { get; set; }
        public long NumberTransitions { get; set; }
    }
}