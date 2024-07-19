using TddXt.AnyExtensibility;

namespace TddXt.Any.NSubstitute;

public static class AnyNSubstituteExtensions
{
  public static T Substitute<T>(this BasicGenerator gen) where T : class
  {
    return gen.InstanceOf(new SubstituteGenerator<T>());
  }
}