using System.Linq;
using System.Threading;
using Kosha.CustomerManager.Web.Domain.Entities;
using Kosha.CustomerManager.Web.Persistence.Attributes;
using Kosha.CustomerManager.Web.Persistence.Helper;

namespace Kosha.CustomerManager.Web.Persistence.Seeders;

/*
[Seed(202601061750)]
public class S202601061750AddingTags(ITagRepository repository, IUnitOfWork unitOfWork) : IDataSeeder
{
    public async System.Threading.Tasks.Task InvokeAsync(CancellationToken cancellation = default)
    {
        for (int index = 1; index <= 100; index++)
        {
            string title = $"Tag-{index}";

            if (!await repository.IsTitleExistAsync(title, cancellation))
            {
                repository.Add(new Tag(){Title = title});

                await unitOfWork.SaveChangesAsync(cancellation);
            }
        }
    }
}
*/