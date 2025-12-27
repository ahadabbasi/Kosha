namespace Kosha.CustomerManager.Web.Domain.Helper;

public interface IRowVersion
{
    byte[] RowVersion { get; set; }
}