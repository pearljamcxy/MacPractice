using System;

using System;

namespace FirstPractice
{
    public static class CheckClockState
    {   
        // 1. 枚举定义（你放这里没问题，非常标准）
        public enum ChestState
        {
            Open,
            Closed,  // 在你的设计里，Closed 就相当于 Unlocked（解锁但没掀开盖子）
            Locked
        }

        public static void Check()
        {
            // 初始化箱子状态变量
            ChestState chestState = ChestState.Locked;

            while (true)
            {
                // 2. 注意这里改成了 chestState 变量！
                Console.WriteLine($"The chest is {chestState}. What do you want to do? ");
                string input = Console.ReadLine();

                switch (chestState)
                {
                    case ChestState.Locked:
                        // 3. 完整的 if 语法
                        if (input == "unlock") 
                        {
                            chestState = ChestState.Closed; // 解锁后，变成合上(Closed)的状态
                        }
                        break; // 4. case 必须以 break 结尾！

                    case ChestState.Closed:
                        if (input == "open") 
                        {
                            chestState = ChestState.Open; // 打开盖子
                        }
                        else if (input == "lock")
                        {
                            chestState = ChestState.Locked; // 重新上锁
                        }
                        break;

                    case ChestState.Open:
                        if (input == "close")
                        {
                            chestState = ChestState.Closed; // 关上盖子
                        }
                        break;
                }
            }
        }
    }
}