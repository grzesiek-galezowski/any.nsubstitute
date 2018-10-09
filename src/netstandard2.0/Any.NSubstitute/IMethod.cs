using System;

namespace TddXt.Any.NSubstitute
{
  public interface IMethod
  {
    object InvokeWithAnyArgsOn(object instance, Func<Type, object> valueFactory);
    object GenerateAnyReturnValue(Func<Type, object> valueFactory);
  }
}