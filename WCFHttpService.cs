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
using System.ServiceModel;
using Android.Net;
using Java.Lang;

namespace AndroidForBC
{
    public class WCFHttpService
    {
        //private static string _server = "http://192.168.1.195:8382/Service.svc";
        private static string _server = "http://192.168.1.186:8151/Service.svc";



        public static BasicHttpBinding CreateBasicHttp()
        {

            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name = "basicHttpBinding",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };
            TimeSpan timeout = new TimeSpan(0, 10, 30);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            return binding;
        }

        public static EndpointAddress GetEndPoint(string serviceUrl)
        {
            return new EndpointAddress(serviceUrl);
        }

        public static ServiceClient GetService1Client()
        {
            BasicHttpBinding binding = WCFHttpService.CreateBasicHttp();
            return new ServiceClient(binding, WCFHttpService.GetEndPoint(_server));
        }


        //public static Service1Client GetService1Client(Context c)
        //{
        //    BasicHttpBinding binding = WCFHttpService.CreateBasicHttp();
        //     return new Service1Client(binding, WCFHttpService.GetEndPoint("http://asiakaset.dyndns.biz:8085/Service1.svc"));
        //    //return new Service1Client(binding, WCFHttpService.GetEndPoint("http://192.168.1.188:8080/Service1.svc"));
        //    //return new Service1Client(binding, WCFHttpService.GetEndPoint("http://45ab0279e86e.sn.mynetname.net:50000/Service1.svc"));
        //}


        public static NetworkInfo CheckInternet(Context c)
        {
            
            ConnectivityManager connectivityManager = (ConnectivityManager)c.GetSystemService(Context.ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
            //if (networkInfo != null)
            //{
            //    connect = true;
            //}
            return networkInfo;
        }
    }
}