using DeMoAuthen.Data;
using DeMoAuthen.Models;

namespace DeMoAuthen.Repository
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> GetAll();
        public Task<BookModel> GetById(int id);
        public Task<int> AddBook(BookModel model);
        public Task UpdateBook(BookModel model,int id);
        public Task DeleteBook(int id);
    }
}
