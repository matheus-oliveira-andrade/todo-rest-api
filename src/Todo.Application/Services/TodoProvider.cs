using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Data;

namespace Todo.Application.Services
{
    public class TodoProvider : ITodoProvider
    {
        private readonly ITodoRepository _todoRepository;

        public TodoProvider(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task Add(Domain.Todo todo)
        {
            await _todoRepository.Add(todo);
        }

        public async Task Update(Domain.Todo todo)
        {
            await _todoRepository.Update(todo);
        }

        public async Task<List<Domain.Todo>> GetAll()
        {
            return await _todoRepository.GetAll();
        }

        public async Task<Domain.Todo> GetById(Guid id)
        {
            return await _todoRepository.GetById(id);
        }  
        
        public async Task Delete(Guid id)
        {            
            if (await Exist(id))
                throw new Exception("Todo not found");

            await _todoRepository.Delete(id);
        }

        private async Task<bool> Exist(Guid id) => await GetById(id) != null;
    }
}
