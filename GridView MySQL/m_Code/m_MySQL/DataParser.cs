using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Org.Json;
using Object = Java.Lang.Object;

namespace GridView_MySQL.m_Code.m_MySQL
{
    class DataParser : AsyncTask
    {
        private Context c;
        private string jsonData;
        private GridView gv;

        private ProgressDialog pd;
        JavaList<string> spacecrafts=new JavaList<string>(); 

        public DataParser(Context c, string jsonData, GridView gv)
        {
            this.c = c;
            this.jsonData = jsonData;
            this.gv = gv;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            pd=new ProgressDialog(c);
            pd.SetTitle("Parse data");
            pd.SetMessage("Parsing..Please wait");
            pd.Show();
        }

        protected override Object DoInBackground(params Object[] @params)
        {
            return ParseData();
        }

        protected override void OnPostExecute(Object isParsed)
        {
            base.OnPostExecute(isParsed);

            pd.Dismiss();

            if ((bool) isParsed)
            {
                //BIND TO GV
                gv.Adapter=new ArrayAdapter(c,Android.Resource.Layout.SimpleListItem1,spacecrafts);
                gv.ItemClick += gv_ItemClick;
            }
            else
            {
                Toast.MakeText(c,"Unable To Parse",ToastLength.Short).Show();
            }
        }

        void gv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(c,spacecrafts[e.Position],ToastLength.Short).Show();
        }

        private Boolean ParseData()
        {
            try
            {
                JSONArray ja=new JSONArray(jsonData);
                JSONObject jo;

                spacecrafts.Clear();

                for (int i = 0; i < ja.Length(); i++)
                {
                    jo = ja.GetJSONObject(i);

                    int id = jo.GetInt("id");
                    string name = jo.GetString("name");

                    spacecrafts.Add(name);
                }

                return true;

            }
            catch (Exception e)
            {
                
               Console.WriteLine(e.Message);
            }

            return false;
        }
    }
}