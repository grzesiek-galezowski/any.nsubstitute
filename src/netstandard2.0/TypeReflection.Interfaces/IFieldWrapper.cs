using System;

namespace TddXt.Any.TypeReflection.Interfaces
{
  public interface IFieldWrapper
  {
    Type FieldType { get; }
    void SetValue(object result, object instance);
    bool IsNullOrDefault(object result);
  }
}
