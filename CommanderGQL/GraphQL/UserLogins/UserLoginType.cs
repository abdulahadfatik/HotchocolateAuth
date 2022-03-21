using CommanderGQL.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderGQL.GraphQL.UserLogins
{
    public class UserLoginType : ObjectType<UserLogin>
    {
        protected override void Configure(IObjectTypeDescriptor<UserLogin> descriptor)
        {
            descriptor.Field(_ => _.Email).Name("Email").Type<StringType>();
            descriptor.Field(_ => _.Password).Name("Password").Type<StringType>();
        }
    }
}
