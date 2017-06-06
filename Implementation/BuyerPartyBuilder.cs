using System.Collections.Generic;
using FluentUbl.Interfaces;
using InExchange.Format.UBL;

namespace FluentUbl.Implementation
{
  public class BuyerPartyBuilder : IBuyerPartyBuilder
  {
    private UblCustomerParty _ublCustomerParty;

    public BuyerPartyBuilder(UblCustomerParty ublCustomerParty)
    {
      _ublCustomerParty = ublCustomerParty;
      _ublCustomerParty.Party = new UblParty();
    }
    public IBuyerPartyBuilder BuildName(string name)
    {
      _ublCustomerParty.Party.PartyName = new UblPartyName(name);
      return this;
    }

    public IBuyerPartyBuilder BuildGln(string gln)
    {
      _ublCustomerParty.Party.PartyIdentifications = new List<UblPartyIdentification>()
      {
        new UblPartyIdentification(new UblIdentifier
        {
          Value = gln,
          SchemeAgencyId = "GLN",
          SchemeAgencyName = "INX"
        })
      };
      return this;
    }
  }
}