using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace CsvToXml
{
    [XmlRoot("Users")]
    public class Users : List<User>
    {

    }
    public class User
    {
        [XmlElement(typeof(string), ElementName = "FirstName")]
        public string FirstName { get; set; }
        [XmlElement(typeof(string), ElementName = "LastName")]
        public string LastName { get; set; }
        [XmlElement(typeof(string), ElementName = "Address")]
        public string Address { get; set; }
        [XmlElement(typeof(string), ElementName = "City")]
        public string City { get; set; }
        [XmlElement(typeof(string), ElementName = "State")]
        public string State { get; set; }
        [XmlElement(typeof(string), ElementName = "Zip")]
        public string Zip { get; set; }
        [XmlElement(typeof(bool),ElementName = "IsActive")]
        public bool Active { get; set; }
    }
}
