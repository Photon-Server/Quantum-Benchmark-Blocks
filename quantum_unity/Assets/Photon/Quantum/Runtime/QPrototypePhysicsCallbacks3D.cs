// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial  
// declarations in another file.
// </auto-generated>
namespace Quantum {
  using UnityEngine;


  [UnityEngine.DisallowMultipleComponent]
  public partial class QPrototypePhysicsCallbacks3D : Quantum.QuantumUnityComponentPrototype<Quantum.Prototypes.PhysicsCallbacks3DPrototype>, 
    IQuantumUnityPrototypeWrapperForComponent<Quantum.PhysicsCallbacks3D> {
    
    [DrawInline, ReadOnly(InEditMode = false)]
    public Quantum.Prototypes.PhysicsCallbacks3DPrototype Prototype = new();
    
    public override System.Type ComponentType => typeof(Quantum.PhysicsCallbacks3D);
    
    public override Quantum.ComponentPrototype CreatePrototype(Quantum.QuantumEntityPrototypeConverter converter) => base.ConvertPrototype(converter, Prototype);
  }
}