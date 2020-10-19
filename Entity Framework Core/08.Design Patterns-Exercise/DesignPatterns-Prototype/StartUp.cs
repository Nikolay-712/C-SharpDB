using System;

namespace DesignPatterns_Prototype
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SandwichMenu sandwichMenu = new SandwichMenu();

            sandwichMenu["BLT"] = new Sandwich("Wheat","Becon","Lettuce","Tomato");

            Sandwich sandwich = sandwichMenu["BLT"].Clone() as Sandwich;
        }
    }
}
