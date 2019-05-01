namespace server.Models 
{
  public class Coin
  {
    public int Id { get; set; }
    public int Value { get; set; }
    public int Quantity { get; set; }
    public bool IsBlocked { get; set; }
    public Device Device { get; set; }
  }
}