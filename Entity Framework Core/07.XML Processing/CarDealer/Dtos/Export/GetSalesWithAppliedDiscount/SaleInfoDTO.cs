namespace CarDealer.Dtos.Export.GetSalesWithAppliedDiscount
{
    using System.Xml.Serialization;

    [XmlType("sale")]
    public class SaleInfoDTO
    {
        [XmlElement("car")]
        public CarBySaleInfoDTO Car { get; set; }

        [XmlElement("discount")]
        public decimal Discount { get; set; }

        [XmlElement("customer-name")]
        public string CustumerName { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public decimal PriceWithDiscount
        {
            get
            {
                return this.Price - (this.Price * this.Discount / 100);
            }
            set
            {
                this.PriceWithDiscount = value ;
            }
        }

    }
}
