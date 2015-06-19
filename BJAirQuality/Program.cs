using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BJAirQuality
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, JsonModel>> Dic = new Dictionary<string, Dictionary<string, JsonModel>>();
            DataServiceClient client = new DataServiceClient();
            string sourcestr = client.GetWebData();
            string jsonString = DecryptAES(sourcestr, "qjkHuIy9D/9i=", "Mi9l/+7Zujhy12se6Yjy111A");
            jsonString = jsonString.Substring(9, jsonString.Length - 10);
            //using ( FileStream fs = new FileStream("data.txt", FileMode.Create))
            //{
            //    byte[] bs = Encoding.UTF8.GetBytes(jsonString);
            //    fs.Write(bs, 0, bs.Length);
            //    fs.Close();
            //}
            var jsonList = Utils.parse<List<JsonModel>>(jsonString);
            List<DataModel> dataList = new List<DataModel>();
           // List<int> ml = new List<int>();
            string sql = "select PositionName,Timepoint from Beijing where TimePoint = '"+ jsonList[0].Date_Time+" ' "+" order by PositionName DESC";
            DataTable dt = SqlHelper.GetDataSet(SqlHelper.connectionStr, sql, null);
            HashSet<string> hs = new HashSet<string>();
            for(int i=0;i<dt.Rows.Count; i++)
            {
                hs.Add(dt.Rows[i]["PositionName"].ToString());
            }
            for (int i = 0; i < jsonList.Count; i++)
            {
                Dictionary<string, JsonModel> dic = new Dictionary<string, JsonModel>();
                dic.Add("CO", new JsonModel());
                dic.Add("NO2", new JsonModel());
                dic.Add("O3", new JsonModel());
                dic.Add("PM10", new JsonModel());
                dic.Add("PM2.5", new JsonModel());
                dic.Add("SO2", new JsonModel());
                int j= i;
                int m = 0;
                DataModel dm = new DataModel();
                while(i<jsonList.Count&&(jsonList[j].Station==jsonList[i].Station))
                {
                    //dic.Add(jsonList[i].Pollutant,jsonList[i]);
                    if (jsonList[i].Avg24h != "")
                    {
                        dic[jsonList[i].Pollutant] = jsonList[i];
                    }
                    else
                    {
                        dic[jsonList[i].Pollutant] = jsonList[i];
                        dic[jsonList[i].Pollutant].Avg24h = "-9999";
                    }
                    //m++;
                    i++;
                }
                i--;
               // ml.Add(m);
                Dic.Add(jsonList[i].Station, dic);
            }
           
            //MySqlParameter[] parameters = new MySqlParameter[5];
            MySqlConnection conn = new MySqlConnection(SqlHelper.connectionStr);
            string position = null;
            foreach (var item in Dic)
            {
                Dictionary<string, JsonModel> d = item.Value;               
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into Beijing(Area, CO, CO_24h ,NO2 ,NO2_24h, O3, O3_24h ,PM10 ,PM10_24h ,PM2_5, PM2_5_24h,PositionName, SO2, SO2_24h, TimePoint) values (");
                sb.Append("'北京'" + " ,");
                if (d["CO"] != null)
                {
                    sb.Append(d["CO"].Value + " ,");
                    if (d["CO"].Avg24h != "")
                    {
                        sb.Append(d["CO"].Avg24h + " ,");
                    }
                    else
                    {
                        sb.Append(" -9999 " + " ,");
                    }
                }
                else
                {
                    sb.Append(" -9999 " + " ,");
                    sb.Append(" -9999 " + " ,");
                }


                if (d["NO2"] != null)
                {
                    sb.Append(d["NO2"].Value + " ,");
                    sb.Append(d["NO2"].Avg24h + " ,");
                }
                else
                {
                    sb.Append(" -9999 " + " ,");
                    sb.Append(" -9999 " + " ,");
                }


                if (d["O3"] != null)
                {
                    sb.Append(d["O3"].Value + " ,");
                    sb.Append(d["O3"].Avg24h + " ,");
                }
                else
                {
                    sb.Append(" -9999 " + " ,");
                    sb.Append(" -9999 " + " ,");
                }

                if (d["PM10"] != null)
                {
                    sb.Append(d["PM10"].Value + " ,");
                    sb.Append(d["PM10"].Avg24h + " ,");
                }
                else
                {
                    sb.Append(" -9999 " + " ,");
                    sb.Append(" -9999 " + " ,");
                }

                if (d["PM2.5"] != null)
                {
                    sb.Append(d["PM2.5"].Value + " ,");
                    sb.Append(d["PM2.5"].Avg24h + " ,'");
                }
                else
                {
                    sb.Append(" -9999 " + " ,");
                    sb.Append(" -9999 " + " ,'");
                }

                if (d["CO"] != null)
                {
                    position = d["CO"].Station;
                    sb.Append(d["CO"].Station + "' ,");
                }
                else if (d["PM10"] != null)
                {
                    position = d["PM10"].Station;
                    sb.Append(d["PM10"].Station + "' ,");
                }
                else
                {
                    position = d["PM2.5"].Station;
                    sb.Append(d["PM2.5"].Station + "' ,");
                }



                if (d["SO2"] != null)
                {
                    sb.Append(d["SO2"].Value + " ,");
                    sb.Append(d["SO2"].Avg24h + " ,'");
                }
                else
                {
                    sb.Append(" -9999 " + " ,");
                    sb.Append(" -9999 " + " ,'");
                }

                if (d["CO"] != null)
                {
                    sb.Append(d["CO"].Date_Time + "'");
                }
                else if (d["PM10"] != null)
                {
                    sb.Append(d["PM10"].Date_Time + " '");
                }
                else
                {
                    sb.Append(d["PM2.5"].Date_Time + " '");
                }
                sb.Append(")");

                if ( hs.Contains(position))
                {
                    Console.WriteLine("Data already exist!");
                    continue;
                }
                SqlHelper.ExecuteNonQuery1(conn, CommandType.Text, sb.ToString(), null);
                Console.WriteLine("success!");
                //SqlHelper.ExecuteNonQuery(SqlHelper.connectionStr, CommandType.Text, sb.ToString(), null);

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (item.Key != dt.Rows[i]["PositionName"].ToString())
                //    {

                //    }
                //}
                //parameters[0] = new MySqlParameter("Area", "北京");
                //parameters[1] = new MySqlParameter("CO",d["CO"].Value);
                //parameters[2] = new MySqlParameter("CO_24h", d["CO_24h"].Value);                
            }
            client.Close();
        }

        /// <summary>
        /// decoding string
        /// </summary>
        /// <param name="decryptString">source string</param>
        /// <param name="decryptKey">key</param>
        /// <param name="salt"> salt</param>
        /// <returns></returns>
        public static string DecryptAES(string decryptString, string decryptKey, string salt)
        {
            AesManaged managed = null;
            MemoryStream stream = null;
            CryptoStream stream2 = null;
            string str;
            try
            {
                Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(decryptKey, Encoding.UTF8.GetBytes(salt));
                managed = new AesManaged();
                managed.Key = bytes.GetBytes(managed.KeySize / 8);
                managed.IV = bytes.GetBytes(managed.BlockSize / 8);
                stream = new MemoryStream();
                stream2 = new CryptoStream(stream, managed.CreateDecryptor(), CryptoStreamMode.Write);
                byte[] buffer = Convert.FromBase64String(decryptString);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                str = Encoding.UTF8.GetString(stream.ToArray(), 0, stream.ToArray().Length);
            }
            catch
            {
                str = decryptString;
            }
            finally
            {
                if (stream2 != null)
                {
                    stream2.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
                if (managed != null)
                {
                    managed.Clear();
                }
            }
            return str;
        }
    }
}
