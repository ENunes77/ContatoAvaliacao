using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda.Api.Models
{
    [Table("Contato")]
    public class Contato
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]        
        [MinLength(4, ErrorMessage = "O Nome deve conter no mínimo 4 caracteres")]
        public string Nome { get; set; }

        [Required()]
        public DateTime DataNascimento { get; set; }

        public int SexoId{get;set;}
        
        public Sexo Sexo { get; set; }        

        public int Idade { get; set;}
        
            
            
         

        
    }
}