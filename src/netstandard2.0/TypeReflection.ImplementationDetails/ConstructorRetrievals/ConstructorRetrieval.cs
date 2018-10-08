using System.Collections.Generic;
using TddXt.Any.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public interface ConstructorRetrieval
  {
    IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors);
  }
}