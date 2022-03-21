using CommanderGQL.Data;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,
            [ScopedService] AppDbContext context, 
            [Service] ITopicEventSender eventSender,
            CancellationToken canellationToken)
            {
            var platform = new Platform
            {
                Name = input.Name
            };

            context.Platforms.Add(platform);
            await context.SaveChangesAsync(canellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, canellationToken);

            return new AddPlatformPayload(platform);
            }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input,
            [ScopedService] AppDbContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId

            };

            context.Commands.Add(command);
            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }

        private List<Employee> Employees = new List<Employee>
        {
            new Employee{
                Id = 1,
                FirstName = "Naveen",
                LastName = "Bommidi",
                Email = "naveen@gmail.com",
                Password="1234",
                PhoneNumber="8888899999"
            }
        };

        public string UserLogin([Service] IOptions<TokenSetting> tokenSettings, UserLogin login)
        {
            var currentUser = Employees.Where(_ => _.Email.ToLower() == login.Email.ToLower() &&
            _.Password == login.Password).FirstOrDefault();

            if (currentUser != null)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    issuer: tokenSettings.Value.Issuer,
                    audience: tokenSettings.Value.Audience,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(jwtToken);

            }
            return "";
        }
    }
}
