﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Users;

namespace Tiger.Volo.Abp.Identity.Users
{
    /* This entity shares the same table/collection ("AbpUsers" by default) with the
     * IdentityUser entity of the Identity module.
     *
     * - You can define your custom properties into this class.
     * - You never create or delete this entity, because it is Identity module's job.
     * - You can query users from database with this entity.
     * - You can update values of your custom properties.
     */

    /// <summary>
    /// 用户实体，扩展abp框架自带的实体
    /// </summary>
    /// <remarks>
    /// ABP扩展实体：https://www.cnblogs.com/xhznl/p/13529942.html
    /// </remarks>
    public class AppUser : FullAuditedAggregateRoot<Guid>, IUser
    {
        #region Base properties

        /* These properties are shared with the IdentityUser entity of the Identity module.
         * Do not change these properties through this class. Instead, use Identity module
         * services (like IdentityUserManager) to change them.
         * So, this properties are designed as read only!
         */

        public virtual Guid? TenantId { get; private set; }

        public virtual string UserName { get; private set; }

        public virtual string Name { get; private set; }

        public virtual string Surname { get; private set; }

        public virtual string Email { get; private set; }

        public virtual bool EmailConfirmed { get; private set; }

        public virtual string PhoneNumber { get; private set; }

        public virtual bool PhoneNumberConfirmed { get; private set; }

        #endregion

        /* Add your own properties here. Example:
         *
         * public string MyProperty { get; set; }
         *
         * If you add a property and using the EF Core, remember these;
         *
         * 1. Update AbpDbContext.OnModelCreating
         * to configure the mapping for your new property
         * 2. Update AbpEfCoreEntityExtensionMappings to extend the IdentityUser entity
         * and add your new property to the migration.
         * 3. Use the Add-Migration to add a new database migration.
         * 4. Run the .DbMigrator project (or use the Update-Database command) to apply
         * schema change to the database.
         */

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 个人介绍
        /// </summary>
        public string Introduction { get; set; }

        private AppUser()
        {

        }
    }
}
