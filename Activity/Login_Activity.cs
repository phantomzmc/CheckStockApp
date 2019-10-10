
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CheckStockApp
{
    [Activity(Label = "Login_Activity", MainLauncher = true)]
    public class Login_Activity : Activity
    {
        EditText usernameTextView;
        Button loginButton;

        ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            usernameTextView = FindViewById<EditText>(Resource.Id.username_textInput);
            loginButton = FindViewById<Button>(Resource.Id.login_btn);
            loginButton.Click += LoginButton_Click;

            // Create your application here
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextView.Text.ToString();
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("keyUsername", username);
            editor.Apply();
        }
        public void goRoundCount()
        {
            var m_roundCount = new Intent(this, typeof(RoundCount_Activity));
            //m_listSpaceLayout.PutExtra("Object_Event", JsonConvert.SerializeObject(_spacePartList));
            this.StartActivity(m_roundCount);
        }
    }
}
