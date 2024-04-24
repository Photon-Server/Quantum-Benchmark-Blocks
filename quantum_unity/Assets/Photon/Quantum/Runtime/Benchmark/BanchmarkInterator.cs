namespace Quantum {
  using Photon.Deterministic;
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.IO;
  using ComponentAlwaysAdded = Transform3D;
  
  using WithoutInFirstBlock = ComponentTest040;
  using AnyInFirstBlock = ComponentTest041;


    /*
   using ComponentWithInMiddleBlock = ComponentTest250;
   using ComponentWithInLastBlock = ComponentTest510;

   using WithoutInMiddleBlock = ComponentTest251;
   using WithoutInLastBlock = ComponentTest511;

   using AnyInMiddleBlock = ComponentTest252;
   using AnyInLastBlock = ComponentTest512;
    */

   using ComponentWithInMiddleBlock = ComponentTest100;
   using ComponentWithInLastBlock = ComponentTest200;
  
   using WithoutInMiddleBlock = ComponentTest101;
   using WithoutInLastBlock = ComponentTest201;

   using AnyInMiddleBlock = ComponentTest102;
   using AnyInLastBlock = ComponentTest202;

  
  public unsafe class BanchmarkInterator : SystemMainThread {

    Benchmark benchmark = new Benchmark();

    ComponentSet WithoutSet;
    ComponentSet AnySet;
    
    public override void OnInit(Frame f) {
      var singleton = f.GetOrAddSingleton<BenchmarkSingleton>();
      WithoutSet = ComponentSet.Create<WithoutInFirstBlock, WithoutInMiddleBlock, WithoutInLastBlock>();
      AnySet = ComponentSet.Create<AnyInFirstBlock, AnyInMiddleBlock, AnyInLastBlock>();
    }

    public void CreateEntity(Frame f, int ammount) {
      int createdWithAmount = 0;
      while (createdWithAmount < ammount) {
        var entity = f.Create();
        f.Add<ComponentAlwaysAdded>(entity);
        if (f.RNG->Next() > FP._0_33) {
          createdWithAmount++;
          f.Add<ComponentWithInMiddleBlock>(entity);
          f.Add<ComponentWithInLastBlock>(entity);

          if (f.RNG->Next() > FP._0_50) {
            f.Add<WithoutInLastBlock>(entity);
          } else if (f.RNG->Next() > FP._0_50) {
            f.Add<AnyInLastBlock>(entity);
          }
        }
      }
    }

    public void ShuffleEntities(Frame f, FP percent) {
      int destroyedEntities = 0;
      foreach (var pair in f.Unsafe.GetComponentBlockIterator<ComponentWithInMiddleBlock>()){
        if(f.RNG->Next() <= percent){
          destroyedEntities++;
          f.Destroy(pair.Entity);
        }
      }
      f.Unsafe.CommitAllCommands();
      CreateEntity(f, destroyedEntities);
    }

    public void EvaluateResults(Frame f) {
      var singleton = f.Unsafe.GetPointerSingleton<BenchmarkSingleton>();
      string header = "entities;average ms;total ms;samples;longest ms; std dev\n";
      string dataText = "";
      var benchData = benchmark.GetData();
        
      foreach(var data in benchData) {
        dataText += ($"{data.Key};" +
          $"{data.Value.AverageElapsedMs.ToString("0.0000",culture)};" +
          $"{data.Value.TotalMs.ToString("0.0000",culture)};" +
          $"{data.Value.SampleCount.ToString("0",culture)};" +
          $"{data.Value.LongestElapsedMs.ToString("0.0000",culture)};" +
          $"{data.Value.StdDevMs.ToString("0.0000",culture)}\n");
      }
      f.Events.WriteLog($"\nLog document saved at: {singleton->path}");
      string name = DateTime.Now.ToString("yyyyMMdd_HHmmss_") + singleton->filename;
      string path = Path.Combine(singleton->path, name);
      File.WriteAllText(path, header + dataText);
    }

    CultureInfo culture = CultureInfo.InvariantCulture;

    public override void Update(Frame f) {

      var i = f.GetPlayerInput(0);
      var singleton = f.Unsafe.GetPointerSingleton<BenchmarkSingleton>();
      bool shouldLogFilteredCount = false;

      //When click to start
      if(i->runnig == true && singleton->running == false) {
        shouldLogFilteredCount = true;
        singleton->running = true;
        singleton->startedFrame = f.Number;
        CreateEntity(f, i->grown);
      }

      //When click to stop
      if(i->runnig == false && singleton->running == true){
        singleton->running = false;
        benchmark = new Benchmark();
        List<EntityRef> result = new List<EntityRef>();
        f.GetAllEntityRefs(result);
        for(int index = 1; index < result.Count; index++) {
          f.Destroy(result[index]);
        }
      }

      if(i->runnig == false) return;

      //FIXED NUMBER OF ENTITIES TEST
      int SAMPLES = i->samples;
      int currentFrame = (f.Number - singleton->startedFrame);
      int numberOfEntities = i->grown;

      if(currentFrame > SAMPLES * 2) return;

      if(currentFrame == SAMPLES * 2) {
        Log.Debug("Finish Test at frame: " + currentFrame);
        f.Events.Log("\nFinish Test at frame: " + currentFrame);
        EvaluateResults(f);
        return;
      }

      //SHUFFLE
      if(currentFrame == SAMPLES) {
        Log.Debug("Shuffling at frame: " + currentFrame);
        f.Events.Log("\nShuffling at frame: " + currentFrame);

        FP percent = FP._0_20;// shuffle 20%
        FP n = FP._1 / percent;
        for(int interations = 0; interations < n; interations++) {
          ShuffleEntities(f, FP._0_20);
        }

        shouldLogFilteredCount = true;
      }
      
      //CREATE FILTERS
      var With_Filter = f.Filter<ComponentAlwaysAdded, ComponentWithInMiddleBlock, ComponentWithInLastBlock>();
      var With_Without_Filter = f.Filter<ComponentAlwaysAdded, ComponentWithInMiddleBlock, ComponentWithInLastBlock>(without: WithoutSet);
      var With_Without_Any_Filter = f.Filter<ComponentAlwaysAdded, ComponentWithInMiddleBlock, ComponentWithInLastBlock>(without: WithoutSet, any: AnySet);

      Timer timer = new Timer();
      string testName = "";
      int count = 0;
      string isShuffle = "";
      if(currentFrame >= SAMPLES) isShuffle = "shuffled_";

      //TEST 1 - WITH
      timer.Reset();
      testName = isShuffle + "with_" + numberOfEntities + "_entities";
      count = 0;
      benchmark.NewTimer(testName);
      timer.Start();
      while (With_Filter.Next(out var e, out var a, out var b, out var c)) {
        count++;
      }
      timer.Stop();
      if (shouldLogFilteredCount) {
        Log.Info($"TEST 1 - WITH filtered: {count} entities");
      }
      benchmark.RegisterTimer(testName, timer, true);

      //TEST 2 - WITH, WITHOUT
      timer.Reset();
      testName = isShuffle + "with_without_" + numberOfEntities + "_entities";
      count = 0;
      benchmark.NewTimer(testName);
      timer.Start();
      while (With_Without_Filter.Next(out var e, out var a, out var b, out var c)) {
        count++;
      }
      timer.Stop();
      if (shouldLogFilteredCount) {
        Log.Info($"TEST 2 - WITH, WITHOUT filtered: {count} entities");
      }
      benchmark.RegisterTimer(testName, timer, true);

      //TEST 3 - WITH, WITHOUT, ANY
      timer.Reset();
      testName = isShuffle + "with_without_any_" + numberOfEntities + "_entities";
      count = 0;
      benchmark.NewTimer(testName);
      timer.Start();
      while (With_Without_Any_Filter.Next(out var e, out var a, out var b, out var c)) {
        count++;
      }
      timer.Stop();
      if (shouldLogFilteredCount) {
        Log.Info($"TEST 3 - WITH, WITHOUT, ANY filtered: {count} entities");
      }
      benchmark.RegisterTimer(testName, timer, true);
    }
  }
}
