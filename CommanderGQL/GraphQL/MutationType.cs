using CommanderGQL.GraphQL.UserLogins;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderGQL.GraphQL
{
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(_ => _.UserLogin(default, default))
            .Name("UserLogin")
            .Type<StringType>()
            .Argument("login", a => a.Type<UserLoginType>());
        }
    }
}
