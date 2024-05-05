using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Utility
{
    public static class ItemNameGenerator
    {
        private static string[] HeadNames = ["Bandana","Helmet","Mask","Crown","Hat"];
        private static string[] BodyNames = ["Gambeson", "Cuirass", "Corslet", "Mail", "Armor"];
        private static string[] LegsNames = ["Leggings", "Skirt", "Greaves"];
        private static string[] ArmsNames = ["Pauldron","Gauntlet"];
        private static string[] RingNames = ["Ring","Band"];
        private static string[] NecklaceNames = ["Pendant", "Necklace","Collar", "Medallion"];
        private static string[] TrinketNames = ["Talisman", "Gem", "Blade","Tome"];

        private static string[] EquippableAdjectives = ["Leather","Iron", "Steel", "Sturdy", "Wooden", "Jeweled"];
        private static string[] TrinketAdjectives = ["Fancy","Powerful","Magical","Shiny"];

        public static string GenerateItemTitle(ItemType type)
        {
            string name = GenerateItemName(type);
            string adjective = GenerateItemAdejctive(type);
            return adjective + " " + name;  
        }


        private static string GenerateItemName(ItemType type)
        {
            string name = string.Empty;
            switch (type)
            {
                case ItemType.Head:
                    name = GenerateHeadName();
                    break;

                case ItemType.Body:
                    name = GenerateBodyName();
                    break;

                case ItemType.Legs:
                    name = GenerateLegsName();
                    break;

                case ItemType.Arms:
                    name = GenerateArmsName();
                    break;

                case ItemType.Ring:
                    name = GenerateRingName();
                    break;

                case ItemType.Necklace:
                    name = GenerateNecklaceName();
                    break;

                case ItemType.Trinket:
                    name = GenerateTrinketName();
                    break;
            }

            return name;

        }

        private static string GenerateItemAdejctive(ItemType type)
        {  
            if(type == ItemType.Trinket)
            {
                return GenerateTrinketAdjective(); 
            }

            return GenerateEquippableAdjective();
        }

        private static string GenerateHeadName()
        {
            int chosenName = RandomIndex(HeadNames.Length);
            return HeadNames[chosenName];
        }
        private static string GenerateBodyName()
        {
            int chosenName = RandomIndex(BodyNames.Length);
            return BodyNames[chosenName];

        }
        private static string GenerateLegsName()
        {
            int chosenName = RandomIndex(LegsNames.Length);
            return LegsNames[chosenName];
        }
        private static string GenerateArmsName()
        {
            int chosenName = RandomIndex(ArmsNames.Length);
            return ArmsNames[chosenName];
        }
        private static string GenerateRingName()
        {
            int chosenName = RandomIndex(RingNames.Length);
            return RingNames[chosenName];
        }
        private static string GenerateNecklaceName()
        {
            int chosenName = RandomIndex(NecklaceNames.Length);
            return NecklaceNames[chosenName];
        }
        private static string GenerateTrinketName()
        {
            int chosenName = RandomIndex(TrinketNames.Length);
            return TrinketNames[chosenName];
        }
      
        private static string GenerateEquippableAdjective()
        {
            int chosenName = RandomIndex(EquippableAdjectives.Length);
            return EquippableAdjectives[chosenName];
        }
        private static string GenerateTrinketAdjective()
        {
            int chosenName = RandomIndex(TrinketAdjectives.Length);
            return TrinketAdjectives[chosenName];
        }


    }
}
