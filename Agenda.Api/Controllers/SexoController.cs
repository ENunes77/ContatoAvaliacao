using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Api.Data;
using Agenda.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("Sexo")]
public class SexoController : ControllerBase
{
    
    [HttpGet]
    [Route("")]
    //[AllowAnonymous]        
    public async Task<ActionResult<List<Sexo>>> Get([FromServices]DataContext context)
    {
        var sexos = await context.Sexos.AsNoTracking().ToListAsync();
        return Ok(sexos);
    }


    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Sexo>> GetById(int id, [FromServices]DataContext context)
    {
        var sexo = await context.Sexos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (sexo == null)
        {
            return NotFound(new {message = "Sexo não localizado"});
        }
        return Ok(sexo);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<List<Sexo>>> Post(
        [FromBody]Sexo model,
        [FromServices]DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try 
        {
            context.Sexos.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch
        {
            return BadRequest(new {message = "Não foi possível criar o sexo !"});
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    //[Authorize(Roles="User")]
    public async Task<ActionResult<Sexo>> Put(
        int id,
        [FromBody]Sexo model,
        [FromServices]DataContext context)
    {
        if (model.Id != id)
            return NotFound(new {message = "Sexo não localizado !"});
       
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try{
            context.Entry<Sexo>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(model);
        }        
        catch
        {
            return BadRequest(new {message = "Não foi possível atualizar o sexo !"});
        }            

        

    }

    [HttpDelete]
    [Route("{id:int}")]
    //[Authorize(Roles="Admin")]
    public async Task<ActionResult<List<Sexo>>> Delete(
        int id,
        [FromServices]DataContext context
    )
    {
        var sexo = await context.Sexos.FirstOrDefaultAsync(x => x.Id == id);
        if (sexo == null)
            return NotFound(new {message = "Sexo não localizado"});
        
        try{
            context.Sexos.Remove(sexo);
            await context.SaveChangesAsync();
            return Ok(new {message = "Sexo removido"});
        }
        catch(Exception)
        {
            return BadRequest(new{message = "Não foi possivel excluir o Sexo"});
        }
        
    }

}