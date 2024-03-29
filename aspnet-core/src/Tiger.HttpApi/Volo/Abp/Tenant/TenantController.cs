﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp;
using Volo.Abp.TenantManagement;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Tenant
{
    ///// <summary>
    ///// 租户信息管理
    ///// </summary>
    //[RemoteService(Name = TenantManagementRemoteServiceConsts.RemoteServiceName)]
    //[Area("multi-tenancy")]
    //[ControllerName("Tenant")]
    //[Route("api/multi-tenancy/tenants")]
    //[ApiExplorerSettings(GroupName = "admin")]
    //public class TenantController : AbpController, ITigerTenantAppService
    //{   
    //    private readonly ITigerTenantAppService _tenantAppService;


    //    public TenantController(ITigerTenantAppService tigerTenantAppService)
    //    {
    //        _tenantAppService=tigerTenantAppService;
    //    }


    //    /// <summary>
    //    /// 通过id查询租户信息
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <param name="includeDetails"></param>
    //    /// <returns></returns>
    //    [HttpGet]
    //    [Route("findById/{id}")]
    //    public TenantDto FindById(Guid id, bool includeDetails = true)
    //    {
    //        return _tenantAppService.FindById(id, includeDetails);
    //    }
    //}
}
