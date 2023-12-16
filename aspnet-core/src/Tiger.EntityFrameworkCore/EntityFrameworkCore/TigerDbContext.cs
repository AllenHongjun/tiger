using Microsoft.EntityFrameworkCore;
using Tiger.Module.Exams;
using Tiger.Module.Notifications;
using Tiger.Module.QuestionBank;
using Tiger.Module.Schools;
using Tiger.Module.System.Area;
using Tiger.Module.System.Localization;
using Tiger.Module.System.Platform.Datas;
using Tiger.Module.System.Platform.Layouts;
using Tiger.Module.System.Platform.Menus;
using Tiger.Module.System.TextTemplate;
using Tiger.Module.TaskManagement;
using Tiger.Module.Train;
using Tiger.Volo.Abp.Identity.Post;
using Tiger.Volo.Abp.Sass.Editions;
using Tiger.Volo.Abp.Sass.Tenants;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace Tiger.EntityFrameworkCore
{

    /// <summary>
    /// 自定义的DbContext
    /// </summary>
    // 配置连接字符串选择
    [ConnectionStringName("Default")]
    public class TigerDbContext : AbpDbContext<TigerDbContext>
        , IIdentityDbContext
        , IPermissionManagementDbContext
        , IAuditLoggingDbContext
    {

        /* This is your actual DbContext used on runtime.
         * It includes only your entities.
         * It does not include entities of the used modules, because each module has already
         * its own DbContext class. If you want to share some database tables with the used modules,
         * just create a structure like done for AppUser.
         *
         * Don't use this DbContext for database migrations since it does not contain tables of the
         * used modules (as explained above). See TigerMigrationsDbContext for migrations.
         * 
         * 模块通常使用 ConnectionStringName attribute 为 DbContext 类关联一个唯一的连接字符串名称. 示例:
         */

        public DbSet<PermissionGrant> PermissionGrants { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        // 其他模块可以，身份认证模块一替换就无法启动 
        // FTL: should inherit/implement Volo.Abp.Identity.EntityFrameworkCore.IIdentityDbContext,

        //替换其他仓储 https://docs.abp.io/zh-Hans/abp/7.4/Entity-Framework-Core  仓储层直接不能继承原来的模块仓储，不然无法替换
        public DbSet<IdentityUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }

        public DbSet<IdentityClaimType> ClaimTypes { get; set; }

        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

        public DbSet<IdentitySecurityLog> IdentitySecurityLogs { get; set; }

        public DbSet<IdentityUserLogin> IdentityUserLogins { get; set; }


        // 如何扩展abp原有的租户表 迁移里面文档了解
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserSubscribe> UserSubscribes { get; set; }
        public DbSet<NotificationDefinitionGroupRecord> NotificationDefinitionGroupRecords { get; set; }
        public DbSet<NotificationDefinitionRecord> NotificationDefinitionRecords { get; set; }


        public DbSet<Post> Posts { get; set; }
        public DbSet<TextTemplate> TextTemplates { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Tiger.Module.System.Localization.Resource> Resources { get; set; }
        public DbSet<LanguageText> LanguageTexts { get; set; }


        public DbSet<Data> Datas { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<BackgroundJobInfo> BackgroundJobInfos { get; set; }
        public DbSet<BackgroundJobAction> BackgroundJobActions { get; set; }
        public DbSet<BackgroundJobLog> BackgroundJobLogs { get; set; }

        public DbSet<Region> Regions { get; set; }
        
        /// <summary>
        /// 学校管理
        /// </summary>
        public DbSet<School> Schools { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public DbSet<ClassInfo> ClassInfos { get; set; }
        /// <summary>
        /// 课程
        /// </summary>
        public DbSet<Course> Courses { get; set; }
        /// <summary>
        /// 试卷
        /// </summary>
        public DbSet<TestPaper> TestPapers { get; set; }
        
        /// <summary>
        /// 题目分类
        /// </summary>
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        /// <summary>
        /// 题目表
        /// </summary>
        public DbSet<Question> Questions { get; set; }
        /// <summary>
        /// 题目附件表
        /// </summary>
        public DbSet<QuestionAttachment> QuestionAttachments { get; set; }
        /// <summary>
        /// 题目答案
        /// </summary>
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        /// <summary>
        /// 组卷策略配置表
        /// </summary>
        public DbSet<TestPaperStrategy> TestPaperStrategies { get; set; }
        /// <summary>
        /// 试卷内容(题目)表
        /// </summary>
        public DbSet<TestPaperQuestion> TestPaperQuestions { get; set; }
        /// <summary>
        /// 考试
        /// </summary>
        public DbSet<Exam> Exams { get; set; }
        /// <summary>
        /// 考试人员表
        /// </summary>
        public DbSet<Examinee> Examiners { get; set; }
        /// <summary>
        /// 答卷表
        /// </summary>
        public DbSet<AnswerSheet> AnswerSheets { get; set; }
        /// <summary>
        /// 答卷明细表
        /// </summary>
        public DbSet<AnswerSheetDetail> AnswerSheetDetails { get; set; }
        /// <summary>
        /// 试卷大题
        /// </summary>
        public DbSet<TestPaperSection> TestPaperSections { get; set; }
        /// <summary>
        /// 实训平台
        /// </summary>
        public DbSet<TrainPlatform> TrainPlatforms { get; set; }
        /// <summary>
        /// 考试时间调整表
        /// </summary>
        public DbSet<ExamModifyTime> ExamModifyTimes { get; set; }
        /// <summary>
        /// 考试人员表(应试人；参加考试者)
        /// </summary>
        public DbSet<Examinee> Examinees { get; set; }
        /// <summary>
        /// 试卷评委表
        /// </summary>
        /// <remarks>    评卷人只有关联了试卷才能改卷（默认只有超管能改卷）    </remarks>
        public DbSet<TestPaperJudge> TestPaperJudges { get; set; }

        public TigerDbContext(DbContextOptions<TigerDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// https://docs.microsoft.com/zh-cn/ef/core/dbcontext-configuration/#other-dbcontext-configuration
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        /// <summary>
        /// 重写 OnModelCreating 方法并且做一些配置
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Always call the base method
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            /* Configure your own tables/entities inside the ConfigureTiger method */

            builder.ConfigureTiger();
            builder.ConfigureTaskManagement();
            builder.ConfigureNotifications();
        }

        

    }
}
