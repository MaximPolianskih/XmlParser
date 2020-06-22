using System;
using System.Threading;
using System.Xml.Serialization;

namespace XmlParser.Models
{
    [XmlRoot(ElementName = "par:LicenseBroadcasting")]
    public class LicenseBroadcasting
    {
        public int Id { get; set; }
        public int ParamId { get; set; }
        public int LicNum { get; set; }
        public int LicEisId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateServiceStart { get; set; }
        public int TypeDateServiceStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime? DateSuspension { get; set; }
        public DateTime? DateRenewal { get; set; }
        public DateTime? DateAnnulment { get; set; }
        public string Seria { get; set; }
        public int ActivityKind { get; set; }
        public int BroadcastArea { get; set; }
        public string BroadcastAreaName { get; set; }
        public int MaxTimeWeek { get; set; }
        public string PerTime { get; set; }
        public string Place { get; set; }
        public string TenderInfo { get; set; }
        public byte ConceptIsProgram { get; set; }
        public byte IsAdv { get; set; }
        public Owner Owner { get; set; }
        public string OrderNum { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte IsActual { get; set; }
        public DateTime? LastChangeDate { get; set; }
    }
}
