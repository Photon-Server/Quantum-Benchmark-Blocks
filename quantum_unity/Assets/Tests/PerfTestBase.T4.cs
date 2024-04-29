

namespace Tests {
  using NUnit.Framework;
  using Photon.Deterministic;
  using Quantum;
  using UnityEngine;
  using Assert = NUnit.Framework.Assert;

  using ComponentAlwaysAdded = Quantum.Transform3D;

  partial class PerfTestBase {
    public void NoShuffle<T0>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0>();
          while (filter.Next(out EntityRef e, out T0 t0)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs); 
        }
      );
    }

    public void WithShuffle<T0>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0>();
          while (filter.Next(out EntityRef e, out T0 t0)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, 
        oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          for (int i = 0; i < 5; i++) {
            int count = DestroyEntities<ComponentAlwaysAdded>(f, FP._0_20);
            CreateEntities(f, count, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          }
        }
      );
    }
    public void NoShuffle<T0, T1>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs); 
        }
      );
    }

    public void WithShuffle<T0, T1>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, 
        oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          for (int i = 0; i < 5; i++) {
            int count = DestroyEntities<ComponentAlwaysAdded>(f, FP._0_20);
            CreateEntities(f, count, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          }
        }
      );
    }
    public void NoShuffle<T0, T1, T2>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs); 
        }
      );
    }

    public void WithShuffle<T0, T1, T2>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, 
        oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          for (int i = 0; i < 5; i++) {
            int count = DestroyEntities<ComponentAlwaysAdded>(f, FP._0_20);
            CreateEntities(f, count, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          }
        }
      );
    }
    public void NoShuffle<T0, T1, T2, T3>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs); 
        }
      );
    }

    public void WithShuffle<T0, T1, T2, T3>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, 
        oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          for (int i = 0; i < 5; i++) {
            int count = DestroyEntities<ComponentAlwaysAdded>(f, FP._0_20);
            CreateEntities(f, count, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          }
        }
      );
    }
    public void NoShuffle<T0, T1, T2, T3, T4>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3, T4>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs); 
        }
      );
    }

    public void WithShuffle<T0, T1, T2, T3, T4>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3, T4>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, 
        oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          for (int i = 0; i < 5; i++) {
            int count = DestroyEntities<ComponentAlwaysAdded>(f, FP._0_20);
            CreateEntities(f, count, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          }
        }
      );
    }
    public void NoShuffle<T0, T1, T2, T3, T4, T5>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent where T5 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3, T4, T5>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs); 
        }
      );
    }

    public void WithShuffle<T0, T1, T2, T3, T4, T5>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent where T5 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3, T4, T5>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, 
        oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          for (int i = 0; i < 5; i++) {
            int count = DestroyEntities<ComponentAlwaysAdded>(f, FP._0_20);
            CreateEntities(f, count, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          }
        }
      );
    }
    public void NoShuffle<T0, T1, T2, T3, T4, T5, T6>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent where T5 : unmanaged, IComponent where T6 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3, T4, T5, T6>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs); 
        }
      );
    }

    public void WithShuffle<T0, T1, T2, T3, T4, T5, T6>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent where T5 : unmanaged, IComponent where T6 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3, T4, T5, T6>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, 
        oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          for (int i = 0; i < 5; i++) {
            int count = DestroyEntities<ComponentAlwaysAdded>(f, FP._0_20);
            CreateEntities(f, count, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          }
        }
      );
    }
    public void NoShuffle<T0, T1, T2, T3, T4, T5, T6, T7>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent where T5 : unmanaged, IComponent where T6 : unmanaged, IComponent where T7 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3, T4, T5, T6, T7>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs); 
        }
      );
    }

    public void WithShuffle<T0, T1, T2, T3, T4, T5, T6, T7>(params ComponentSpec[] specs) where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent where T5 : unmanaged, IComponent where T6 : unmanaged, IComponent where T7 : unmanaged, IComponent {
      RunTest(frame => {
          int count  = 0;
          var filter = frame.Filter<T0, T1, T2, T3, T4, T5, T6, T7>();
          while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7)) {
            count++;
          }
          Assert.NotZero(count);
          return count;
        }, 
        oneTimeSetUp: f => { 
          CreateEntities(f, DefaultEntityCount, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          for (int i = 0; i < 5; i++) {
            int count = DestroyEntities<ComponentAlwaysAdded>(f, FP._0_20);
            CreateEntities(f, count, alwaysAdd: typeof(ComponentAlwaysAdded), specs);
          }
        }
      );
    }
  }
}