using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task.Collectors.Contact;

internal sealed class ContactTaskDetailsHandler(
    IEntityRepository<Domain.Entities.Tasks.Contact> repository,
    IOptions<ContactTaskInformation> options
) : ITaskDetailsHandler<ContactTaskDetailsRequest>
{
    private ContactTaskCaptionInformation Caption => options.Value.Caption;

    public async ValueTask<Result<IEnumerable<ITaskDetailsResponse>>> Handle(ContactTaskDetailsRequest query, CancellationToken cancellationToken)
    {
        IEnumerable<ITaskDetailsResponse>? data = null;

        Domain.Entities.Tasks.Contact? entity =
            await repository.GetByIdAsync(query.Record, cancellationToken);

        if (entity is not null)
            data =
            [
                new TaskDetailsResponse(Caption.Name, entity.Name),
                new TaskDetailsResponse(Caption.Organization, entity.Organization),
                new TaskDetailsResponse(Caption.Post, entity.Post),
                new TaskDetailsResponse(Caption.PhoneNumber, entity.PhoneNumber),
                new TaskDetailsResponse(Caption.Description, entity.Description)
            ];

        return data is not null
            ? Result.Success(data)
            : Result.Failed<IEnumerable<ITaskDetailsResponse>>(ErrorConfiguration.TaskNotFound);
    }
}