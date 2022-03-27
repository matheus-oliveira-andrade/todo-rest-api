using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Application.Services
{
    public interface ITodoProvider
    {
        Task Add(API.Domain.Todo todo);
        Task Delete(Guid id);
        Task<List<API.Domain.Todo>> GetAll();
        Task<API.Domain.Todo> GetById(Guid id);
        Task Update(API.Domain.Todo todo);
    }
}