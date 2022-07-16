using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Application.Services
{
    public interface ITodoProvider
    {
        Task Add(Domain.Entities.Todo todo);
        Task Delete(Guid id);
        Task<List<Domain.Entities.Todo>> GetAll();
        Task<Domain.Entities.Todo> GetById(Guid id);
        Task Update(Domain.Entities.Todo todo);
    }
}