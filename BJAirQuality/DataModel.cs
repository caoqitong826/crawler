using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJAirQuality
{
    public class DataModel
    {
        private string station;

        public string Station
        {
            get { return station; }
            set { station = value; }
        }

        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }
        private float co;

        public float Co
        {
            get { return co; }
            set { co = value; }
        }
        private float co_24h;

        public float Co_24h
        {
            get { return co_24h; }
            set { co_24h = value; }
        }
        private float no2;

        public float No2
        {
            get { return no2; }
            set { no2 = value; }
        }
        private float no2_24h;

        public float No2_24h
        {
            get { return no2_24h; }
            set { no2_24h = value; }
        }
        private float o3;

        public float O3
        {
            get { return o3; }
            set { o3 = value; }
        }

        private float o3_24h;

        public float O3_24h
        {
            get { return o3_24h; }
            set { o3_24h = value; }
        }
        private float pm10;

        public float Pm10
        {
            get { return pm10; }
            set { pm10 = value; }
        }
        private float pm10_24h;

        public float Pm10_24h
        {
            get { return pm10_24h; }
            set { pm10_24h = value; }
        }
        private float pm2_5;

        public float Pm2_5
        {
            get { return pm2_5; }
            set { pm2_5 = value; }
        }
        private float pm2_5_24h;

        public float Pm2_5_24h
        {
            get { return pm2_5_24h; }
            set { pm2_5_24h = value; }
        }

        private float so2;

        public float So2
        {
            get { return so2; }
            set { so2 = value; }
        }

        private float so2_24h;

        public float So2_24h
        {
            get { return so2_24h; }
            set { so2_24h = value; }
        }
    }
}
