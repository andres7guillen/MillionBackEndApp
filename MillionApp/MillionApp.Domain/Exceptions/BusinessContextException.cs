using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionApp.Domain.SharedKernel;

namespace MillionApp.Domain.Exceptions;

public class BusinessContextException : BusinessException
{
    public BusinessContextException(BusinessContextExceptionEnum comercioContextExceptionEnum)
        : base(Detail(comercioContextExceptionEnum).Item2)
    {
        Code = Detail(comercioContextExceptionEnum).Item1;
    }

    public static Tuple<int, string> Detail(BusinessContextExceptionEnum comercioContextExceptionEnum)
    {
        var code = (int)comercioContextExceptionEnum;
        var detail = comercioContextExceptionEnum switch
        {
            BusinessContextExceptionEnum.PasswordOrUserInvalid => new Tuple<int, string>(code, "Password or email invalid."),
            BusinessContextExceptionEnum.ErrorRegisteringUser => new Tuple<int, string>(code, "an error occurred while registering or adding a user."),
            BusinessContextExceptionEnum.NoPropertiesFound => new Tuple<int, string>(code, "No propertires found"),
            BusinessContextExceptionEnum.PropertyIdIsmatch => new Tuple<int, string>(code, "Property ID mismatch"),
            BusinessContextExceptionEnum.ErrorCreatingProperty => new Tuple<int, string>(code, "An error ocurred while trying to save the property."),
            BusinessContextExceptionEnum.ErrorUpdatingProperty => new Tuple<int, string>(code, "An error ocurred while trying to update the property"),
            BusinessContextExceptionEnum.ErrorChangingPrice => new Tuple<int, string>(code, "An error ocurred while trying to change the price of the entity"),
            BusinessContextExceptionEnum.ErrorRemovingProperty => new Tuple<int, string>(code, "An error ocurred while trying to remove the property"),
            
            _ => new Tuple<int, string>(code, "Unknown Error")
        };

        return detail;
    }

    public string GetFormattedDetail()
    {
        return $"Code: {Code}, Message: {Message}";
    }
}
public enum BusinessContextExceptionEnum
{
    //1000 Login Security
    PasswordOrUserInvalid = 1000,
    ErrorRegisteringUser = 1001,
        
    //2000 Business
    NoPropertiesFound = 2001,
    PropertyIdIsmatch = 2002,
    ErrorCreatingProperty = 2003,
    ErrorUpdatingProperty = 2004,
    ErrorChangingPrice = 2005,
    ErrorRemovingProperty = 2006
}
