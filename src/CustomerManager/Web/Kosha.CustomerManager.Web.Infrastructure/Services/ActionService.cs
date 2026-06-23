using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Action;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Action;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class ActionService(
    IEntityRepository<Domain.Entities.Action> repository,
    IUnitOfWork unitOfWork, IAuthorizeService authorizeService
) : IActionService
{
    public async Task<Result<IEnumerable<ActionResponse>>> FetchTaskActionsAsync(
        Guid task, CancellationToken cancellation = default
    ) =>
        Result.Success<IEnumerable<ActionResponse>>(
            await repository.Query()
                .Where(item => item.TaskId.Equals(task))
                .Include(item => item.User)
                .Select(item =>
                    new ActionResponse(
                        item.Id,
                        item.Description,
                        new UserInformationResponse(
                            item.UserId,
                            item.User.Username,
                            item.User.Name,
                            item.User.Family
                        ),
                        item.Modified ?? item.Inserted
                    )
                ).ToArrayAsync(cancellation)
        );
    

    public async Task<Result> AppendActionToTaskAsync(Guid task, ActionRequest request, CancellationToken cancellation = default)
    {
        bool result = true;

        Result<AuthorizationResponse> resultUser = authorizeService.Authenticate();

        if (resultUser && resultUser.Data != null)
        {
            repository.Add(
                new Domain.Entities.Action
                {
                    TaskId = task, 
                    Description = request.Description,
                    UserId = resultUser.Data.Id
                }
            );

            try
            {
                await unitOfWork.SaveChangesAsync(cancellation);

                result = true;
            }
            catch (Exception )
            {
                //
            }
        }

        return result;
    }
}