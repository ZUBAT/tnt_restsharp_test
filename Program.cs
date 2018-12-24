using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGet
{
    class Program
    {
        public static string Get(string url)
        {
            var Out = "";
            try
            {
                
                WebRequest req = WebRequest.Create(url);
                
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                Out = sr.ReadToEnd();
                sr.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Out;
        }

        public static string Post(string url, string data)
        {
            WebRequest req = WebRequest.Create(url);
            req.Method = "POST";
            req.Timeout = Timeout.Infinite;
            //req.ReadWriteTimeout = 32000;
            req.ContentType = "application/x-www-form-urlencoded";
            ServicePointManager.Expect100Continue = false;
            //ServicePointManager.
            byte[] sentData = Encoding.UTF8.GetBytes(data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();

            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }
        static void Main(string[] args)
        {
            if (args.Count() < 1) return;
            try
            {
                var data = Get(args[0]);
                Console.WriteLine(data);
            }
            catch
            {

            }
        }
    }
}
