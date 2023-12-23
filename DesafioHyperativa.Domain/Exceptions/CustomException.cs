using System.Runtime.Serialization;

namespace DesafioHyperativa.Domain.Exceptions;

public class CustomException : Exception
{
    public CustomException()
    {
    }

    public CustomException(string message) : base(message)
    {
    }

    public CustomException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public virtual int ErrorCode => 500;
    public virtual string ErrorTitle => "Internal Error Server";
}
