using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Common
{
  public class MileageReport
  {
    private readonly Car _car;
    private readonly string _message;

    public MileageReport(Car car, string message)
    {
      _car = car;
      _message = message;
    }

    public override string ToString()
    {
      return _car == null ? _message : _message + " " + _car;
    }

    public bool IsFailed => _car == null;
  }
}
