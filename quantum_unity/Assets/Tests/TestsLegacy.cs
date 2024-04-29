namespace Tests {
  using System;
  using NUnit.Framework;
  using Photon.Deterministic;
  using Quantum;
  using Unity.PerformanceTesting;
  using Unity.PerformanceTesting.Measurements;
  using UnityEngine;
  using Assert = NUnit.Framework.Assert;
  using Input = Quantum.Input;
  
  using ComponentWithInMiddleBlock = Quantum.ComponentTest100;
  using ComponentWithInLastBlock = Quantum.ComponentTest200;
  using WithoutInMiddleBlock = Quantum.ComponentTest101;
  using WithoutInLastBlock = Quantum.ComponentTest201;
  using AnyInMiddleBlock = Quantum.ComponentTest102;
  using AnyInLastBlock = Quantum.ComponentTest202;
  using ComponentAlwaysAdded = Quantum.Transform3D;
  using WithoutInFirstBlock = Quantum.ComponentTest040;
  using AnyInFirstBlock = Quantum.ComponentTest041;
  
  public class TestsLegacy : PerfTestBase {
    
    protected static ComponentSpec[] LegacySpecs = new ComponentSpec[] {
      (ComponentSet.Create<ComponentAlwaysAdded, ComponentWithInMiddleBlock, ComponentWithInLastBlock, WithoutInLastBlock>(), 0.33f),
      (ComponentSet.Create<ComponentAlwaysAdded, ComponentWithInMiddleBlock, ComponentWithInLastBlock, AnyInLastBlock>(), 0.165f),
      (ComponentSet.Create<ComponentAlwaysAdded, ComponentWithInMiddleBlock, ComponentWithInLastBlock>(), 0.165f),
      (ComponentSet.Create<ComponentAlwaysAdded>(), 1.0f)
    };

    [Test, Performance]
    [TestCase(true)]
    [TestCase(false)]
    public void TestWith(bool shuffle) {
      RunTest(f => {
        var With_Filter = f.Filter<ComponentAlwaysAdded, ComponentWithInMiddleBlock, ComponentWithInLastBlock>();
        int count       = 0;
        while (With_Filter.Next(out var e, out var a, out var b, out var c)) {
          count++;
        }
        return count;
      }, oneTimeSetUp: f => SetUp(f, shuffle));
    }

    [Test, Performance]
    [TestCase(true)]
    [TestCase(false)]
    public void TestWithWithout(bool shuffle) {
      var WithoutSet = ComponentSet.Create<WithoutInFirstBlock, WithoutInMiddleBlock, WithoutInLastBlock>();
      RunTest(f => {
        var With_Without_Filter = f.Filter<ComponentAlwaysAdded, ComponentWithInMiddleBlock, ComponentWithInLastBlock>(without: WithoutSet);
        int count               = 0;
        while (With_Without_Filter.Next(out var e, out var a, out var b, out var c)) {
          count++;
        }

        return count;
      }, oneTimeSetUp: f => SetUp(f, shuffle));
    }

    [Test, Performance]
    [TestCase(true)]
    [TestCase(false)]
    public void TestWithWithoutAny(bool shuffle) {
      var AnySet = ComponentSet.Create<AnyInFirstBlock, AnyInMiddleBlock, AnyInLastBlock>();
      var WithoutSet = ComponentSet.Create<WithoutInFirstBlock, WithoutInMiddleBlock, WithoutInLastBlock>();
      RunTest(f => {
        var With_Without_Any_Filter = f.Filter<ComponentAlwaysAdded, ComponentWithInMiddleBlock, ComponentWithInLastBlock>(without: WithoutSet, any: AnySet);
        int count                   = 0;
        while (With_Without_Any_Filter.Next(out var e, out var a, out var b, out var c)) {
          count++;
        }

        return count;
      }, oneTimeSetUp: f => SetUp(f, shuffle));
    }
    
    void SetUp(Frame f, bool shuffle) {
      CreateEntities(f, DefaultEntityCount, null, LegacySpecs);
      if (shuffle) {
        for (int i = 0; i < 5; i++) {
          int count = DestroyEntities<ComponentAlwaysAdded>(f, FP._0_20);
          CreateEntities(f, count, null, LegacySpecs);
        }
      }
    }
  }
}