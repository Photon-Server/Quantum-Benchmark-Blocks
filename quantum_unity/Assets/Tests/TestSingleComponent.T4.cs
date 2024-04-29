
using NUnit.Framework;
using Quantum;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

using Assert = NUnit.Framework.Assert;
using ComponentTest001 = Quantum.Transform2D;

namespace Tests
{


    public class TestSingleComponent : PerfTestBase
    {
        [Test, Performance] public void Single_NoShuffle_100Percent_ComponentTest030() => NoShuffle<ComponentTest030>((typeof(ComponentTest030), 1f));     
        [Test, Performance] public void Single_WithShuffle_100Percent_ComponentTest030() => WithShuffle<ComponentTest030>((typeof(ComponentTest030), 1f));     

        [Test, Performance] public void Single_NoShuffle_20Percent_ComponentTest030() => NoShuffle<ComponentTest030>((typeof(ComponentTest030), 0.2f));     
        [Test, Performance] public void Single_WithShuffle_20Percent_ComponentTest030() => WithShuffle<ComponentTest030>((typeof(ComponentTest030), 0.2f));     

        [Test, Performance] public void Single_NoShuffle_5Percent_ComponentTest030() => NoShuffle<ComponentTest030>((typeof(ComponentTest030), 0.05f));     
        [Test, Performance] public void Single_WithShuffle_5Percent_ComponentTest030() => WithShuffle<ComponentTest030>((typeof(ComponentTest030), 0.05f));     

        [Test, Performance] public void Single_NoShuffle_100Percent_ComponentTest064() => NoShuffle<ComponentTest064>((typeof(ComponentTest064), 1f));     
        [Test, Performance] public void Single_WithShuffle_100Percent_ComponentTest064() => WithShuffle<ComponentTest064>((typeof(ComponentTest064), 1f));     

        [Test, Performance] public void Single_NoShuffle_20Percent_ComponentTest064() => NoShuffle<ComponentTest064>((typeof(ComponentTest064), 0.2f));     
        [Test, Performance] public void Single_WithShuffle_20Percent_ComponentTest064() => WithShuffle<ComponentTest064>((typeof(ComponentTest064), 0.2f));     

        [Test, Performance] public void Single_NoShuffle_5Percent_ComponentTest064() => NoShuffle<ComponentTest064>((typeof(ComponentTest064), 0.05f));     
        [Test, Performance] public void Single_WithShuffle_5Percent_ComponentTest064() => WithShuffle<ComponentTest064>((typeof(ComponentTest064), 0.05f));     

        [Test, Performance] public void Single_NoShuffle_100Percent_ComponentTest128() => NoShuffle<ComponentTest128>((typeof(ComponentTest128), 1f));     
        [Test, Performance] public void Single_WithShuffle_100Percent_ComponentTest128() => WithShuffle<ComponentTest128>((typeof(ComponentTest128), 1f));     

        [Test, Performance] public void Single_NoShuffle_20Percent_ComponentTest128() => NoShuffle<ComponentTest128>((typeof(ComponentTest128), 0.2f));     
        [Test, Performance] public void Single_WithShuffle_20Percent_ComponentTest128() => WithShuffle<ComponentTest128>((typeof(ComponentTest128), 0.2f));     

        [Test, Performance] public void Single_NoShuffle_5Percent_ComponentTest128() => NoShuffle<ComponentTest128>((typeof(ComponentTest128), 0.05f));     
        [Test, Performance] public void Single_WithShuffle_5Percent_ComponentTest128() => WithShuffle<ComponentTest128>((typeof(ComponentTest128), 0.05f));     

        [Test, Performance] public void Single_NoShuffle_100Percent_ComponentTest192() => NoShuffle<ComponentTest192>((typeof(ComponentTest192), 1f));     
        [Test, Performance] public void Single_WithShuffle_100Percent_ComponentTest192() => WithShuffle<ComponentTest192>((typeof(ComponentTest192), 1f));     

        [Test, Performance] public void Single_NoShuffle_20Percent_ComponentTest192() => NoShuffle<ComponentTest192>((typeof(ComponentTest192), 0.2f));     
        [Test, Performance] public void Single_WithShuffle_20Percent_ComponentTest192() => WithShuffle<ComponentTest192>((typeof(ComponentTest192), 0.2f));     

        [Test, Performance] public void Single_NoShuffle_5Percent_ComponentTest192() => NoShuffle<ComponentTest192>((typeof(ComponentTest192), 0.05f));     
        [Test, Performance] public void Single_WithShuffle_5Percent_ComponentTest192() => WithShuffle<ComponentTest192>((typeof(ComponentTest192), 0.05f));     

        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest030_ComponentTest064() => SimpleFilter<ComponentTest030, ComponentTest064>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest030_ComponentTest064() => PairComponentWithShuffle<ComponentTest030, ComponentTest064>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest030_ComponentTest064() => SimpleFilter<ComponentTest030, ComponentTest064>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest030_ComponentTest064() => PairComponentWithShuffle<ComponentTest030, ComponentTest064>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest030_ComponentTest064() => SimpleFilter<ComponentTest030, ComponentTest064>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest030_ComponentTest064() => PairComponentWithShuffle<ComponentTest030, ComponentTest064>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest030_ComponentTest128() => SimpleFilter<ComponentTest030, ComponentTest128>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest030_ComponentTest128() => PairComponentWithShuffle<ComponentTest030, ComponentTest128>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest030_ComponentTest128() => SimpleFilter<ComponentTest030, ComponentTest128>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest030_ComponentTest128() => PairComponentWithShuffle<ComponentTest030, ComponentTest128>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest030_ComponentTest128() => SimpleFilter<ComponentTest030, ComponentTest128>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest030_ComponentTest128() => PairComponentWithShuffle<ComponentTest030, ComponentTest128>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest030_ComponentTest192() => SimpleFilter<ComponentTest030, ComponentTest192>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest030_ComponentTest192() => PairComponentWithShuffle<ComponentTest030, ComponentTest192>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest030_ComponentTest192() => SimpleFilter<ComponentTest030, ComponentTest192>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest030_ComponentTest192() => PairComponentWithShuffle<ComponentTest030, ComponentTest192>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest030_ComponentTest192() => SimpleFilter<ComponentTest030, ComponentTest192>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest030_ComponentTest192() => PairComponentWithShuffle<ComponentTest030, ComponentTest192>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest064_ComponentTest030() => SimpleFilter<ComponentTest064, ComponentTest030>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest064_ComponentTest030() => PairComponentWithShuffle<ComponentTest064, ComponentTest030>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest064_ComponentTest030() => SimpleFilter<ComponentTest064, ComponentTest030>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest064_ComponentTest030() => PairComponentWithShuffle<ComponentTest064, ComponentTest030>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest064_ComponentTest030() => SimpleFilter<ComponentTest064, ComponentTest030>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest064_ComponentTest030() => PairComponentWithShuffle<ComponentTest064, ComponentTest030>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest064_ComponentTest128() => SimpleFilter<ComponentTest064, ComponentTest128>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest064_ComponentTest128() => PairComponentWithShuffle<ComponentTest064, ComponentTest128>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest064_ComponentTest128() => SimpleFilter<ComponentTest064, ComponentTest128>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest064_ComponentTest128() => PairComponentWithShuffle<ComponentTest064, ComponentTest128>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest064_ComponentTest128() => SimpleFilter<ComponentTest064, ComponentTest128>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest064_ComponentTest128() => PairComponentWithShuffle<ComponentTest064, ComponentTest128>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest064_ComponentTest192() => SimpleFilter<ComponentTest064, ComponentTest192>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest064_ComponentTest192() => PairComponentWithShuffle<ComponentTest064, ComponentTest192>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest064_ComponentTest192() => SimpleFilter<ComponentTest064, ComponentTest192>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest064_ComponentTest192() => PairComponentWithShuffle<ComponentTest064, ComponentTest192>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest064_ComponentTest192() => SimpleFilter<ComponentTest064, ComponentTest192>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest064_ComponentTest192() => PairComponentWithShuffle<ComponentTest064, ComponentTest192>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest128_ComponentTest030() => SimpleFilter<ComponentTest128, ComponentTest030>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest128_ComponentTest030() => PairComponentWithShuffle<ComponentTest128, ComponentTest030>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest128_ComponentTest030() => SimpleFilter<ComponentTest128, ComponentTest030>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest128_ComponentTest030() => PairComponentWithShuffle<ComponentTest128, ComponentTest030>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest128_ComponentTest030() => SimpleFilter<ComponentTest128, ComponentTest030>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest128_ComponentTest030() => PairComponentWithShuffle<ComponentTest128, ComponentTest030>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest128_ComponentTest064() => SimpleFilter<ComponentTest128, ComponentTest064>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest128_ComponentTest064() => PairComponentWithShuffle<ComponentTest128, ComponentTest064>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest128_ComponentTest064() => SimpleFilter<ComponentTest128, ComponentTest064>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest128_ComponentTest064() => PairComponentWithShuffle<ComponentTest128, ComponentTest064>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest128_ComponentTest064() => SimpleFilter<ComponentTest128, ComponentTest064>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest128_ComponentTest064() => PairComponentWithShuffle<ComponentTest128, ComponentTest064>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest128_ComponentTest192() => SimpleFilter<ComponentTest128, ComponentTest192>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest128_ComponentTest192() => PairComponentWithShuffle<ComponentTest128, ComponentTest192>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest128_ComponentTest192() => SimpleFilter<ComponentTest128, ComponentTest192>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest128_ComponentTest192() => PairComponentWithShuffle<ComponentTest128, ComponentTest192>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest128_ComponentTest192() => SimpleFilter<ComponentTest128, ComponentTest192>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest128_ComponentTest192() => PairComponentWithShuffle<ComponentTest128, ComponentTest192>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest192_ComponentTest030() => SimpleFilter<ComponentTest192, ComponentTest030>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest192_ComponentTest030() => PairComponentWithShuffle<ComponentTest192, ComponentTest030>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest192_ComponentTest030() => SimpleFilter<ComponentTest192, ComponentTest030>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest192_ComponentTest030() => PairComponentWithShuffle<ComponentTest192, ComponentTest030>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest192_ComponentTest030() => SimpleFilter<ComponentTest192, ComponentTest030>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest192_ComponentTest030() => PairComponentWithShuffle<ComponentTest192, ComponentTest030>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest192_ComponentTest064() => SimpleFilter<ComponentTest192, ComponentTest064>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest192_ComponentTest064() => PairComponentWithShuffle<ComponentTest192, ComponentTest064>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest192_ComponentTest064() => SimpleFilter<ComponentTest192, ComponentTest064>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest192_ComponentTest064() => PairComponentWithShuffle<ComponentTest192, ComponentTest064>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest192_ComponentTest064() => SimpleFilter<ComponentTest192, ComponentTest064>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest192_ComponentTest064() => PairComponentWithShuffle<ComponentTest192, ComponentTest064>(0.05f);
        //[Test, Performance] public void Pair_NoShuffle_100Percent_ComponentTest192_ComponentTest128() => SimpleFilter<ComponentTest192, ComponentTest128>(1f);     
        //[Test, Performance] public void Pair_WithShuffle_100Percent_ComponentTest192_ComponentTest128() => PairComponentWithShuffle<ComponentTest192, ComponentTest128>(1f);
        //[Test, Performance] public void Pair_NoShuffle_20Percent_ComponentTest192_ComponentTest128() => SimpleFilter<ComponentTest192, ComponentTest128>(0.2f);     
        //[Test, Performance] public void Pair_WithShuffle_20Percent_ComponentTest192_ComponentTest128() => PairComponentWithShuffle<ComponentTest192, ComponentTest128>(0.2f);
        //[Test, Performance] public void Pair_NoShuffle_5Percent_ComponentTest192_ComponentTest128() => SimpleFilter<ComponentTest192, ComponentTest128>(0.05f);     
        //[Test, Performance] public void Pair_WithShuffle_5Percent_ComponentTest192_ComponentTest128() => PairComponentWithShuffle<ComponentTest192, ComponentTest128>(0.05f);
    }
}

