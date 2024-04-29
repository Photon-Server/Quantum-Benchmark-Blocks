#if false
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Quantum {
  public class Benchmark {

    public class BenchmarkData {
      public double AverageElapsedMs => _totalElapsedMs / _sampleCount;
      public double LongestElapsedMs => _longestElapsedMs;
      public double TotalMs => _totalElapsedMs;
      public int SampleCount => _sampleCount;
      public double StdDevMs {
        get {
          var mean = AverageElapsedMs;
          return Math.Sqrt((_sumOfSquaredElapsedMs / _sampleCount) - (mean * mean));
        }
      }

      private double _totalElapsedMs;
      private double _sumOfSquaredElapsedMs;
      private int _sampleCount;
      private double _longestElapsedMs;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void Register(Timer timer, bool accumulate) {
        timer.Stop();
        if (accumulate) {
          var elapsedMs = timer.ElapsedInMilliseconds;
          _totalElapsedMs += elapsedMs;
          _sumOfSquaredElapsedMs += elapsedMs * elapsedMs;
          _sampleCount++;
        
          if (timer.ElapsedInMilliseconds > _longestElapsedMs) {
            _longestElapsedMs = timer.ElapsedInMilliseconds;
          }
        } else {
          var elapsedMs = timer.ElapsedInMilliseconds;
          _totalElapsedMs = elapsedMs;
          _sumOfSquaredElapsedMs = elapsedMs * elapsedMs;
          _sampleCount = 1;
          _longestElapsedMs = elapsedMs;
        }
      }
    }
    
    private string path = "/LagCompensationBenchmark";

    private Dictionary<string, BenchmarkData> _benchmarkData = new Dictionary<string, BenchmarkData>();

    public Benchmark() {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void NewTimer(string name) {
      if (_benchmarkData.ContainsKey(name) == false) {
        BenchmarkData newData = new BenchmarkData();
        _benchmarkData.Add(name, newData);
      }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RegisterTimer(string name, Timer timer, bool accumulate) {
      if (_benchmarkData.TryGetValue(name, out var data)) {
        data.Register(timer, accumulate);
      }
    }

    public Dictionary<string, BenchmarkData> GetData() {
      return _benchmarkData;
    }

    //[Conditional("LAG_COMP_BENCHMARK")]
    public void WriteBenchmarkResults() {
      foreach (var data in _benchmarkData) {
        Log.Debug($"{data.Key} Average : {data.Value.AverageElapsedMs}ms | Total : {data.Value.TotalMs}ms | Samples : {data.Value.SampleCount} | Longest : {data.Value.LongestElapsedMs}ms");
      }
    }

    private string ResolveFilePath(string name) {
      if (!Directory.Exists(path)) {
        Directory.CreateDirectory(path);
      }
      return Path.Combine(path, name);
    }

    private void WriteData(string data, string fileName) {
      var errorPath = ResolveFilePath($"{fileName}.txt");
      using (StreamWriter writer = File.AppendText(errorPath)) {
        writer.WriteLine(data);
      }
    }
  }
}
#endif