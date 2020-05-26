using System;
using System.Collections.Generic;

namespace NorthwindDatabaseApp.UI
{
    public class KeyboardInput : IInput
    {
        private IDisplay _display;

        public KeyboardInput(IDisplay display)
        {
            _display = display;
        }
        
        public string GetMenuOption(Dictionary<string, string> menuOptions)
        {
            _display.ShowMessage("\nEnter the number of one of the following options:\n");
            foreach (var option in menuOptions)
            {
                _display.ShowMessage(option.Key + ". " + option.Value + "\n");
            }

            var userChoice = GetStringInput();
            while (!menuOptions.ContainsKey(userChoice))
            {
                userChoice = GetStringInput("Input not valid. Try again:");
            }

            return userChoice;
        }

        public string GetStringInput(string inputMessage)
        {
            _display.ShowMessage(inputMessage);
            return Console.ReadLine();
        }

        public string GetStringInput()
        {
            return Console.ReadLine();
        }

        public int GetIntInput(string inputMessage)
        {
            var stringInput = GetStringInput(inputMessage);
            int intInput;
            while (!int.TryParse(stringInput, out intInput))
            {
                stringInput = GetStringInput("Enter a valid integer:");
            }

            return intInput;
        }

        public decimal GetDecimalInput(string inputMessage)
        {
            var stringInput = GetStringInput(inputMessage);
            decimal decimalInput;
            while (!decimal.TryParse(stringInput, out decimalInput))
            {
                stringInput = GetStringInput("Enter a valid decimal:");
            }

            return decimalInput;
        }

        public short GetShortInput(string inputMessage)
        {
            var stringInput = GetStringInput(inputMessage);
            short shortInput;
            while (!short.TryParse(stringInput, out shortInput))
            {
                stringInput = GetStringInput("Enter a valid decimal:");
            }

            return shortInput;
        }

        public bool GetConfirmation(string confirmationMessage)
        {
            var userChoice = GetStringInput(confirmationMessage);
            while (userChoice == null || (userChoice.ToUpper() != "Y" && userChoice.ToUpper() != "N"))
            {
                userChoice = GetStringInput("Please enter 'y' for yes or 'n' for no:\n");
            }

            return userChoice.ToUpper() == "Y";
        }
    }
}