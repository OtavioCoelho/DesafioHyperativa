using System.Runtime.Serialization;

namespace DesafioHyperativa.Domain.Exceptions;

public class ManyRequestsException : CustomException
{
    public ManyRequestsException()
    {
    }

    public ManyRequestsException(string message) : base(message)
    {
    }

    public ManyRequestsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ManyRequestsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public override int ErrorCode => 429;
    public override string ErrorTitle => "Too Many Requests";
}
