using Volo.Abp.Account.Localization;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.Localization;
using Volo.Abp.TextTemplating;

namespace Tiger.Volo.Abp.Account.Emailing.Templates;

public class AccountTemplateDefinitionProvider : TemplateDefinitionProvider
{
    public override void Define(ITemplateDefinitionContext context)
    {
        context.Add(
            new TemplateDefinition(
                AppAccountEmailTemplates.MailSecurityVerifyLink,
                displayName: L($"TextTemplate:{AppAccountEmailTemplates.MailSecurityVerifyLink}"),
                layout: StandardEmailTemplates.Layout,
                localizationResource: typeof(AccountResource)
            ).WithVirtualFilePath("/Volo/Abp/Account/Emailing/Templates/MailSecurityVerify.tpl", true),
            new TemplateDefinition(
                AppAccountEmailTemplates.MailConfirmLink,
                displayName: L($"TextTemplate:{AppAccountEmailTemplates.MailConfirmLink}"),
                layout: StandardEmailTemplates.Layout,
                localizationResource: typeof(AccountResource)
            ).WithVirtualFilePath("/Volo/Abp/Account/Emailing/Templates/MailConfirm.tpl", true)
        );
    }

    private static ILocalizableString L(string name)
    {
        return LocalizableString.Create<AccountResource>(name);
    }
}
