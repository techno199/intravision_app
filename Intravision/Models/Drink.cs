namespace server.Models
{
  public class Drink
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public Device Device { get; set; } 
  }
}