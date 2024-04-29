namespace Tests {
  using NUnit.Framework;
  using Quantum;
  using Unity.PerformanceTesting;
  using Assert = NUnit.Framework.Assert;

  public class TestSimple : PerfTestBase {
    private const int Repetitions = 1000;


    [Test]
    [Performance]
    public void NoMatches() {
      RunTest(frame => {
        int count  = 0;
        var filter = frame.Filter<Transform2D>();
        while (filter.Next(out EntityRef e, out Transform2D a)) count++;
        Assert.Zero(count);
        return count;
      });
    }

    [Test]
    [Performance]
    public void TestFilterSingleComponent() {
      RunTest(frame => {
        int count  = 0;
        var filter = frame.Filter<Transform2D>();
        while (filter.Next(out EntityRef e, out Transform2D a)) count++;

        Assert.IsTrue(count == DefaultEntityCount);// to avoid allocs
        return count;
      }, oneTimeSetUp: f => {
        CreateEntities(f, DefaultEntityCount, typeof(Transform2D));
      });
    }
  }
}