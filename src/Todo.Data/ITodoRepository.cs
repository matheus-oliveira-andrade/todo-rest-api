using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Data
{
    public interface ITodoRepository
    {
        public Task Add(Models.Todo todo);
        public Task Delete(Guid id);
        Task Update(Models.Todo todo);

        public Task<Models.Todo> GetById(Guid id);
        public Task<List<Models.Todo>> GetAll();
    }
}
