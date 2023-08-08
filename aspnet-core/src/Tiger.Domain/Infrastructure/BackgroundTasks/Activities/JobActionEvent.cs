﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks.Activities;

public class JobActionEvent : JobEventBase<JobActionEvent>, ITransientDependency
{
    protected async override Task OnJobAfterExecutedAsync(JobEventContext context)
    {
        if (context.EventData.Type.IsDefined(typeof(DisableJobActionAttribute), true))
        {
            return;
        }

        var actions = await GetJobActions(context);
        if (!actions.Any())
        {
            return;
        }

        var provider = context.ServiceProvider.GetRequiredService<IJobActionDefinitionManager>();

        foreach (var action in actions)
        {
            var definition = provider.GetOrNull(action.Name);

            if (definition == null)
            {
                Logger.LogWarning($"Cannot execute job action {definition.Name}, Because it's not registered.");
                continue;
            }

            await ExecuteAction(context, definition, action);
        }
    }

    private async Task<List<JobAction>> GetJobActions(JobEventContext context)
    {
        var store = context.ServiceProvider.GetRequiredService<IJobActionStore>();

        return await store.GetActionsAsync(context.EventData.Key);
    }

    private async Task ExecuteAction(JobEventContext context, JobActionDefinition actionDefinition, JobAction action)
    {
        var missingRequiredParams = actionDefinition.Paramters
                    .Where(p => p.Required)
                    .Where(rp => !action.Paramters.Any(p => string.Equals(rp.Name, p.Key)))
                    .Select(rp => rp.Name);

        if (missingRequiredParams.Any())
        {
            Logger.LogWarning($"Cannot execute job action {actionDefinition.Name}, The required parameters are missing: {missingRequiredParams.JoinAsString(Environment.NewLine)}");
            return;
        }

        var notifierContext = new JobActionExecuteContext(action, context);

        foreach (var notifierType in actionDefinition.Providers)
        {
            if (context.ServiceProvider.GetService(notifierType) is IJobExecutedProvider notifier)
            {
                if (context.EventData.Exception != null)
                {
                    if (actionDefinition.Type == JobActionType.Failed)
                    {
                        await notifier.NotifyErrorAsync(notifierContext);
                        Logger.LogInformation($"Executed Failed action with {notifierType.Name} was Successed.");
                    }
                    continue;
                }

                if (actionDefinition.Type == JobActionType.Successed)
                {
                    await notifier.NotifySuccessAsync(notifierContext);
                    Logger.LogInformation($"Executed Successed action with {notifierType.Name} was Successed.");
                }

                await notifier.NotifyComplateAsync(notifierContext);
                Logger.LogInformation($"Executed Complated action with {notifierType.Name} was Successed.");
            }
        }
    }
}
