using Tiger.Module.QuestionBank;
using Tiger.Module.Exams;
using Tiger.Module.Teachings;
using Tiger.Module.Schools;
using Tiger.Module.System.Area;
using Tiger.Module.System.Platform.Datas;
using Tiger.Module.System.Localization;
using Tiger.Module.System.TextTemplate;
using Tiger.Volo.Abp.Identity.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Tiger.Module.Train;

namespace Tiger.EntityFrameworkCore
{
    /// <summary>
    /// </summary>
    [DependsOn(
        typeof(TigerDomainModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
        )]
    public class TigerEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            TigerEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<TigerDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);

                options.AddRepository<Post, PostRepository>();
                options.AddRepository<TextTemplate, TextTemplateRepository>();
                options.AddRepository<Language, LanguageRepository>();
                options.AddRepository<Resource, ResourceRepository>();
                options.AddRepository<LanguageText, LanguageTextRepository>();
                options.AddRepository<Tiger.Module.System.Platform.Datas.Data, DataRepository>();
                options.AddRepository<Region, RegionRepository>();
                options.AddRepository<School, SchoolRepository>();
                options.AddRepository<ClassInfo, ClassInfoRepository>();
            options.AddRepository<Course, CourseRepository>();
            options.AddRepository<TestPaper, TestPaperRepository>();
            options.AddRepository<QuestionCategory, QuestionCategoryRepository>();
            options.AddRepository<Question, QuestionRepository>();
            options.AddRepository<QuestionAttachment, QuestionAttachmentRepository>();
            options.AddRepository<QuestionAnswer, QuestionAnswerRepository>();
            options.AddRepository<TestPaperStrategy, TestPaperStrategyRepository>();
            options.AddRepository<TestPaperQuestion, TestPaperQuestionRepository>();
            options.AddRepository<Exam, ExamRepository>();
            options.AddRepository<AnswerSheet, AnswerSheetRepository>();
            options.AddRepository<AnswerSheetDetail, AnswerSheetDetailRepository>();
            options.AddRepository<TestPaperSection, TestPaperSectionRepository>();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                         * See also TigerMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
                
            });
        }
    }
}
