using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Configurations;

public class ErrorConfiguration
{
    public static readonly Error NotImplemented =
        new(
            nameof(NotImplemented),
            "The method or operation is not implemented."
        );

    public static readonly Error UsernameOrPasswordIsWrong =
        new(
            nameof(UsernameOrPasswordIsWrong),
            "Username or password is wrong."
        );

    public static readonly Error UsernameNotFound =
        new(
            nameof(UsernameNotFound),
            "Username couldn't found"
        );

    public static readonly Error UsernameExist =
        new(
            nameof(UsernameExist),
            "Username already exist"
        );

    public static readonly Error PhoneNumberExist =
        new(
            nameof(PhoneNumberExist),
            "Phone number already exist"
        );

    public static readonly Error HashedNotMatch =
        new (
            nameof(HashedNotMatch),
            "Hashed is not match."
        );

    public static readonly Error TaskTypeInvalid =
        new (
            nameof(TaskTypeInvalid),
            "Task type requested is invalid."
        );

    public static readonly Error TaskNotFound =
        new (
            nameof(TaskNotFound),
            "Task couldn't found."
        );

    public static readonly Error CategoryNotFound =
        new(
            nameof(CategoryNotFound),
            "Task couldn't found."
        );

    public static readonly Error CategoryAlreadyExist =
        new(
            nameof(CategoryAlreadyExist),
            "Task couldn't found."
        );

    public static readonly Error TagNotFound =
        new(
            nameof(TagNotFound),
            "Task couldn't found."
        );

    public static readonly Error TagAlreadyExist =
        new(
            nameof(TagAlreadyExist),
            "Task couldn't found."
        );
}