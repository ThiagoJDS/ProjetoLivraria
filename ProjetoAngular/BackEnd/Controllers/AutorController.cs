using System;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutorController : ControllerBase
    {

       private readonly IRepository _repository;
       private readonly IAutorRepository _autorRepository;

       public AutorController(IRepository repository, IAutorRepository autorRepository)
       {
           _repository = repository;
           _autorRepository = autorRepository;
       }

       [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var listaDeAutores = await _autorRepository.ObterTodos(incluirLivro: true);
                return Ok(listaDeAutores);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todos os autores, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> ObterPeloAutorId(int id)
        {
            try
            {
                var autor = await _autorRepository.ObterPeloAutorId(id,incluirLivro: true);
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter o autor, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("livroid={livroId}")]
        public async Task<IActionResult> ObterTodosPeloLivroId(int livroId)
        {
            try
            {
                var listaDeAutores = await _autorRepository.ObterTodosPeloLivroId(livroId,incluirLivro: true);
                return Ok(listaDeAutores);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter os autores pelo livro, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Autor autor)
        {
            try
            {
                _repository.Add(autor);

                if(await _repository.SaveChanges())
                {
                    return Ok(autor);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao cadastrar o autor, ocorreu o erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> Editar(int id, Autor autor)
        {
           try
           {
               var autorCadastrado = await _autorRepository.ObterPeloAutorId(id, incluirLivro: false);

                if(autorCadastrado == null)
                {
                    return NotFound("Autor não localizado!");
                }

                _repository.Update(autor);

                if(await _repository.SaveChanges())
                {
                    return Ok(autor);
                }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao editar o autor, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var autorCadastrado = await _autorRepository.ObterPeloAutorId(id, incluirLivro: false);

               if(autorCadastrado == null)
               {
                   return NotFound("Autor não localizado");
               } 

               _repository.Delete(autorCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok();
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar o autor, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }
    }
}