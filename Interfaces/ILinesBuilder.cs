using System;

namespace FluentUbl.Interfaces
{
  public interface ILinesBuilder
  {
    ILinesBuilder AddLine(Action<ILineBuilder> action);
  }
}