using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;
using Volo.Abp.TextTemplating;

namespace Tiger.Infrastructure.Notification
{
    internal class NotificationsCommonTemplateProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(CreateEmailTemplate());
        }

        protected virtual TemplateDefinition[] CreateEmailTemplate()
        {
            return new TemplateDefinition[]
            {
            new TemplateDefinition(
                name: "EmailNotifierLayout",
                displayName: L("EmailNotifierLayout"),
                defaultCultureName: "en",
                isLayout: true)
                .WithVirtualFilePath( "/Infrastructure/Notifications/Common/Templates/layout.tpl", isInlineLocalized: true)
            };
        }

        protected LocalizableString L(string name)
        {
            return LocalizableString.Create<NotificationsResource>(name);
        }
    }
}
