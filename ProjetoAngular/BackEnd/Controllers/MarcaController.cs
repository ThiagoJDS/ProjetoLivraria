using System;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarcaController : ControllerBase
    {
       private readonly IRepository _repository;
       private readonly IMarcaRepository _marcaRepository;

       public MarcaController(IRepository repository, IMarcaRepository marcaRepository)
       {
           _repository = repository;
           _marcaRepository = marcaRepository;
       }

       [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var listaDeMarcas = await _marcaRepository.ObterTodos(incluirLivro: true);
                return Ok(listaDeMarcas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todas as marcas, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> ObterPelaMarcaId(int id)
        {
            try
            {
                var marca = await _marcaRepository.ObterPelaMarcaId(id,incluirLivro: true);
                return Ok(marca);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter a marca, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("livroid={livroId}")]
        public async Task<IActionResult> ObterTodosPeloLivroId(int livroId)
        {
            try
            {
                var listaDeMarcas = await _marcaRepository.ObterTodosPeloLivroId(livroId,incluirLivro: true);
                return Ok(listaDeMarcas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todas as marcas pelos livros, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Marca marca)
        {
            try
            {
                _repository.Add(marca);

                if(await _repository.SaveChanges())
                {
                    return Ok(marca);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao cadastrar a marca, ocorreu o erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> Editar(int id, Marca marca)
        {
           try
           {
               var marcaCadastrado = await _marcaRepository.ObterPelaMarcaId(id, incluirLivro: false);

                if(marcaCadastrado == null)
                {
                    return NotFound("Marca não localizado!");
                }

                _repository.Update(marca);

                if(await _repository.SaveChanges())
                {
                    return Ok(marca);
                }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao editar a marca, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
        } 

        [HttpDelete("id={id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var marcaCadastrado = await _marcaRepository.ObterPelaMarcaId(id, incluirLivro: false);

               if(marcaCadastrado == null)
               {
                   return NotFound("Marca não localizado");
               } 

               _repository.Delete(marcaCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok(
                       new {
                           message = "Marca removido"
                       }
                   );
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar a marca, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }
    }
}