using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LivroController  : ControllerBase
    {
       private readonly IRepository _repository;
       private readonly ILivroRepository _livroRepository;
       private readonly IWebHostEnvironment _hostEnvironment;

       public LivroController(IRepository repository, ILivroRepository livroRepository, IWebHostEnvironment hostEnvironment)
       {
           _repository = repository;
           _livroRepository = livroRepository;
           _hostEnvironment = hostEnvironment;
       }

       [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var listaDeLivros = await _livroRepository.ObterTodos(incluirSegmento : true, 
                                                                      incluirMarca : true,
                                                                      incluirAutor : true,
                                                                      incluirClassificacao : true,
                                                                      incluirGenero : true);
                return Ok(listaDeLivros);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todos os livros, ocorreu um erro: {ex.Message}");
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> ObterPeloLivroId(int id)
        {
            try
            {
                var livro = await _livroRepository.ObterPeloLivroId(id, incluirSegmento : true, 
                                                                        incluirMarca : true,
                                                                        incluirAutor : true,
                                                                        incluirClassificacao : true,
                                                                        incluirGenero : true);
                return Ok(livro);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter o livro, ocorreu um erro: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Salvar(Livro livro)
        {
            try
            {
                _repository.Add(livro);

                if(await _repository.SaveChanges())
                {
                    return Ok(livro);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao cadastrar o livro, ocorreu o erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> Editar(int id, Livro livro)
        {
           try
           {
               var livroCadastrado = await _livroRepository.ObterPeloLivroId(id, incluirSegmento : false, 
                                                                                 incluirMarca : false,
                                                                                 incluirAutor : false,
                                                                                 incluirClassificacao : false,
                                                                                 incluirGenero : false);

                if(livroCadastrado == null)
                {
                    return NotFound("Livro não localizado!");
                }

                _repository.Update(livro);

                if(await _repository.SaveChanges())
                {
                    return Ok(livro);
                }
           }
           catch (Exception ex)
           {
               return BadRequest($"Ao editar o livro, ocorreu um erro: {ex.Message}");
           }
           return BadRequest();
        } 

        [HttpDelete("id={id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
               var livroCadastrado = await _livroRepository.ObterPeloLivroId(id, incluirSegmento : false, 
                                                                                 incluirMarca : false,
                                                                                 incluirAutor : false,
                                                                                 incluirClassificacao : false,
                                                                                 incluirGenero : false);

               if(livroCadastrado == null)
               {
                   return NotFound("Livro não localizado");
               } 

               _repository.Delete(livroCadastrado);

               if(await _repository.SaveChanges())
               {
                   return Ok(
                       new {
                           message = "Livro removido"
                       }
                   );
               }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao deletar o livro, ocorreu um erro: {ex.Message}");
            }
            return BadRequest();
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                              .Take(10)
                                              .ToArray()
                                         ).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}