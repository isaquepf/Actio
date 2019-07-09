using System;

namespace Actio.Common.Exceptions
{
  public class ActioException : Exception
  {
    public string Code { get; }

    public ActioException() { }

    public ActioException(string code)
        => Code = code;

    public ActioException(string message, string code, params object[] args)
    {
    }
  }
}