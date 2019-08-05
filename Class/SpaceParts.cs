using System;
using System.Collections.Generic;

namespace CheckStockApp
{
    public static class SpacePartsList
    {
        private static List<SpacePart> listSpaceParts { get; set; }

        static SpacePartsList()
        {
            var temp = new List<SpacePart>();

            //AddBooth(temp);
            //AddBooth(temp);
            ////AddBooth(temp);

            //Booths = temp.OrderBy(i => i.PlateBooth).ToList();
        }
        public class SpacePart
        {
            public String ID_Item { get; set; }
            public String Name_Item { get; set; }
            public String Group_Item { get; set; }
            public double Sell_Price_Unit { get; set; }
            public double Sell_Price_All { get; set; }
            public double Cost_Price_Unit { get; set; }
            public double Cost_Price_All { get; set; }
            public String Self_Main { get; set; }
            public String Self_Try { get; set; }
            public DateTime Date_Count_Stock { get; set; }
            public double Total_Stock { get; set; }
            public double Amound_Sold { get; set; }
            public double Number_Parts_Booking { get; set; }
            public double Inventory_Last_Month { get; set; }

            public SpacePart()
            {
                this.ID_Item = null;
                this.Name_Item = null;
                this.Group_Item = null;
                this.Sell_Price_Unit = 0.0;
                this.Sell_Price_All = 0.0;
                this.Cost_Price_Unit = 0.0;
                this.Cost_Price_All = 0.0;
                this.Self_Main = null;
                this.Self_Try = null;
                this.Date_Count_Stock = DateTime.MinValue;
                this.Total_Stock = 0.0;
                this.Amound_Sold = 0.0;
                this.Number_Parts_Booking = 0.0;
                this.Inventory_Last_Month = 0.0;
            }

        }
    }

}