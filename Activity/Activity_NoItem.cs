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

namespace CheckStockApp
{
    [Activity(Label = "ไม่มีรายการสินค้า")]
    public class Activity_NoItem : AppCompatActivity
    {
        TextView noItemTextView;
        Button backSearchBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_noitem);

            noItemTextView = FindViewById<TextView>(Resource.Id.noitemsTextView);
            backSearchBtn = FindViewById<Button>(Resource.Id.backSearch_btn);
            backSearchBtn.Click += BackSearchBtn_Click;
        }

        private void BackSearchBtn_Click(object sender, EventArgs e)
        {
            var m_main = new Intent(this, typeof(MainActivity));
            m_main.AddFlags(ActivityFlags.ClearTop);
            m_main.AddFlags(ActivityFlags.ClearTask);
            m_main.AddFlags(ActivityFlags.NewTask);
            this.StartActivity(m_main);
            Finish();
        }
    }
}