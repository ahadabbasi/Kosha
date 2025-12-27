using System;
using System.Collections.Generic;
using System.Linq;

namespace Kosha.CustomerManager.Web.Models.Layers.Shared.Results;

public class Result
{
    internal Result(bool isSuccess, Error[] errors)
    {
        if (
            isSuccess && errors.Any() ||
            !isSuccess && !errors.Any()
        )
        {
            throw new Exception();
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }


    public IEnumerable<Error> Errors { get; }

    public static Result Success() => new(true, []);

    public static Result Failed(Error error) => Failed([error]);

    public static Result Failed(Error[] errors) => new Result(false, errors);

    public static Result<TData> Success<TData>(TData data) => new(true, data, []);

    public static Result<TData> Failed<TData>(Error error) => Failed<TData>([error]);

    public static Result<TData> Failed<TData>(Error[] errors) => new(false, default, errors);

    public static implicit operator bool(Result entry) => entry.IsSuccess;

    public static implicit operator Result(bool entry) => entry ? Success() : Failed(Error.None);

    public static implicit operator Result(Error entry) => Failed(entry);

}

public class Result<TData> : Result
{
    internal Result(bool isSuccess, TData? data, Error[] errors) : base(isSuccess, errors)
    {
        if (isSuccess && data == null)
        {
            throw new Exception();
        }

        Data = data;
    }


    public TData? Data { get; }


    public static implicit operator TData(Result<TData> entry) => 
        entry.IsSuccess && entry.Data != null ?
            entry.Data : 
            throw new InvalidOperationException();
}