namespace InExchange.CustomerSpecific.OrderConverters.Incoming.PyramidOrder.Model
{
  internal class PyramidOrderItem
  {
    public string LineNumber { set; get; }
    public string SellerItemIdentification { set; get; }
    public string UnitsOrdered { set; get; }
    public string UnitPrice { set; get; }
    public string RequestedDeliveryDate { set; get; }
    public PyramidOrderItemNote Note { set; get; }
    public string Description { get; set; }
  }
}