namespace TddXt.Any.NSubstitute;

public static class AnyNSubstituteExtensions
{
    extension(AnyRoot.Any)
    {
        public static T Substitute<T>() where T : class
        {
            return AnyRoot.Any.InstanceOf(new SubstituteGenerator<T>());
        }
    }
}