namespace MillionApp.Domain.SharedKernel;

public abstract class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
    public virtual int Code { get; set; }
}
