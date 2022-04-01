using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Todo.Domain;

namespace Todo.Application.Tests.Mappings.Fixtures
{
    public class TodoMapperFixture
    {
        public List<Domain.Todo> GenerateRandomTodos(int quantity)
        {
            var fakerTodo = new Faker<Domain.Todo>("pt_BR")
                .RuleFor(x => x.Id, x => x.Random.Guid())
                .RuleFor(x => x.Title, x => x.Lorem.Sentence())
                .RuleFor(x => x.Description, x => x.Lorem.Paragraphs(2))
                .RuleFor(x => x.Tags, x => x.Lorem.Words(6).ToList())
                .RuleFor(x => x.CreatedAt, x => x.Date.Recent(90))
                .RuleFor(x => x.ModifiedAt, x => DateTime.Now.AddHours(-1))
                .RuleFor(x => x.Status, x => x.PickRandom<TodoStatus>());

            return fakerTodo.Generate(quantity);
        }
    }
}