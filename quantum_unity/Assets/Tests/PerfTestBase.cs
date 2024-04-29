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

  public abstract partial class PerfTestBase {
    public const int DefaultEntityCount = 20000;

    [Test]
    [Performance]
    public void __WarmupAndOverhead() {
      RunTest(frame => 0);
    }

    protected static QuantumRunner CreateRunner() {
      RuntimeConfig runtimeConfig = new() {
        SimulationConfig = QuantumDefaultConfigs.Global.SimulationConfig,
        SystemsConfig    = QuantumDefaultConfigs.Global.SystemsConfig
      };

      SessionRunner.Arguments arguments = new() {
        RunnerFactory         = QuantumRunnerUnityFactory.DefaultFactory,
        GameParameters        = QuantumRunnerUnityFactory.CreateGameParameters,
        RuntimeConfig         = runtimeConfig,
        SessionConfig         = QuantumDeterministicSessionConfigAsset.DefaultConfig,
        GameMode              = DeterministicGameMode.Local,
        RunnerId              = "LOCALDEBUG",
        PlayerCount           = Input.MAX_COUNT,
        InstantReplaySettings = default,
        InitialDynamicAssets  = default,
        DeltaTimeType         = SimulationUpdateTime.EngineDeltaTime
      };

      Debug.Log("Creating runner");
      return QuantumRunner.StartGame(arguments);
    }

    protected static unsafe void CreateEntities(Frame f, int count, Type alwaysAdd, params ComponentSpec[] components) {
      if (alwaysAdd != null) {
        var newComponents = new ComponentSpec[components.Length + 1];
        Array.Copy(components, newComponents, components.Length);
        
        for (int i = 0; i < newComponents.Length; ++i) {
          newComponents[i].Components.Add(ComponentTypeId.GetComponentIndex(alwaysAdd));
        }

        newComponents[^1].Probability = 1;
        
        CreateEntities(f, count, newComponents);
      } else {
        CreateEntities(f, count, components);
      }
    }

    protected static unsafe void CreateEntities(Frame f, int count, params ComponentSpec[] components) {
      for (int i = 0; i < count; ++i) {
        EntityRef entity = f.Create();

        var p = f.RNG->Next();

        foreach (var spec in components) {
          var componentSet = spec.Components;
          var probability  = spec.Probability;
          p -= probability;
          if (p > 0) {
            continue;
          }
          
          for (int c = 0; c < ComponentSet.MAX_COMPONENTS; ++c) {
            if (!componentSet.IsSet(c)) {
              continue;
            }
            f.Add(entity, c, null);
          }
          break;
        }
      }
    }

    public static ComponentSpec[] WithComponents(params ComponentSpec[] components) {
      return components;
    }

    
    
    public void RunTest(Func<Frame, int> test, Action<Frame> setUp = null, Action<Frame> oneTimeSetUp = null, int frameCount = 50, int entityCount = DefaultEntityCount) {
      Assert.IsNull(DelegatingSystem._Update);
      Assert.IsNull(DelegatingSystem._OnInit);

      try {
        DelegatingSystem._OnInit = f => { oneTimeSetUp?.Invoke(f); };

        using QuantumRunner runner = CreateRunner();
        // spin everything up
        runner.Service(1.0);

        double      delta       = 1.0 / runner.Game.Session.SimulationRate;
        SampleGroup sampleGroup = new("UpdateTime", SampleUnit.Microsecond);
        int         lastValue   = -1;


        DelegatingSystem._Update = f => {
          setUp?.Invoke(f);
          test(f);
        };

        runner.Service(delta);
        

        DelegatingSystem._Update = f => {
          setUp?.Invoke(f);
          int value;
          using (Measure.Scope(sampleGroup)) {
            value = test(f);
          }
          lastValue = value;
        };

        for (int i = 0; i < frameCount; i++) {
          runner.Service(delta);
        }
        Debug.Log($"Last value: {lastValue}");
      } finally {
        DelegatingSystem._Update = null;
        DelegatingSystem._OnInit = null;
      }
    }

    protected unsafe int DestroyEntities<T>(Frame f, FP percent) where T : unmanaged, IComponent {
      int destroyedEntities = 0;
      foreach (var pair in f.Unsafe.GetComponentBlockIterator<T>())
        if (f.RNG->Next() <= percent) {
          destroyedEntities++;
          f.Destroy(pair.Entity);
        }

      f.Unsafe.CommitAllCommands();
      return destroyedEntities;
    }
    
    public struct ComponentSpec {
      public ComponentSet Components;
      public FP           Probability;


      public static implicit operator ComponentSpec(Type type) {
        var set = new ComponentSet();
        set.Add(ComponentTypeId.GetComponentIndex(type));
        return new ComponentSpec {
          Components  = set,
          Probability = 1
        };
      }

      public static implicit operator ComponentSpec((Type, float) tuple) {
        var set = new ComponentSet();
        set.Add(ComponentTypeId.GetComponentIndex(tuple.Item1));
        return new ComponentSpec {
          Components  = set,
          Probability = FP.FromFloat_UNSAFE(tuple.Item2)
        };
      }

      public static implicit operator ComponentSpec((ComponentSet set, float probability) tuple) {
        return new ComponentSpec {
          Components  = tuple.set,
          Probability = FP.FromFloat_UNSAFE(tuple.probability)
        };
      }
      
      public static implicit operator ComponentSpec(int typeId) {
        var set = new ComponentSet();
        set.Add(typeId);
        return new ComponentSpec {
          Components  = set,
          Probability = 1
        };
      }
    }
  }
}