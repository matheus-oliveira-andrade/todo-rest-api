using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Todo.Application.ViewModels;

namespace Todo.Application.Common;

[ExcludeFromCodeCoverage]
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Domain.Entities.Todo, TodoViewModel>().ReverseMap();
    }
}