using System.Runtime.Serialization;

namespace DesafioHyperativa.Domain.Exceptions;

public class UnauthorizedException : CustomException
{
    public UnauthorizedException()
    {
    }

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public override int ErrorCode => 401;
    public override string ErrorTitle => "Unauthorized";
}
