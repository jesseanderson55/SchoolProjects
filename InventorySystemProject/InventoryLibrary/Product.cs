using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary
{
    public class Product
    {
        // acccessing any property of a nonstatic class use the class object name eg. o.Products
        // o.AssociatedParts.Add{
        private BindingList<Part> associatedParts =
            new BindingList<Part>();
        public BindingList<Part> AssociatedParts
        { get { return associatedParts; } set { associatedParts = value; } }


        //public BindingList<Part> AssociatedParts { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public void addAssociatedPart(Part part)
        {
            this.AssociatedParts.Add(part);
        }

        public bool removeAssociatedPart(int paID)
        {
            for (int i = 0; i < this.associatedParts.Count; i++)
            {
                if (this.associatedParts[i].PartID == paID)
                {
                    associatedParts.RemoveAt(i);
                }

            }
            return true;
        }

        public Part lookupAssociatedPart(int listAssoc)
        {
            var partTemp = AssociatedParts[listAssoc];
            return partTemp;
        }

        public Product() { }
        public Product(int prodID, string prdName, decimal prdPrice, int stock, int minimum, int maximum)
        {
            ProductID = prodID;
            Name = prdName;
            Price = prdPrice;
            InStock = stock;
            Min = minimum;
            Max = maximum;
        }
        public Product(int prodID, string prdName, decimal prdPrice, int stock, int minimum, int maximum, Part assocPart)
        {
            ProductID = prodID;
            Name = prdName;
            Price = prdPrice;
            InStock = stock;
            Min = minimum;
            Max = maximum;
            AssociatedParts.Add(assocPart);
        }
        public Product(int prodID, string prdName, decimal prdPrice, int stock, int minimum, int maximum, Part assocPart, Part assocPart2)
        {
            ProductID = prodID;
            Name = prdName;
            Price = prdPrice;
            InStock = stock;
            Min = minimum;
            Max = maximum;
            AssociatedParts.Add(assocPart);
            AssociatedParts.Add(assocPart2);
        }
        public Product(int prodID, string prdName, decimal prdPrice, int stock, int minimum, int maximum, Part assocPart, Part assocPart2, Part assocPart3)
        {
            ProductID = prodID;
            Name = prdName;
            Price = prdPrice;
            InStock = stock;
            Min = minimum;
            Max = maximum;
            AssociatedParts.Add(assocPart);
            AssociatedParts.Add(assocPart2);
            AssociatedParts.Add(assocPart3);
        }
        public Product(int prodID, string prdName, decimal prdPrice, int stock, int minimum, int maximum, Part assocPart, Part assocPart2, Part assocPart3, Part assocPart4)
        {
            ProductID = prodID;
            Name = prdName;
            Price = prdPrice;
            InStock = stock;
            Min = minimum;
            Max = maximum;
            AssociatedParts.Add(assocPart);
            AssociatedParts.Add(assocPart2);
            AssociatedParts.Add(assocPart3);
            AssociatedParts.Add(assocPart4);
        }
    }
}
