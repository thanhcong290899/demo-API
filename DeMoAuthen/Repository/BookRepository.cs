using AutoMapper;
using DeMoAuthen.Data;
using DeMoAuthen.Models;
using Microsoft.EntityFrameworkCore;

namespace DeMoAuthen.Repository
{
    public class BookRepository : IBookRepository

    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookDbContext context ,IMapper mapper) {
        _context =context;
        _mapper = mapper;

        }
        public async Task<int> AddBook(BookModel model)
        {
            var book = _mapper.Map<BookDB>(model);
            _context.BookDBs.Add(book);
            await _context.SaveChangesAsync();
            return book.ID;
        }

        public async Task DeleteBook(int id)
        {
            var book = _context.BookDBs.SingleOrDefault(a => a.ID == id);
            if(book != null)
            {
                _context.BookDBs.Remove(book);
                await _context.SaveChangesAsync();  
            }
        }

        public async Task<List<BookModel>> GetAll()
        {
            var book = await _context.BookDBs.ToListAsync();
            return _mapper.Map<List<BookModel>>(book);
        }

        public async Task<BookModel> GetById(int id)
        {
           var book = await _context.BookDBs.FindAsync(id);
            return _mapper.Map<BookModel>(book);
        }

        public async Task UpdateBook(BookModel model, int id)
        {
            if(id== model.ID)
            {
                var book = _mapper.Map<BookDB>(model);
                _context.BookDBs.Update(book);
                await _context.SaveChangesAsync() ;
            }
        }
    }
}
