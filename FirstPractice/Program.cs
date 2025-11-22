using System;

namespace FirstPractice
{
    internal class Corps
    {
        private int growingDay = 0;
        private int harvestDay = 3;

        private bool hasBug = false;
        private int bugPenalty = 0; // 虫害惩罚

        private bool fertilizerApplied = false;
        private Random rng = new Random();

        public bool IsDead { get; private set; } = false;
        public bool HarvestBoost { get; private set; } = false;
        public bool IsMature => growingDay >= harvestDay;

        // -----------------------------
        // 每天生长
        // -----------------------------
        public void GrowOneDay()
        {
            if (IsDead)
            {
                Console.WriteLine("💀 The plant is dead. Nothing grows...");
                return;
            }

            growingDay++;

            // 肥料效果：额外增加1天生长
            if (fertilizerApplied)
            {
                growingDay++;
                fertilizerApplied = false;
                Console.WriteLine("✨ Fertilizer effect activated! Growth +1!");
            }

            // -----------------------------
            // C) 虫害统一发生概率（每天 20%）
            // -----------------------------
            if (!hasBug && rng.Next(1, 100) <= 20)
            {
                hasBug = true;
                Console.WriteLine("🐛 BUG APPEARED! Your crop quality is now reduced!");
            }

            // -----------------------------
            // D) 虫害累积损害（每日bugPenalty++）
            // -----------------------------
            if (hasBug)
            {
                bugPenalty++;
                Console.WriteLine($"⚠️ BUG DAMAGE: Penalty increased to {bugPenalty}!");
            }

            ShowGrowthInfo();
        }

        // -----------------------------
        // 显示状态
        // -----------------------------
        public void ShowGrowthInfo()
        {
            Console.WriteLine($"\n📅 Day {growingDay} of growth:");

            if (IsDead)
            {
                Console.WriteLine("💀 The plant is dead.");
                return;
            }

            if (hasBug)
            {
                Console.WriteLine($"🐛 STATUS: BUG PRESENT (Penalty: -{bugPenalty})");
            }

            if (growingDay == 1)
                Console.WriteLine("🌱 A tiny sprout has appeared!");
            else if (growingDay == 2)
                Console.WriteLine("🌿 The sprout grows taller~");
            else if (IsMature)
                Console.WriteLine("🥕 Mature crop! Ready to harvest!");
        }

        // -----------------------------
        // 使用肥料
        // -----------------------------
        public void UseFertilizer()
        {
            if (IsDead)
            {
                Console.WriteLine("❌ Fertilizer is useless on a dead plant.");
                return;
            }

            fertilizerApplied = true;
            HarvestBoost = true;

            Console.WriteLine("🌟 Fertilizer applied! Next day: growth +1 and boosted harvest!");
        }

        // -----------------------------
        // 使用除虫剂
        // -----------------------------
        public void UseBugDealer()
        {
            if (!hasBug)
            {
                Console.WriteLine("✨ No bugs to kill. Plant is healthy!");
                return;
            }

            hasBug = false;
            Console.WriteLine("🔫🐛 All bugs eliminated!");
        }

        // -----------------------------
        // 收获
        // -----------------------------
        public void Harvest()
        {
            if (!IsMature)
            {
                Console.WriteLine("⛔ Not ready! Crop is still growing!");
                return;
            }

            int baseYield = HarvestBoost ? rng.Next(7, 10) : rng.Next(3, 5);
            HarvestBoost = false;

            // A) 虫害降低产量
            int finalYield = baseYield - bugPenalty;
            if (finalYield < 1) finalYield = 1;

            Console.WriteLine($"🥕 You harvested {finalYield} crops! (Base {baseYield}, Penalty -{bugPenalty})");

            // 重置虫害
            hasBug = false;
            bugPenalty = 0;

            // 重置生长
            growingDay = 0;
            Console.WriteLine("🔄 New cycle begins...");
        }
    }
}
