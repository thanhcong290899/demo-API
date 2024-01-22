/*
using DeMoAuthen.Models;
using DeMoAuthen.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeMoAuthen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRepoController : ControllerBase
    {
        private readonly IBookRepository _repo;

        public BookRepoController(IBookRepository repo) {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _repo.GetAll();
                return Ok(books);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _repo.GetById(id);
            if(book == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(book);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBook(BookModel model)
        {
            var book = await _repo.AddBook(model);
            return Ok(book);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> updateBook(int id,BookModel model)
        {
            await _repo.UpdateBook(model, id);
            return Ok();

        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _repo.DeleteBook(id);
          return Ok();

        }
    }
}
*/