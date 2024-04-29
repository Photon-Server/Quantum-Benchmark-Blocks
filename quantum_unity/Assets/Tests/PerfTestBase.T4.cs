

namespace Tests {
  using NUnit.Framework;
  using Photon.Deterministic;
  using Quantum;
  using UnityEngine;
  using System;
  using Assert = NUnit.Framework.Assert;

  using ComponentAlwaysAdded = Quantum.Transform3D;

  partial class PerfTestBase {
    public static Func<Frame, int> MakeSimpleFilter<T0>() where T0 : unmanaged, IComponent {
      return frame => {
        int count  = 0;
        var filter = frame.Filter<T0>();
        while (filter.Next(out EntityRef e, out T0 t0)) {
          count++;
        }
        return count;
      };
    }
    public static Func<Frame, int> MakeSimpleFilter<T0, T1>() where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent {
      return frame => {
        int count  = 0;
        var filter = frame.Filter<T0, T1>();
        while (filter.Next(out EntityRef e, out T0 t0, out T1 t1)) {
          count++;
        }
        return count;
      };
    }
    public static Func<Frame, int> MakeSimpleFilter<T0, T1, T2>() where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent {
      return frame => {
        int count  = 0;
        var filter = frame.Filter<T0, T1, T2>();
        while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2)) {
          count++;
        }
        return count;
      };
    }
    public static Func<Frame, int> MakeSimpleFilter<T0, T1, T2, T3>() where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent {
      return frame => {
        int count  = 0;
        var filter = frame.Filter<T0, T1, T2, T3>();
        while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3)) {
          count++;
        }
        return count;
      };
    }
    public static Func<Frame, int> MakeSimpleFilter<T0, T1, T2, T3, T4>() where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent {
      return frame => {
        int count  = 0;
        var filter = frame.Filter<T0, T1, T2, T3, T4>();
        while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4)) {
          count++;
        }
        return count;
      };
    }
    public static Func<Frame, int> MakeSimpleFilter<T0, T1, T2, T3, T4, T5>() where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent where T5 : unmanaged, IComponent {
      return frame => {
        int count  = 0;
        var filter = frame.Filter<T0, T1, T2, T3, T4, T5>();
        while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5)) {
          count++;
        }
        return count;
      };
    }
    public static Func<Frame, int> MakeSimpleFilter<T0, T1, T2, T3, T4, T5, T6>() where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent where T5 : unmanaged, IComponent where T6 : unmanaged, IComponent {
      return frame => {
        int count  = 0;
        var filter = frame.Filter<T0, T1, T2, T3, T4, T5, T6>();
        while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6)) {
          count++;
        }
        return count;
      };
    }
    public static Func<Frame, int> MakeSimpleFilter<T0, T1, T2, T3, T4, T5, T6, T7>() where T0 : unmanaged, IComponent where T1 : unmanaged, IComponent where T2 : unmanaged, IComponent where T3 : unmanaged, IComponent where T4 : unmanaged, IComponent where T5 : unmanaged, IComponent where T6 : unmanaged, IComponent where T7 : unmanaged, IComponent {
      return frame => {
        int count  = 0;
        var filter = frame.Filter<T0, T1, T2, T3, T4, T5, T6, T7>();
        while (filter.Next(out EntityRef e, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7)) {
          count++;
        }
        return count;
      };
    }
  }
}