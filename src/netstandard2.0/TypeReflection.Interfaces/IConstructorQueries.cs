using System.Collections.Generic;
using TddXt.Any.Substitute.CommonTypes;

namespace TddXt.Any.TypeReflection.Interfaces
{
  public interface IConstructorQueries
  {
    Maybe<IConstructorWrapper> GetNonPublicParameterlessConstructorInfo();
    Maybe<IConstructorWrapper> GetPublicParameterlessConstructor();
    List<IConstructorWrapper> TryToObtainInternalConstructorsWithoutRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithoutRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainInternalConstructorsWithRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainPrimitiveTypeConstructor();
    IEnumerable<IConstructorWrapper> TryToObtainPublicStaticFactoryMethodWithoutRecursion();
    IEnumerable<IConstructorWrapper> TryToObtainPrivateAndProtectedConstructorsWithoutRecursiveArguments();
  }
}