﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Volo.Abp.Sass.Editions
{   

    public interface IEditionDataSeeder
    {
        Task SeedDefaultEditionAsync();
    }
}
