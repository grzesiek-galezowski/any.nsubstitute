using System.Collections.Generic;

namespace TddXt.Any.NSubstitute;

public interface IType
{
  IEnumerable<IMethod> GetAllPublicInstanceMethodsWithReturnValue();
}