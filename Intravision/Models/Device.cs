using System.Collections.Generic;

namespace server.Models 
{
  public class Device
  {
    public int Id { get; set; }
    public string AdminCode { get; set; }
    public IList<Drink> Drinks{ get; set; } = new List<Drink>();
    public IList<Coin> Coins { get; set; } = new List<Coin>();
  }
}