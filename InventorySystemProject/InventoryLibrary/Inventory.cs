using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary
{
    public static class Inventory
    {
        
        // accessing any property of a static class use the class name eg. Inventory.Products
        private static BindingList<Product> allProducts =
            new BindingList<Product>();
        public static BindingList<Product> Products
        { get { return allProducts; } set { allProducts = value; } }

        private static BindingList<Part> allParts =
            new BindingList<Part>();
        public static BindingList<Part> AllParts
        { get { return allParts; } set { allParts = value; } }

        private static BindingList<Product> searchProdList =
            new BindingList<Product>();
        public static BindingList<Product> SearchProductList
        { get { return searchProdList; } set { searchProdList = value; } }

        private static BindingList<Part> searchAParts =
            new BindingList<Part>();
        public static BindingList<Part> SearchPartList
        { get { return searchAParts; } set { searchAParts = value; } }

        private static BindingList<Part> tempAssocParts =
        new BindingList<Part>();
        public static BindingList<Part> TempAssocParts
        { get { return tempAssocParts; } set { tempAssocParts = value; } }

        public static void addProduct(Product addProd)
        {
            Products.Add(addProd);
        }

        public static bool RemoveProduct(int j)
        {
            Inventory.Products.RemoveAt(j);
            return true;
        }

        public static Product lookupProduct(int j)
        {
            return Inventory.Products[j];
        }

        public static void addPart(Part addPart)
        {
            AllParts.Add(addPart);
        }

        public static bool deletePart(int j)
        {
            AllParts.RemoveAt(j);
            return true;
        }

        public static Part lookupPart(int j)
        {
            return Inventory.AllParts[j];
        }

        public static void updatePart(int k, Part part)
        {
            AllParts.RemoveAt(k);
            AllParts.Add(part);
        }

        public static void testData()
        {
            AllParts.Add(new Inhouse(2333, 4011, "Small Wheel",
                10.33m, 12, 2, 50));
            AllParts.Add(new Inhouse(2335, 4016, "Tiny Screw",
                15.00m, 6, 2, 56));
            AllParts.Add(new Inhouse(2337, 4020, "Funny Horn",
                60m, 128, 122, 500));
            AllParts.Add(new Inhouse(2411, 4152, "Sounding Brass",
                45.12m, 5, 2, 12));
            AllParts.Add(new Inhouse(2615, 4144, "Bunny Ears",
                6.66m, 8, 7, 20));
            AllParts.Add(new Inhouse(2768, 4516, "Noob Sticker",
                91.13m, 12, 3, 300));
            AllParts.Add(new Inhouse(2222, 4689, "Inside-Out Slinky",
                82.11m, 47, 40, 244));
            AllParts.Add(new Outsourced("Chain Store", 4152, "Jingley Chain",
                17.12m, 7, 1, 50));
            AllParts.Add(new Outsourced("Local Connectors", 4651, "THE Nut",
                13.12m, 84, 152, 500));
            AllParts.Add(new Outsourced("Recycled Parts", 4993, "Yellowed Seat",
                12.10m, 17, 17, 50));
            AllParts.Add(new Outsourced("Recycled Parts", 4731, "Broken Tire",
                147.10m, 10, 2, 544));
            AllParts.Add(new Outsourced("Music Store", 4278, "8 Track",
                1412.42m, 47, 2, 147));
            AllParts.Add(new Outsourced("Foreign Imports", 4676, "Chopsticks",
                71.10m, 177, 172, 500));
            Products.Add(new Product(5001, "Offroad Unicycle", 100.00m,
                14, 8, 52, AllParts[1], AllParts[3], AllParts[6]));
            Products.Add(new Product(5021, "Random Organ Teleporter", 1000.00m,
                122, 56, 123, AllParts[2], AllParts[1] ));
            Products.Add(new Product(5087, "Ice Cream Launcher", 4.00m,
                6, 1, 43, AllParts[3], AllParts[5]));
            Products.Add(new Product(1234, "Missing Pin", 1m, 40, 11, 45, AllParts[4], AllParts[8]));
            Products.Add(new Product(5271, "Potato Shaped Balloon", 111.00m,
                16, 6, 57, AllParts[12], AllParts[5], AllParts[7]));
            Products.Add(new Product(5501, "Tie Fighter", 121.00m,
                57, 7, 72, AllParts[11], AllParts[9], AllParts[6]));
            Products.Add(new Product(5901, "Space/Time Parachute", 30.00m,
                17, 8, 28, AllParts[10], AllParts[6], AllParts[9]));
        }
    }
}

