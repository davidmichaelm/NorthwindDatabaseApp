using System.Collections.Generic;

namespace NorthwindDatabaseApp.UI
{
    public interface IInput
    {
        string GetMenuOption(Dictionary<string, string> menuOptions);
        string GetStringInput(string inputMessage);
        string GetStringInput();
        int GetIntInput(string inputMessage);
        decimal GetDecimalInput(string inputMessage);
        short GetShortInput(string inputMessage);
        bool GetConfirmation(string confirmationMessage);
    }
}