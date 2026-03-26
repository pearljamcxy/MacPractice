using System;

using System;

namespace FirstPractice
{
    public static class ChestCheck
    {
        enum ChestState
        {
            Open,
            Closed,
            Locked
        }

        public static void Check()
        {
            ChestState chestState = ChestState.Locked;

            while (true)
            {
                Console.WriteLine($"The chest is {chestState}. What do you want to do?");
                string input = Console.ReadLine()??"Please Write the right command!!";
                
                switch (chestState)
                {
                    case ChestState.Locked:
                        if (input == "unlock")
                        {
                            chestState = ChestState.Closed;
                        }
                    break;

                    case ChestState.Closed:
                        if(input == "open")
                        {
                            chestState = ChestState.Open;
                        }
                        else if (input == "lock")
                        {
                            chestState = ChestState.Locked;
                        }
                    break;
                    
                    case ChestState.Open:
                        if(input == "close")
                        {
                            chestState = ChestState.Closed;
                        }
                    break;
                }
            }
        }
    }
}