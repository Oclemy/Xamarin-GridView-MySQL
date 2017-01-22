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
using Java.Net;

namespace GridView_MySQL.m_Code.m_MySQL
{
    class Connector
    {
        public static HttpURLConnection connect(string urlAddress)
        {
            try
            {
                URL url=new URL(urlAddress);
                HttpURLConnection con = (HttpURLConnection) url.OpenConnection();

                //CON PROPERTIES
                con.RequestMethod = "GET";
                con.ReadTimeout = 15000;
                con.ConnectTimeout = 15000;
                con.DoInput = true;

                return con;
            }
            catch (Exception e)
            {
                
               Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}