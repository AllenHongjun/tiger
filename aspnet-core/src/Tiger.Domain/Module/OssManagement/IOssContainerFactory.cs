using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.OssManagement
{
    /// <summary>
    /// Oss容器构建工厂
    /// </summary>
    public interface IOssContainerFactory
    {
        IOssContainer Create();
    }

    /// <summary>
    /// Oss容器构建工厂
    /// </summary>
    public interface IOssContainerFactory<TConfiguration>
    {
        IOssContainer Create(TConfiguration configuration);
    }
}
