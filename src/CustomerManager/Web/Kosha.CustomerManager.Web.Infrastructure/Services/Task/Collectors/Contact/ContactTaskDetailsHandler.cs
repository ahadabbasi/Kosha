using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.Extensions.Options;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task.Collectors.Contact;

internal sealed class ContactTaskDetailsHandler(
    IEntityRepository<Domain.Entities.Tasks.Contact> repository,
    IOptions<ContactTaskInformation> options
) : ITaskDetailsHandler<ContactTaskDetailsRequest>
{
    private ContactTaskInformation Information => options.Value;

    public async ValueTask<Result<ITaskDetailsResponse>> Handle(ContactTaskDetailsRequest query, CancellationToken cancellationToken)
    {
        ITaskDetailsResponse? data = null;

        Domain.Entities.Tasks.Contact? entity =
            await repository.GetByIdAsync(query.Record, cancellationToken);

        if (entity is not null)
            data =
                new TaskDetailsResponse(
                    entity.Organization ?? Information.UnknownOrganization,
                    [
                        new TaskDetailsInformationResponse(Information.Caption.Name, entity.Name),
                        new TaskDetailsInformationResponse(Information.Caption.Organization, entity.Organization),
                        new TaskDetailsInformationResponse(Information.Caption.Post, entity.Post),
                        new TaskDetailsInformationResponse(Information.Caption.PhoneNumber, entity.PhoneNumber),
                        new TaskDetailsInformationResponse(Information.Caption.Description, entity.Description)
                    ]
                );

        return data is not null
            ? Result.Success(data)
            : Result.Failed<ITaskDetailsResponse>(ErrorConfiguration.TaskNotFound);
    }
}