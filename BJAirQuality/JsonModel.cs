using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJAirQuality
{
    public class JsonModel
    {
        private string date_Time;

        public string Date_Time
        {
            get { return date_Time; }
            set { date_Time = value; }
        }

        private string station;

        public string Station
        {
            get { return station; }
            set { station = value; }
        }
        private string pollutant;

        public string Pollutant
        {
            get { return pollutant; }
            set { pollutant = value; }
        }
        private string value = "-9999";

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private int iaqi;

        public int IAQI
        {
            get { return iaqi; }
            set { iaqi = value; }
        }
        private string avg24h="-9999";

        public string Avg24h
        {
            get { return avg24h; }
            set { avg24h = value; }
        }

      

       
        private string qlevel;

        public string Qlevel
        {
            get { return qlevel; }
            set { qlevel = value; }
        }

        private string quality;

        public string Quality
        {
            get { return quality; }
            set { quality = value; }
        }

        private string priPollutant;

        public string PriPollutant
        {
            get { return priPollutant; }
            set { priPollutant = value; }
        }
        private string realTimeQL;

        public string RealTimeQL
        {
            get { return realTimeQL; }
            set { realTimeQL = value; }
        }
        private string over24h;

        public string Over24h
        {
            get { return over24h; }
            set { over24h = value; }
        }
    }
}
