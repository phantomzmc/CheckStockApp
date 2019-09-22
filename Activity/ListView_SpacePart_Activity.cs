using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace CheckStockApp
{
    [Activity(Label = "รายการสินค้า")]
    public class ListView_SpacePart_Activity : AppCompatActivity
    {
        SwipeRefreshLayout refreshLayout;

        ListView spaceListView;
        List<SpacePartsList.SpacePart> spacePartsList;
        SpacePartsItemAdapter adapters;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            var ObjectEvent = JsonConvert.DeserializeObject<List<SpacePartsList.SpacePart>>(Intent.GetStringExtra("Object_Event"));
            spacePartsList = ObjectEvent;

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.spacepart_listview);
            try
            {


                if (spacePartsList.Count != 0)
                {
                    adapters = new SpacePartsItemAdapter(this, spacePartsList);
                    spaceListView = FindViewById<ListView>(Resource.Id.spacepart_listView);
                    spaceListView.Adapter = adapters;
                    spaceListView.ItemClick += spacepartlist_ItemClick;

                    refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
                    refreshLayout.SetColorSchemeColors(Color.Red, Color.Green, Color.Blue, Color.Yellow);
                    refreshLayout.Refresh += RefreshLayout_Refresh;

                }
                else if (spacePartsList.Count == 0)
                {
                    var m_main = new Intent(this, typeof(Activity_NoItem));
                    this.StartActivity(m_main);
                }
            }
            catch(Exception ex)
            {
                string errror = ex.Message;
            }

            // Create your application here
        }

        private void RefreshLayout_Refresh(object sender, EventArgs e)
        {
            //Data Refresh Place  
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += Work_DoWork;
            work.RunWorkerCompleted += Work_RunWorkerCompleted;
            work.RunWorkerAsync();

        }
        private void Work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            refreshLayout.Refreshing = false;
        }
        private void Work_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
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
            //this.Remoteitem(position);
        }

        public void Remoteitem(List<SpacePartsList.SpacePart> spacePartsList,int position)
        {
            spacePartsList.RemoveAt(position);

            adapters = new SpacePartsItemAdapter(this, spacePartsList);
            adapters.NotifyDataSetChanged();

            
        }
    }
}