using System;
using System.Collections.Generic;
using InExchange.Format.UBL;

namespace FluentUbl.Implementation
{
  public class LinesBuilder : Interfaces.ILinesBuilder
  {
    private List<UblOrderLine> _ublOrderLines;

    public LinesBuilder(List<UblOrderLine> ublOrderLines)
    {
      _ublOrderLines = ublOrderLines;
    }

    public Interfaces.ILinesBuilder AddLine(Action<Interfaces.ILineBuilder> action)
    {
      var line = new UblOrderLine()
      {
        LineItem = new UblLineItem()
      };
      _ublOrderLines.Add(line);
      var lineBuilder = new LineBuilder(line.LineItem);
      action(lineBuilder);
      return this;
    }
  }
}