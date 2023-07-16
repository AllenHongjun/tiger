﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Datas
{
    public class DataDictionaryDataSeeder : IDataDictionaryDataSeeder, ITransientDependency
    {
        public DataDictionaryDataSeeder(
            ICurrentTenant currentTenant, 
            IGuidGenerator guidGenerator, 
            IDataRepository dataRepository)
        {
            CurrentTenant=currentTenant;
            GuidGenerator=guidGenerator;
            DataRepository=dataRepository;
        }

        protected ICurrentTenant CurrentTenant { get; }

        protected IGuidGenerator GuidGenerator { get; }

        protected IDataRepository DataRepository { get; }

        public async Task<Data> SeedAsync(
            string name, 
            string code, 
            string displayName, 
            string description = "", 
            Guid? parentId = null, 
            Guid? tenantId = null, 
            bool isStatic = false, 
            CancellationToken cancellationToken = default)
        {
            using (CurrentTenant.Change(tenantId))
            {
                var data = await DataRepository.FindByNameAsync(name, cancellationToken: cancellationToken);

                if (data == null)
                {
                    data = new Data(
                        GuidGenerator.Create(),
                        name,
                        code,
                        displayName,
                        description,
                        parentId,
                        tenantId)
                    {
                        IsStatic = isStatic,
                    };

                    data = await DataRepository.InsertAsync(data, true);
                }

                return data;
            }
        }
    }
}
