using System;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SegmentoController : ControllerBase
    {
       private readonly IRepository _repository;
       private readonly ISegmentoRepository _segmentoRepository;

       public SegmentoController(IRepository repository, ISegmentoRepository segmentoRepository)
       {
           _repository = repository;
           _segmentoRepository = segmentoRepository;
       }

       [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var listaDeSegmentos = await _segmentoRepository.ObterTodos(incluirLivro: true);
                return Ok(listaDeSegmentos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todos os segmentos, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> ObterPeloSegmentoId(int id)
        {
            try
            {
                var segmento = await _segmentoRepository.ObterPeloSegmentoId(id,incluirLivro: true);
                return Ok(segmento);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter o segmento, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("livroid={livroId}")]
        public async Task<IActionResult> ObterTodosPeloLivroId(int livroId)
        {
            try
            {
                var listaDeSegmentos = await _segmentoRepository.ObterTodosPeloLivroId(livroId,incluirLivro: true);
                return Ok(listaDeSegmentos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todos segmentos pelo livro, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Segmento segmento)
        {
            try
            {
                _repository.Add(segmento);

                if(await _repository.SaveChanges())
                {
                    return Ok(segmento);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao cadastrar o segmento, ocorreu o erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> Editar(int id, Segmento segmento)
        {
           try
           {
               var segmentoCadastrado = await _segmentoRepository.ObterPeloSegmentoId(id, incluirLivro: false);

                if(segmentoCadastrado == null)
                {
                    return NotFound("Segmento não localizado!");
                }

                _repository.Update(segmento);

                if(await _repository.SaveChanges())
                {
                    return Ok(segmento);
                }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao editar o segmento, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
        } 

        [HttpDelete("id={id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var segmentoCadastrado = await _segmentoRepository.ObterPeloSegmentoId(id, incluirLivro: false);

               if(segmentoCadastrado == null)
               {
                   return NotFound("Segmento não localizado");
               } 

               _repository.Delete(segmentoCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok(
                       new {
                           message = "Segmento removido"
                       }
                   );
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar o segmento, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }
    }
}