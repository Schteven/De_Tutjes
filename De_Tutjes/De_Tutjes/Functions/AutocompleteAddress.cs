using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace De_Tutjes.Functions
{
    public class dataroot
    {
        [XmlElement("Address")]
        public List<XMLAddress> addressList = new List<XMLAddress>();
    }

    public class XMLAddress
    {
        public string StreetId;
        public string Street;
        public string Municipal;
        public string Postalcode;
        public string Borough;
        public string StreetBorough;

        public XMLAddress() { }
    }

    public class AutocompleteAddress
    {
        public dataroot XmlData;
        public AutocompleteAddress() { }

        public void Initialize()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(dataroot));
            TextReader reader = new StreamReader(HostingEnvironment.MapPath(@"~/App_Data/Address.xml"));
            object obj = deserializer.Deserialize(reader);
            XmlData = (dataroot)obj;
            reader.Close();
        }

    }
}
