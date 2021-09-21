using System;

namespace YoHome
{
    class Program
    {
        static void Main(string[] arguments)
        {
            string GetArgument(int index, string name)
            {
                if (arguments.Length < 1)
                {
                    throw new ArgumentNullException(nameof(name), $"Argument `{name}` is empty.");
                }
                return arguments[index];
            }
            string command = GetArgument(0, nameof(command)).ToLowerInvariant();
            HouseholdChoreManager householdChoreManager = new HouseholdChoreManager();
            switch (command)
            {
                case "new":
                    {
                        int frequency = -1;
                        bool isNumber = Int32.TryParse(arguments[2], out frequency);
                        if (!isNumber || frequency < 0)
                        {
                            Console.WriteLine($"{arguments[1]}'s frequency({frequency}) is unreasonable.");
                            break;
                        }
                        householdChoreManager.CreateHouseholdChore(arguments[1], frequency);
                    }
                    break;
                default:
                    Console.WriteLine($"System don't recognize this command: {command}");
                    break;
            }
        }
    }
}
