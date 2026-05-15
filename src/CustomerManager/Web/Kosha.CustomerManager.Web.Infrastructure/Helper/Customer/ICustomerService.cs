using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Customer;

public interface ICustomerService
{
    /// <summary>
    /// Paginate all customer has been store 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<PaginateResponse<CustomerPaginateResponse>>> PaginateAsync(
        PaginateRequest? request = null,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<CustomerResponse>> FindByIdAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Create new customer
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> CreateAsync(
        CustomerRequest request,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Update customer information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> UpdateAsync(
        Guid id,
        CustomerRequest request,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Delete customer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> DeleteAsync( 
        Guid id, 
        CancellationToken cancellation = default 
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<CustomerContactListResponse>> ListOfCustomerContactAsync(
        Guid customer, 
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Attach new contact to the customer
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="contact"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> AddNewContactToCustomerAsync(
        Guid customer,
        CustomerContactRequest contact,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Update contact information of customer
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="contact"></param>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> UpdateContactOfCustomerAsync(
        Guid customer,
        Guid contact,
        CustomerContactRequest request,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Remove contact information from customer
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="contact"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> RemoveContactFromCustomerAsync(
        Guid customer,
        Guid contact,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Return only specific contact information of the customer
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="contact"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<CustomerContactResponse>> FindContactOfUser(
        Guid customer,
        Guid contact,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// All acceptable contact types 
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<CustomerContactTypeResponse>>> AcceptableContactTypesAsync(
        CancellationToken cancellation = default
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<CustomerResponse>> FetchTaskCustomerAsync(
        Guid task, 
        CancellationToken cancellation = default
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="customer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> AssignCustomerToTaskAsync(
        Guid task, 
        Guid customer, 
        CancellationToken cancellation = default
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<CustomerResponse>>> SearchCustomerAsync(
        string? fullName, 
        CancellationToken cancellation = default
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<CustomerInformationResponse>> TaskCustomerInformationAsync(
        Guid task,
        CancellationToken cancellation = default
    );
}