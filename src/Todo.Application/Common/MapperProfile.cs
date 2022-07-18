using AutoMapper;
using Todo.Application.ViewModels;

namespace Todo.Application.Common;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Domain.Entities.Todo, TodoViewModel>().ReverseMap();
    }
}