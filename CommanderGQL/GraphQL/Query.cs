using CommanderGQL.Models;
using CommanderGQL.Data;
using HotChocolate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Data;

namespace CommanderGQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
        {
            return context.Platforms;
        }
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommand([ScopedService] AppDbContext context)
        {
            return context.Commands;

        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<Employee> GetEmployee([ScopedService] AppDbContext context)
        {
            return context.Employees;

        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<UserLogin> GetUser([ScopedService] AppDbContext context)
        {
            return context.UserLogins;

        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<TokenSetting> GetToken([ScopedService] AppDbContext context)
        {
            return context.TokenSettings;

        }
    }
}
