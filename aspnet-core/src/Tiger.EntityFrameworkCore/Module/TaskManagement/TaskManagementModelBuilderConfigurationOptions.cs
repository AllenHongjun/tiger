using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tiger.Module.TaskManagement;

public class TaskManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
{
    public TaskManagementModelBuilderConfigurationOptions(
        [NotNull] string tablePrefix = "",
        [CanBeNull] string schema = null)
        : base(
            tablePrefix,
            schema)
    {

    }
}
