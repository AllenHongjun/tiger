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
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Tiger.Module.System.Platform.Menus;
using Tiger.Module.System.Platform.Routes;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiger.Module.System.Platform.Layouts;
using Tiger.Volo.Abp.Sass.Tenants;
using Tiger.Volo.Abp.Sass;
using Tiger.Volo.Abp.Sass.Editions;
using Volo.Abp.Identity;
using Tiger.Module.Train;

namespace Tiger.EntityFrameworkCore
{
    /// <summary>
    /// 配置数据结构表约束
    /// </summary>
    /// <remarks>
    /// EF Core 使用 fluent API 配置模型: https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0
    /// ABP框架配置实体基类的约定：https://docs.abp.io/zh-Hans/abp/latest/Entities
    /// </remarks>
    public static class TigerDbContextModelCreatingExtensions
    {
        public static void ConfigureTiger(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */
            

            #region Sass
            //if (builder.IsTenantOnlyDatabase())
            //{
            //    return;
            //}
            builder.Entity<Edition>(b =>
            {
                b.ToTable(AbpSaasDbProperties.DbTablePrefix + "Editions", AbpSaasDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(t => t.DisplayName)
                    .HasMaxLength(EditionConsts.MaxDisplayNameLength)
                    .IsRequired()
                    .HasComment("显示名称");

                b.HasIndex(u => u.DisplayName);

                //b.ApplyObjectExtensionMappings();
            });

            //重用模块的表 https://docs.abp.io/zh-Hans/abp/4.4/Entity-Framework-Core-Migrations
            // 它映射到 AbpTenents 表,与 Abp 自带的Tenant 实体共享.
            builder.Entity<Tenant>(b =>
            {
                b.ToTable(AbpSaasDbProperties.DbTablePrefix + "Tenants", AbpSaasDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(t => t.Name).IsRequired().HasMaxLength(TenantConsts.MaxNameLength).HasComment("租户名称");

                b.HasMany(u => u.ConnectionStrings).WithOne().HasForeignKey(uc => uc.TenantId).IsRequired();

                b.HasIndex(u => u.Name);

                //b.ApplyObjectExtensionMappings();
            });
            builder.Entity<TenantConnectionString>(b =>
            {
                b.ToTable(AbpSaasDbProperties.DbTablePrefix + "TenantConnectionStrings", AbpSaasDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.HasKey(x => new { x.TenantId, x.Name });

                b.Property(cs => cs.Name).IsRequired().HasMaxLength(TenantConnectionStringConsts.MaxNameLength);
                b.Property(cs => cs.Value).IsRequired().HasMaxLength(TenantConnectionStringConsts.MaxValueLength);
                
                //b.ApplyObjectExtensionMappings();
            });
            #endregion

            #region System



            builder.Entity<Layout>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Layouts", TigerConsts.DbSchema);

                b.Property(p => p.Framework)
                    .HasMaxLength(LayoutConsts.MaxFrameworkLength)
                    .HasColumnName(nameof(Layout.Framework))
                    .IsRequired();

                b.ConfigureRoute();
            });

            #region Menu
            builder.Entity<Menu>(b =>
                {
                    b.ToTable(TigerConsts.DbTablePrefix + "Menus", TigerConsts.DbSchema);

                    b.ConfigureRoute();

                    b.Property(p => p.Framework)
                        .HasMaxLength(LayoutConsts.MaxFrameworkLength)
                        .HasColumnName(nameof(Menu.Framework))
                        .IsRequired()
                        .HasComment("框架");
                    b.Property(p => p.Component)
                        .HasMaxLength(MenuConsts.MaxComponentLength)
                        .HasColumnName(nameof(Menu.Component))
                        .IsRequired()
                        .HasComment("组件");
                    b.Property(p => p.Code)
                        .HasMaxLength(MenuConsts.MaxCodeLength)
                        .HasColumnName(nameof(Menu.Code))
                        .IsRequired()
                        .HasComment("菜单编码");
                });

            builder.Entity<RoleMenu>(x =>
            {
                x.ToTable(TigerConsts.DbTablePrefix  +  "RoleMenus", TigerConsts.DbSchema);

                x.Property(p => p.RoleName)
                    .IsRequired()
                    .HasMaxLength(RoleRouteConsts.MaxRoleNameLength)
                    .HasColumnName(nameof(RoleMenu.RoleName));

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.RoleName, i.MenuId });
            });

            builder.Entity<UserMenu>(x =>
            {
                x.ToTable(TigerConsts.DbTablePrefix  + "UserMenus", TigerConsts.DbSchema);

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.UserId, i.MenuId });
            }); 
            #endregion

            builder.Entity<Post>(b =>
                {
                    b.ToTable(TigerConsts.DbTablePrefix + "Posts", TigerConsts.DbSchema);
                    b.ConfigureByConvention();


                    /* Configure more properties here */
                });


            builder.Entity<TextTemplate>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "TextTemplates", TigerConsts.DbSchema);
                b.Property(x => x.Name).IsRequired().HasMaxLength(256).HasComment("名称");
                b.Property(x => x.DisplayName).HasMaxLength(256).HasComment("显示名称");
                b.Property(x => x.Content).HasMaxLength(1024 * 4).HasComment("模板内容");
                b.Property(x => x.Culture).HasMaxLength(256).HasComment("文化名称");
                b.ConfigureByConvention();
            });


            #region Localization Language
            builder.Entity<Language>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Languages", TigerConsts.DbSchema);
                b.Property(e => e.CultureName).IsRequired().HasMaxLength(128).HasComment("语言名称");
                b.Property(e => e.UiCultureName).IsRequired().HasMaxLength(128).HasComment("Ui语言名称");
                b.Property(e => e.DisplayName).IsRequired().HasMaxLength(128).HasComment("显示名称");
                b.Property(e => e.FlagIcon).HasMaxLength(128).HasComment("图标");
                b.Property<bool>(x => x.IsEnabled).IsRequired();
                b.HasIndex(e => e.CultureName).IsUnique();
                b.ConfigureByConvention();


                /* Configure more properties here */
            });


            builder.Entity<Resource>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Resources", TigerConsts.DbSchema);
                b.ConfigureByConvention();


                /* Configure more properties here */
            });


            builder.Entity<LanguageText>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "LanguageTexts", TigerConsts.DbSchema);
                b.Property(e => e.ResourceName).IsRequired().HasMaxLength(128).HasComment("资源名称");
                b.Property(e => e.CultrueName).IsRequired().HasMaxLength(128).HasComment("语言名称");
                b.Property(e => e.Key).IsRequired().HasMaxLength(256).HasComment("键名称");
                b.Property(e => e.Value).IsRequired().HasMaxLength(256).HasComment("值");
                b.HasIndex(x => new { x.TenantId, x.ResourceName, x.CultrueName });
                b.ConfigureByConvention();


                /* Configure more properties here */
            });
            #endregion
            #endregion

            #region Data
            builder.Entity<Data>(x =>
                {
                    x.ToTable(TigerConsts.DbTablePrefix + "Datas", TigerConsts.DbSchema);

                    x.Property(p => p.Code)
                        .HasMaxLength(DataConsts.MaxCodeLength)
                        .HasColumnName(nameof(Data.Code))
                        .IsRequired()
                        .HasComment("编码");
                    x.Property(p => p.Name)
                        .HasMaxLength(DataConsts.MaxNameLength)
                        .HasColumnName(nameof(Data.Name))
                        .IsRequired()
                        .HasComment("数据字典名称");
                    x.Property(p => p.DisplayName)
                       .HasMaxLength(DataConsts.MaxDisplayNameLength)
                       .HasColumnName(nameof(Data.DisplayName))
                       .IsRequired()
                       .HasComment("数据字典显示名称");
                    x.Property(p => p.Description)
                        .HasMaxLength(DataConsts.MaxDescriptionLength)
                        .HasColumnName(nameof(Data.Description))
                        .HasComment("数据字典描述");

                    x.ConfigureByConvention();

                    //EFcore 一对多关系配置 https://learn.microsoft.com/zh-cn/ef/core/modeling/relationships/one-to-many
                    x.HasMany(p => p.Items)
                        .WithOne(p => p.Data)
                        .HasForeignKey(fk => fk.DataId)
                        .IsRequired();

                    x.HasIndex(i => new { i.Name });
                });

            builder.Entity<DataItem>(x =>
            {
                x.ToTable(TigerConsts.DbTablePrefix + "DataItems", TigerConsts.DbSchema);

                x.Property(p => p.DefaultValue)
                    .HasMaxLength(DataItemConsts.MaxValueLength)
                    .HasColumnName(nameof(DataItem.DefaultValue))
                    .HasComment("默认值");
                x.Property(p => p.Name)
                    .HasMaxLength(DataItemConsts.MaxNameLength)
                    .HasColumnName(nameof(DataItem.Name))
                    .IsRequired()
                    .HasComment("字典数据名称");
                x.Property(p => p.DisplayName)
                   .HasMaxLength(DataItemConsts.MaxDisplayNameLength)
                   .HasColumnName(nameof(DataItem.DisplayName))
                   .IsRequired()
                   .HasComment("显示名称");
                x.Property(p => p.Description)
                    .HasMaxLength(DataItemConsts.MaxDescriptionLength)
                    .HasColumnName(nameof(DataItem.Description))
                    .HasComment("描述");

                x.Property(p => p.AllowBeNull).HasDefaultValue(true);

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.Name });
            });
            #endregion




            builder.Entity<Region>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Regions", TigerConsts.DbSchema);
                b.ConfigureByConvention(); 
            });


            builder.Entity<School>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Schools", TigerConsts.DbSchema);
                b.Property(p => p.Name)
                    .HasMaxLength(SchoolConsts.MaxNameLength)
                    .IsRequired()
                    .HasColumnName(nameof(School.Name))
                    .HasComment("名称");
                b.Property(p => p.ShortName)
                    .HasMaxLength(SchoolConsts.MaxShortNameLength)
                    .HasColumnName(nameof(School.ShortName))
                    .HasComment("简称");
                b.Property(p => p.Sort)
                    .HasComment("顺序");
                b.Property(p => p.IsEnable)
                    .HasComment("是否启用");
                b.Property(p => p.Number).HasMaxLength(SchoolConsts.MaxShortNameLength)
                    .HasComment("编号");
                b.Property(p => p.ImpowerDate)
                    .HasComment("授权截止时间");
                b.Property(p => p.MaxPerson)
                    .HasComment("最大人数");
                b.Property(p => p.IsAudit)
                    .HasComment("是否需要审核帖子");
                b.Property(p => p.VipLevel)
                    .HasComment("Vip等级：1.免费客户 2.付费客户");

                b.HasMany(p => p.ClassInfos)
                        .WithOne(p => p.School)
                        .HasForeignKey(fk => fk.SchoolId)
                        .IsRequired();

                b.ConfigureByConvention(); 
            });


            builder.Entity<ClassInfo>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Classes", TigerConsts.DbSchema);
                b.Property(p => p.Name)
                    .HasMaxLength(ClassConsts.MaxNameLength)
                    .IsRequired()
                    .HasComment("名称");
                b.Property(p => p.SchoolId)
                    .HasComment("学校Id");
                b.Property(p => p.Sorting)
                    .HasComment("顺序");
                b.Property(p => p.IsEnable)
                    .HasComment("顺序");

                b.ConfigureByConvention(); 

            });


            builder.Entity<Course>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Courses", TigerConsts.DbSchema);
                b.Property(p => p.Name)
                   .HasMaxLength(CourseConsts.MaxNameLength)
                   .IsRequired()
                   .HasComment("名称");
                b.Property(p => p.Description)
                   .HasMaxLength(CourseConsts.MaxDescriptionLength)
                   .IsRequired()
                   .HasComment("描述");
                b.Property(p => p.Cover)
                    .HasMaxLength(CourseConsts.MaxCoverLenght)
                    .HasComment("封面图片");
                b.ConfigureByConvention();
                b.Property(p => p.Sorting)
                    .HasComment("顺序");
                b.Property(p => p.Enable)
                    .HasComment("启用");

                

                /* Configure more properties here */
            });


            builder.Entity<TestPaper>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "TestPapers", TigerConsts.DbSchema);
                b.Property(p => p.Name).HasMaxLength(TestPaperConsts.MaxNameLength).IsRequired().HasComment("名称");
                b.Property(p => p.Number).HasMaxLength(TestPaperConsts.MaxNumberLength).IsRequired().HasComment("编号");
                b.Property(p => p.Type)
                    .HasComment("类型 1.固定题目（手动或自动选题） 2.随机题目（根据策略每个学员的题目都不同） 3.固定题目打乱显示");
                b.Property(p => p.IsComposing)
                    .HasComment("是否已组卷");
                b.Property(p => p.Enable)
                    .HasComment("启用");
                b.Property(p => p.IsIncludeAllSchoolTeachers)
                    .HasComment("是否包含全校老师");
                b.Property(p => p.IsLimitJudgeTime)
                    .HasComment("是否限制评卷时间");
                b.Property(p => p.JudgeStartTime)
                    .HasComment("评卷开始时间");
                b.Property(p => p.JudgeEndTime)
                    .HasComment("评卷结束时间");

                b.HasMany(t => t.TestPaperStrategies).WithOne(tps => tps.TestPaper).HasForeignKey(tjs => tjs.TestPaperId).IsRequired();
                b.HasMany(t => t.TestPaperQuestions).WithOne(tpq => tpq.TestPaper).HasForeignKey(tjs => tjs.TestPaperId).IsRequired();
                b.ConfigureByConvention(); 

            });

            builder.Entity<TestPaperSection>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "TestPaperSections", TigerConsts.DbSchema);
                b.Property(p => p.TestPaperId).HasComment("试卷Id");
                b.Property(p => p.Name).HasMaxLength(TestPaperSectionConsts.MaxNameLength).IsRequired().HasComment("名称");
                b.Property(p => p.Description).HasMaxLength(TestPaperSectionConsts.MaxDesctiptionLength).IsRequired(false).HasComment("大题描述");
                b.Property(p => p.Type).IsRequired().HasComment("类型:固定题目大题:1,随机题目大题:2,抽题大题:3");
                b.Property(p => p.QuestionCount).HasComment("题目数量");
                b.Property(p => p.TotalScore).HasComment("题目数量");
                b.Property(p => p.Sort).HasComment("序号");
                b.ConfigureByConvention();

                /* Configure more properties here */
            });

            builder.Entity<TestPaperQuestion>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "TestPaperQuestions", TigerConsts.DbSchema);
                b.Property(p => p.TestPaperId).HasComment("试卷ID");
                b.Property(p => p.TestPaperSectionId).HasComment("试卷大题Id");
                b.Property(p => p.QuestionCategoryId).HasComment("题目分类");
                b.Property(p => p.QuestionId).HasComment("试题ID");
                b.Property(p => p.TestPaperType).HasComment("选题方式 1.自主选题 2.随机生成");
                b.Property(p => p.QuestionDegree).HasComment("难易度：1.简单 2.普通 3.困难");
                b.Property(p => p.Sorting).HasComment("顺序");
                b.Property(p => p.Score).HasComment("每题分数");

                b.HasOne( p => p.TestPaperSection).WithMany(p => p.Questions).HasForeignKey(p => p.TestPaperSectionId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.ConfigureByConvention();


                /* Configure more properties here */
            });

            builder.Entity<TestPaperStrategy>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "TestPaperStrategies", TigerConsts.DbSchema);
                b.Property(p => p.TestPaperId).HasComment("试卷Id");
                b.Property(p => p.QuestionCategoryId).HasComment("题目分类Id");
                b.Property(p => p.QuestionType).HasComment("题型");
                b.Property(p => p.UnlimitedDifficultyCount).HasComment("不限难度数量");
                b.Property(p => p.SimpleCount).HasComment("简单的数量");
                b.Property(p => p.OrdinaryCount).HasComment("普通的数量");
                b.Property(p => p.DifficultCount).HasComment("困难的数量");
                b.Property(p => p.ScorePerQuestion).IsRequired(true).HasDefaultValue(decimal.Zero).HasComment("每题分数");

                // bug: 多重级联的问题 外键级联删除 级联更新配置移除
                b.HasOne(p => p.TestPaperSection).WithMany(p => p.Strategies).HasForeignKey(p => p.TestPaperSectionId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });


            builder.Entity<QuestionCategory>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "QuestionCategories", TigerConsts.DbSchema);
                b.Property(p => p.Name)
                   .HasMaxLength(QuestionCategoryConsts.MaxNameLength)
                   .IsRequired()
                   .HasComment("名称");
                b.Property(p => p.Cover)
                   .HasMaxLength(QuestionCategoryConsts.MaxCodeLength)
                   .IsRequired()
                   .HasComment("封面");
                b.Property(p => p.Code)
                    .HasComment("层次编码");
                b.Property(p => p.Enable)
                    .HasComment("启用");
                b.Property(p => p.Sorting)
                    .HasComment("顺序");

                b.HasOne( qc => qc.Parent).WithMany( qc => qc.Children).HasForeignKey(qc => qc.ParentId); // ParentId可以为空 注意不要IsRequired() 需要把ChildRen写明确
                b.HasMany(qc => qc.Questions).WithOne(q => q.QuestionCategory).HasForeignKey(q => q.QuestionCategoryId).IsRequired();

                b.ConfigureByConvention();
                /* Configure more properties here */
            });


            builder.Entity<Question>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Questions", TigerConsts.DbSchema);
                b.Property(p => p.Type).HasComment("类型 1.判断 2.单选 3.多选 4.填空 5.计算题 6.问答题 7.B型题,8.简答题 9.实训任务");
                //b.Property(p => p.Name).HasMaxLength(QuestionConsts.MaxNameLength).IsRequired(false).HasComment("题目名称");
                b.Property(p => p.Content).HasMaxLength(QuestionConsts.MaxContentLength).IsRequired().HasComment("题目内容");
                b.Property(p => p.Answer).HasMaxLength(QuestionConsts.MaxAnswerLength).HasComment("答案 判断题答案：正确/错误；多个填空之间的答案使用竖线 |分隔，如果一个填空有多个答案请用 & 开隔;");
                b.Property(p => p.OptionContent).HasMaxLength(QuestionConsts.MaxOptionLength).IsRequired(false).HasComment("选项内容");
                b.Property(p => p.Score).HasMaxLength(QuestionConsts.MaxOptionLength).HasComment("分数");
               
                b.Property(p => p.Degree).HasComment("难易度：1.简单 2.普通 3.困难");
                b.Property(p => p.Analysis).HasMaxLength(QuestionConsts.MaxAnalysisLength).HasComment("试题解析");
                b.Property(p => p.Source).HasMaxLength(QuestionConsts.MaxSourceLength).HasComment("出处");
                b.Property(p => p.HelpMessage).HasMaxLength(QuestionConsts.MaxHelpMessageLength).HasComment("帮助文本");
                b.Property(p => p.FileUrl).HasMaxLength(QuestionConsts.MaxFileUrlLength).HasComment("附件URL");
                b.Property(p => p.FileName).HasMaxLength(QuestionConsts.MaxFileNameLength).HasComment("附件名称");
                b.Property(p => p.IsShowTextButton).HasComment("是否显示上传文本按钮");
                b.Property(p => p.IsShowImageButton).HasComment("是否显示上传图片按钮");
                b.Property(p => p.IsShowLinkButton).HasComment("是否显示上传附件按钮");

                b.HasMany( q => q.QuestionAttachments).WithOne( qa => qa.Question).HasForeignKey( qa => qa.QuestionId).IsRequired();
                b.HasMany(q => q.QuestionAnswers).WithOne(qa => qa.Question).HasForeignKey(qa => qa.QuestionId).IsRequired();
                b.ConfigureByConvention(); 

                /* Configure more properties here */
            });


            builder.Entity<QuestionAttachment>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "QuestionAttachments", TigerConsts.DbSchema);
                b.Property(p => p.QuestionId).HasComment("题目Id");
                b.Property(p => p.AttachmentType).HasComment("附件类型：1.内容，2.照片，3.文档，4.本地附件，5.本地视频，6.添加链接");
                b.Property(p => p.Name).HasMaxLength(QuestionAttachmentConsts.MaxNameLength).IsRequired().HasComment("名称");
                b.Property(p => p.Content).HasMaxLength(QuestionAttachmentConsts.MaxContextLength).IsRequired().HasComment("附件内容");
                b.Property(p => p.Sorting).HasComment("排序");

                b.ConfigureByConvention(); 

                /* Configure more properties here */
            });


            builder.Entity<QuestionAnswer>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "QuestionAnswers", TigerConsts.DbSchema);
                b.Property(p => p.QuestionId).HasComment("题目Id");
                b.Property(p => p.Answer).HasMaxLength(QuestionConsts.MaxAnswerLength).IsRequired().HasComment("答案");
                b.ConfigureByConvention(); 

                /* Configure more properties here */
            });

            builder.Entity<Exam>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Exams", TigerConsts.DbSchema);
                b.Property(p => p.CourseId).HasComment("课程Id");
                b.Property(p => p.TestPaperId).HasComment("考试的试卷");
                b.Property(p => p.QuestionCategoryId).HasComment("题目分类");
                b.Property(p => p.Name).HasMaxLength(ExamConsts.MaxNameLength).IsRequired().HasComment("考试名称");
                b.Property(p => p.CoverUrl).HasMaxLength(ExamConsts.MaxCoverLength).IsRequired(false).HasComment("封面");
                b.Property(p => p.Number).HasMaxLength(ExamConsts.MaxNumberLength).IsRequired(false).HasComment("编号");
                b.Property(p => p.ExamType).HasComment("类型：1.考试 2.练习 , 3 比赛");
                b.Property(p => p.StartDate).HasComment("开始时间");
                b.Property(p => p.EndDate).HasComment("结束时间");
                b.Property(p => p.ExamDuration).HasComment("考试时长 单位：分钟");
                b.Property(p => p.PassingScore).HasComment("及格分数");
                b.Property(p => p.IsDifferent).HasComment("是否每个人都不同");
                b.Property(p => p.IsDifferentOrder).HasComment("顺序不同");
                b.Property(p => p.IsShowScore).HasComment("提交后是否显示成绩");
                b.Property(p => p.IsShowError).HasComment("是否可以查看错题");
                b.Property(p => p.IsEnable).HasComment("启用状态");
                b.Property(p => p.IsExamAnyTime).HasComment("是否随到随考");
                b.Property(p => p.IsShowWindowOnblur).HasComment("提示切屏次数");
                b.Property(p => p.MaxExamCount).HasComment("考试最大次数");
                b.Property(p => p.Sorting).HasComment("顺序");
                b.Property(p => p.OnlyExamDayVisible).HasComment("仅考试当天可见");
                b.Property(p => p.IsStartSync).HasComment("是否启动自动实操评分");
                b.Property(p => p.IsShowHelp).HasComment("是否显示帮助内容");
                b.Property(p => p.HalftimeFlag).HasComment("是否中场休息");
                b.Property(p => p.HalftimeStart).HasComment("中场休息开始时间");
                b.Property(p => p.HalftimeEnd).HasComment("中场休息结束时间");
                b.Property(p => p.DeductionAmounnt).HasComment("扣款金额");
                b.Property(p => p.DeductionInterval).HasComment("扣款间隔（单位: 分钟）");
                b.Property(p => p.DeductionInterval).HasComment("扣款间隔（单位: 分钟）");
                b.Property(p => p.Interval).HasComment("比赛间隔（单位: 分钟）");

                b.HasOne( e => e.TestPaper).WithMany( t => t.Exams).HasForeignKey(e => e.TestPaperId).IsRequired();

                b.ConfigureByConvention(); 
            

                /* Configure more properties here */
            });


            builder.Entity<AnswerSheet>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "AnswerSheets", TigerConsts.DbSchema);
                b.Property(p => p.TestPaperMainId).HasComment("主试卷、固定题目时0，随机题目或打乱顺序时录入主试卷的ID");
                b.Property(p => p.TestPaperId).HasComment("试卷ID");
                b.Property(p => p.ExamId).HasComment("考试ID");
                b.Property(p => p.StudentId).HasComment("学员Id");
                b.Property(p => p.TotalScore).HasComment("总分数");
                b.Property(p => p.IsSubmit).HasComment("是否交卷 True为交卷");
                b.Property(p => p.SubmitDateTime).HasComment("交卷时间");
                b.Property(p => p.IP).HasMaxLength(64).HasComment("客户端IP");
                b.Property(p => p.DeviceType).HasComment("设备类型： 1.电脑 2.手机 3.平板");
                b.Property(p => p.ExamDuration).HasComment("考试总时长");
                b.Property(p => p.AnswerTotalDuration).HasComment("答题总时长（分钟）");
                b.Property(p => p.WindowOnblur).HasComment("考试切屏次数");
                b.Property(p => p.OperateAutoScore).HasComment("实操题自动评分");
                b.Property(p => p.OperateAutoScoreTime).HasComment("实操自动评分时间");
                b.Property(p => p.OperateManualScore).HasComment("实操题人工打分");
                b.Property(p => p.OperateManualScoreTime).HasComment("实操题自动评分时间");
                b.Property(p => p.ObjectiveScore).HasComment("客观题评分");
                b.Property(p => p.ObjectiveScoreTime).HasComment("客观题评分时间");
                b.Property(p => p.Status).HasComment("答卷状态1:未交卷;2:已交卷;3:已阅卷");
                b.Property(p => p.IsPass).HasComment("是否及格1:是; 0:否");
                b.ConfigureByConvention(); 

                b.HasOne( ans => ans.Exam).WithMany(e => e.AnswerSheets).HasForeignKey( ans => ans.ExamId).IsRequired();
                b.HasMany(ans => ans.AnswerSheetDetails).WithOne(asd => asd.AnswerSheet).HasForeignKey(asd => asd.AnswerSheetId).IsRequired();
                /* Configure more properties here */
            });


            builder.Entity<AnswerSheetDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "AnswerSheetDetails", TigerConsts.DbSchema);
                b.Property(p => p.AnswerSheetId).HasComment("答卷Id");
                b.Property(p => p.TestPaperId).HasComment("试卷Id");
                b.Property(p => p.TestPaperDetailId).HasComment("试卷详情Id");
                b.Property(p => p.QuestionId).HasComment("试题Id");
                b.Property(p => p.Answer).HasMaxLength(512).IsRequired(false).HasComment("答案");
                b.Property(p => p.ObjectiveScore).HasComment("客观题评分");
                b.Property(p => p.OperateManualScore).HasComment("实操题自动评分分数");
                b.Property(p => p.OperateId).HasComment("实操Id");
                b.Property(p => p.SyncTime).HasComment("上次自动评分同步时间");
                b.Property(p => p.SyncMessage).HasComment("上次自动评分同步结果");
                b.ConfigureByConvention(); 

                /* Configure more properties here */
            });


            
        }



        public static EntityTypeBuilder<TRoute> ConfigureRoute<TRoute>(
            this EntityTypeBuilder<TRoute> builder)
            where TRoute : Route
        {
            builder
                .Property(p => p.DisplayName)
                .HasMaxLength(RouteConsts.MaxDisplayNameLength)
                .HasColumnName(nameof(Route.DisplayName))
                .IsRequired();
            builder
                .Property(p => p.Name)
                .HasMaxLength(RouteConsts.MaxNameLength)
                .HasColumnName(nameof(Route.Name))
                .IsRequired();
            builder
                .Property(p => p.Path)
                .HasMaxLength(RouteConsts.MaxPathLength)
                .HasColumnName(nameof(Route.Path));
            builder
                .Property(p => p.Redirect)
                .HasMaxLength(RouteConsts.MaxRedirectLength)
                .HasColumnName(nameof(Route.Redirect));

            builder
                .Property(p => p.Status)
                .HasColumnName(nameof(Route.Status))
                .IsRequired();

            builder.ConfigureByConvention();

            return builder;
        }

        public static OwnedNavigationBuilder<TEntity, TRoute> ConfigureRoute<TEntity, TRoute>(
            [NotNull] this OwnedNavigationBuilder<TEntity, TRoute> builder,
            [CanBeNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            where TEntity : class
            where TRoute : Route
        {
            builder.ToTable(tablePrefix + "Routes", schema);

            builder
                .Property(p => p.DisplayName)
                .HasMaxLength(RouteConsts.MaxDisplayNameLength)
                .HasColumnName(nameof(Route.DisplayName))
                .IsRequired();
            builder
                .Property(p => p.Name)
                .HasMaxLength(RouteConsts.MaxNameLength)
                .HasColumnName(nameof(Route.Name))
                .IsRequired();
            builder
                .Property(p => p.Path)
                .HasMaxLength(RouteConsts.MaxPathLength)
                .HasColumnName(nameof(Route.Path));
            builder
                .Property(p => p.Redirect)
                .HasMaxLength(RouteConsts.MaxRedirectLength)
                .HasColumnName(nameof(Route.Redirect));

            return builder;
        }
    }
}
