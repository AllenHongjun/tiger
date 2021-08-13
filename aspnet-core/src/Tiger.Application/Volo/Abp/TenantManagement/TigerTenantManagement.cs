/**
 * 类    名：TigerTenantManagement   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/13 9:17:38       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Volo.Abp.TenantManagement;
using Volo.Abp.Data;
using Volo.Abp.TenantManagement;

namespace Tiger.Volo.Abp.TenantManagement
{
    public class TigerTenantManagement : TenantAppService, ITigerTenantAppService
    {
        public TigerTenantManagement(
            ITenantRepository tenantRepository, 
            ITenantManager tenantManager, 
            IDataSeeder dataSeeder
            ) : base(tenantRepository, tenantManager, dataSeeder)
        {
        }
    }
}
