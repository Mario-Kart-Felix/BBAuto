namespace BBAuto.Logic.Services.Account
{
  public class AccountModel
  {
    public int Id { get; private set; }
    public string Number { get; private set; }
    public int Agreed { get; private set; }
    public int PolicyTypeId { get; private set; }
    public int OwnerId { get; private set; }
    public int PaymentNumber { get; private set; }
    public int BusinessTrip { get; private set; }
    public string File { get; private set; }
  }
}
