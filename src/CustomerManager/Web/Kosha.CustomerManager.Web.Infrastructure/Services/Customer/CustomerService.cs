using System;
using System.Collections.Generic;
using System.Linq;
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
                await customerContactService.TypesAsync(cancellation);

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
        CancellationToken cancelToken = default
    )
    {
        Result result = 
            await ContactBelongsToCustomerAsync(
                customer, 
                contact, 
                cancelToken
            );

        if (result)
        {
            result = false;

            
        }

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
            await customerContactService.TypesAsync(cancellation);

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
}