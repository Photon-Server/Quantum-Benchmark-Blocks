
using NUnit.Framework;
using Quantum;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

using Assert = NUnit.Framework.Assert;
using ComponentTest001 = Quantum.Transform2D;

namespace Tests {
  public class PerfTestJustWith : PerfTestBase {
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_100Percent_ComponentTest030(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_100Percent_ComponentTest030_ComponentTest064(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_100Percent_ComponentTest030_ComponentTest064_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064, ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064, ComponentTest128>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Quartet_100Percent_ComponentTest030_ComponentTest064_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064, ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064, ComponentTest128, ComponentTest192>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_100Percent_ComponentTest030_ComponentTest064_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064, ComponentTest192>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_100Percent_ComponentTest030_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest128>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_100Percent_ComponentTest030_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest128, ComponentTest192>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_100Percent_ComponentTest030_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest192>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_100Percent_ComponentTest064(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_100Percent_ComponentTest064_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064, ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064, ComponentTest128>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_100Percent_ComponentTest064_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064, ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064, ComponentTest128, ComponentTest192>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_100Percent_ComponentTest064_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064, ComponentTest192>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_100Percent_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest128>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_100Percent_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest128, ComponentTest192>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_100Percent_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest192>(), 1f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_20Percent_ComponentTest030(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_20Percent_ComponentTest030_ComponentTest064(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_20Percent_ComponentTest030_ComponentTest064_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064, ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064, ComponentTest128>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Quartet_20Percent_ComponentTest030_ComponentTest064_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064, ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064, ComponentTest128, ComponentTest192>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_20Percent_ComponentTest030_ComponentTest064_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064, ComponentTest192>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_20Percent_ComponentTest030_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest128>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_20Percent_ComponentTest030_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest128, ComponentTest192>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_20Percent_ComponentTest030_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest192>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_20Percent_ComponentTest064(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_20Percent_ComponentTest064_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064, ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064, ComponentTest128>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_20Percent_ComponentTest064_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064, ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064, ComponentTest128, ComponentTest192>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_20Percent_ComponentTest064_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064, ComponentTest192>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_20Percent_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest128>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_20Percent_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest128, ComponentTest192>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_20Percent_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest192>(), 0.2f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_5Percent_ComponentTest030(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_5Percent_ComponentTest030_ComponentTest064(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_5Percent_ComponentTest030_ComponentTest064_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064, ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064, ComponentTest128>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Quartet_5Percent_ComponentTest030_ComponentTest064_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064, ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064, ComponentTest128, ComponentTest192>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_5Percent_ComponentTest030_ComponentTest064_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest064, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest064, ComponentTest192>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_5Percent_ComponentTest030_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest128>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_5Percent_ComponentTest030_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest128, ComponentTest192>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_5Percent_ComponentTest030_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest030, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest030, ComponentTest192>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_5Percent_ComponentTest064(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_5Percent_ComponentTest064_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064, ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064, ComponentTest128>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Triplet_5Percent_ComponentTest064_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064, ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064, ComponentTest128, ComponentTest192>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_5Percent_ComponentTest064_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest064, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest064, ComponentTest192>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_5Percent_ComponentTest128(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest128>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest128>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Pair_5Percent_ComponentTest128_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest128, ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest128, ComponentTest192>(), 0.05f))
    );
    [Test, Performance, TestCaseSource(nameof(DefaultTestParameters))]
    public void Single_5Percent_ComponentTest192(TestParams t) => RunTest(
      MakeSimpleFilter<ComponentTest192>(), 
      f => SimpleSetUp(f, t, (ComponentSet.Create<ComponentTest192>(), 0.05f))
    );


  }
}

