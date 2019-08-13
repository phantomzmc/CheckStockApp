using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace CheckStockApp
{
    [Activity(Label = "Custom_Modal_Item")]
    public class Custom_Modal_Item : AppCompatActivity
    {
        private List<SpacePartsList.SpacePart> spaceParts;
        private int position;
        TextView IDItemTextView;
        TextView NameItemTextView;
        TextView TotalTextView;
        TextView LastMonthTextView;
        TextView AmoundTotalTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.custom_modal_item);
            try
            {
                var ObjectEvent = JsonConvert.DeserializeObject<List<SpacePartsList.SpacePart>>(Intent.GetStringExtra("Object_Event"));
                position = Convert.ToInt32(Intent.GetStringExtra("Index"));
                spaceParts = ObjectEvent;

                IDItemTextView = FindViewById<TextView>(Resource.Id.idItem_textView);
                NameItemTextView = FindViewById<TextView>(Resource.Id.nameItem_textView);
                TotalTextView = FindViewById<TextView>(Resource.Id.total_textView);
                LastMonthTextView = FindViewById<TextView>(Resource.Id.last_month_textView);
                AmoundTotalTextView = FindViewById<TextView>(Resource.Id.amound_total_textView);

                this.setText();
            }
            catch
            {

            }
            // Create your application here

        }
        private void setText()
        {
            IDItemTextView.Text = spaceParts[position].ID_Item.ToString();
            //IDItemTextView.Text = "spaceParts[position].ID_Item";
            NameItemTextView.Text = spaceParts[position].Name_Item.ToString();
            LastMonthTextView.Text = spaceParts[position].Inventory_Last_Month.ToString().ToString();
            AmoundTotalTextView.Text = spaceParts[position].Amound_Sold.ToString().ToString();
            TotalTextView.Text = spaceParts[position].Total_Stock.ToString().ToString();

        }
    }
}