using System;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompraController : ControllerBase
    {
       private readonly IRepository _repository;
       private readonly ICompraRepository _compraRepository;

       public CompraController(IRepository repository, ICompraRepository compraRepository)
       {
           _repository = repository;
           _compraRepository = compraRepository;
       }

       [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var listaDeCompras = await _compraRepository.ObterTodos(incluirCliente: true, incluirCompraItem: true);
                return Ok(listaDeCompras);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todas as compras, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> ObterPelaCompraId(int id)
        {
            try
            {
                var compra = await _compraRepository.ObterPelaCompraId(id,incluirCliente: true, incluirCompraItem: true);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter a compra, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Compra compra)
        {
            try
            {
                _repository.Add(compra);

                if(await _repository.SaveChanges())
                {
                    return Ok(compra);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao cadastrar a compra, ocorreu o erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> Editar(int id, Compra compra)
        {
           try
           {
               var compraCadastrado = await _compraRepository.ObterPelaCompraId(id, incluirCliente: false, incluirCompraItem: false);

                if(compraCadastrado == null)
                {
                    return NotFound("Compra não localizado!");
                }

                _repository.Update(compra);

                if(await _repository.SaveChanges())
                {
                    return Ok(compra);
                }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao editar a compra, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
        } 

        [HttpDelete("id={id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var compraCadastrado = await _compraRepository.ObterPelaCompraId(id, incluirCliente: false, incluirCompraItem: false);

               if(compraCadastrado == null)
               {
                   return NotFound("Compra não localizado");
               } 

               _repository.Delete(compraCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok(
                       new {
                           message = "Compra removido"
                       }
                   );
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar a compra, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }
    }
}