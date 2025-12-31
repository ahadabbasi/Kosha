using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Http;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class TaskBinderService(
    IHttpContextAccessor accessor
) : ITaskBinderService
{
    private HttpContext Context =>
        accessor.HttpContext ?? throw new InvalidOperationException();

    public async Task<Result<TRequest>> BindAsync<TRequest>(CancellationToken cancellation = default)
        where TRequest : class, ITaskCollectorRequest
    {
        TRequest? data = null;

        /*
        Type modelType = typeof(TRequest);

        ModelMetadata metadata = modelMetadataProvider.GetMetadataForType(modelType);

        ModelBindingContext bindingContext =
            DefaultModelBindingContext.CreateBindingContext(
                new ActionContext(
                    Context, 
                    Context.GetRouteData(),
                    new ActionDescriptor()
                ), 
                await CompositeValueProvider.CreateAsync(
                    new ControllerContext(
                        new ActionContext(
                            Context,
                            Context.GetRouteData(),
                            new ControllerActionDescriptor()
                        )
                    )
                ), 
                metadata, 
                new BindingInfo()
                {
                    BindingSource = BindingSource.Body
                },
                string.Empty
            );

        IModelBinder modelBinder =
            modelBinderFactory.CreateBinder(
                new ModelBinderFactoryContext()
                {
                    Metadata = metadata, 
                    BindingInfo = 
                        new BindingInfo()
                        {
                            BindingSource = BindingSource.Body
                        }
                }
            );

        await modelBinder.BindModelAsync(bindingContext);

        if (bindingContext.Result.IsModelSet)
        {
            data = bindingContext.Result.Model as TRequest;
        }

        */

        try
        {
            Context.Request.EnableBuffering();

            int position = (int)Context.Request.Body.Position;

            Context.Request.Body.Position = 0;

            using StreamReader reader = new StreamReader(Context.Request.Body, leaveOpen: true);
            string body = await reader.ReadToEndAsync(cancellation);

            data = 
                JsonSerializer.Deserialize<TRequest>(
                    body, 
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

            Context.Request.Body.Position = position;
        }
        catch (Exception exception)
        {
            //
        }

        return
            data != null
            ? Result.Success(data)
            : Result.Failed<TRequest>(new Error("ModelBindingFailed", "Failed to bind the model."));
    }
}