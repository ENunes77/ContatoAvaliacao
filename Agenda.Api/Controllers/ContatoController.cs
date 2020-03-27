using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Agenda.Api.Data;
using Agenda.Api.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Agenda.Controllers
{
    
    [Route("Contatos")]
    public class EquipamentoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        ///[Authorize(Roles="Admin")]
        public async Task<ActionResult<List<Contato>>> Get([FromServices]DataContext context)
        {
            var contato = await context.Contatos.Include(x => x.Sexo).AsNoTracking().ToListAsync();
            return contato;

        }

        [HttpGet]
        [Route("{id:int}")]
        //[AllowAnonymous]
        public async Task<ActionResult<List<Contato>>> GetById(int id, [FromServices]DataContext context)
        {
            var contato = await context.Contatos.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (contato == null)
            {
                return NotFound(new {message = "Contato não localizado"});
            }
            return Ok(contato);
        }

        [HttpPost]
        [Route("")]
        //[Authorize(Roles="Admin")]
        public async Task<ActionResult<Contato>> Post(
            [FromBody]Contato model,
            [FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try 
            {
                context.Contatos.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new {message = "Não foi possível criar o contato !"});
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles="Admin")]
        public async Task<ActionResult<Contato>> Put(
            int id,
            [FromBody]Contato model,
            [FromServices]DataContext context)
        {

            if (id != model.Id)
            {
                return NotFound(new{ message = "Contato não localizado"});
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try 
            {
                context.Entry<Contato>(model).State = EntityState.Modified;                
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new {message = "Não foi possívewl atualizar o Contato !"});
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles="Funcionario")]
        public async Task<ActionResult<Contato>> Delete(
            int id,
            [FromServices]DataContext context
        )
        {
            var contato = await context.Contatos.FirstOrDefaultAsync(x => x.Id == id);
            if (contato == null)
                return NotFound(new {message = "Contato não localizado"});
            
            try{
                context.Contatos.Remove(contato);
                await context.SaveChangesAsync();
                return Ok(new {message = "Contato Removido"});
            }
            catch(Exception)
            {
                return BadRequest(new{message = "Não foi possivel excluir o contato"});
            }
            
        }
    }
}