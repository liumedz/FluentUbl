using System;
using System.Collections.Generic;

namespace InExchange.CustomerSpecific.OrderConverters.Incoming.PyramidOrder.Model
{
  internal class PyramidOrder
  {
    public PyramidOrder()
    {
        this.Items = new List<PyramidOrderItem>();
    }
    public string SellerGLN { set; get; }
    public string BuyerGLN { set; get; }
    public string BuyerName { get; set; }
    public string BuyerName2 { get; set; }
    public string Currency { set; get; }
    public string DeliveryStreetName { set; get; }
    public string SellerCompanyName { get; set; }
    public string BuyerCompanyName { set; get; }
    public string DeliveryPostbox { set; get; }
    public string DeliveryCountry { set; get; }
    public string SellerStreetName { get; set; }
    public string SellerPostbox { get; set; }
    // AccountingCost is a reference for the invoice later on, nothing that identifies the order itself
    // it's the mark/reference to be used on the invoice, so that the invoice gets sent to the person who ordered it 
    public string AccountingCost { get; set; }
    [Obsolete]
    public string PurchaseOrderNumber { set; get; }
    public IList<PyramidOrderItem> Items { private set; get; }
  }
}