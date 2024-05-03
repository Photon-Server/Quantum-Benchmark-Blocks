namespace Quantum {
  using System;

  public class DelegatingSystemBase<T> : SystemMainThread {
    public static Action<Frame> _Update;
    public static Action<Frame> _OnInit;

    public override void Update(Frame f) {
      _Update?.Invoke(f);
    }

    public override void OnInit(Frame f) {
      _OnInit?.Invoke(f);
    }
  }

  // public class PreDelegatingSystem : DelegatingSystemBase<PreDelegatingSystem> {
  // }
  //
  // public class PostDelegatingSystem : DelegatingSystemBase<PostDelegatingSystem> {
  // }

  public class DelegatingSystem : DelegatingSystemBase<DelegatingSystem> {
  }
}