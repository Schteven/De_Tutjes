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
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Addresses
    {
        List<datarootAddress> adresses { get; set; }
    }
    public partial class dataroot
    { 

            private datarootAddress[] addressField;

            private System.DateTime generatedField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Address")]
            public datarootAddress[] Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime generated
            {
                get
                {
                    return this.generatedField;
                }
                set
                {
                    this.generatedField = value;
                }
            }
        }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class datarootAddress
    {

        private uint streetIdField;

        private string streetField;

        private string municipalField;

        private ushort postalcodeField;

        private string boroughField;

        private string streetBoroughField;

        /// <remarks/>
        public uint StreetId
        {
            get
            {
                return this.streetIdField;
            }
            set
            {
                this.streetIdField = value;
            }
        }

        /// <remarks/>
        public string Street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
            }
        }

        /// <remarks/>
        public string Municipal
        {
            get
            {
                return this.municipalField;
            }
            set
            {
                this.municipalField = value;
            }
        }

        /// <remarks/>
        public ushort Postalcode
        {
            get
            {
                return this.postalcodeField;
            }
            set
            {
                this.postalcodeField = value;
            }
        }

        /// <remarks/>
        public string Borough
        {
            get
            {
                return this.boroughField;
            }
            set
            {
                this.boroughField = value;
            }
        }

        /// <remarks/>
        public string StreetBorough
        {
            get
            {
                return this.streetBoroughField;
            }
            set
            {
                this.streetBoroughField = value;
            }
        }
    }

    public class AutocompleteAddress
    {

        public AutocompleteAddress() { }

        public void Initialize()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(datarootAddress));
            TextReader reader = new StreamReader(HostingEnvironment.MapPath(@"~/App_Data/Address.txt"));
            object obj = deserializer.Deserialize(reader);
            datarootAddress address = (datarootAddress)obj;
            reader.Close();
        }
    }
}
