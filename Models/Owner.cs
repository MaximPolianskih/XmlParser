using System;
using System.Xml.Serialization;

namespace XmlParser.Models
{
    [XmlRoot(ElementName = "Owner")]
    public class Owner
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public int OrgVId { get; set; }
        public string OrgName { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string AddressLegal { get; set; }
        public string INN { get; set; }
        public string ORGN { get; set; }
        public string KPP { get; set; }
        public int OrgTypeId { get; set; }
        public string OrgTypeName { get; set; }
        public int BusinessKindId { get; set; }
        public string BusinessKindName { get; set; }
    }
}
