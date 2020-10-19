namespace CarDealer.Dtos.Export.CarsWithListOfParts
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("car")]
    public class CarInfoDTO
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledlDistance { get; set; }

        [XmlArray("parts")]
        public List<CarPartInfoDTO> Parts { get; set; }
    }
}
