using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AirQuality.Models
{
    public class AirModel
    {
        public bool include_total { get; set; }
        public string resource_id { get; set; }
        public Field[] fields { get; set; }
        public __Extras __extras { get; set; }
        public string records_format { get; set; }
        public Record[] records { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public _Links _links { get; set; }
        public int total { get; set; }
    }

    public class __Extras
    {
        public string api_key { get; set; }
    }

    public class _Links
    {
        public string start { get; set; }
        public string next { get; set; }
    }

    public class Field
    {
        public Info info { get; set; }
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Info
    {
        public string notes { get; set; }
        public string label { get; set; }
    }

    public class Record
    {
        [Required]
        public string Site { get; set; }
        [Required]
        public string county { get; set; }
        public string PM25 { get; set; }  
        public string DataCreationDate { get; set; }
        public string ItemUnit { get; set; }
    }
    public class AirService
    {
        
        public AirModel GetData()
        {
            var url = "https://data.epa.gov.tw/api/v1/aqx_p_02?limit=1000&api_key=9be7b239-557b-4c10-9775-78cadfc555e9&format=json";
            var request = WebRequest.Create(url);

            var response = request.GetResponse() as HttpWebResponse;
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            var srcString = reader.ReadToEnd();
            var jsonData = JsonConvert.DeserializeObject<AirModel>(srcString);

            return jsonData;
        }

        public static Record[] Query(string county, string site)
        {
            Record[] result = new Record[1];
            
            
            var url = "https://data.epa.gov.tw/api/v1/aqx_p_02?limit=1000&api_key=9be7b239-557b-4c10-9775-78cadfc555e9&format=json";
            var request = WebRequest.Create(url);

            var response = request.GetResponse() as HttpWebResponse;
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            var srcString = reader.ReadToEnd();
            var jsonData = JsonConvert.DeserializeObject<AirModel>(srcString);
            foreach (var item in jsonData.records)
            {
                if (item.county == county && item.Site == site)
                {
                    result[0] = item;
                }               
            }

            return result;

        }
        

    }
}

