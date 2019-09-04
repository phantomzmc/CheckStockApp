﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidForBC;
using Newtonsoft.Json;

namespace CheckStockApp
{
    [Activity(Label = "รายการสินค้า")]
    public class ListView_SpacePart_Activity : AppCompatActivity
    {
        private ServiceClient _client;
        SwipeRefreshLayout refreshLayout;
        ListView spaceListView;
        List<SpacePartsList.SpacePart> spacePartsList;

        ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);

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

                    refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
                    refreshLayout.SetColorSchemeColors(Color.Red, Color.Green, Color.Blue, Color.Yellow);
                    refreshLayout.Refresh += RefreshLayout_Refresh;

                    this.InitializeServiceClient();
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

        private void InitializeServiceClient()
        {
            //สร้างการเชื่อมต่อระหว่าง Clicent กับ Webservice
            BasicHttpBinding binding = WCFHttpService.CreateBasicHttp();
            _client = WCFHttpService.GetService1Client();
            _client.selectSpacePartCompleted += _client_selectSpacePartCompleted; ;
        }

        private void _client_selectSpacePartCompleted(object sender, selectSpacePartCompletedEventArgs e)
        {
            string msg = null;
            if (e.Error != null)
            {
                msg = e.Error.Message;
                Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertDiag.SetTitle("ไม่ค้นหารายการสินค้าได้");
                alertDiag.SetMessage("กรุณาตรวจสอบอินเทอร์เน็ต อีกครั้ง!!");

                Dialog diag = alertDiag.Create();
                diag.Show();
            }
            else if (e.Cancelled)
            {
                msg = "Request was cancelled.";
                Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertDiag.SetTitle("ไม่พบค้นหารายการสินค้า");
                alertDiag.SetMessage("กรุณากรอกรหัสชั้นวางสินค้าให้ถูกต้อง !!");

                Dialog diag = alertDiag.Create();
                diag.Show();
            }
            else
            {
                this.spacePartsList = new List<SpacePartsList.SpacePart>();

                int round_count = Convert.ToInt32(prefs.GetString("keyRound_Count", null));

                if (round_count == 1)
                {
                    for (int i = 0; i < e.Result.Length; i++)
                    {
                        SpacePartsList.SpacePart spacepart = new SpacePartsList.SpacePart()
                        {
                            ID_Item = e.Result[i].ID_Item,
                            Name_Item = e.Result[i].Name_Item,
                            Group_Item = e.Result[i].Group_Item,
                            Sell_Price_Unit = e.Result[i].Sell_Price_Unit,
                            Sell_Price_All = e.Result[i].Sell_Price_All,
                            Cost_Price_Unit = e.Result[i].Cost_Price_Unit,
                            Cost_Price_All = e.Result[i].Cost_Price_All,
                            Self_Main = e.Result[i].Self_Main,
                            Self_Try = e.Result[i].Self_Try,
                            Date_Count_Stock = e.Result[i].Date_Count_Stock,
                            Total_Stock = e.Result[i].Total_Stock,
                            Amound_Sold = e.Result[i].Amound_Sold,
                            Number_Parts_Booking = e.Result[i].Number_Parts_Booking,
                            Inventory_Last_Month = e.Result[i].Inventory_Last_Month,
                            Count1 = Convert.ToDouble(e.Result[i].Count1),
                            Count2 = Convert.ToDouble(e.Result[i].Count2),
                            Count3 = Convert.ToDouble(e.Result[i].Count3),
                        };
                        this.spacePartsList.Add(spacepart);
                    }
                }
                else if (round_count == 2)
                {
                    for (int i = 0; i < e.Result.Length; i++)
                    {
                        if (e.Result[i].Total_Stock != Convert.ToDouble(e.Result[i].Count1))
                        {
                            SpacePartsList.SpacePart spacepart = new SpacePartsList.SpacePart()
                            {
                                ID_Item = e.Result[i].ID_Item,
                                Name_Item = e.Result[i].Name_Item,
                                Group_Item = e.Result[i].Group_Item,
                                Sell_Price_Unit = e.Result[i].Sell_Price_Unit,
                                Sell_Price_All = e.Result[i].Sell_Price_All,
                                Cost_Price_Unit = e.Result[i].Cost_Price_Unit,
                                Cost_Price_All = e.Result[i].Cost_Price_All,
                                Self_Main = e.Result[i].Self_Main,
                                Self_Try = e.Result[i].Self_Try,
                                Date_Count_Stock = e.Result[i].Date_Count_Stock,
                                Total_Stock = e.Result[i].Total_Stock,
                                Amound_Sold = e.Result[i].Amound_Sold,
                                Number_Parts_Booking = e.Result[i].Number_Parts_Booking,
                                Inventory_Last_Month = e.Result[i].Inventory_Last_Month,
                                Count1 = Convert.ToDouble(e.Result[i].Count1),
                                Count2 = Convert.ToDouble(e.Result[i].Count2),
                                Count3 = Convert.ToDouble(e.Result[i].Count3),
                            };
                            this.spacePartsList.Add(spacepart);
                        }
                    }
                }
                else if (round_count == 3)
                {
                    for (int i = 0; i < e.Result.Length; i++)
                    {
                        if (e.Result[i].Total_Stock != Convert.ToDouble(e.Result[i].Count2))
                        {
                            SpacePartsList.SpacePart spacepart = new SpacePartsList.SpacePart()
                            {
                                ID_Item = e.Result[i].ID_Item,
                                Name_Item = e.Result[i].Name_Item,
                                Group_Item = e.Result[i].Group_Item,
                                Sell_Price_Unit = e.Result[i].Sell_Price_Unit,
                                Sell_Price_All = e.Result[i].Sell_Price_All,
                                Cost_Price_Unit = e.Result[i].Cost_Price_Unit,
                                Cost_Price_All = e.Result[i].Cost_Price_All,
                                Self_Main = e.Result[i].Self_Main,
                                Self_Try = e.Result[i].Self_Try,
                                Date_Count_Stock = e.Result[i].Date_Count_Stock,
                                Total_Stock = e.Result[i].Total_Stock,
                                Amound_Sold = e.Result[i].Amound_Sold,
                                Number_Parts_Booking = e.Result[i].Number_Parts_Booking,
                                Inventory_Last_Month = e.Result[i].Inventory_Last_Month,
                                Count1 = Convert.ToDouble(e.Result[i].Count1),
                                Count2 = Convert.ToDouble(e.Result[i].Count2),
                                Count3 = Convert.ToDouble(e.Result[i].Count3),
                            };
                            this.spacePartsList.Add(spacepart);
                        }
                    }
                }
                msg = "complete";
            }
            //var m_listSpaceLayout = new Intent(this, typeof(ListView_SpacePart_Activity));
            //m_listSpaceLayout.PutExtra("Object_Event", JsonConvert.SerializeObject(spacePartsList));

            //this.StartActivity(m_listSpaceLayout);
        }


        private void RefreshLayout_Refresh(object sender, EventArgs e)
        {

            int round_count = Convert.ToInt32(prefs.GetString("keyRound_Count", null));
            string date_count = prefs.GetString("keyDate_Count", null);
            int brach_id = Convert.ToInt32(prefs.GetString("keyBrach_ID", null));
            string selfMain = prefs.GetString("keySelf_Main", null);

            _client.selectSpacePartAsync(selfMain, date_count, brach_id, round_count);

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
        }
    }
}