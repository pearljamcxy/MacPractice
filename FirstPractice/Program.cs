// See https://aka.ms/new-console-template for more information

using System;
using System.Dynamic;
using System.IO.Compression;
using System.Runtime.CompilerServices;

namespace FirstPractice
{
    internal class Corps
    {
        //定义属性(包装访问条件)Pascal   类内部:字段 私有方法内部 临时:变量 camel
        private int growingDay = 0;
        private int harvestDay = 3;
        private bool hasBug = false;
        private int bugsDay = 0;
        public bool IsDead {get; private set;} = false;
        public bool HarvestBoost {get; private set;} = false;
        public bool IsMature => growingDay >= harvestDay;
        private Random rng = new Random();


        public void GrowOneDay()
        {
            if (IsDead)
            {
                Console.WriteLine("");
                return;
            }
            growingDay++;

            if (!IsMature)
            {
                if(!hasBug && rng.Next(1, 100) < 20)
                {
                    hasBug = true;
                    bugsDay = 0;
                    Console.WriteLine("");
                } 
            }
            if (hasBug)
            {
                bugsDay++;
                Console.WriteLine("");
                if (bugsDay >= 3)
                {
                    IsDead = true;
                    growingDay = 0;
                    Console.WriteLine("");
                    return;
                }
            }
            ShowGrowthInfo();
        } 
        public void ShowGrowthInfo()
        {
            Console.WriteLine($"\n📅 This is the {growingDay} day.");
            if (IsDead)
            {
                Console.WriteLine("💀crops dead");
                return;
            }
            if (hasBug)
            {
                Console.WriteLine("🐛 STATUS:BUG");
            }
            if (growingDay == 1)
            {
                Console.WriteLine("🌱A small sprout has emerged!");
            }
            else if (growingDay == 2)
            { 
                Console.WriteLine("🌿A small sprout has emerged!");
            }
            else if(growingDay >= harvestDay)
            {
                Console.WriteLine("🥕You can harvest your crops now!!");
            }

        }
        public void UseFertilizer()
        {
            if (IsDead)
            {
                Console.WriteLine("");
                return;
            }
            growingDay++;
            HarvestBoost = true;


        }
        public void UseBugDealer()   
        {
            if (IsDead)
            {
                Console.WriteLine("");
                return;
            }
            if (hasBug)
            {
                hasBug = false;
                bugsDay = 0;
                Console.WriteLine("You've killed all bugs!The plant is growing");
            }
        }
        public void Harvest()
        {
            if (IsMature)
            {
                if (HarvestBoost)
                {
                    int yieldBoost = rng.Next(5,8);
                    Console.WriteLine($"{yieldBoost}");
                }
                int yield = rng.Next(2,5);
                Console.WriteLine($"{yield}");
            }
            
                
            
        }













    }
}
       