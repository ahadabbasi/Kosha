using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Helper.Time;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task.Collectors.Contact;

internal sealed class ContactTaskPaginateHandler(
    IEntityRepository<Domain.Entities.Tasks.Contact> repository,
    IPersianService persianService, IOptions<ContactTaskInformation> options
) : ITaskPaginateHandler<ContactTaskPaginateRequest>
{
    private ContactTaskInformation Information => options.Value;

    public async ValueTask<Result<IEnumerable<ITaskPaginateResponse>>> Handle(ContactTaskPaginateRequest query, CancellationToken cancellationToken)
    {
        IEnumerable<ContactTaskRepositoryResponse> data =
            await repository.Query()
                .Where(item => query.Records.Contains(item.Id))
                .Select(item =>
                    new ContactTaskRepositoryResponse(
                        item.Id, item.Organization,
                        item.Description, item.Inserted
                    )
                ).ToListAsync(cancellationToken);

        return
            Result.Success(
                data.Select(item =>
                    (ITaskPaginateResponse)new TaskPaginateResponse(
                        item.Id,
                        item.Organization ?? Information.UnknownOrganization,
                        item.Description ?? Information.DefaultDescription,
                        persianService.Parse(item.Inserted)
                    )
                )
            );
    }
}