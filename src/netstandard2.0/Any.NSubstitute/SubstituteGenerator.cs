using NSubstitute;
using TddXt.AnyExtensibility;

namespace TddXt.Any.NSubstitute
{
  public class SubstituteGenerator<T> : InlineGenerator<T> where T : class
  {
    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      var type = typeof(T);
      var sub = Substitute.For<T>();

      var methods = SmartType.For(type).GetAllPublicInstanceMethodsWithReturnValue();

      foreach (var method in methods)
      {
        method.InvokeWithAnyArgsOn(sub, argType => instanceGenerator.Instance(argType, request))
          .ReturnsForAnyArgs(method.GenerateAnyReturnValue(returnType => instanceGenerator.Instance(returnType, request)));
      }

      return sub;
    }
  }
}