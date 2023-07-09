using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.TextTemplating;

namespace Tiger.Module.System.TextTemplate
{
    /// <summary>
    /// 定义模板
    /// </summary>
    /// <remarks>
    /// 注意选用指定版本的包
    /// </remarks>
    public class CommonTemplateDefinitionProvide:TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(
                new TemplateDefinition("Hello") //template name: "Hello"  每个模板必须具有唯一的名称（将在呈现模板时使用）
                    .WithVirtualFilePath(
                        "/Demos/Hello/Hello.cshtml", //template content path
                        isInlineLocalized: true  // true 每种语言使用不同的模板
                    )
            );
        }
    }
}
