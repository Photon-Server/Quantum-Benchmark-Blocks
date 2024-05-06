namespace Tests {
  using NUnit.Framework;
  using Quantum;
  using Unity.PerformanceTesting;
  using Assert = NUnit.Framework.Assert;

  public class PerfTestBasic : PerfTestBase {
    
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

        if (count != 10000) {
          Assert.Fail($"Expected 10000, got {count}");
        }
        return count;
      }, oneTimeSetUp: f => {
        CreateEntities(f, 10000, typeof(Transform2D));
      });
    }
  }
}