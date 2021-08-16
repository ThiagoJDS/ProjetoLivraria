using System;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministradorController : ControllerBase
    {
       private readonly IRepository _repository;
       private readonly IAdministradorRepository _administradorRepository;

       public AdministradorController(IRepository repository, IAdministradorRepository administradorRepository)
       {
           _repository = repository;
           _administradorRepository = administradorRepository;
       }

       [HttpGet]
       public async Task<IActionResult> ObterTodos()
       {
           try
           {
               var resultado = await _administradorRepository.ObterTodos();
               return Ok(resultado);
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao obter todos os administradores ocorreu um erro: {ex.Message}");
           }
       }

       [HttpGet("id={id}")]
       public async Task<IActionResult> ObterPeloAdministradorId(int id)
       {
           try
           {
               var resultado = await _administradorRepository.ObterPeloAdministradorId(id);
               return Ok(resultado);
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao obter o administrador, ocorreu um erro: {ex.Message}");
           }
       }

       [HttpPost]
       public async Task<IActionResult> Salvar(Administrador administrador)
       {
           try
           {
               _repository.Add(administrador);

               if(await _repository.SaveChanges())
               {
                   return Ok(administrador);
               }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao salvar o administrador, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
       } 

       [HttpPut("id={id}")]
       public async Task<IActionResult> Editar(int id, Administrador administrador)
       {
           try
           {
               var administradorCadastrado = await _administradorRepository.ObterPeloAdministradorId(id);

                if(administradorCadastrado == null)
                {
                    return NotFound("Administrador não localizado!");
                }

                _repository.Update(administrador);

                if(await _repository.SaveChanges())
                {
                    return Ok(administrador);
                }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao editar o administrador, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
       } 

       [HttpDelete("id={id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var administradorCadastrado = await _administradorRepository.ObterPeloAdministradorId(id);

               if(administradorCadastrado == null)
               {
                   return NotFound("Cliente não localizado");
               } 

               _repository.Delete(administradorCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok(
                       new {
                           message = "Administrador removido"
                       }
                   );
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar o administrador, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }
    }
}