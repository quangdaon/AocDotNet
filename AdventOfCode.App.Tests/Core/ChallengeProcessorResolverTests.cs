using AdventOfCode.App.Core;
using AdventOfCode.App.Exceptions;
using AdventOfCode.App.Tests.Mocks;
using Xunit;

namespace AdventOfCode.App.Tests.Core;

public class ChallengeProcessorResolverTests
{
  [Fact]
  public void Resolve_GivenBaselineInputs_CreatesCorrectProcessor()
  {
    var processor = ChallengeProcessorResolver.Resolve<IMockChallengeProcessor>(2000, 1);

    Assert.IsType<MockAoc2000Day01Processor>(processor);
  }

  [Fact]
  public void Resolve_GivenChangedYear_CreatesCorrectProcessor()
  {
    var processor = ChallengeProcessorResolver.Resolve<IMockChallengeProcessor>(2001, 1);

    Assert.IsType<MockAoc2001Day01Processor>(processor);
  }

  [Fact]
  public void Resolve_GivenChangedDay_CreatesCorrectProcessor()
  {
    var processor = ChallengeProcessorResolver.Resolve<IMockChallengeProcessor>(2000, 2);

    Assert.IsType<MockAoc2000Day02Processor>(processor);
  }

  [Fact]
  public void Resolve_GivenUnresolvableParameters_Throws()
  {
    var createProcessor = () => ChallengeProcessorResolver.Resolve<IMockChallengeProcessor>(2022, 2);

    Assert.Throws<UnresolvableProcessorException>(createProcessor);
  }
}