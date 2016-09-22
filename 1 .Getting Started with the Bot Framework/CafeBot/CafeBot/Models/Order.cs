using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeBot.Models
{
    public enum Drinks
    {
        None = 1, Americana, Espresso, Lemonade, Water, OrangeJuice, Cola
    };

    public enum Snacks
    {
        None = 1, ChoclateCookie, FrenchFries,
    };

    public enum Food
    {
        None = 1, BreakfastQuiche, Omelette, VegieWrap, ChickenSandwich, BigBurger
    }

    public enum DeliveryOption
    {
        Pickup = 1, Delivery
    }

    [Serializable]
    public class Order
    {
        public List<Drinks> Drinks;
        public List<Snacks> Snacks;
        public List<Food> Meal;
        public DeliveryOption DeliveryOption;
    }
}