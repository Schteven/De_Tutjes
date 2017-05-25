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
        dataroot XmlData;
        public AutocompleteAddress() { }

        public void Initialize()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(dataroot));
            TextReader reader = new StreamReader("Address.xml");
            object obj = deserializer.Deserialize(reader);
            XmlData = (dataroot)obj;
            reader.Close();
        }

        public List<string> GetPostalcodes()
        {
            List<string> postalCodes = new List<string>();
            foreach (XMLAddress address in XmlData.addressList)
            {
                switch (address.Postalcode)
                {
                    case "3300": // Tienen
                    case "3370": // Boutersem
                    case "3211": // Binkom
                    case "3380": // Glabbeek
                    case "3381": // Kapellen
                    case "3384": // Attenrode
                    case "3471": // Hoeleden
                    case "3440": // Zoutleeuw
                    case "3350": // Linter
                    case "3400": // Landen
                    case "3404": // Attenhoven
                    case "3321": // Outgaarden
                    case "3320": // Hoegaarden
                        postalCodes.Add(address.Postalcode);
                        break;
                }
            }
            return postalCodes;
        }

        public List<string> GetBoroughsInPostalCode(string postalCode)
        {
            List<string> boroughs = new List<string>();
            foreach (XMLAddress address in XmlData.addressList)
            {
                if (address.Postalcode == postalCode)
                {
                    if (address.Borough.Contains('/'))
                    {
                        string[] boroughList = address.Borough.Split('/');
                        foreach (string borough in boroughList)
                        {
                            boroughs.Add(address.Postalcode +" (" + borough + ")");
                        }
                    }
                    else
                    {
                        boroughs.Add(address.Borough);
                    }
                }
            }
            return boroughs;
        }

        public List<string> GetStreetInPostalCode(string postalCode)
        {
            List<string> streets = new List<string>();
            foreach (XMLAddress address in XmlData.addressList)
            {
                if (address.Postalcode == postalCode)
                {
                    streets.Add(address.Street);
                }
            }
            return streets;
        }
    }
}
