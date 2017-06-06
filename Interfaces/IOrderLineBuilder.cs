using System;
using System.Collections.Generic;

namespace FluentUbl.Interfaces
{
  public interface IOrderLineBuilder
  {
    IOrderBuilder BuildLines(Func<IEnumerable<ILineBuilder>> lineBuilder);
    IOrderBuilder BuildLines(Action<ILinesBuilder> action);
    ILineBuilder BuildLine();
  }
}