using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Data
{
    public interface ITodoRepository
    {
        public Task Add(API.Domain.Todo todo);
        public Task Delete(Guid id);
        Task Update(API.Domain.Todo todo);

        public Task<API.Domain.Todo> GetById(Guid id);
        public Task<List<API.Domain.Todo>> GetAll();
    }
}
