using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstPractice{
    //先把架构搭出来 再往里面填东西并且修改
    public static class Dish
    {
        //枚举我需要的材料Material：世界里允许存在的材料（宪法）
        public enum Material
        {
            Bread,
            Bacon,
            Cheese,
            Tomato,
            Potato,
            Greenleafs
        }

        //MaterialPrices：材料价格规则（世界定律） 所以加上static,因为字典不能单独存在,所以需要定义一个class来包裹
        public static class MaterialPrice
        {
            public static readonly Dictionary<Material, int> MaterialCost = new()
            {
                {Material.Bread, 2},
                {Material.Bacon, 10},
                {Material.Cheese, 15},
                {Material.Tomato, 5},
                {Material.Potato, 4},
                {Material.Greenleafs, 8},
            };   
            public static int GetPrice(Material material)
            {
                if(!MaterialCost.TryGetValue(material, out int price))
                {
                    throw new InvalidOperationException($"No price defined for {material}");
                }
                return price;
            }

            public static void ShowPriceMap()
            {
                Console.WriteLine("======Material price are as follows======");
                foreach (var pair in MaterialCost)
                {
                    Console.WriteLine($"{pair.Key} : {pair.Value}");
                }
                
            }
        }

        //接下来定义菜谱Dish 也就是如何用一个class来构建不同的dish
        public class DishMaker
        {
            public string Name {get; } 
            public int CookTime {get; } 
            public int SpoilTime {get; } 
            public IReadOnlyList<Material> Materials {get; } 
            public DishMaker(string name, int cookTime, int spoilTime, List<Material> materials)
            {
                Name = name;
                CookTime = cookTime;
                SpoilTime = spoilTime;
                Materials = materials;
            }
                public decimal Cost()
                {
                    decimal total = 0m;
                    foreach (var item in Materials)
                    {
                        total += MaterialPrice.GetPrice(item);
                    } 
                    return total;                 
                }
                
                public decimal SellPrice()
                {
                    return Cost() * 1.5m;
                }
        }

        public class Player
        {
            public decimal Gold = 500m;
            public Dictionary<Material, int> MaterialBag = new();
            public Dictionary<DishMaker, int> DishBag = new();

            //一个一个买材料,并非一口气把需要的都买了,好判断背包里是不是存在,是叠加还是重新创建一个
            public void BuyMaterial(Material material, int amount)
            {
                //先判断是不是足够金币
                int SingleCost = MaterialPrice.GetPrice(material) * amount;
                if (Gold < SingleCost)
                {
                    Console.WriteLine("You dont have enough Gold!!");
                    return;
                }
                Gold -= SingleCost;
                //再判断背包里是不是有重复材料
                if (MaterialBag.ContainsKey(material))
                {
                    MaterialBag[material] += amount;
                }
                else
                {
                    MaterialBag[material] = amount;
                }
                
                Console.WriteLine($"You bought {material} -- {amount}");
            }

            public void ShowPlayerInfo()
            {
                Console.WriteLine("=====You are looking into your bag....======");
                Console.WriteLine($"You have {Gold} Gold");
                Console.WriteLine($"=====You have Materials:=====");
                foreach (var itemM in MaterialBag)
                {
                    Console.WriteLine($"{itemM.Key} -- {itemM.Value}");
                }

                Console.WriteLine($"=====You have Dishes:=====");
                foreach (var itemD in DishBag)
                {
                    Console.WriteLine($"{itemD.Key} -- {itemD.Value}");
                }
            }

            public void SellDishes()
            {
                decimal SoldGold = 0m;
                foreach (var dish in DishBag)
                {
                    SoldGold += dish.Key.SellPrice() * dish.Value;
                }
                Gold += SoldGold;
                DishBag.Clear();
                Console.WriteLine($"Sold all dishes, earned {SoldGold} gold!!");
            }

        }

        public class Kitchen
        {
            public void CookDish(Player player, DishMaker dish)
            {
                Console.WriteLine($"Cooking {dish.Name} please wait {dish.CookTime} seconds......");
                Thread.Sleep(dish.CookTime * 1000);
                //最简单的稳健的 判断某个key在不在字典里的方法,如果存在TryGetValue返回true,并且把这个key的value储存在count里
                //如果不存在 count为int的默认值0;
                player.DishBag.TryGetValue(dish, out int count);
                player.DishBag[dish] = 1 + count;

                Console.WriteLine($"{dish.Name} is ready! 🍽️");
            }
        }
    }

}