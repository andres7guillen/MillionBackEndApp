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
    ErrorRegisteringUser = 1001    
}
