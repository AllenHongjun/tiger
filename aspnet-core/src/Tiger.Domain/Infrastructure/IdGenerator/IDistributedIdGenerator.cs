using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.IdGenerator
{
    public interface IDistributedIdGenerator
    {
        long Create();
    }
}
