using System;

namespace TddXt.Any.TypeReflection.Interfaces
{
  public interface IPropertyWrapper
  {
    Type PropertyType { get; }
    bool HasAbstractGetter();
    void SetValue(object result, object value);
  }
}
