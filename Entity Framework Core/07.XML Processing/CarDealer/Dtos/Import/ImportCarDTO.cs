namespace CarDealer.Dtos.Import
{
    
    using System.Collections.Generic;
    using System.Xml.Serialization;


    [XmlType("Car")]
    public class ImportCarDTO
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public int TraveledDistance { get; set; }

        [XmlArray("parts")]
        [XmlArrayItem("partId")]
        public List<CarPartsInfoDTO> Parts { get; set; }
    }
}
