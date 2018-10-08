using System;
using System.Collections.Generic;
using System.Reflection;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.Any.TypeReflection.Interfaces
{
  public interface IConstructorWrapper
  {
    IEnumerable<ParameterInfo> Parameters { get; }
    bool HasNonPointerArgumentsOnly();
    bool HasLessParametersThan(int numberOfParams);
    int GetParametersCount();
    bool HasAbstractOrInterfaceArguments();

    List<object> GenerateAnyParameterValues(Func<Type, GenerationTrace, object> instanceGenerator,
      GenerationTrace trace);

    bool IsParameterless();
    object InvokeWithParametersCreatedBy(Func<Type, GenerationTrace, object> instanceGenerator, GenerationTrace trace);
    bool IsInternal();
    bool IsNotRecursive();
    bool IsRecursive();

    object Invoke(IEnumerable<object> parameters);

    void DumpInto(GenerationTrace trace);
  }
}
