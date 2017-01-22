using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Java.Net;
using Console = System.Console;
using Exception = System.Exception;
using Object = Java.Lang.Object;

namespace GridView_MySQL.m_Code.m_MySQL
{
    class Downloader : AsyncTask
    {
        private Context c;
        private string urlAddress;
        private GridView gv;

        private ProgressDialog pd;

        public Downloader(Context c, string urlAddress, GridView gv)
        {
            this.c = c;
            this.urlAddress = urlAddress;
            this.gv = gv;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();

            pd=new ProgressDialog(c);
            pd.SetTitle("Fetch Data");
            pd.SetMessage("Fetching Data...Please wait");
            pd.Show();
        }

        protected override Object DoInBackground(params Object[] @params)
        {
            return DownloadData();
        }

        protected override void OnPostExecute(Object jsonData)
        {
            base.OnPostExecute(jsonData);

            pd.Dismiss();

            if (jsonData == null)
            {
                Toast.MakeText(c,"UnSuccessful,No Data Retrieved",ToastLength.Short).Show();
            }
            else
            {
                //PARSE
                new DataParser(c, jsonData.ToString(), gv).Execute();
            }
        }
         
        private string DownloadData()
        {
            HttpURLConnection con = Connector.connect(urlAddress);
            if (con == null)
            {
                return null;
            }

            try
            {
                Stream s=new BufferedStream(con.InputStream);
                BufferedReader br=new BufferedReader(new InputStreamReader(s));

                string line;
                StringBuffer jsonData=new StringBuffer();

                while ((line=br.ReadLine()) != null)
                {
                    jsonData.Append(line + "\n");
                }

                br.Close();
                s.Close();

                return jsonData.ToString();

            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}






