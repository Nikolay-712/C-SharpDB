using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Dtos.Export
{
    [XmlType("Kvo da e")]
    public class SoldProductsInfoDTO
    {
        [XmlElement("cont")]
        public int Count { get; set; }

        [XmlArray("products")]
        public List<SoldPruductInfoDTO> ProductsSold { get; set; }
    }
}
