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
  public unsafe partial class QPrototypeComponentTest254 : QuantumUnityComponentPrototype<Quantum.Prototypes.ComponentTest254Prototype>, IQuantumUnityPrototypeWrapperForComponent<Quantum.ComponentTest254> {
    [DrawInline()]
    [ReadOnly(InEditMode = false)]
    public Quantum.Prototypes.ComponentTest254Prototype Prototype;
    public override System.Type ComponentType {
      get {
        return typeof(Quantum.ComponentTest254);
      }
    }
    public override ComponentPrototype CreatePrototype(Quantum.QuantumEntityPrototypeConverter converter) {
      return Prototype;
    }
  }
}
#pragma warning restore 0109
#pragma warning restore 1591
