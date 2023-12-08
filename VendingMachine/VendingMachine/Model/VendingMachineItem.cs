using System;


//This could be an abstraction class to allow to reuse functionality for many different item types
public class VendingMachineItem
{

    public string Name { get; set; }
    public int Price { get; set; }
    public int Volume { get; set; }
    public string MeasuringUnit { get; set; }


    public VendingMachineItem(string name, int price, int volume, string measuringUnit)
    {
        Name = name;
        Price = price;
        Volume = volume;
        MeasuringUnit = measuringUnit;
    }

    public void DisplayProductInfo()
    {
        //could allow user to input their currency to then display on item (with conversion)
        Console.WriteLine($"{this.Name}, {this.Price}NOK, {this.Volume}{this.MeasuringUnit}");
    }
}