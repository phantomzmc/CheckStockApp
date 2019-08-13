using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using AndroidForBC;
using System.ServiceModel;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;


namespace CheckStockApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<SpacePartsList.SpacePart> _spacePartList;
        ProgressDialog progress;
        EditText numSelf1;
        EditText numSelf2;
        EditText numSelf3;
        EditText numSelf4;
        Button searchItem;
        private ServiceClient _client;
        public String selfMain;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            numSelf1 = FindViewById<EditText>(Resource.Id.numself1_textInput);
            numSelf2 = FindViewById<EditText>(Resource.Id.numself2_textInput);
            numSelf3 = FindViewById<EditText>(Resource.Id.numself3_textInput);
            numSelf4 = FindViewById<EditText>(Resource.Id.numself4_textInput);
            searchItem = FindViewById<Button>(Resource.Id.searchItem_btn);
            searchItem.Click += searchItem_Click;

            InitializeServiceClient();
        }
        private void InitializeServiceClient()
        {
            //สร้างการเชื่อมต่อระหว่าง Clicent กับ Webservice
            BasicHttpBinding binding = WCFHttpService.CreateBasicHttp();
            _client = WCFHttpService.GetService1Client();
            _client.selectSpacePartCompleted += _client_selectSpacePartCompleted;
        }

        private void _client_selectSpacePartCompleted(object sender, selectSpacePartCompletedEventArgs e)
        {
            progress.Dispose();
            string msg = null;
            if (e.Error != null)
            {
                msg = e.Error.Message;
                //Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);
                //alertDiag.SetTitle("ไม่สามารถเข้าสู่ระบบได้");
                //alertDiag.SetMessage("กรุณากรอก Username และ Password อีกครั้ง!!");

                //Dialog diag = alertDiag.Create();
                //diag.Show();
            }
            else if (e.Cancelled)
            {
                msg = "Request was cancelled.";
                //Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);
                //alertDiag.SetTitle("ไม่สามารถเข้าสู่ระบบได้");
                //alertDiag.SetMessage("กรุณากรอก Username และ Password อีกครั้ง!!");

                //Dialog diag = alertDiag.Create();
                //diag.Show();
            }
            else
            {
                this._spacePartList = new List<SpacePartsList.SpacePart>();
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
                    };
                    this._spacePartList.Add(spacepart);
                }
                msg = "complete";


                //this.checkFetchData();
            }
            var m_listSpaceLayout = new Intent(this, typeof(ListView_SpacePart_Activity));
            m_listSpaceLayout.PutExtra("Object_Event", JsonConvert.SerializeObject(_spacePartList));

            this.StartActivity(m_listSpaceLayout);
        }

        private void searchItem_Click(object sender,EventArgs e)
        {
            selfMain = numSelf1 + "-" + numSelf2 + "-" + numSelf3 + "-" + numSelf4;
            _client.selectSpacePartAsync(selfMain, "2019-08-05", 1);
            //this.InitializeServiceClient;
            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading is Progress...");

            progress.SetCancelable(false);
            progress.Show();
        }

    }
}