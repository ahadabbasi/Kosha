using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Kosha.CustomerManager.Web.Domain.Enums;

public enum AnsEnum
{
    [EnumMember, Display(Name = "بله")]
    Yes = 1,

    [EnumMember, Display(Name = "خیر")]
    No = 0
}