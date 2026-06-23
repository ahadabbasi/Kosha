using System;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Helper.Time;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task.Collectors.Contact;

internal sealed class ContactTaskCreateHandler(
    IEntityRepository<Domain.Entities.Tasks.Contact> repository, IUnitOfWork unitOfWork
) : ITaskCreateHandler<ContactTaskCreateRequest>
{
    public async ValueTask<Result<Guid>> Handle(ContactTaskCreateRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Tasks.Contact entity =
            new Domain.Entities.Tasks.Contact
            {
                Name = request.Name,
                Description = request.Description,
                Organization = request.Organization,
                PhoneNumber = request.PhoneNumber,
                Post = request.Post
            };

        repository.Add(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(entity.Id);
    }
}