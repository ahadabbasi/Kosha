using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task.Collectors.Comment;

internal sealed class CommentTaskPaginateHandler : ITaskPaginateHandler<CommandTaskPaginateRequest>
{
    public ValueTask<Result<IEnumerable<ITaskPaginateResponse>>> Handle(CommandTaskPaginateRequest query, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            Result.Success<IEnumerable<ITaskPaginateResponse>>(
                [
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "بازرسی ماهانه ایمنی",
                        Description: "بازرسی کامل کف کارخانه و تجهیزات را تا پایان ماه انجام دهید.",
                        DateTime.Now
                    ),
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "صورت‌های مالی فصلی",
                        Description: "صورت‌های مالی سه‌ماهه سوم را تهیه و به سازمان امور مالیاتی ارسال کنید.",
                        DateTime.Now
                    ),
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "پشتیبان‌گیری از داده‌ها",
                        Description: "از تمام پایگاه‌های داده مشتریان در محل امن پشتیبان تهیه نمایید.",
                        DateTime.Now
                    ),
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "آموزش کارکنان",
                        Description: "همه اعضای تیم باید دوره امنیت سایبری اجباری را تکمیل نمایند.",
                        DateTime.Now
                    ),
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "بازبینی قرارداد فروشندگان",
                        Description: "قراردادهای همکاری با تامین کنندگان اصلی را بررسی و تمدید کنید.",
                        DateTime.Now
                    ),
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "کالیبراسیون تجهیزات",
                        Description: "برای دریافت گواهینامه ISO، کالیبراسیون دستگاه‌های آزمایشگاه را انجام دهید.",
                        DateTime.Now
                    ),
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "آماده‌سازی حسابرسی",
                        Description: "تمامی مدارک مورد نیاز برای حسابرسی ماه آینده را آماده نمایید.",
                        DateTime.Now
                    ),
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "تمدید لایسنس نرم‌افزار",
                        Description: "تمدید لایسنس نرم‌افزارهای سازمانی را پیش از انقضا انجام دهید.",
                        DateTime.Now
                    ),
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "مانور اضطراری",
                        Description: "مانور تخلیه اضطراری ساختمان اداری را برگزار و هماهنگی کنید.",
                        DateTime.Now
                    ),
                    new TaskPaginateResponse(
                        Id: Guid.NewGuid(),
                        Title: "ابلاغ سیاست‌های جدید",
                        Description: "بخشنامه‌های جدید منابع انسانی را ابلاغ و تاییدیه دریافت کنید.",
                        DateTime.Now
                    )
                ]
            )
        );
}