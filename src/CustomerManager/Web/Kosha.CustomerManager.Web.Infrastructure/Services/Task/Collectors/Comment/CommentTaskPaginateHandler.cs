using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Kosha.CustomerManager.Web.Shared.Helper.Time;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task.Collectors.Comment;

internal sealed class CommentTaskPaginateHandler(IPersianService persianService) : ITaskPaginateHandler<CommentTaskPaginateRequest>
{
    public ValueTask<Result<IEnumerable<ITaskPaginateResponse>>> Handle(CommentTaskPaginateRequest query, CancellationToken cancellationToken)
    {
        IEnumerable<ITaskPaginateResponse> responses =
            [
                new TaskPaginateResponse(
                    Guid.Parse("3a9b8c7d-1e2f-4a3b-8c9d-0e1f2a3b4c5d"),
                    "بررسی وضعیت تجهیزات حفاظت فردی",
                    "اطمینان از موجود بودن و سلامت کلاه‌های ایمنی، دستکش‌ها و عینک‌های کارکنان شیفت صبح",
                    persianService.Parse(
                        new DateTime(2026, 6, 8, 14, 30, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("4b2c9d8e-2f3a-5b4c-9d0e-1f2a3b4c5d6e"),
                    "بازبینی سیستم اعلام حریق",
                    "تست کامل سنسورهای دود و آلارم‌های طبقه دوم و سوم انبار",
                    persianService.Parse(
                        new DateTime(2026, 6, 7, 9, 15, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("5c3d0e9f-3a4b-6c5d-0e1f-2a3b4c5d6e7f"),
                    "آموزش ماهانه HSE",
                    "برگزاری کارگاه ایمنی در برابر مواد شیمیایی برای تیم تولید",
                    persianService.Parse(
                        new DateTime(2026, 6, 6, 11, 45, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("6d4e1f0a-4b5c-7d6e-1f2a-3b4c5d6e7f8a"),
                    "بازرسی پله‌های اضطراری",
                    "بررسی سلامت نرده‌ها، نورپردازی و نبود مانع در مسیرهای خروج اضطراری",
                    persianService.Parse(
                        new DateTime(2026, 6, 5, 16, 20, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("7e5f2a1b-5c6d-8e7f-2a3b-4c5d6e7f8a9b"),
                    "کنترل کیفیت هوای محیط کار",
                    "اندازه‌گیری غبار و ذرات معلق در سالن تولید با دستگاه‌های سنجش",
                    persianService.Parse(
                        new DateTime(2026, 6, 4, 8, 0, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("8f6a3b2c-6d7e-9f8a-3b4c-5d6e7f8a9b0c"),
                    "بازبینی جعبه‌های کمک‌های اولیه",
                    "تکمیل اقلام مصرفی و بررسی تاریخ انقضای داروها در تمام ایستگاه‌ها",
                    persianService.Parse(
                        new DateTime(2026, 6, 3, 13, 30, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("9a7b4c3d-7e8f-0a9b-4c5d-6e7f8a9b0c1d"),
                    "بازرسی جرثقیل‌های سقفی",
                    "بررسی سیم‌بکسل‌ها، ترمزها و انجام تست بار سالیانه",
                    persianService.Parse(
                        new DateTime(2026, 6, 2, 10, 10, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("0b8c5d4e-8f9a-1b0c-5d6e-7f8a9b0c1d2e"),
                    "بازدید از تابلوهای برق فشار قوی",
                    "کنترل حرارت، اتصالات و نظافت تابلوهای اصلی برق ساختمان",
                    persianService.Parse(
                        new DateTime(2026, 6, 1, 15, 55, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("1c9d6e5f-9a0b-2c1d-6e7f-8a9b0c1d2e3f"),
                    "بررسی کپسول‌های آتش‌نشانی",
                    "وزن‌گیری، کنترل فشار و تاریخ شارژ کپسول‌های کل کارخانه",
                    persianService.Parse(
                        new DateTime(2026, 5, 31, 12, 0, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("2d0e7f6a-0b1c-3d2e-7f8a-9b0c1d2e3f4a"),
                    "آموزش ایمنی لیفتراک",
                    "بازآموزی قوانین تردد و بارگیری برای رانندگان لیفتراک",
                    persianService.Parse(
                        new DateTime(2026, 5, 30, 9, 30, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("3e1f8a7b-1c2d-4e3f-8a9b-0c1d2e3f4a5b"),
                    "بازرسی مخازن تحت فشار",
                    "بررسی بدنه، شیرهای اطمینان و انجام تست هیدرواستاتیک",
                    persianService.Parse(
                        new DateTime(2026, 5, 29, 17, 45, 0)
                    )
                ),
                new TaskPaginateResponse(
                    Guid.Parse("4f2a9b8c-2d3e-5f4a-9b0c-1d2e3f4a5b6c"),
                    "بازدید از سیستم تهویه",
                    "تمیز کردن فیلترها و بررسی عملکرد فن‌های اگزاست سالن رنگ",
                    persianService.Parse(new DateTime(2026, 5, 28, 7, 15, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("5a3b0c9d-3e4f-6a5b-0c1d-2e3f4a5b6c7d"),
                    "کنترل زمین حفاظتی دستگاه‌ها",
                    "اندازه‌گیری مقاومت ارت و بررسی اتصالات ارت تجهیزات",
                    persianService.Parse(new DateTime(2026, 5, 27, 14, 0, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("6b4c1d0e-4f5a-7b6c-1d2e-3f4a5b6c7d8e"),
                    "بررسی روشنایی محیط کار",
                    "اندازه‌گیری لوکس در ایستگاه‌های کاری و راهروهای اصلی",
                    persianService.Parse(new DateTime(2026, 5, 26, 11, 20, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("7c5d2e1f-5a6b-8c7d-2e3f-4a5b6c7d8e9f"),
                    "بازرسی سالیانه پله برقی",
                    "بررسی موتور، ترمز و حسگرهای ایمنی پله برقی ورودی",
                    persianService.Parse(new DateTime(2026, 5, 25, 18, 30, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("8d6e3f2a-6b7c-9d8e-3f4a-5b6c7d8e9f0a"),
                    "بازدید از انبار مواد شیمیایی",
                    "بررسی ظروف، برچسب‌ها و رعایت فاصله مجاز بین مواد",
                    persianService.Parse(new DateTime(2026, 5, 24, 8, 45, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("9e7f4a3b-7c8d-0e9f-4a5b-6c7d8e9f0a1b"),
                    "آموزش اطفای حریق",
                    "برگزاری مانور عملی اطفا حریق با کپسول CO2 برای کارکنان جدید",
                    persianService.Parse(new DateTime(2026, 5, 23, 13, 10, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("0f8a5b4c-8d9e-1f0a-5b6c-7d8e9f0a1b2c"),
                    "بازرسی تسمه نقاله",
                    "بررسی سلامت تسمه، غلطک‌ها و محافظ‌های ایمنی خط تولید",
                    persianService.Parse(new DateTime(2026, 5, 22, 10, 30, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("1a9b6c5d-9e0f-2a1b-6c7d-8e9f0a1b2c3d"),
                    "کنترل صدا در محیط کار",
                    "اندازه‌گیری تراز صدا در نقاط مختلف و بررسی گوشی‌های حفاظتی",
                    persianService.Parse(new DateTime(2026, 5, 21, 16, 15, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("2b0c7d6e-0f1a-3b2c-7d8e-9f0a1b2c3d4e"),
                    "بازبینی نشانگرهای خروج اضطراری",
                    "اطمینان از وجود و روشنایی تابلوهای راهنمای خروج",
                    persianService.Parse(new DateTime(2026, 5, 20, 9, 0, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("3c1d8e7f-1a2b-4c3d-8e9f-0a1b2c3d4e5f"),
                    "بازرسی جک‌های پنوماتیک",
                    "بررسی نشتی هوا، سلامت شیلنگ‌ها و اتصالات",
                    persianService.Parse(new DateTime(2026, 5, 19, 14, 45, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("4d2e9f8a-2b3c-5d4e-9f0a-1b2c3d4e5f6a"),
                    "بررسی ضدعفونی سرویس‌های بهداشتی",
                    "انجام نظافت عمیق و کنترل موجودی مواد شوینده",
                    persianService.Parse(new DateTime(2026, 5, 18, 11, 55, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("5e3f0a9b-3c4d-6e5f-0a1b-2c3d4e5f6a7b"),
                    "بازرسی درب‌های اتوماتیک",
                    "بررسی سنسورها، عملکرد موتور و ترمزهای ایمنی",
                    persianService.Parse(new DateTime(2026, 5, 17, 17, 20, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("6f4a1b0c-4d5e-7f6a-1b2c-3d4e5f6a7b8c"),
                    "آموزش حرکات کششی محیط کار",
                    "برگزاری کلاس ۳۰ دقیقه‌ای پیشگیری از آسیب‌های اسکلتی",
                    persianService.Parse(new DateTime(2026, 5, 16, 7, 45, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("7a5b2c1d-5e6f-8a7b-2c3d-4e5f6a7b8c9d"),
                    "بازرسی جعبه فرمان پرس",
                    "بررسی دکمه‌های توقف اضطراری و دو دستی بودن فرمان",
                    persianService.Parse(new DateTime(2026, 5, 15, 12, 30, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("8b6c3d2e-6f7a-9b8c-3d4e-5f6a7b8c9d0e"),
                    "کنترل مواد نشت‌کننده",
                    "بازدید از شیرآلات و اتصالات برای نشتی روغن و سوخت",
                    persianService.Parse(new DateTime(2026, 5, 14, 15, 40, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("9c7d4e3f-7a8b-0c9d-4e5f-6a7b8c9d0e1f"),
                    "بازرسی بالابر ساختمان",
                    "تست کامل کابین، کابل‌ها و ترمزهای اضطراری بالابر",
                    persianService.Parse(new DateTime(2026, 5, 13, 10, 5, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("0d8e5f4a-8b9c-1d0e-5f6a-7b8c9d0e1f2a"),
                    "بررسی MSDS مواد",
                    "به‌روزرسانی برگه‌های اطلاعات ایمنی مواد در تمام ایستگاه‌ها",
                    persianService.Parse(new DateTime(2026, 5, 12, 18, 15, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("1e9f6a5b-9c0d-2e1f-6a7b-8c9d0e1f2a3b"),
                    "بازدید از سیستم آتش نشانی",
                    "تست پمپ، فلومتر و شیرهای اصلی شبکه آتش‌نشانی",
                    persianService.Parse(new DateTime(2026, 5, 11, 8, 30, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("2f0a7b6c-0d1e-3f2a-7b8c-9d0e1f2a3b4c"),
                    "آموزش ایمنی برق",
                    "برگزاری جلسه آشنایی با خطرات برق گرفتگی و قفل‌کردن انرژی",
                    persianService.Parse(new DateTime(2026, 5, 10, 13, 50, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("3a1b8c7d-1e2f-4a3b-8c9d-0e1f2a3b4c5e"),
                    "بازرسی نرده‌های محافظ",
                    "بررسی استحکام نرده‌های کنار پله‌ها و ارتفاع بالای ۲ متر",
                    persianService.Parse(new DateTime(2026, 5, 9, 16, 25, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("4b2c9d8f-2f3a-5b4c-9d0e-1f2a3b4c5d6f"),
                    "کنترل تجهیزات دسته جمعی",
                    "بررسی سلامت بالابرها، جک‌ها و تجهیزات تیمی حمل بار",
                    persianService.Parse(new DateTime(2026, 5, 8, 11, 0, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("5c3d0e9a-3a4b-6c5d-0e1f-2a3b4c5d6e7a"),
                    "بازبینی دوش‌های ایمنی",
                    "تست دوش‌های اضطراری و شستشوی چشم در آزمایشگاه",
                    persianService.Parse(new DateTime(2026, 5, 7, 14, 35, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("6d4e1f0b-4b5c-7d6e-1f2a-3b4c5d6e7f8b"),
                    "بررسی علائم هشدار",
                    "کنترل برچسب‌های خطر، تابلوهای ممنوعیت و الزامات",
                    persianService.Parse(new DateTime(2026, 5, 6, 9, 20, 0))
                ),
                new TaskPaginateResponse(
                    Guid.Parse("7e5f2a1c-5c6d-8e7f-2a3b-4c5d6e7f8a9c"),
                    "بازرسی فصلی تاسیسات",
                    "بررسی کامل موتورخانه، چیلرها و بویلرهای ساختمان",
                    persianService.Parse(new DateTime(2026, 5, 5, 17, 55, 0))
                )
            ];

        return 
            ValueTask.FromResult(
                Result.Success(
                    responses.Where(item => query.Records.Contains(item.Id))
                )
            );
    }
}