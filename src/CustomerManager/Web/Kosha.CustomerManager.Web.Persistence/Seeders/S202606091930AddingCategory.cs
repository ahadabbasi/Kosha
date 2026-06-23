using System.Linq;
using System.Threading;
using Kosha.CustomerManager.Web.Domain.Entities;
using Kosha.CustomerManager.Web.Domain.Enums;
using Kosha.CustomerManager.Web.Persistence.Attributes;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Seeders;

[Seed(202606091930)]
internal sealed class S202606091930AddingCategory(IEntityRepository<Category> repository, IUnitOfWork unitOfWork) : IDataSeeder
{
    public async System.Threading.Tasks.Task InvokeAsync(CancellationToken cancellation = default)
    {
        const string defaultCategory = "شروع نشده";

        if (!await repository.Query().AnyAsync(item => item.IsDefault == AnsEnum.Yes, cancellation))
        {
            IQueryable<Category> query =
                repository.Query().Where(item => item.Name.Equals(defaultCategory));

            bool exist = await query.AnyAsync(cancellation);

            Category entity = exist ? await query.FirstAsync(cancellation) : new Category();

            entity.Name = defaultCategory;
            entity.IsDefault = AnsEnum.Yes;

            if(exist)
                repository.Update(entity);
            else
                repository.Add(entity);

            await unitOfWork.SaveChangesAsync(cancellation);
        }
    }
}