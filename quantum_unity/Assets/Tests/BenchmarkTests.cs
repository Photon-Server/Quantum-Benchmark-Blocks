using System.Diagnostics;
using NUnit.Framework;
using Photon.Deterministic;
using Quantum;
using Unity.PerformanceTesting;
using Debug = UnityEngine.Debug;
using Input = Quantum.Input;

namespace Tests
{
    public class BenchmarkTests
    {
        private const float Delta = 0.05f;

        QuantumRunner CreateRunner()
        {
            var runtimeConfig = new RuntimeConfig()
            {
                SimulationConfig = QuantumDefaultConfigs.Global.SimulationConfig,
                SystemsConfig = QuantumDefaultConfigs.Global.SystemsConfig,
            };

            var arguments = new SessionRunner.Arguments
            {
                RunnerFactory = QuantumRunnerUnityFactory.DefaultFactory,
                GameParameters = QuantumRunnerUnityFactory.CreateGameParameters,
                RuntimeConfig = runtimeConfig,
                SessionConfig = QuantumDeterministicSessionConfigAsset.DefaultConfig,
                GameMode = DeterministicGameMode.Local,
                RunnerId = "LOCALDEBUG",
                PlayerCount = Input.MAX_COUNT,
                InstantReplaySettings = default,
                InitialDynamicAssets = default,
                DeltaTimeType = SimulationUpdateTime.EngineDeltaTime,
            };
            
            Debug.Log("Creating runner");
            return QuantumRunner.StartGame(arguments);
        }

        [Test, Performance]
        [TestCase(10, 10, 10)]
        public unsafe void SampleTest(int grow, int max, int samples)
        {   
            using var runner = CreateRunner();
            
            using var playerHandler = QuantumCallback.SubscribeManual((CallbackGameStarted c) =>
            {
                c.Game.AddPlayer(new RuntimePlayer());
            });

            // spin up
            runner.Service(1.0);
            
            // now set up the input to enable tests
            using var inputHandler = QuantumCallback.SubscribeManual((CallbackPollInput c) =>
            {
                Input input;
                input.runnig = true;
                input.grown = grow;
                input.max = max;
                input.samples = samples;
                c.SetInput(input, DeterministicInputFlags.Repeatable);
            });
            
            var sampleGroup = new SampleGroup("UpdateTime");
            using (Measure.Scope())
            {
                for (int i = 0; i < 20; ++i)
                {
                    var stopwatch = Stopwatch.StartNew();
                    runner.Service(Delta);
                    Measure.Custom(sampleGroup, stopwatch.ElapsedMilliseconds);
                }
            }
        }
    }
}