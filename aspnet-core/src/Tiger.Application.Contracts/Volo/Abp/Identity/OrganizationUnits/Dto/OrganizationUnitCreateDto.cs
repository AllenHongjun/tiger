using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{
    public class OrganizationUnitCreateDto : OrganizationUnitCreateOrUpdateDtoBase
    {
        /// <summary>
        /// 父级id
        /// </summary>
        public Guid? ParentId { get; set; }

        //TODO:Consider custom coding later
        //[Required]
        //// there will be throw NullReferenceException  exception;see https://github.com/abpframework/abp/pull/4524;https://github.com/abpframework/abp/issues/5056
        ////[DynamicStringLength(typeof(OrganizationUnitConsts), nameof(OrganizationUnitConsts.MaxCodeLength))]
        //[DynamicStringLength(typeof(HelloOrganizationUnitConsts), nameof(HelloOrganizationUnitConsts.MaxCodeLength))]
        //public string Code { get; set; }
    }
}
