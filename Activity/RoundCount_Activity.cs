using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace CheckStockApp
{
    [Activity(Label = "RoundCount_Activity", MainLauncher = true)]

    public class RoundCount_Activity : AppCompatActivity
    {
        private List<KeyValuePair<string, int>> planets;
        Spinner spinner;
        EditText round_EditText;
        Button datePicker;
        Button nextButton;
        TextView datePick_TextView;

        TextView f_TitleTextView;
        TextView f_branchTextView;

        DateTime date_count;
        int round;
        int idbrach;

        ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_round_count);
            // Create your application here
            spinner = FindViewById<Spinner>(Resource.Id.branch_spinner);
            round_EditText = FindViewById<EditText>(Resource.Id.roundcount_textInput);
            datePick_TextView = FindViewById<TextView>(Resource.Id.datePick_textView);
            datePicker = FindViewById<Button>(Resource.Id.datePick_Btn);
            nextButton = FindViewById<Button>(Resource.Id.next_Btn);
            f_TitleTextView = FindViewById<TextView>(Resource.Id.f_titleApp_textView);
            f_branchTextView = FindViewById<TextView>(Resource.Id.f_branch_spinner);


            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            datePicker.Click += DateSelect_OnClick;
            nextButton.Click += NextButton_Click;
            this.setSpinner();
            this.setText();
        }

        public void setSpinner()
        {
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            planets = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("--เลือกสาขา--", 0),
                new KeyValuePair<string, int>("ดอนจั่น", 1),
                new KeyValuePair<string, int>("หน้าปริ้น", 2),
                new KeyValuePair<string, int>("สันทราย", 3),
                new KeyValuePair<string, int>("สันป่าตอง" , 4),
                new KeyValuePair<string, int>("จอมทอง", 5)
            };

            List<string> planetNames = new List<string>();
            foreach (var item in planets)
                planetNames.Add(item.Key);
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The mean temperature for planet {0} is {1}",
                spinner.GetItemAtPosition(e.Position), planets[e.Position].Value);
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            idbrach = planets[e.Position].Value;
        }
        public void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            try
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (string time)
                {
                    datePick_TextView.Text = time.ToString();
                    //date_count = time;
                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            }
            catch
            {
                Toast.MakeText(this, "error", ToastLength.Long).Show();

            }

        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            ISharedPreferencesEditor editor = prefs.Edit();
            //editor.PutInt("your_key1", 10);
            editor.PutString("keyRound_Count", round_EditText.Text.ToString());
            editor.PutString("keyBrach_ID", idbrach.ToString());
            //editor.PutString("keyDate_Count", date_count.ToString("yyyy-MM-dd"));
            editor.PutString("keyDate_Count", datePick_TextView.Text.ToString());
            
            editor.Apply();
            var m_main = new Intent(this, typeof(MainActivity));
            //m_listSpaceLayout.PutExtra("Object_Event", JsonConvert.SerializeObject(_spacePartList));
            this.StartActivity(m_main);

        }
        public void setText()
        {
            Typeface openSansRegular = Typeface.CreateFromAsset(Application.Context.Assets, "Prompt-Light.ttf");
            round_EditText.Typeface = openSansRegular;
            datePick_TextView.Typeface = openSansRegular;
            datePicker.Typeface = openSansRegular;
            nextButton.Typeface = openSansRegular;
            f_TitleTextView.Typeface = openSansRegular;
            f_branchTextView.Typeface = openSansRegular;
        }
    }
}