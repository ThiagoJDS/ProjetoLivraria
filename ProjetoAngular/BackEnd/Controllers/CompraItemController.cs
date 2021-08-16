using System;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompraItemController : ControllerBase
    {
       private readonly IRepository _repository;
       private readonly ICompraItemRepository _compraItemRepository;

       public CompraItemController(IRepository repository, ICompraItemRepository compraItemRepository)
       {
           _repository = repository;
           _compraItemRepository = compraItemRepository;
       }

       [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var listaDeCompraItems = await _compraItemRepository.ObterTodos(incluirLivro: true, incluirCompra: true);
                return Ok(listaDeCompraItems);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todos os compraItems, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> ObterPeloCompraItemId(int id)
        {
            try
            {
                var compraItem = await _compraItemRepository.ObterPeloCompraItemId(id, incluirLivro: true, incluirCompra: true);
                return Ok(compraItem);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter o compraItem, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(CompraItem compraItem)
        {
            try
            {
                _repository.Add(compraItem);

                if(await _repository.SaveChanges())
                {
                    return Ok(compraItem);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao cadastrar o compraItem, ocorreu o erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> Editar(int id, CompraItem compraItem)
        {
           try
           {
               var compraItemCadastrado = await _compraItemRepository.ObterPeloCompraItemId(id, incluirLivro: false, incluirCompra: false);

                if(compraItemCadastrado == null)
                {
                    return NotFound("CompraItem não localizado!");
                }

                _repository.Update(compraItem);

                if(await _repository.SaveChanges())
                {
                    return Ok(compraItem);
                }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao editar o compraItem, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
        } 

        [HttpDelete("id={id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var compraItemCadastrado = await _compraItemRepository.ObterPeloCompraItemId(id, incluirLivro: false, incluirCompra: false);

               if(compraItemCadastrado == null)
               {
                   return NotFound("CompraItem não localizado");
               } 

               _repository.Delete(compraItemCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok(
                       new {
                           message = "CompraItem removido"
                       }
                   );
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar o CompraItem, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }
    }
}