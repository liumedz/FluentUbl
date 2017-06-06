using System;
using System.Collections.Generic;
using InExchange.Format.UBL;

namespace FluentUbl.Interfaces
{
  public interface IOrderBuilder
  {
    IOrderBuilder BuildId(string id);
    IOrderBuilder BuildIssueDate(DateTime date);
    IOrderBuilder BuildIssueTime(DateTime time);
    IOrderBuilder BuildBuyerParty(Func<IBuyerPartyBuilder, IBuyerPartyBuilder> buyerPartyBuilder);
    IOrderBuilder BuildLines(Func<IEnumerable<ILineBuilder>> lineBuilder);
    IOrderBuilder BuildLines(Action<ILinesBuilder> action);
    ILineBuilder BuildLine();
    UblOrder GetUblOrder();
  }
}