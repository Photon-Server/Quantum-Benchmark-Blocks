// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial
// declarations in another file.
// </auto-generated>
#pragma warning disable 0109
#pragma warning disable 1591


namespace Quantum {
  using UnityEngine;
  
  [UnityEngine.DisallowMultipleComponent()]
  public unsafe partial class QPrototypeComponentTest097 : QuantumUnityComponentPrototype<Quantum.Prototypes.ComponentTest097Prototype>, IQuantumUnityPrototypeWrapperForComponent<Quantum.ComponentTest097> {
    [DrawInline()]
    [ReadOnly(InEditMode = false)]
    public Quantum.Prototypes.ComponentTest097Prototype Prototype;
    public override System.Type ComponentType {
      get {
        return typeof(Quantum.ComponentTest097);
      }
    }
    public override ComponentPrototype CreatePrototype(Quantum.QuantumEntityPrototypeConverter converter) {
      return Prototype;
    }
  }
}
#pragma warning restore 0109
#pragma warning restore 1591
