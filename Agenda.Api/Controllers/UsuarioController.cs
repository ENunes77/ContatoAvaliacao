using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Agenda.Api.Data;
using Agenda.Api.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Agenda.Api.Services;

[Route("usuarios")]
public class UsuarioController : Controller
{

    [HttpGet]
    [Route("")]
    //[Authorize(Roles="User")]
    public async Task<ActionResult<List<Usuario>>> Get([FromServices]DataContext context)
    {
        var users = await context.Usuarios.AsNoTracking().ToListAsync();
        return Ok(users);
    }

    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    //[Authorize(Roles="Admin")]
    public async Task<ActionResult<Usuario>> Post(
        [FromServices] DataContext context,
        [FromBody]Usuario model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try{
            context.Usuarios.Add(model);
            await context.SaveChangesAsync();

            model.Password = "";
            return Ok(model);
        }
        catch (Exception)
        {
            return BadRequest(new {message = "Não foi possível criar o usuário"});
        }
    }

    [HttpPost]
    [Route("login")]    
    public async Task<ActionResult<dynamic>> Authenticate(
        [FromServices]DataContext context,
        [FromBody]Usuario model)
    {
        var user = await context.Usuarios
                    .AsNoTracking()
                    .Where(x => x.Username == model.Username & x.Password == model.Password)
                    .FirstOrDefaultAsync();

        if (user == null)
            return NotFound(new {message = "Usuário ou senha invalidos"});

        var token = TokenService.GenereteToken(user);
        user.Password = "";
        
        return new {
            user = user,
            token = token
        };

    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Usuario>> Put(
        [FromServices] DataContext context,
        int id,
        [FromBody]Usuario model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != model.Id)
            return NotFound(new {message = "Usuario não encontrado"});

        try{
            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return model;
        }
        catch(Exception)
        {
            return BadRequest(new {message = "Não foi possível alterar o usuário !"});
        }
    }

}