using System;
using System.Collections.Generic;

namespace FirstPractice
{
    public static class DishTest
    {
        public static void Run()
        {
            Console.WriteLine("===== Material Price Table =====");
            Dish.MaterialPrice.ShowPriceMap();
            Console.WriteLine();

            // 测试菜 1：培根披萨
            var baconPizza = new Dish.DishMaker(
                name: "Bacon Pizza",
                cookTime: 5,
                spoilTime: 10,
                materials: new List<Dish.Material>
                {
                    Dish.Material.Bread,
                    Dish.Material.Bacon,
                    Dish.Material.Cheese,
                    Dish.Material.Tomato
                }
            );

            // 测试菜 2：素食沙拉
            var veggieSalad = new Dish.DishMaker(
                name: "Veggie Salad",
                cookTime: 2,
                spoilTime: 5,
                materials: new List<Dish.Material>
                {
                    Dish.Material.Greenleafs,
                    Dish.Material.Tomato,
                    Dish.Material.Potato
                }
            );

            ShowDishInfo(baconPizza);
            ShowDishInfo(veggieSalad);
        }

        private static void ShowDishInfo(Dish.DishMaker dish)
        {
            Console.WriteLine("===== Dish Info =====");
            Console.WriteLine($"Name      : {dish.Name}");
            Console.WriteLine($"Cook Time : {dish.CookTime}");
            Console.WriteLine($"SpoilTime : {dish.SpoilTime}");
            Console.WriteLine($"Cost      : {dish.Cost()}");
            Console.WriteLine($"SellPrice : {dish.SellPrice()}");
            Console.WriteLine();
        }
    }
}
