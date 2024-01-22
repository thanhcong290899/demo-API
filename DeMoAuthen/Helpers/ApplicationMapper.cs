using AutoMapper;
using DeMoAuthen.Data;
using DeMoAuthen.Models;

namespace DeMoAuthen.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() { 
        CreateMap<BookDB,BookModel>().ReverseMap();
                }
    }
}
