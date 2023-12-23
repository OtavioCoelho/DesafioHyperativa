using System.Runtime.Serialization;

namespace DesafioHyperativa.Domain.Exceptions;

public class BusinessException : CustomException
{
    public BusinessException()
    {
    }

    public BusinessException(string message) : base(message)
    {
        this.Data["Message"] = message;
    }

    public BusinessException(string message, Exception innerException) : base(message, innerException)
    {
        this.Data["Message"] = message;
    }

    protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
    public BusinessException(params string[] messages)
    {
        this.Data["Message"] = new HashSet<string>(messages).ToArray();
    }
    public override int ErrorCode => 400;
    public override string ErrorTitle => "Bad Request";
}