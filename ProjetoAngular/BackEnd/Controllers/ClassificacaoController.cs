using System;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassificacaoController : ControllerBase
    {
       private readonly IRepository _repository;
       private readonly IClassificacaoRepository _classificacaoRepository;

       public ClassificacaoController(IRepository repository, IClassificacaoRepository classificacaoRepository)
       {
           _repository = repository;
           _classificacaoRepository = classificacaoRepository;
       }

       [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var listaDeClassificacao = await _classificacaoRepository.ObterTodos(incluirLivro: true);
                return Ok(listaDeClassificacao);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todas as classificação, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> ObterPelaClassificacaoId(int id)
        {
            try
            {
                var classificacao = await _classificacaoRepository.ObterPelaClassificacaoId(id, incluirLivro: true);
                return Ok(classificacao);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter a classificacao, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("livroid={livroId}")]
        public async Task<IActionResult> ObterTodosPeloLivroId(int livroId)
        {
            try
            {
                var listaDeClassificacao = await _classificacaoRepository.ObterTodosPeloLivroId(livroId,incluirLivro: true);
                return Ok(listaDeClassificacao);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter as classificacões pelo livro, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Classificacao classificacao)
        {
            try
            {
                _repository.Add(classificacao);

                if(await _repository.SaveChanges())
                {
                    return Ok(classificacao);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao cadastrar a classificacao, ocorreu o erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> Editar(int id, Classificacao classificacao)
        {
           try
           {
               var classificacaoCadastrado = await _classificacaoRepository.ObterPelaClassificacaoId(id, incluirLivro: false);

                if(classificacaoCadastrado == null)
                {
                    return NotFound("Classificacao não localizado!");
                }

                _repository.Update(classificacao);

                if(await _repository.SaveChanges())
                {
                    return Ok(classificacao);
                }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao editar a classificacao, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
        } 

        [HttpDelete("id={id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var classificacaoCadastrado = await _classificacaoRepository.ObterPelaClassificacaoId(id, incluirLivro: false);

               if(classificacaoCadastrado == null)
               {
                   return NotFound("Classificacao não localizado");
               } 

               _repository.Delete(classificacaoCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok(
                       new {
                           message = "Classificacao removido"
                       }
                   );
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar a classificacao, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }
    }
}