using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda.Api.Models
{
    [Table("Sexo")]
    public class Sexo{

        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [MaxLength(50, ErrorMessage= "O Descricao deve conter entre 4 e 50 caracteres" )]
        [MinLength(4, ErrorMessage = "O Descricao deve conter entre 4 e 50 caracteres")]        
        public string Descricao { get; set; }

        
    }
}