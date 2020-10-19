namespace CarDealer.Dtos.Export.CarsWithListOfParts
{
    using System.Xml.Serialization;

    [XmlType("part")]
    public class CarPartInfoDTO
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
