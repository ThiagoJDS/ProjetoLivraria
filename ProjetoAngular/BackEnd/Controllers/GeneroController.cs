using System;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneroController : ControllerBase
    {
       private readonly IRepository _repository;
       private readonly IGeneroRepository _generoRepository;

       public GeneroController(IRepository repository, IGeneroRepository generoRepository)
       {
           _repository = repository;
           _generoRepository = generoRepository;
       }

       [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var listaDeGeneros = await _generoRepository.ObterTodos(incluirLivro: true);
                return Ok(listaDeGeneros);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todos os generos, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> ObterPeloGeneroId(int id)
        {
            try
            {
                var genero = await _generoRepository.ObterPeloGeneroId(id,incluirLivro: true);
                return Ok(genero);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter o genero, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("livroid={livroId}")]
        public async Task<IActionResult> ObterTodosPeloLivroId(int livroId)
        {
            try
            {
                var listaDeGeneros = await _generoRepository.ObterTodosPeloLivroId(livroId,incluirLivro: true);
                return Ok(listaDeGeneros);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todos os generos pelo livro, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Genero genero)
        {
            try
            {
                _repository.Add(genero);

                if(await _repository.SaveChanges())
                {
                    return Ok(genero);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao cadastrar o genero, ocorreu o erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> Editar(int id, Genero genero)
        {
           try
           {
               var generoCadastrado = await _generoRepository.ObterPeloGeneroId(id, incluirLivro: false);

                if(generoCadastrado == null)
                {
                    return NotFound("Genero não localizado!");
                }

                _repository.Update(genero);

                if(await _repository.SaveChanges())
                {
                    return Ok(genero);
                }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao editar o genero, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
        } 

        [HttpDelete("id={id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var generoCadastrado = await _generoRepository.ObterPeloGeneroId(id, incluirLivro: false);

               if(generoCadastrado == null)
               {
                   return NotFound("Genero não localizado");
               } 

               _repository.Delete(generoCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok(
                       new {
                           message = "Genero removido"
                       }
                   );
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar o genero, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }
    }
}