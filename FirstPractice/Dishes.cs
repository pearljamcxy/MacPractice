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
                return MaterialCost[material];
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
                public int Cost()
                {
                    int total = 0;
                    foreach (var item in Materials)
                    {
                        total += MaterialPrice.GetPrice(item);
                    } 
                    return total;                 
                }
                
                public double SellPrice()
                {
                    return Cost() * 1.5;
                }
        }
    }
}