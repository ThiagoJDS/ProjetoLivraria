using System;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IRepository repository, IClienteRepository clienteRepository)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var resultado = await _clienteRepository.ObterTodos();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todos os clientes, ocorreu o erro: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> ObTerPeloClienteId(int id)
        {
            try
            {
                var resultado = await _clienteRepository.ObterPeloClienteId(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter o cliente, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Cliente cliente)
        {
            try
            {
                _repository.Add(cliente);

                if(await _repository.SaveChanges())
                {
                    return Ok(cliente);
                }     
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao salvar o cliente, ocorreu o erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> Editar(int id, Cliente cliente)
        {
            try
            {
                var clienteCadastrado = await _clienteRepository.ObterPeloClienteId(id);

                if(clienteCadastrado == null)
                {
                    return NotFound("Cliente não localizado!");
                }

                _repository.Update(cliente);

                if(await _repository.SaveChanges())
                {
                    return Ok(cliente);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao editar o cliente ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var clienteCadastrado = await _clienteRepository.ObterPeloClienteId(id);

               if(clienteCadastrado == null)
               {
                   return NotFound("Cliente não localizado");
               } 

               _repository.Delete(clienteCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok(
                       new {
                           message = "Cliente removido"
                       }
                   );
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar o cliente, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }
    }
}