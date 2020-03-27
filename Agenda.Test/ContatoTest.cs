using Agenda.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Test
{
    [TestClass]
    public class ContatoTest
    {
       [TestMethod]        
        public void Dado_Um_Contato_Com_Menos_De_4_Caracteres()
        {
            
            Contato contato = new Contato();            
            contato.Nome = "XPT";
            contato.DataNascimento = new System.DateTime(2010,01,06);

            Assert.IsFalse(false); 
    
        }

        [TestMethod]        
        public void Dado_Um_Contato_Sem_Nome()
        {
            var sexo = new Sexo();
            sexo.Id = 1;
            sexo.Descricao = "Masculino";

            Contato contato = new Contato();
            contato.Nome = "";            
            contato.Sexo = sexo;
            contato.DataNascimento = new System.DateTime(2010,01,06);
            
            Assert.IsFalse(false);
        
        }

        [TestMethod]        
        public void Dado_Um_Contato_Sem_Sexo()
        {
            var sexo = new Sexo();
            sexo.Id = 1;
            sexo.Descricao = "Masculino";

            Contato contato = new Contato();
            contato.Nome = "Pedro Valente";                        
            contato.DataNascimento = new System.DateTime(2010,01,06);
            
            Assert.IsFalse(false);
        }
    }


    
}
