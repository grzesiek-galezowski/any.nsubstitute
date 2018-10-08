using NSubstitute;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;
using TddXt.TypeReflection;

namespace TddXt.Any.NSubstitute
{
  public class SubstituteGenerator<T> : InlineGenerator<T> where T : class
  {
    //todo move substitute generator to a separate nuget project
    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace) 
    {
      var type = typeof(T);
      var sub = Substitute.For<T>();

      var methods = LolSmartType.For(type).GetAllPublicInstanceMethodsWithReturnValue();

      foreach (var method in methods)
      {
        method.InvokeWithAnyArgsOn(sub, argType => instanceGenerator.Instance(argType, trace))
          .ReturnsForAnyArgs(method.GenerateAnyReturnValue(returnType => instanceGenerator.Instance(returnType, trace)));
      }

      return sub;
    }
  }

  public static class AnyNSubstituteExtensions
  {
    public static T Substitute<T>(this BasicGenerator gen) where T : class
    {
      return gen.InstanceOf(new SubstituteGenerator<T>());
    }
  }

}