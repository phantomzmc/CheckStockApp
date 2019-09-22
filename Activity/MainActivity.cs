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
using Android.Graphics;

namespace CheckStockApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        List<SpacePartsList.SpacePart> _spacePartList;
        ProgressDialog progress;
        TextView titleTextView;
        EditText numSelf1;
        EditText numSelf2;
        EditText numSelf3;
        EditText numSelf4;
        Button searchItem;
        Button searchItemOutShelf;
        Button changeRoundCount;
        private ServiceClient _client;
        public String selfMain;

        ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            titleTextView = FindViewById<TextView>(Resource.Id.titleApp_textView);
            numSelf1 = FindViewById<EditText>(Resource.Id.numself1_textInput);
            numSelf2 = FindViewById<EditText>(Resource.Id.numself2_textInput);
            numSelf3 = FindViewById<EditText>(Resource.Id.numself3_textInput);
            numSelf4 = FindViewById<EditText>(Resource.Id.numself4_textInput);
            searchItem = FindViewById<Button>(Resource.Id.searchItem_btn);
            searchItemOutShelf = FindViewById<Button>(Resource.Id.searchItemOutShelf_btn);
            changeRoundCount = FindViewById<Button>(Resource.Id.changeRoundCount__btn);
            searchItem.Click += searchItem_Click;
            searchItemOutShelf.Click += SearchItemOutShelf_Click;
            changeRoundCount.Click += ChangeRoundCount_Click;

            this.setText();
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
            try
            {
                progress.Dismiss();
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
                    this._spacePartList = new List<SpacePartsList.SpacePart>();

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
                            this._spacePartList.Add(spacepart);
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
                                this._spacePartList.Add(spacepart);
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
                                this._spacePartList.Add(spacepart);
                            }
                        }
                    }
                    msg = "complete";
                }
                var m_listSpaceLayout = new Intent(this, typeof(ListView_SpacePart_Activity));
                m_listSpaceLayout.PutExtra("Object_Event", JsonConvert.SerializeObject(_spacePartList));

                this.StartActivity(m_listSpaceLayout);
            }
            catch(Exception ex)
            {
                string error = ex.Message;
            }
            
        }

        private void searchItem_Click(object sender,EventArgs e)
        {
            int round_count = Convert.ToInt32(prefs.GetString("keyRound_Count", null));
            string date_count = prefs.GetString("keyDate_Count", null);
            int brach_id = Convert.ToInt32(prefs.GetString("keyBrach_ID", null));

            selfMain = numSelf1.Text.ToString() + "-" + numSelf2.Text.ToString() + "-" + numSelf3.Text.ToString() + "-" + numSelf4.Text.ToString();
            _client.selectSpacePartAsync(selfMain, date_count, brach_id, round_count);
            //this.InitializeServiceClient;
            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading is Progress...");

            progress.SetCancelable(false);
            progress.Show();
        }

        private void SearchItemOutShelf_Click(object sender, EventArgs e)
        {
            int round_count = Convert.ToInt32(prefs.GetString("keyRound_Count", null));
            string date_count = prefs.GetString("keyDate_Count", null);
            int brach_id = Convert.ToInt32(prefs.GetString("keyBrach_ID", null));

            _client.selectSpacePartAsync("", date_count, brach_id, round_count);
            //this.InitializeServiceClient;
            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading is Progress...");

            progress.SetCancelable(false);
            progress.Show();
        }

        private void ChangeRoundCount_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
            //var m_roundCount = new Intent(this, typeof(RoundCount_Activity));
            //this.StartActivity(m_roundCount);
        }
        private void setText()
        {
            Typeface openSansRegular = Typeface.CreateFromAsset(Application.Context.Assets, "Prompt-Light.ttf");
            titleTextView.Typeface = openSansRegular;
            numSelf1.Typeface = openSansRegular;
            numSelf2.Typeface = openSansRegular;
            numSelf3.Typeface = openSansRegular;
            numSelf4.Typeface = openSansRegular;
            searchItem.Typeface = openSansRegular;
            searchItemOutShelf.Typeface = openSansRegular;

        }

    }
}