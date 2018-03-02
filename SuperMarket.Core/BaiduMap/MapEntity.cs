using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Core.BaiduMap
{
    public class BaiduApiStatus
    {
        public string status { get; set; }
    }
    public class BaiduApiMessage
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class BaiduApiLocation
    {
        public string address { get; set; }
        public Content content { get; set; }
        public string status { get; set; }
    }
    public class Content
    {
        public string address { get; set; }
        public Address_detail address_detail { get; set; }
        public Point point { get; set; }
    }
    public class Address_detail
    {
        public string city { get; set; }
        public string city_code { get; set; }
        public string district { get; set; }
        public string province { get; set; }
        public string street { get; set; }
        public string street_number { get; set; }

    }
    public class Point
    {
        public string x { get; set; }
        public string y { get; set; }
    }
}
