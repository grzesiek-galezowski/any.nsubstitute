using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TddXt.Any.NSubstitute
{
  public class SmartType : IType
  {
    private readonly TypeInfo _typeInfo;

    public SmartType(Type type)
    {
      _typeInfo = type.GetTypeInfo();
    }

    public IEnumerable<IMethod> GetAllPublicInstanceMethodsWithReturnValue()
    {
      return _typeInfo.GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.ReturnType != typeof(void)).
        Select(p => new SmartMethod(p));
    }
    //TODO even strict mocks can be done this way...

    public static IType For(Type type)
    {
      return new SmartType(type);
    }
  }

}