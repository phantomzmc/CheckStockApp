using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidForBC;
using Newtonsoft.Json;

namespace CheckStockApp
{
    [Activity(Label = "รายละเอียดสินค้า")]
    public class Custom_Modal_Item : Fragment
    {
        ServiceClient _client;
        Android.Support.V7.App.AlertDialog.Builder alertDiag;
        private List<SpacePartsList.SpacePart> spaceParts;
        private int position;
        TextView titleTextView;
        TextView IDItemTextView;
        TextView NameItemTextView;
        TextView TotalTextView;
        TextView LastMonthTextView;
        TextView AmoundTotalTextView;

        TextView f_IDItemTextView;
        TextView f_NameItemTextView;
        TextView f_TotalTextView;
        TextView f_LastMonthTextView;
        TextView f_AmoundTotalTextView;
        TextView f_InputcountEditText;

        EditText InputCountEditText;
        Button submitBtn;

        public int round_count;
        public string date_count;
        public int brach_id;
        public int _status = 0;
        ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);

        public Custom_Modal_Item(List<SpacePartsList.SpacePart> _spaceParts,int _position)
        {
            spaceParts = _spaceParts;
            position = _position;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            try
            {
                titleTextView = view.FindViewById<TextView>(Resource.Id.title_textView);
                IDItemTextView = view.FindViewById<TextView>(Resource.Id.idItem_textView);
                NameItemTextView = view.FindViewById<TextView>(Resource.Id.nameItem_textView);
                TotalTextView = view.FindViewById<TextView>(Resource.Id.total_textView);
                LastMonthTextView = view.FindViewById<TextView>(Resource.Id.last_month_textView);
                AmoundTotalTextView = view.FindViewById<TextView>(Resource.Id.amound_total_textView);

                f_IDItemTextView = view.FindViewById<TextView>(Resource.Id.f_idItem_textView);
                f_NameItemTextView = view.FindViewById<TextView>(Resource.Id.f_nameItem_textView);
                f_TotalTextView = view.FindViewById<TextView>(Resource.Id.f_total_textView);
                f_LastMonthTextView = view.FindViewById<TextView>(Resource.Id.f_last_month_textView);
                f_AmoundTotalTextView = view.FindViewById<TextView>(Resource.Id.f_amound_total_textView);
                f_InputcountEditText = view.FindViewById<TextView>(Resource.Id.f_inputcount_textView);

                InputCountEditText = view.FindViewById<EditText>(Resource.Id.inputcount_editText);
                submitBtn = view.FindViewById<Button>(Resource.Id.submit_button);
                submitBtn.Click += SubmitBtn_Click;

                this.setValues();
                this.setText();
                this.InitializeServiceClient();
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
            _client.updateDetail_CountStockCompleted += _client_updateDetail_CountStockCompleted;
        }

        private void _client_updateDetail_CountStockCompleted(object sender, updateDetail_CountStockCompletedEventArgs e)
        {
            string msg = null;
            Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(Activity);

           
            if (e.Error != null)
            {
                msg = e.Error.Message;
                alertDiag.SetTitle("ไม่ค้นหารายการสินค้าได้");
                alertDiag.SetMessage("กรุณาตรวจสอบอินเทอร์เน็ต อีกครั้ง!!");
                _status = 0;

            }
            else if (e.Cancelled)
            {
                msg = "Request was cancelled.";
                alertDiag.SetTitle("ไม่พบค้นหารายการสินค้า");
                alertDiag.SetMessage("กรุณากรอกรหัสชั้นวางสินค้าให้ถูกต้อง !!");

                _status = 1;
            }
            else
            {
                _status = 2;
            }
        }
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            alertDiag = new Android.Support.V7.App.AlertDialog.Builder(Activity);
            alertDiag.SetTitle("ยืนยันการกรอกข้อมูล");
            alertDiag.SetMessage("กรุณาตรวจสอบการกรอกข้อมูลที่ถูกต้อง !");
            alertDiag.SetPositiveButton("แก้ไข", dismissAlert);
            alertDiag.SetNegativeButton("ยืนยัน", goMain);

            Dialog diag = alertDiag.Create();
            diag.Show();
        }

        private void dismissAlert(object sender, EventArgs e)
        {
            alertDiag.Dispose();
        }
        private void goMain(object sender, EventArgs e)
        {
            alertDiag.Dispose();
            this.updateStock();
            this.checkUpdate();

            //await Navigation.PushAsync(new ListView_SpacePart_Activity());

        }
        public async void checkUpdate()
        {
            string toast = "บันทึกรายการ : " + spaceParts[position].ID_Item.ToString() + " เรียบร้อย";           
            Toast.MakeText(Activity, toast, ToastLength.Long).Show();
        }
        public void updateStock()
        {
            string iditem = spaceParts[position].ID_Item.ToString();
            int round = round_count;
            int brach = brach_id;
            string date_count_stock = date_count;
            int count_value = Convert.ToInt32(InputCountEditText.Text);
            _client.updateDetail_CountStockAsync(iditem,round,brach,date_count_stock,count_value);
            _client.selectSpacePartAsync(spaceParts[position].Self_Main.ToString(), date_count, brach, round);
        }

        private void setText()
        {
            Typeface openSansRegular = Typeface.CreateFromAsset(Application.Context.Assets, "Prompt-Light.ttf");

            titleTextView.Typeface = openSansRegular;
            IDItemTextView.Typeface = openSansRegular;
            NameItemTextView.Typeface = openSansRegular;
            LastMonthTextView.Typeface = openSansRegular;
            AmoundTotalTextView.Typeface = openSansRegular;
            TotalTextView.Typeface = openSansRegular;
            IDItemTextView.Typeface = openSansRegular;

            f_IDItemTextView.Typeface = openSansRegular;
            f_NameItemTextView.Typeface = openSansRegular;
            f_LastMonthTextView.Typeface = openSansRegular;
            f_AmoundTotalTextView.Typeface = openSansRegular;
            f_TotalTextView.Typeface = openSansRegular;
            f_InputcountEditText.Typeface = openSansRegular;

            InputCountEditText.Typeface = openSansRegular;
            submitBtn.Typeface = openSansRegular;


            IDItemTextView.Text = spaceParts[position].ID_Item.ToString();
            NameItemTextView.Text = spaceParts[position].Name_Item.ToString();
            LastMonthTextView.Text = spaceParts[position].Inventory_Last_Month.ToString().ToString();
            AmoundTotalTextView.Text = spaceParts[position].Amound_Sold.ToString().ToString();
            TotalTextView.Text = spaceParts[position].Total_Stock.ToString().ToString();

        }

        private void setValues()
        {
            //var ObjectEvent = JsonConvert.DeserializeObject<List<SpacePartsList.SpacePart>>(Intent.GetStringExtra("Object_Event"));
            //position = Convert.ToInt32(Intent.GetStringExtra("Index"));
            //spaceParts = ObjectEvent;

            round_count = Convert.ToInt32(prefs.GetString("keyRound_Count", null));
            date_count = prefs.GetString("keyDate_Count", null);
            brach_id = Convert.ToInt32(prefs.GetString("keyBrach_ID", null));
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            try
            {
                return inflater.Inflate(Resource.Layout.custom_modal_item, container, false);

            }
            catch
            {
                return inflater.Inflate(Resource.Layout.custom_modal_item, container, false);

            }

        }
    }
}