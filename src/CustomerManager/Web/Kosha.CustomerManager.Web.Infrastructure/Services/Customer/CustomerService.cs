using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Extensions;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Customer;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Customer;

internal sealed class CustomerService(
    IAuditRepository<Domain.Entities.Customer> repository,
    IAuditRepository<Domain.Entities.CustomerContact> contactRepository,
    IAuditRepository<Domain.Entities.Task> taskRepository,
    PaginateHelperService paginateHelperService,
    ICustomerContactService customerContactService,
    IUnitOfWork unitOfWork
) : ICustomerService
{
    public async Task<Result<PaginateResponse<CustomerPaginateResponse>>> PaginateAsync(
        PaginateRequest? request = null,
        CancellationToken cancellation = default
    ) =>
        await repository
            .Query()
            .OrderBy(item => item.Inserted)
            .Select(item => 
                new CustomerPaginateResponse(
                    item.Id, 
                    item.Name, 
                    item.Family
                )
            )
            .ToPaginateAsync(
                await paginateHelperService.ValidateAsync(request),
                cancellation
            );

    public async Task<Result<CustomerResponse>> FindByIdAsync(
        Guid id, 
        CancellationToken cancellation = default
    )
    {
        Result<CustomerResponse> result = 
                Result.Failed<CustomerResponse>(Error.None);

        IQueryable<Domain.Entities.Customer> query =
            repository.Query()
                .Where(item => item.Id == id);

        if (await query.AnyAsync(cancellation))
            result =
                Result.Success(
                    await query
                        .Select(MapCustomer())
                        .FirstAsync(cancellation)
                );

        return result;
    }

    public async Task<Result> CreateAsync(
        CustomerRequest request, 
        CancellationToken cancellation = default
    )
    {
        Result result = false;

        Domain.Entities.Customer entity =
            new Domain.Entities.Customer
            {
                Name = request.Name,
                Family = request.Family
            };

        repository.Add(entity);

        try
        {
            await unitOfWork.SaveChangesAsync(cancellation);

            result = true;
        }
        catch (Exception )
        {
            //
        }

        return result;
    }

    public async Task<Result> UpdateAsync(
        Guid id,
        CustomerRequest request, 
        CancellationToken cancellation = default
    )
    {
        Result result =
            await repository.ExistsAsync(id, cancellation);

        if (result)
        {
            result = false;

            Domain.Entities.Customer? entity =
                await repository.GetByIdAsync(id, cancellation);

            if (entity is not null)
            {
                entity.Name = request.Name;
                entity.Family = request.Family;

                try
                {
                    await unitOfWork.SaveChangesAsync(cancellation);

                    result = true;
                }
                catch
                {
                    //
                }
            }
        }

        return result;
    }

    public async Task<Result> DeleteAsync(
        Guid id,
        CancellationToken cancellation = default
    )
    {
        Result result =
            await repository.ExistsAsync(id, cancellation);

        if (result)
        {
            result = false;

            repository.Delete(id);

            try
            {
                await unitOfWork.SaveChangesAsync(cancellation);

                result = true;
            }
            catch
            {
                //
            }
        }

        return result;
    }

    public async Task<Result<CustomerContactListResponse>> ListOfCustomerContactAsync(
        Guid customer,
        CancellationToken cancellation = default
    )
    {
        Result<CustomerContactListResponse> result =
            Result.Failed<CustomerContactListResponse>(Error.None);

        if (await repository.ExistsAsync(customer, cancellation))
        {
            Result<IEnumerable<CustomerContactTypeResponse>> resultAcceptable =
                await AcceptableContactTypesAsync(cancellation);

            if (resultAcceptable && resultAcceptable.Data is not null)
            {
                CustomerRequest data =
                    await repository
                        .Query()
                        .Where(item => item.Id == customer)
                        .Select(item =>
                            new CustomerRequest(
                                item.Name,
                                item.Family
                            )
                        ).FirstAsync(cancellation);

                List<CustomerContactResponse> contacts =
                    await contactRepository.Query()
                        .Where(item => item.CustomerId == customer)
                        .Select(item =>
                            new CustomerContactResponse(
                                item.Id,
                                item.Type,
                                item.Value
                            )
                        )
                        .ToListAsync(cancellation);

                result =
                    Result.Success(
                        new CustomerContactListResponse(
                            customer,
                            data.Name,
                            data.Family,
                            contacts.Select(
                                item =>
                                {
                                    CustomerContactTypeResponse? type =
                                        resultAcceptable.Data
                                            .FirstOrDefault(typeItem => typeItem.Type.Equals(item.Type));

                                    if (type is not null)
                                        item =
                                            new CustomerContactResponse(
                                                item.Id, 
                                                type.Title, 
                                                item.Value
                                            );
                                    
                                    return item;
                                }
                            )
                        )
                    );
            }
        }

        return result;
    }

    public async Task<Result> AddNewContactToCustomerAsync(
        Guid customer, 
        CustomerContactRequest contact,
        CancellationToken cancellation = default
    )
    {
        Result result = await repository.ExistsAsync(customer, cancellation);

        if (result)
        {
            result = false;

            Result<IEnumerable<CustomerContactTypeResponse>> resultTypes =
                await AcceptableContactTypesAsync(cancellation);

            if (
                resultTypes &&
                resultTypes.Data != null
            )
            {
                Result<CustomerContactTypeResponse> resultTypeResponse =
                    await ValidateContactType(contact, cancellation);

                if (resultTypeResponse && resultTypeResponse.Data != null)
                {
                    string type = resultTypeResponse.Data.Type;

                    if (
                        !await contactRepository.Query()
                            .AnyAsync(
                                item => item.CustomerId == customer &&
                                        item.Type == type &&
                                        item.Value == contact.Value,
                                cancellation
                            )
                    )
                    {
                        Domain.Entities.CustomerContact entity =
                            new Domain.Entities.CustomerContact
                            {
                                CustomerId = customer,
                                Type = type,
                                Value = contact.Value
                            };

                        contactRepository.Add(entity);

                        try
                        {
                            await unitOfWork.SaveChangesAsync(cancellation);

                            result = true;
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }
        }

        return result;
    }

    public async Task<Result> UpdateContactOfCustomerAsync(
        Guid customer, 
        Guid contact, 
        CustomerContactRequest request,
        CancellationToken cancellation = default
    )
    {
        Result result =
            await ContactBelongsToCustomerAsync(
                customer,
                contact,
                cancellation
            );

        if (result)
        {
            result = false;

            Result<CustomerContactTypeResponse> resultType =
                await ValidateContactType(request, cancellation);

            if (
                resultType && 
                resultType.Data is not null
            )
            {
                string type = resultType.Data.Type;

                if (
                    !await contactRepository.Query()
                        .AnyAsync(item =>
                                item.Id != contact &&
                                item.CustomerId == customer &&
                                item.Type == type &&
                                item.Value == request.Value,
                            cancellation
                        )
                )
                {
                    Domain.Entities.CustomerContact? entity =
                        await contactRepository.GetByIdAsync(contact, cancellation);

                    if (entity is not null)
                    {
                        entity.Type = type;
                        entity.Value = request.Value;

                        contactRepository.Update(entity);

                        try
                        {
                            await unitOfWork.SaveChangesAsync(cancellation);

                            result = true;
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }
        }

        return result;
    }

    public async Task<Result> RemoveContactFromCustomerAsync(
        Guid customer, 
        Guid contact, 
        CancellationToken cancellation = default
    )
    {
        Result result = 
            await ContactBelongsToCustomerAsync(
                customer, 
                contact,
                cancellation
            );

        if (result)
        {
            result = false;

            contactRepository.Delete(contact);

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

    public async Task<Result<CustomerContactResponse>> FindContactOfUser(
        Guid customer,
        Guid contact,
        CancellationToken cancellation = default
    )
    {
        Result<CustomerContactResponse> result =
            Result.Failed<CustomerContactResponse>(Error.None);

        if (
            await ContactBelongsToCustomerAsync(
                customer,
                contact,
                cancellation
            )
        )
            result = 
                Result.Success(
                    await contactRepository
                        .Query()
                        .Where(item => item.Id == contact)
                        .Select(item =>
                            new CustomerContactResponse(
                                item.Id,
                                item.Type,
                                item.Value
                            )
                        )
                        .FirstAsync(cancellation)
                );

        return result;
    }

    private async Task<bool> ContactBelongsToCustomerAsync(
        Guid customer,
        Guid contact,
        CancellationToken cancellation
    ) =>
        await contactRepository.Query()
            .AnyAsync(
                item => item.CustomerId == customer &&
                        item.Id == contact,
                cancellation
            );

    private async Task<Result<CustomerContactTypeResponse>> ValidateContactType(
        CustomerContactRequest request,
        CancellationToken cancellation
    )
    {
        CustomerContactTypeResponse? data = null;

        Result<IEnumerable<CustomerContactTypeResponse>> resultTypes =
            await AcceptableContactTypesAsync(cancellation);

        if (
            resultTypes &&
            resultTypes.Data != null
        )
            data =
                resultTypes.Data.FirstOrDefault(item =>
                    item.Type.Equals(
                        request.Type,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

        return 
            data is null ? 
                Result.Failed<CustomerContactTypeResponse>(Error.None) : 
                Result.Success(data);
    }

    public Task<Result<IEnumerable<CustomerContactTypeResponse>>> AcceptableContactTypesAsync(
        CancellationToken cancellation = default
    ) =>
        customerContactService.TypesAsync(cancellation);

    public async Task<Result<CustomerResponse>> FetchTaskCustomerAsync(
        Guid task, 
        CancellationToken cancellation = default
    )
    {
        CustomerResponse? data =
            await taskRepository.Query()
                .Where(item => item.Id.Equals(task))
                .Select(item => item.Customer)
                .Select(MapCustomer())
                .FirstOrDefaultAsync(cancellation);

        return data is not null ? Result.Success(data) : Result.Failed<CustomerResponse>(Error.None);
    }

    public async Task<Result> AssignCustomerToTaskAsync(
        Guid task, 
        Guid customer, 
        CancellationToken cancellation = default
    )
    {
        Result result = false;

        if (await repository.ExistsAsync(customer, cancellation))
        {
            Domain.Entities.Task? taskEntity = await taskRepository.GetByIdAsync(task, cancellation);

            if (taskEntity is not null)
            {
                taskEntity.CustomerId = customer;

                taskRepository.Update(taskEntity);

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
        }

        return result;
    }

    public async Task<Result<IEnumerable<CustomerResponse>>> SearchCustomerAsync(
        string? fullName,
        CancellationToken cancellation = default
    )
    {
        IQueryable<CustomerSearchRepositoryResponse> query =
            repository.Query()
                .Select(item =>
                    new CustomerSearchRepositoryResponse(
                        item.Id,
                        item.Name,
                        item.Family,
                        string.Concat(item.Name, " ", item.Family),
                        item.Inserted
                    )
                );

        if (!string.IsNullOrEmpty(fullName))
            query = query.Where(item => item.FullName.Contains(fullName));

        return Result.Success<IEnumerable<CustomerResponse>>(
            await query.OrderBy(item => item.Inserted).Take(10).ToArrayAsync(cancellation)
        );
    }

    public async Task<Result<CustomerInformationResponse>> TaskCustomerInformationAsync(
        Guid task,
        CancellationToken cancellation = default
    )
    {
        CustomerInformationResponse? customer =
            await taskRepository.Query().Where(item => item.Id.Equals(task))
                .Select(item =>
                    new CustomerInformationResponse(
                        item.Customer.Id,
                        item.Customer.Name,
                        item.Customer.Family,
                        item.Customer.Contacts.Select(contact =>
                            new CustomerContactResponse(
                                contact.Id,
                                contact.Type,
                                contact.Value
                            )
                        )
                    )
                ).FirstOrDefaultAsync(cancellation);

        return customer != null ? 
            Result.Success(customer) : 
            Result.Failed<CustomerInformationResponse>(Error.None);
    }

    private Expression<Func<Domain.Entities.Customer, CustomerResponse>> MapCustomer() =>
        item => new CustomerResponse(
            item.Id,
            item.Name,
            item.Family
        );
}