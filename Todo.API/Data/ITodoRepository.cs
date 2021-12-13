using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.API.Data
{
    public interface ITodoRepository
    {
        public Task Add(Domain.Todo todo);
        public Task Delete(Guid id);
        Task Update(Domain.Todo todo);

        public Task<Domain.Todo> GetById(Guid id);
        public Task<List<Domain.Todo>> GetAll();
    }
}
