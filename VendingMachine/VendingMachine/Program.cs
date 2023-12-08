using System;

internal class Program
{
    private static void Main(string[] args)
    {

        List<VendingMachineItem> vendingMachineItems = new List<VendingMachineItem>();
        FillMachine(vendingMachineItems);

        int credit = 0;
        bool operational = true;

        Console.WriteLine("Welcome to Jaydin's affordable vending machine");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("Start by checking out our range of products by typing 'list'");
        Console.WriteLine("Or if you need help just type 'help'");
        Console.WriteLine("If you are finished using the vending machine type 'exit'");

        while (operational)
        {

            string? userInput = Console.ReadLine();

            if (!string.IsNullOrEmpty(userInput))
            {
                userInput = userInput.ToLower().Trim();
                string sanitizedInput = System.Security.SecurityElement.Escape(userInput);
                string[] inputElements = sanitizedInput.ToLower().Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);

                string action = inputElements[0];
                string actionParameter = "";

                if (inputElements.Length == 2)
                {
                    actionParameter = inputElements[1];
                }

                switch (action)
                {

                    case "exit":
                        operational = false;
                        break;

                    case "list":
                        vendingMachineItems.ForEach(item => item.DisplayProductInfo());
                        break;

                    case "balance":
                        Console.WriteLine($"Your credit balance is {credit}.");
                        break;

                    case "insert":

                        if (int.TryParse(actionParameter, out int parsedNumber) && parsedNumber > 0)
                        {
                            int creditValue = int.Parse(actionParameter);
                            credit += creditValue;
                            Console.WriteLine($"Your credit balance is now {credit}.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid credit amount. Please enter a valid positive integer.");

                        }

                        break;

                    case "recall":
                        if (credit != 0)
                        {
                            Console.WriteLine($"Giving out {credit}.");
                            credit = 0;
                            Console.WriteLine($"Your credit balance is now {credit}.");

                        }
                        else
                        {
                            Console.WriteLine("You don't have any credit to recall");
                        }
                        break;

                    case "order":

                        if (!string.IsNullOrEmpty(actionParameter))
                        {

                            VendingMachineItem? foundItem = vendingMachineItems.FirstOrDefault(item => item.Name.Equals(actionParameter, StringComparison.OrdinalIgnoreCase));
                            if (foundItem != null)
                            {
                                if (credit > foundItem.Price)
                                {
                                    Console.WriteLine($"Giving out {foundItem.Name}. Giving back change of {credit - foundItem.Price}");
                                    credit = 0;
                                    Console.WriteLine($"Your credit balance is now {credit}.");
                                }
                                else if (credit == foundItem.Price)
                                {
                                    Console.WriteLine($"Giving out {foundItem.Name}. No Change Given");
                                    credit = 0;
                                    Console.WriteLine($"Your credit balance is now {credit}.");
                                }
                                else
                                {
                                    Console.WriteLine($"Not enough credit, need {foundItem.Price - credit} more");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Item '{actionParameter}' not found in the vending machine.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid product name. Please enter a valid item for sale.");

                        }



                        break;

                    case "help":
                        Console.WriteLine($"----------------------------------------------------------------------------");
                        Console.WriteLine($"Our available options are 'list', 'balance', 'insert', 'recall', 'order', and 'exit'.");
                        Console.WriteLine($"'list' will display all of our available products for you.");
                        Console.WriteLine($"'balance' will give you your current credit balance to spend.");
                        Console.WriteLine($"'insert' will give you credit in the vending machine when followed by a number e.g. 'insert 30'.");
                        Console.WriteLine($"'recall' will return all your credit back to you.");
                        Console.WriteLine($"'order' will allow you to order an item from the vending machine e.g. 'order coke'.");
                        Console.WriteLine($"'exit' will close the vending machine for sales.");
                        Console.WriteLine($"----------------------------------------------------------------------------");
                        break;

                    default:
                        Console.WriteLine($"You entered: {userInput}");
                        Console.WriteLine($"Our available options are 'list', 'balance', 'insert', 'recall', 'order', and 'exit'");
                        break;
                }
            }
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }



    private static void FillMachine(List<VendingMachineItem> vendingMachineItems)
    {

        VendingMachineItem coke = new VendingMachineItem("Coke", 5, 330, "ml");
        VendingMachineItem fanta = new VendingMachineItem("Fanta", 10, 500, "ml");
        VendingMachineItem mountainDew = new VendingMachineItem("Mountain Dew", 3, 330, "ml");
        VendingMachineItem kvikkLunsj = new VendingMachineItem("Kvikk Lunsj", 10, 30, "g");
        VendingMachineItem coffee = new VendingMachineItem("Coffee", 6, 200, "ml");
        VendingMachineItem hotChocolate = new VendingMachineItem("Hot Chocolate", 4, 200, "ml");
        VendingMachineItem skittles = new VendingMachineItem("Skittles", 9, 250, "g");
        VendingMachineItem noodles = new VendingMachineItem("Instant Noodles", 3, 100, "g");
        VendingMachineItem orangeJuice = new VendingMachineItem("Orange Juice", 10, 500, "ml");
        VendingMachineItem marsBar = new VendingMachineItem("Mars Bar", 4, 20, "g");


        vendingMachineItems.Add(coke);
        vendingMachineItems.Add(fanta);
        vendingMachineItems.Add(mountainDew);
        vendingMachineItems.Add(kvikkLunsj);
        vendingMachineItems.Add(coffee);
        vendingMachineItems.Add(hotChocolate);
        vendingMachineItems.Add(skittles);
        vendingMachineItems.Add(noodles);
        vendingMachineItems.Add(orangeJuice);
        vendingMachineItems.Add(marsBar);
    }
}