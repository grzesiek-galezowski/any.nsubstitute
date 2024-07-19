using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using TddXt.Any.NSubstitute;
using static TddXt.AnyRoot.Root;

namespace TddToolkitSpecification;

public class AnySubstituteSpecification
{
  [Test]
  public void ShouldBeAbleToWrapSubstitutesAndOverrideDefaultValues()
  {
    //GIVEN
    var instance = Any.Substitute<RecursiveInterface>();

    //WHEN
    var result = instance.Number;

    //THEN
    ClassicAssert.AreNotEqual(default(int), result);
  }

  [Test]
  public void ShouldBeAbleToWrapSubstitutesAndNotOverrideStubbedValues()
  {
    //GIVEN
    var instance = Any.Substitute<RecursiveInterface>();
    instance.Number.Returns(44543);

    //WHEN
    var result = instance.Number;

    //THEN
    ClassicAssert.AreEqual(44543, result);
  }

  [Test]
  public void ShouldBeAbleToWrapSubstitutesAndStillAllowVerifyingCalls()
  {
    //GIVEN
    var instance = Any.Substitute<RecursiveInterface>();

    //WHEN
    instance.VoidMethod();

    //THEN
    instance.Received(1).VoidMethod();
  }

  [Test]
  public void ShouldReturnNonNullImplementationsOfInnerObjects()
  {
    //GIVEN
    var instance = Any.Substitute<RecursiveInterface>();

    //WHEN
    var result = instance.Nested;

    //THEN
    ClassicAssert.NotNull(result);
  }

  [Test]
  public void ShouldBeAbleToWrapSubstitutesAndSkipOverridingResultsStubbedWithNonDefaultValues()
  {
    var instance = Any.Substitute<RecursiveInterface>();
    var anotherInstance = Substitute.For<RecursiveInterface>();
    instance.Nested.Returns(anotherInstance);

    ClassicAssert.AreEqual(anotherInstance, instance.Nested);
  }
}