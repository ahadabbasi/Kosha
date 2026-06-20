using System;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Models;

public sealed record ObligationTaskItemVm(
    ITaskPaginateResponse Item, Guid? Task = null
);