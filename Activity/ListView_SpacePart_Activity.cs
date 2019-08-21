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
    [Activity(Label = "รายการสินค้า")]
    public class ListView_SpacePart_Activity : AppCompatActivity
    {
        ListView spaceListView;
        List<SpacePartsList.SpacePart> spacePartsList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.spacepart_listview);
            try
            {
                var ObjectEvent = JsonConvert.DeserializeObject<List<SpacePartsList.SpacePart>>(Intent.GetStringExtra("Object_Event"));
                spacePartsList = ObjectEvent;

                if (spacePartsList.Count != 0)
                {
                    spaceListView = FindViewById<ListView>(Resource.Id.spacepart_listView);
                    spaceListView.Adapter = new SpacePartsItemAdapter(this, spacePartsList);
                    spaceListView.ItemClick += spacepartlist_ItemClick;
                }
                else if (spacePartsList.Count == 0)
                {
                    var m_main = new Intent(this, typeof(Activity_NoItem));
                    this.StartActivity(m_main);
                }
            }
            catch
            {

            }

            // Create your application here
        }

        private void spacepartlist_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int position = e.Position;
            //var m_listSpaceLayout = new Intent(this, typeof(Custom_Modal_Item));
            //m_listSpaceLayout.PutExtra("Object_Event", JsonConvert.SerializeObject(spacePartsList));
            //m_listSpaceLayout.PutExtra("Index", position.ToString());
            //this.StartActivity(m_listSpaceLayout);
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            Custom_Modal_Item fragment = new Custom_Modal_Item(spacePartsList, position);
            transaction.Replace(Resource.Id.fragments, fragment);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }
    }
}