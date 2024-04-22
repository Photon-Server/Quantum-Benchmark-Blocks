#if !QUANTUM_DEV

#region Assets/Photon/Quantum/Simulation/AssemblyAttributes.cs

[assembly:Quantum.QuantumSimulationAssemblyAttribute()]

#endregion


#region Assets/Photon/Quantum/Simulation/Core/CodeGenStub.cs

namespace Quantum {
  using UnityEngine;

  public unsafe partial struct _globals_ {
    
    static partial void SerializeCodeGen(void* ptr, FrameSerializer serializer);
    partial void ClearPointersPartial(Quantum.Core.FrameBase f, EntityRef entity);
    partial void AllocatePointersPartial(Quantum.Core.FrameBase f, EntityRef entity);
    
    public static void Serialize(void* ptr, FrameSerializer serializer) {
      SerializeCodeGen(ptr, serializer);
    }
    
    public void ClearPointers(Quantum.Core.FrameBase f, EntityRef entity) {
      ClearPointersPartial(f, entity);
    }

    public void AllocatePointers(Quantum.Core.FrameBase f, EntityRef entity) {
      AllocatePointersPartial(f, entity);
    }
  }

  public unsafe partial struct Input {

    static partial void SerializeCodeGen(void* ptr, FrameSerializer serializer);
    static partial void GetMaxCountCodeGen(ref int maxCount);
    
    public static Input Read(FrameSerializer serializer) {
      Input i = new Input();
      Serialize(&i, serializer);
      return i;
    }

    public static void Write(FrameSerializer serializer, Input i) {
      Serialize(&i, serializer);
    }
    
    public static void Serialize(void* input, FrameSerializer serializer) {
      SerializeCodeGen(input, serializer);
    }

    public static int MAX_COUNT => MaxCount;

    public static int MaxCount {
      get {
        int result = 0;
        GetMaxCountCodeGen(ref result);
        return result;
      }
    }
  }

  public unsafe partial class Frame {
    partial void GetPlayerLastConnectionStateCodeGen(ref BitSetRef bitSet);
    partial void SetPlayerInputCodeGen(PlayerRef player, Input input);
    partial void ResetPhysicsCodeGen();
    
    public void SetPlayerInput(PlayerRef player, Input input) {
      SetPlayerInputCodeGen(player, input);
    }

    partial struct FrameEvents {
      
      static partial void GetParentEventIDCodeGen(int eventID, ref int parentEventID);
      static partial void GetEventTypeCountCodeGen(ref int eventCount);
      static partial void GetEventTypeCodeGen(int eventID, ref System.Type eventType);
      
      public static int GetParentEventID(int eventID) {
        int result = -1;
        GetParentEventIDCodeGen(eventID, ref result);
        return result;
      }

      public static int EVENT_TYPE_COUNT => EventTypeCount; 
      
      public static int EventTypeCount {
        get {
          int result = -1;
          GetEventTypeCountCodeGen(ref result);
          return result;
        }
      }
      
      public static System.Type GetEventType(int eventID) {
        System.Type result = null;
        GetEventTypeCodeGen(eventID, ref result);
        if (result == null) {
          throw new System.ArgumentOutOfRangeException(nameof(eventID));
        }
        return result;
      }
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Core/Collision.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;

  /// <summary>
  /// Interface for receiving callbacks once per frame while two non-trigger 2D colliders are touching.
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics2D.PhysicsEngine2D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics2dApi
  public interface ISignalOnCollision2D : ISignal {
    /// <summary>
    /// Called once per frame while two non-trigger 2D colliders are touching.
    /// </summary>
    /// <param name="f">The frame in which the collision happened.</param>
    /// <param name="info">The <see cref="CollisionInfo2D"/> with data about the collision.</param>
    /// \ingroup Physics2dApi
    void OnCollision2D(Frame f, CollisionInfo2D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once two non-trigger 2D colliders start touching.
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics2D.PhysicsEngine2D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics2dApi
  public interface ISignalOnCollisionEnter2D : ISignal {
    /// <summary>
    /// Called once two non-trigger 2D colliders start touching.
    /// </summary>
    /// <param name="f">The frame in which the collision happened.</param>
    /// <param name="info">The <see cref="CollisionInfo2D"/> with data about the collision.</param>
    /// \ingroup Physics2dApi
    void OnCollisionEnter2D(Frame f, CollisionInfo2D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once two non-trigger 2D colliders stop touching.
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics2D.PhysicsEngine2D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics2dApi
  public interface ISignalOnCollisionExit2D : ISignal {
    /// <summary>
    /// Called once two non-trigger 2D colliders stop touching.
    /// </summary>
    /// <param name="f">The frame in which the entities stopped touching.</param>
    /// <param name="info">The <see cref="ExitInfo2D"/> with the entities that were touching.</param>
    /// \ingroup Physics2dApi
    void OnCollisionExit2D(Frame f, ExitInfo2D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once per frame while a non-trigger and a trigger 2D colliders are touching.
  /// <remarks>No collision is checked between two kinematic colliders that are both trigger or both non-trigger.</remarks>
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics2D.PhysicsEngine2D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics2dApi
  public interface ISignalOnTrigger2D : ISignal {
    /// <summary>
    /// Called once per frame while a non-trigger and a trigger 2D colliders are touching.
    /// </summary>
    /// <param name="f">The frame in which the collision happened.</param>
    /// <param name="info">The <see cref="TriggerInfo2D"/> with data about the trigger collision.</param>
    /// \ingroup Physics2dApi
    void OnTrigger2D(Frame f, TriggerInfo2D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once a non-trigger and a trigger 2D colliders start touching.
  /// <remarks>No collision is checked between two kinematic colliders that are both trigger or both non-trigger.</remarks>
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics2D.PhysicsEngine2D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics2dApi
  public interface ISignalOnTriggerEnter2D : ISignal {
    /// <summary>
    /// Called once a non-trigger and a trigger 2D colliders start touching.
    /// </summary>
    /// <param name="f">The frame in which the collision happened.</param>
    /// <param name="info">The <see cref="TriggerInfo2D"/> with data about the trigger collision.</param>
    /// \ingroup Physics2dApi
    void OnTriggerEnter2D(Frame f, TriggerInfo2D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once a non-trigger and a trigger 2D colliders stop touching.
  /// <remarks>No collision is checked between two kinematic colliders that are both trigger or both non-trigger.</remarks>
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics2D.PhysicsEngine2D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics2dApi
  public interface ISignalOnTriggerExit2D : ISignal {
    /// <summary>
    /// Called once a non-trigger and a trigger 2D colliders stop touching.
    /// </summary>
    /// <param name="f">The frame in which the entities stopped touching.</param>
    /// <param name="info">The <see cref="ExitInfo2D"/> with the entities that were touching.</param>
    /// \ingroup Physics2dApi
    void OnTriggerExit2D(Frame f, ExitInfo2D info);
  }
  
  /// <summary>
  /// Interface for receiving callbacks once per frame while two non-trigger 3D colliders are touching.
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics3D.PhysicsEngine3D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics3dApi
  public interface ISignalOnCollision3D : ISignal {
    /// <summary>
    /// Called once per frame while two non-trigger 3D colliders are touching.
    /// </summary>
    /// <param name="f">The frame in which the collision happened.</param>
    /// <param name="info">The <see cref="CollisionInfo3D"/> with data about the collision.</param>
    /// \ingroup Physics3dApi
    void OnCollision3D(Frame f, CollisionInfo3D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once two non-trigger 3D colliders start touching.
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics3D.PhysicsEngine3D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics3dApi
  public interface ISignalOnCollisionEnter3D : ISignal {
    /// <summary>
    /// Called once two non-trigger 3D colliders start touching.
    /// </summary>
    /// <param name="f">The frame in which the collision happened.</param>
    /// <param name="info">The <see cref="CollisionInfo3D"/> with data about the collision.</param>
    /// \ingroup Physics3dApi
    void OnCollisionEnter3D(Frame f, CollisionInfo3D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once two non-trigger 3D colliders stop touching.
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics3D.PhysicsEngine3D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics3dApi
  public interface ISignalOnCollisionExit3D : ISignal {
    /// <summary>
    /// Called once two non-trigger 3D colliders stop touching.
    /// </summary>
    /// <param name="f">The frame in which the entities stopped touching.</param>
    /// <param name="info">The <see cref="ExitInfo3D"/> with the entities that were touching.</param>
    /// \ingroup Physics3dApi
    void OnCollisionExit3D(Frame f, ExitInfo3D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once per frame while a non-trigger and a trigger 3D colliders are touching.
  /// <remarks>No collision is checked between two kinematic colliders that are both trigger or both non-trigger.</remarks>
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics3D.PhysicsEngine3D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics3dApi
  public interface ISignalOnTrigger3D : ISignal {
    /// <summary>
    /// Called once per frame while a non-trigger and a trigger 3D colliders are touching.
    /// </summary>
    /// <param name="f">The frame in which the collision happened.</param>
    /// <param name="info">The <see cref="TriggerInfo3D"/> with data about the trigger collision.</param>
    /// \ingroup Physics3dApi
    void OnTrigger3D(Frame f, TriggerInfo3D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once a non-trigger and a trigger 3D colliders start touching.
  /// <remarks>No collision is checked between two kinematic colliders that are both trigger or both non-trigger.</remarks>
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics3D.PhysicsEngine3D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics3dApi
  public interface ISignalOnTriggerEnter3D : ISignal {
    /// <summary>
    /// Called once a non-trigger and a trigger 3D colliders start touching.
    /// </summary>
    /// <param name="f">The frame in which the collision happened.</param>
    /// <param name="info">The <see cref="TriggerInfo3D"/> with data about the trigger collision.</param>
    /// \ingroup Physics3dApi
    void OnTriggerEnter3D(Frame f, TriggerInfo3D info);
  }

  /// <summary>
  /// Interface for receiving callbacks once a non-trigger and a trigger 3D colliders stop touching.
  /// <remarks>No collision is checked between two kinematic colliders that are both trigger or both non-trigger.</remarks>
  /// <remarks>At least one of the entities involved in a collision must have the respective <see cref="CallbackFlags"/> set for the callback to be called.</remarks>
  /// <remarks>See <see cref="Quantum.Physics3D.PhysicsEngine3D.Api.SetCallbacks"/> for setting the callbacks flags to an entity.</remarks>
  /// </summary>
  /// \ingroup Physics3dApi
  public interface ISignalOnTriggerExit3D : ISignal {
    /// <summary>
    /// Called once a non-trigger and a trigger 3D colliders stop touching.
    /// </summary>
    /// <param name="f">The frame in which the entities stopped touching.</param>
    /// <param name="info">The <see cref="ExitInfo3D"/> with the entities that were touching.</param>
    /// \ingroup Physics3dApi
    void OnTriggerExit3D(Frame f, ExitInfo3D info);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Core/CommandSetup.cs

namespace Quantum {
  using System.Collections.Generic;
  using Photon.Deterministic;

  public static partial class DeterministicCommandSetup {
    public static IDeterministicCommandFactory[] GetCommandFactories(RuntimeConfig gameConfig, SimulationConfig simulationConfig) {
      var factories = new List<IDeterministicCommandFactory>() {
        // pre-defined core commands
        Core.DebugCommand.CreateCommand(),
        new DeterministicCommandPool<Core.CompoundCommand>(),
      };

      AddCommandFactoriesUser(factories, gameConfig, simulationConfig);

      return factories.ToArray();
    }

    static partial void AddCommandFactoriesUser(ICollection<IDeterministicCommandFactory> factories, RuntimeConfig gameConfig, SimulationConfig simulationConfig);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Core/Frame.cs

namespace Quantum {
  using Photon.Deterministic;
  using Quantum.Core;
  using Quantum.Profiling;
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.IO;
  using System.Linq;
  using System.Runtime.CompilerServices;

  /// <summary>
  /// The user implementation of <see cref="FrameBase"/> that resides in the project quantum_state and has access to all user relevant classes.
  /// </summary>
  /// \ingroup FrameClass
  public unsafe partial class Frame : Core.FrameBase {

    public const int DumpFlag_NoSimulationConfig           = 1 << 1;
    public const int DumpFlag_NoRuntimeConfig              = 1 << 3;
    public const int DumpFlag_NoDeterministicSessionConfig = 1 << 4;
    public const int DumpFlag_NoRuntimePlayers             = 1 << 5;
    public const int DumpFlag_NoDynamicDB                  = 1 << 6;
    public const int DumpFlag_ReadableDynamicDB            = 1 << 7;
    public const int DumpFlag_PrintRawValues               = 1 << 8;
    public const int DumpFlag_ComponentChecksums           = 1 << 9;
    public const int DumpFlag_AssetDBCheckums              = 1 << 10;
    public const int DumpFlag_NoIsVerified                 = 1 << 11;

    struct RuntimePlayerData {
      public Int32         ActorId;
      public Int32         PlayerSlot;
      [Obsolete("Will not be set anymore")]
      public Byte[]        Data => null;
      public RuntimePlayer Player;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    _globals_* _globals;

    // configs
    RuntimeConfig              _runtimeConfig;
    SimulationConfig           _simulationConfig;
    DeterministicSessionConfig _sessionConfig;

    // systems
    SystemBase[]            _systemsAll;
    SystemBase[]            _systemsRoots;
    Dictionary<Type, Int32> _systemIndexByType;

    // player data
    PersistentMap<Int32, RuntimePlayerData> _playerData;

#pragma warning disable CS0618 // Type or member is obsolete
    ISignalOnPlayerDataSet[] _ISignalOnPlayerDataSet;
#pragma warning restore CS0618 // Type or member is obsolete
    ISignalOnPlayerAdded[] _ISignalOnPlayerAdded;
    ISignalOnPlayerRemoved[] _ISignalOnPlayerRemoved;

    // 2D Physics collision signals
    ISignalOnCollision2D[]      _ISignalOnCollision2DSystems;
    ISignalOnCollisionEnter2D[] _ISignalOnCollisionEnter2DSystems;
    ISignalOnCollisionExit2D[]  _ISignalOnCollisionExit2DSystems;

    // 2D Physics trigger signals
    ISignalOnTrigger2D[]      _ISignalOnTrigger2DSystems;
    ISignalOnTriggerEnter2D[] _ISignalOnTriggerEnter2DSystems;
    ISignalOnTriggerExit2D[]  _ISignalOnTriggerExit2DSystems;

    // 3D Physics collision signals
    ISignalOnCollision3D[]      _ISignalOnCollision3DSystems;
    ISignalOnCollisionEnter3D[] _ISignalOnCollisionEnter3DSystems;
    ISignalOnCollisionExit3D[]  _ISignalOnCollisionExit3DSystems;

    // 3D Physics trigger signals
    ISignalOnTrigger3D[]      _ISignalOnTrigger3DSystems;
    ISignalOnTriggerEnter3D[] _ISignalOnTriggerEnter3DSystems;
    ISignalOnTriggerExit3D[]  _ISignalOnTriggerExit3DSystems;

    ISignalOnNavMeshWaypointReached[] _ISignalOnNavMeshWaypointReachedSystems;
    ISignalOnNavMeshSearchFailed[]    _ISignalOnNavMeshSearchFailedSystems;
    ISignalOnNavMeshMoveAgent[]       _ISignalOnNavMeshMoveAgentSystems;

    ISignalOnMapChanged[]                   _ISignalOnMapChangedSystems;
    ISignalOnEntityPrototypeMaterialized[]  _ISignalOnEntityPrototypeMaterializedSystems;

    ISignalOnPlayerConnected[]    _ISignalOnPlayerConnectedSystems;
    ISignalOnPlayerDisconnected[] _ISignalOnPlayerDisconnectedSystems;

    /// <summary>
    /// Access the global struct with generated values from the DSL.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public _globals_* Global { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _globals;  }

    /// <summary>
    /// Whether this is safe is verified at the codegen stage.
    /// </summary>
    private GlobalsCore* GlobalsCore { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (GlobalsCore*)_globals; }

    /// <summary>
    /// The randomization session started with the seed from the <see cref="RuntimeConfig"/> used to start the simulation with.
    /// </summary>
    /// <para>Supports determinism under roll-backs.</para>
    /// <para>If random is used in conjunction with the prediction area feature the session needs to be stored on the entities themselves.</para>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public RNGSession* RNG { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => &GlobalsCore->RngSession; } 

    /// <summary>
    /// Returns the max player count that the simulation was started with <see cref="DeterministicSessionConfig.PlayerCount"/>.
    /// </summary>
    public Int32 PlayerCount { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _sessionConfig.PlayerCount; }

    /// <summary>
    /// Returns the number of players that are currently connected, requires the <see cref="PlayerConnectedSystem"/>.
    /// </summary>
    public Int32 PlayerConnectedCount { 
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => GlobalsCore->PlayerConnectedCount; 
      internal set => GlobalsCore->PlayerConnectedCount = value;
    }

    /// <summary>
    /// Returns the global navmesh region mask that controls toggling on/off regions.
    /// </summary>
    public override NavMeshRegionMask* NavMeshRegionMask { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => &GlobalsCore->NavMeshRegions; } 

    /// <summary>
    /// Returns the frame meta data.
    /// </summary>
    public override FrameMetaData* FrameMetaData { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => &GlobalsCore->FrameMetaData; } 

    /// <summary>
    /// Returns the physics 2D engine state.
    /// </summary>
    protected override PhysicsEngineState* _physicsState2D { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => &GlobalsCore->PhysicsState2D; } 

    /// <summary>
    /// Returns the physics 3d engine state.
    /// </summary>
    protected override PhysicsEngineState* _physicsState3D { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => &GlobalsCore->PhysicsState3D; }

    /// <summary>
    /// Returns the mode that commands are commited to the simulation.
    /// </summary>
    public override CommitCommandsModes CommitCommandsMode { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => SimulationConfig.Entities.CommitCommandsMode; }

    /// <summary>
    /// Access the signal API.\n
    /// Signals are function signatures used as a decoupled inter-system communication API (a bit like a publisher/subscriber API or observer pattern).
    /// </summary>
    /// Custom signals are defined in the DSL.
    public FrameSignals Signals;

    /// <summary>
    /// Access the event API.\n
    /// Events are a fine-grained solution to communicate things that happen inside the simulation to the rendering engine (they should never be used to modify/update part of the game state).
    /// </summary>
    /// Custom events are defined in the DSL.
    public FrameEvents Events;
    
    /// <summary>
    /// The frame user context
    /// </summary>
    public new FrameContextUser Context { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (FrameContextUser)base.Context; }

    /// <summary>
    /// The <see cref="RuntimeConfig"/> used for this session.
    /// </summary>
    public RuntimeConfig RuntimeConfig {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => _runtimeConfig;
      internal set => _runtimeConfig = value;
    }

    /// <summary>
    /// The <see cref="SimulationConfig"/> used for this session.
    /// </summary>
    public SimulationConfig SimulationConfig {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => _simulationConfig;
      internal set => _simulationConfig = value;
    }

    /// <summary>
    /// The <see cref="DeterministicSessionConfig"/> used for this session.
    /// </summary>
    public DeterministicSessionConfig SessionConfig {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => _sessionConfig;
      internal set => _sessionConfig = value; 
    }

    /// <summary>
    /// All systems running in the session.
    /// </summary>
    public SystemBase[] SystemsAll { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _systemsAll; }

    /// <summary>
    /// See <see cref="DeterministicSession.SimulationRate"/>. This getter acquires the value from the <see cref="SessionConfig"/> though.
    /// </summary>
    public override int UpdateRate { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _sessionConfig.UpdateFPS; }

    /// <summary>
    /// Globally access the physics settings which are taken from the <see cref="SimulationConfig"/> during the Frame constructor.
    /// </summary>
    public sealed override PhysicsSceneSettings* PhysicsSceneSettings { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => &GlobalsCore->PhysicsSettings; }

    /// <summary>
    /// Delta time in seconds. Can be set during run-time.
    /// </summary>
    public override FP DeltaTime {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => GlobalsCore->DeltaTime;
      set => GlobalsCore->DeltaTime = value;
    }

    /// <summary>
    /// Retrieves the Quantum map asset. Can be set during run-time.
    /// </summary>
    /// If assigned value is different than the current one, signal <see cref="ISignalOnMapChanged"/> is raised.
    public override sealed Map Map {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => FindAsset<Map>(MapAssetRef);
      set => MapAssetRef = value;
    }

    public AssetRef<Map> MapAssetRef {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => GlobalsCore->Map;
      set {
        var previousValue = GlobalsCore->Map;
        if (previousValue.Id != value.Id) {
          GlobalsCore->Map = value;
          Signals.OnMapChanged(previousValue);
        }
      }
    }

    internal BitSetRef PlayerLastConnectionState {
      get {
        BitSetRef result = default;
        GetPlayerLastConnectionStateCodeGen(ref result);
        return result;
      }
    }

      public Frame(FrameContext context, SystemBase[] systemsAll, SystemBase[] systemsRoots, DeterministicSessionConfig sessionConfig, RuntimeConfig runtimeConfig, SimulationConfig simulationConfig, FP deltaTime) : base(context) {
      Assert.Check(context != null);

      _systemsAll   = systemsAll;
      _systemsRoots = systemsRoots;

      _runtimeConfig    = runtimeConfig;
      _simulationConfig = simulationConfig;
      _sessionConfig    = sessionConfig;

      _playerData = new PersistentMap<Int32, RuntimePlayerData>();

      AllocGen();
      InitStatic();
      InitGen();
      
      Events     = new FrameEvents(this);
      Signals    = new FrameSignals(this);
      Unsafe     = new FrameBaseUnsafe(this);
      
      Physics2D  = new Physics2D.PhysicsEngine2D.Api(this, context.TaskContext.ThreadCount);
      Physics3D  = new Physics3D.PhysicsEngine3D.Api(this, context.TaskContext.ThreadCount);

      // player data set signal
#pragma warning disable CS0618 // Type or member is obsolete
      _ISignalOnPlayerDataSet = BuildSignalsArray<ISignalOnPlayerDataSet>();
#pragma warning restore CS0618 // Type or member is obsolete
      _ISignalOnPlayerAdded = BuildSignalsArray<ISignalOnPlayerAdded>();
      _ISignalOnPlayerRemoved = BuildSignalsArray<ISignalOnPlayerRemoved>();

      // 2D Physics collision signals
      _ISignalOnCollision2DSystems      = BuildSignalsArray<ISignalOnCollision2D>();
      _ISignalOnCollisionEnter2DSystems = BuildSignalsArray<ISignalOnCollisionEnter2D>();
      _ISignalOnCollisionExit2DSystems  = BuildSignalsArray<ISignalOnCollisionExit2D>();

      // 2D Physics trigger signals
      _ISignalOnTrigger2DSystems      = BuildSignalsArray<ISignalOnTrigger2D>();
      _ISignalOnTriggerEnter2DSystems = BuildSignalsArray<ISignalOnTriggerEnter2D>();
      _ISignalOnTriggerExit2DSystems  = BuildSignalsArray<ISignalOnTriggerExit2D>();

      // 3D Physics collision signals
      _ISignalOnCollision3DSystems      = BuildSignalsArray<ISignalOnCollision3D>();
      _ISignalOnCollisionEnter3DSystems = BuildSignalsArray<ISignalOnCollisionEnter3D>();
      _ISignalOnCollisionExit3DSystems  = BuildSignalsArray<ISignalOnCollisionExit3D>();

      // 3D Physics trigger signals
      _ISignalOnTrigger3DSystems      = BuildSignalsArray<ISignalOnTrigger3D>();
      _ISignalOnTriggerEnter3DSystems = BuildSignalsArray<ISignalOnTriggerEnter3D>();
      _ISignalOnTriggerExit3DSystems  = BuildSignalsArray<ISignalOnTriggerExit3D>();

      _ISignalOnNavMeshWaypointReachedSystems = BuildSignalsArray<ISignalOnNavMeshWaypointReached>();
      _ISignalOnNavMeshSearchFailedSystems    = BuildSignalsArray<ISignalOnNavMeshSearchFailed>();
      _ISignalOnNavMeshMoveAgentSystems       = BuildSignalsArray<ISignalOnNavMeshMoveAgent>();

      // map changed signal
      _ISignalOnMapChangedSystems = BuildSignalsArray<ISignalOnMapChanged>();

      // prototype materialized signal
      _ISignalOnEntityPrototypeMaterializedSystems = BuildSignalsArray<ISignalOnEntityPrototypeMaterialized>();
      if ( _ISignalOnEntityPrototypeMaterializedSystems.Length > 0 ) {
        base._SignalOnEntityPrototypeMaterialized = (entity, prototype) => Signals.OnEntityPrototypeMaterialized(entity, prototype);
      }

      _ISignalOnPlayerConnectedSystems = BuildSignalsArray<ISignalOnPlayerConnected>();
      _ISignalOnPlayerDisconnectedSystems = BuildSignalsArray<ISignalOnPlayerDisconnected>();

      // assign map, rng session, etc.
      GlobalsCore->Map        = FindAsset<Map>(runtimeConfig.Map.Id);
      GlobalsCore->RngSession = new RNGSession(runtimeConfig.Seed);
      GlobalsCore->DeltaTime  = deltaTime;

      _systemIndexByType = new Dictionary<Type, Int32>(_systemsAll.Length);

      for (Int32 i = 0; i < _systemsAll.Length; ++i) {
        var systemType = _systemsAll[i].GetType();

        if (_systemIndexByType.ContainsKey(systemType) == false) {
          _systemIndexByType.Add(systemType, i);
        }

        // set default enabled systems
        if (_systemsAll[i].StartEnabled) {
          GlobalsCore->Systems.Set(_systemsAll[i].RuntimeIndex);
        }
      }

      // init physics settings
      Quantum.PhysicsSceneSettings.Init(&GlobalsCore->PhysicsSettings, simulationConfig.Physics);

      // Init navmesh regions to all bit fields to be set
      ClearAllNavMeshRegions();

      // user callbacks
      AllocUser();

      // preallocate any pointers globals might need
      _globals->AllocatePointers(this, default);
      
      InitUser();
    }

    /// <summary>
    /// Set the prediction area.
    /// </summary>
    /// <param name="position">Center of the prediction area</param>
    /// <param name="radius">Radius of the prediction area</param>
    /// <para>The Prediction Culling feature must be explicitly enabled in <see cref="SimulationConfig.UsePredictionArea"/>.</para>
    /// <para>This can be safely called from the main-thread.</para>
    /// <para>Prediction Culling allows developers to save CPU time in games where the player has only a partial view of the game scene.
    /// Quantum prediction and rollbacks, which are time consuming, will only run for important entities that are visible to the local player(s). Leaving anything outside that area to be simulated only once per tick with no rollbacks as soon as the inputs are confirmed from server.
    /// It is safe and simple to activate and, depending on the game, the performance difference can be quite large.Imagine a 30Hz game to constantly rollback ten ticks for every confirmed input (with more players, the predictor eventually misses at least for one of them). This requires the game simulation to be lightweight to be able to run at almost 300Hz(because of the rollbacks). With Prediction Culling enabled the full frames will be simulated at the expected 30Hz all the time while the much smaller prediction area is the only one running within the prediction buffer.</para>
    public void SetPredictionArea(FPVector3 position, FP radius) {
      Context.SetPredictionArea(position, radius);
    }

    /// <summary>
    /// See <see cref="SetPredictionArea(FPVector3, FP)"/>.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    public void SetPredictionArea(FPVector2 position, FP radius) {
      Context.SetPredictionArea(position.XOY, radius);
    }

    /// <summary>
    /// Test is a position is inside the prediction area.
    /// </summary>
    /// <param name="position">Position</param>
    /// <returns>True if the position is inside the prediction area.</returns>
    public Boolean InPredictionArea(FPVector3 position) {
      return Context.InPredictionArea(this, position);
    }

    /// <summary>
    /// See <see cref="InPredictionArea(FPVector3)"/>.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Boolean InPredictionArea(FPVector2 position) {
      return Context.InPredictionArea(this, position);
    }


    /// <summary>
    /// Serializes the frame using a temporary buffer (20MB).
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public override Byte[] Serialize(DeterministicFrameSerializeMode mode) {
      return Serialize(mode, new byte[1024 * 1024 * 20], allocOutput: true).Array;
    }

    /// <summary>
    /// Serializes the frame using <paramref name="buffer"/> as a buffer for temporary data. 
    /// 
    /// If <paramref name="allocOutput"/> is set to false, then <paramref name="buffer"/> is also used for the final data - use offset and count from the result to access
    /// the part of <paramref name="buffer"/> where serialized frame is stored.
    /// 
    /// If <paramref name="allocOutput"/> is set to true then a new array is allocated for the result.
    /// 
    /// Despite accepting a buffer, this method still allocates a few small temporary objects. 
    /// <see cref="IAssetSerializer.SerializeAssets(System.Collections.Generic.IEnumerable{AssetObject})"/> is also going
    /// to allocate when serializing DynamicAssetDB, but how much depends on the serializer itself and the number of dynamic assets.
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="allocOutput"></param>
    /// <returns>Segment of <paramref name="buffer"/> where the serialized frame is stored</returns>
    /// <remarks>Do not serialize during GameStart callback because systems have not been initialized, yet. Rather use CallbackSimulateFinished to wait for the first update.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ArraySegment<byte> Serialize(DeterministicFrameSerializeMode mode, byte[] buffer, int offset = 0, bool allocOutput = false) {
      return Serialize(mode, buffer, out _, offset, allocOutput);
    }
    
    public ArraySegment<byte> Serialize(DeterministicFrameSerializeMode mode, byte[] buffer, out FrameSerializer serializer, int offset = 0, bool allocOutput = false) {
      offset = ByteUtils.AddValueBlock((int)mode, buffer, offset);
      offset = ByteUtils.AddValueBlock(Number, buffer, offset);
      offset = ByteUtils.AddValueBlock(CalculateChecksum(false), buffer, offset);

      BitStream stream;

      {
        offset = ByteUtils.BeginByteBlockHeader(buffer, offset, out var blockOffset);

        stream = new BitStream(buffer, buffer.Length - offset, offset) {
          Writing = true
        };

        SerializeRuntimePlayers(stream);

        offset = ByteUtils.EndByteBlockHeader(buffer, blockOffset, stream.BytesRequired);
      }

      {
        offset = ByteUtils.BeginByteBlockHeader(buffer, offset, out var blockOffset);

        stream.SetBuffer(buffer, buffer.Length - offset, offset);
        serializer = new FrameSerializer(mode, this, stream) {
          Writing = true
        };

        SerializeState(serializer);

        offset = ByteUtils.EndByteBlockHeader(buffer, blockOffset, stream.BytesRequired);
      }

      DynamicAssetDB.Serialize(Context.AssetSerializer, out var assetDBHeader, out var assetDBData);

      {
        // write the header for the byte block but don't actually copy into the buffer
        // - we'll do that later during the compression stage
        offset = ByteUtils.BeginByteBlockHeader(buffer, offset, out var blockOffset);
        ByteUtils.EndByteBlockHeader(buffer, blockOffset, assetDBHeader.Length + assetDBData.Length);
      }

      using (var outputStream = allocOutput ? new MemoryStream() : new MemoryStream(buffer, offset, buffer.Length - offset)) {
        using (var compressedOutput = ByteUtils.CreateGZipCompressStream(outputStream)) {
          compressedOutput.Write(buffer, 0, offset);
          compressedOutput.Write(assetDBHeader, 0, assetDBHeader.Length);
          compressedOutput.Write(assetDBData, 0, assetDBData.Length);
        }

        if (allocOutput) {
          return new ArraySegment<byte>(outputStream.ToArray());
        } else {
          return new ArraySegment<byte>(buffer, offset, (int)outputStream.Position);
        }
        
      }
    }

    public override void Deserialize(Byte[] data) {
      var blocks = ByteUtils.ReadByteBlocks(ByteUtils.GZipDecompressBytes(data)).ToArray();
      
      var mode   = (DeterministicFrameSerializeMode)BitConverter.ToInt32(blocks[0], 0);
      
      Number = BitConverter.ToInt32(blocks[1], 0);
      
      var checksum = BitConverter.ToUInt64(blocks[2], 0);

      DeserializeRuntimePlayers(blocks[3]);
      DeserializeDynamicAssetDB(blocks[5]);

      FrameSerializer serializer;
      serializer         = new FrameSerializer(mode, this, blocks[4]);
      serializer.Reading = true;

      SerializeState(serializer);

      serializer.VerifyNoUnresolvedPointers();

      if (CalculateChecksum(false) != checksum) {
        throw new Exception($"Checksum of deserialized frame does not match checksum in the source data");
      }
    }

    void SerializeState(FrameSerializer serializer) {
      FrameBase.Serialize(this, serializer);
      _globals_.Serialize(_globals, serializer);
      SerializeEntitiesGen(serializer);
      SerializeUser(serializer);
    }

    void SerializeRuntimePlayers(BitStream stream) {
      stream.WriteInt(_playerData.Count);

      foreach (var player in _playerData.Iterator()) {
        stream.WriteInt(player.Key);
        stream.WriteInt(player.Value.ActorId);
        stream.WriteInt(player.Value.PlayerSlot);
        Context.AssetSerializer.SerializePlayer(stream, player.Value.Player);
      }
    }

    void DeserializeRuntimePlayers(Byte[] bytes) {
      BitStream stream;
      stream         = new BitStream(bytes);
      stream.Reading = true;

      var count = stream.ReadInt();
      _playerData = new PersistentMap<int, RuntimePlayerData>();
      for (Int32 i = 0; i < count; ++i) {
        var player = stream.ReadInt();

        RuntimePlayerData data;
        data.ActorId = stream.ReadInt();
        data.PlayerSlot = stream.ReadInt();
        data.Player = Context.AssetSerializer.DeserializePlayer<RuntimePlayer>(stream);
        _playerData = _playerData.Add(player, data);
      }
    }

    void DeserializeDynamicAssetDB(Byte[] bytes) {
      DynamicAssetDB.Deserialize(bytes, Context.AssetSerializer, Context.Allocator);
    }

    /// <summary>
    /// Dump the frame in human readable form into a string.
    /// </summary>
    /// <returns>Frame representation</returns>
    public sealed override String DumpFrame(int dumpFlags = 0) {
      var printer = new FramePrinter();
      printer.Reset(this);

      printer.IsRawPrintEnabled = ((dumpFlags & DumpFlag_PrintRawValues) == DumpFlag_PrintRawValues);

      // frame info
      if ((dumpFlags & DumpFlag_NoIsVerified) == DumpFlag_NoIsVerified) {
        printer.AddLine($"#### FRAME DUMP FOR {Number} ####");
      } else {
        printer.AddLine($"#### FRAME DUMP FOR {Number} IsVerified={IsVerified} ####");
      }
      
      if ((dumpFlags & DumpFlag_NoSimulationConfig) != DumpFlag_NoSimulationConfig) {
        printer.AddLine();
        printer.AddObject("# " + nameof(SimulationConfig), SimulationConfig);
      }

      if ((dumpFlags & DumpFlag_NoRuntimeConfig) != DumpFlag_NoRuntimeConfig) {
        printer.AddLine();
        printer.AddObject("# " + nameof(RuntimeConfig), RuntimeConfig);
      }

      if ((dumpFlags & DumpFlag_NoDeterministicSessionConfig) != DumpFlag_NoDeterministicSessionConfig) {
        printer.AddLine();
        printer.AddObject("# " + nameof(SessionConfig), SessionConfig);
      }

      if ((dumpFlags & DumpFlag_NoRuntimePlayers) != DumpFlag_NoRuntimePlayers) {
        printer.AddLine();
        printer.AddLine("# PLAYERS");
        {
          printer.ScopeBegin();
          foreach (var kv in _playerData.Iterator()) {
            printer.AddObject($"[{kv.Key}]", kv.Value);
          }
          printer.ScopeEnd();
        }
      }

      // globals state
      printer.AddLine();
      printer.AddPointer("# GLOBALS", _globals);

      // print entities
      printer.AddLine();
      printer.AddLine("# ENTITIES");
      Print(this, printer, (dumpFlags & DumpFlag_ComponentChecksums) == DumpFlag_ComponentChecksums);

      if ((dumpFlags & DumpFlag_AssetDBCheckums) == DumpFlag_AssetDBCheckums) {
        printer.AddLine();
        printer.AddLine("# ASSETDB CHECKSUMS");
        {
          printer.ScopeBegin();

          var orderedAssets = Context.AssetDB.ResourceManager.LoadAllAssets();
          orderedAssets.Sort((a, b) => a.Guid.CompareTo(b.Guid));
          
          foreach (var asset in orderedAssets) {
            var bytes = this.Context.AssetSerializer.AssetToByteArray(asset);
            fixed (byte* p = bytes) {
              var hash = CRC64.Calculate(0, p, bytes.Length);
              printer.AddLine($"{asset.Identifier}: {hash}");
            }
          }
          printer.ScopeEnd();
        }
      }

      if ((dumpFlags & DumpFlag_NoDynamicDB) != DumpFlag_NoDynamicDB) {
        printer.AddLine();
        printer.AddLine("# DYNAMICDB");
        {
          printer.ScopeBegin();

          var assetSerializer = Context.AssetSerializer;
          if ((dumpFlags & DumpFlag_ReadableDynamicDB) == DumpFlag_ReadableDynamicDB) {
            printer.AddLine($"NextGuid: {DynamicAssetDB.NextGuid}");
            foreach (var asset in DynamicAssetDB.Assets) {
              printer.AddLine($"{asset.GetType().FullName}:");
              printer.ScopeBegin();
              printer.AddLine($"{assetSerializer.PrintObject(asset)}");
              printer.ScopeEnd();
            }
          } else {
            printer.AddLine("Dump: ");
            printer.ScopeBegin();
            var data = DynamicAssetDB.Serialize(assetSerializer);
            fixed (byte* p = data) {
              UnmanagedUtils.PrintBytesHex(p, data.Length, 32, printer);
            }
            printer.ScopeEnd();
          }

          printer.ScopeEnd();
        }
      }

      // physics states
      if (Physics2D != null) {
        printer.AddLine();
        printer.AddLine("# 2D PHYSICS STATE");
        PhysicsEngineState.Print(_physicsState2D, printer);
      }

      if (Physics3D != null) {
        printer.AddLine();
        printer.AddLine("# 3D PHYSICS STATE");
        PhysicsEngineState.Print(_physicsState3D, printer);
      }

      // heap state
      if ((dumpFlags & DumpFlag_NoHeap) != DumpFlag_NoHeap) {
        printer.AddLine();
        printer.AddLine("# HEAP");
        Allocator.Heap.Print(_frameHeap.HeapUnsafe, printer);
      }

      // dump user data
      var dump = printer.ToString();
      DumpFrameUser(ref dump);

      return dump;
    }


    /// <summary>
    /// Calculates a checksum for the current game state. If the game is not started with <see cref="QuantumGameFlags.DisableSharedChecksumSerializer"/> 
    /// flag, this method is not thread-safe, i.e. calling it from multiple threads for frames from the same simulation is going to break.
    /// </summary>
    public sealed override UInt64 CalculateChecksum() {
      return CalculateChecksum(Context.UseSharedChecksumSerializer);
    }

    /// <summary>
    /// Calculates a checksum for the current game state.
    /// </summary>
    /// <param name="useSharedSerializer">True - use shared checksum serializer to avoid allocs (not thread-safe).</param>
    /// <returns></returns>
    public UInt64 CalculateChecksum(bool useSharedSerializer) {
      FrameSerializer frameSerializer;
      if (useSharedSerializer) {
        frameSerializer = Context.SharedChecksumSerializer;
        Assert.Check(frameSerializer != null); 
      } else {
        frameSerializer = new FrameSerializer(DeterministicFrameSerializeMode.Serialize, this, new FrameChecksumerBitStream());
      }
      return CalculateChecksumInternal(frameSerializer);
    }

    internal UInt64 CalculateChecksumInternal(FrameSerializer serializer) {

      if (serializer == null) {
        throw new ArgumentNullException(nameof(serializer));
      }

      if (serializer.Mode != DeterministicFrameSerializeMode.Serialize) {
        throw new ArgumentException($"Serializer needs to be in {nameof(DeterministicFrameSerializeMode.Serialize)} mode", nameof(serializer));
      }
      
      if (serializer.Stream is FrameChecksumerBitStream checksumStream) {

        Profiling.HostProfiler.Start("CalculateChecksumInternal");

        try {
          serializer.Reset();
          serializer.Writing = true;
          serializer.Frame = this;

          checksumStream.Checksum = (ulong)Number;

          // checksum globals
          _globals_.Serialize(_globals, serializer);

          // checksum entity registry
          FrameBase.Serialize(this, serializer);

          // checksum heap
          return checksumStream.Checksum;
        } finally {
          serializer.Frame = null;
          Profiling.HostProfiler.End();
        }
      } else {
        throw new InvalidOperationException($"Serializer's stream needs to be of {nameof(FrameChecksumerBitStream)} type (is: {serializer.Stream?.GetType().FullName}");
      }
    }

    /// <summary>
    /// Copies the complete frame memory.
    /// </summary>
    /// <param name="frame">Input frame object</param>
    protected sealed override void Copy(DeterministicFrame frame) {
      var f = (Frame)frame;

      HostProfiler.Start("Frame Copy");

      if (IsVerified) {
        // TODO(Erick): fix and optimize size see DeterministicFrame.Init()
        if (RawInputs == null) {
          RawInputs = new int[1024 * 32];
        }
        // only copy RawInputs into verified frame buffer, checksum and replay ring buffer, etc
        Array.Copy(f.RawInputs, RawInputs, f.RawInputs.Length);
      }

      // copy player data
      _playerData = f._playerData;

      HostProfiler.Start("Copy Heap");
      // copy heap from frame
      Allocator.Heap.Copy(Context.Allocator, _frameHeap.HeapUnsafe, f._frameHeap.HeapUnsafe);

      HostProfiler.End();

      // copy entity registry
      FrameBase.Copy(this, f);

      // dynamic DB
      DynamicAssetDB.CopyFrom(f.DynamicAssetDB);

      // perform native copy

      HostProfiler.Start("Copy Globals");
      CopyFromGen(f);
      HostProfiler.End();

      HostProfiler.Start("Copy User");
      CopyFromUser(f);
      HostProfiler.End();

      HostProfiler.End();
    }

    public sealed override void Free() {
      _globals->ClearPointers(this, default);
      FreeUser();
      FreeGen();
      base.Free();
    }

    /// <summary>
    /// Test if a system is enabled.
    /// </summary>
    /// <typeparam name="T">System type</typeparam>
    /// <returns>True if the system is enabled</returns>
    /// Logs an error if the system type is not found.
    public Boolean SystemIsEnabledSelf<T>() where T : SystemBase {
      var system = FindSystem<T>();
      if (system.Item0 == null) {
        return false;
      }

      return GlobalsCore->Systems.IsSet(system.Item1);
    }

    public Boolean SystemIsEnabledSelf(Type t) {
      var system = FindSystem(t);
      if (system.Item0 == null) {
        return false;
      }

      return GlobalsCore->Systems.IsSet(system.Item1);
    }

    public Boolean SystemIsEnabledSelf(SystemBase s) {
      if (s == null) {
        return false;
      }

      return GlobalsCore->Systems.IsSet(s.RuntimeIndex);
    }

    public Boolean SystemIsEnabledInHierarchy<T>() where T : SystemBase {
      var system = FindSystem<T>();
      return SystemIsEnabledInHierarchy(system.Item0);
    }

    public Boolean SystemIsEnabledInHierarchy(Type t) {
      var system = FindSystem(t);
      return SystemIsEnabledInHierarchy(system.Item0);
    }

    public Boolean SystemIsEnabledInHierarchy(SystemBase system) {
      if (system == null)
        return false;

      if (GlobalsCore->Systems.IsSet(system.RuntimeIndex) == false)
        return false;
      if (system.ParentSystem == null)
        return true;

      return SystemIsEnabledInHierarchy(system.ParentSystem);
    }

    /// <summary>
    /// Enable a system.
    /// </summary>
    /// <typeparam name="T">System type</typeparam>
    /// Logs an error if the system type is not found.
    public void SystemEnable<T>() where T : SystemBase {
      SystemEnable(typeof(T));
    }
    
    public void SystemEnable(Type t)  {
      var system = FindSystem(t);
      if (system.Item0 == null) {
        return;
      }

      if (GlobalsCore->Systems.IsSet(system.Item1) == false) {
        // set flag
        GlobalsCore->Systems.Set(system.Item1);

        // Fire callback only if it becomes enabled in hierarchy
        if (system.Item0.ParentSystem == null || SystemIsEnabledInHierarchy(system.Item0.ParentSystem)) {
          try {
            system.Item0.OnEnabled(this);
          } catch (Exception exn) {
            Log.Exception(exn);
          }
        }
      }
    }

    /// <summary>
    /// Disables a system.
    /// </summary>
    /// <typeparam name="T">System type</typeparam>
    /// Logs an error if the system type is not found.
    /// <example><code>
    /// // test for a certain asset and disable the system during its OnInit method
    /// public override void OnInit(Frame f) {
    ///   var testSettings = f.FindAsset&lt;NavMeshAgentsSettings&gt;(f.Map.UserAsset.Id);
    ///   if (testSettings == null) {
    ///     f.SystemDisable&lt;NavMeshAgentTestSystem&gt;();
    ///     return;
    ///    }
    ///    //..
    ///  }
    /// </code></example>
    public void SystemDisable<T>() where T : SystemBase {
      SystemDisable(typeof(T));
    }

    public void SystemDisable<T>(T system) where T : SystemBase {
      SystemDisable(system.GetType());
    }

    public void SystemDisable(Type t) {
      var system = FindSystem(t);
      if (system.Item0 == null) {
        return;
      }

      if (GlobalsCore->Systems.IsSet(system.Item1)) {
        // clear flag
        GlobalsCore->Systems.Clear(system.Item1);

        // Fire callback only if it was previously enabled in hierarchy
        if (system.Item0.ParentSystem == null || SystemIsEnabledInHierarchy(system.Item0.ParentSystem)) {
          try {
            system.Item0.OnDisabled(this);
          } catch (Exception exn) {
            Log.Exception(exn);
          }
        }
      }
    }
    
    QTuple<SystemBase, Int32> FindSystem<T>() {
      return FindSystem(typeof(T));
    }
    
    QTuple<SystemBase, Int32> FindSystem(Type t) {
      if (_systemIndexByType.TryGetValue(t, out var i)) {
        return QTuple.Create(_systemsAll[i], i);
      }

      Log.Error($"System '{t.Name}' not found, did you forget to add it to SystemSetup.CreateSystems ?");
      return new QTuple<SystemBase, Int32>(null, -1);
    }


    T[] BuildSignalsArray<T>() {
      return _systemsAll.Where(x => x is T).Cast<T>().ToArray();
    }

    void BuildSignalsArrayOnComponentAdded<T>() where T : unmanaged, IComponent {
      Assert.Check(ComponentTypeId<T>.Id > 0);

      var array = _systemsAll.Where(x => x is ISignalOnComponentAdded<T>).Cast<ISignalOnComponentAdded<T>>().ToArray();
      if (array.Length > 0) {
        _ComponentSignalsOnAdded[ComponentTypeId<T>.Id] = (entity, componentData) => {
          var component = (T*)componentData;
          var systems   = &(GlobalsCore->Systems);
          for (Int32 i = 0; i < array.Length; ++i) {
            if (SystemIsEnabledInHierarchy((SystemBase)array[i])) {
              array[i].OnAdded(this, entity, component);
            }
          }
        };
      } else {
        _ComponentSignalsOnAdded[ComponentTypeId<T>.Id] = null;
      }
    }

    void BuildSignalsArrayOnComponentRemoved<T>() where T : unmanaged, IComponent {
      Assert.Check(ComponentTypeId<T>.Id > 0);

      var array = _systemsAll.Where(x => x is ISignalOnComponentRemoved<T>).Cast<ISignalOnComponentRemoved<T>>().ToArray();
      if (array.Length > 0) {
        _ComponentSignalsOnRemoved[ComponentTypeId<T>.Id] = (entity, componentData) => {
          var component = (T*)componentData;
          var systems   = &(GlobalsCore->Systems);
          for (Int32 i = 0; i < array.Length; ++i) {
            if (SystemIsEnabledInHierarchy((SystemBase)array[i])) {
              array[i].OnRemoved(this, entity, component);
            }
          }
        };
      } else {
        _ComponentSignalsOnRemoved[ComponentTypeId<T>.Id] = null;
      }
    }

    void AddEvent(EventBase evnt) {
      // set evnt.Tick
      evnt.Tick = Number;

      // add ast last
      Context.Events.AddLast(evnt);
    }

    public static void InitStatic() {
      Statics.Init();
    }

    // partial declarations populated from code generator
    partial void InitGen();
    partial void FreeGen();
    partial void AllocGen();
    partial void CopyFromGen(Frame                    frame);
    partial void SerializeEntitiesGen(FrameSerializer serializer);

    partial void InitUser();
    partial void FreeUser();
    partial void AllocUser();
    partial void CopyFromUser(Frame frame);

    partial void SerializeUser(FrameSerializer serializer);
    partial void DumpFrameUser(ref String      dump);


    /// <summary>
    /// Gets the runtime player configuration data for a certain player.
    /// </summary>
    /// <param name="player">Player ref</param>
    /// <returns>Player config or null if player was not found</returns>
    public RuntimePlayer GetPlayerData(PlayerRef player) {
      RuntimePlayerData data;

      if (_playerData.TryFind(player, out data)) {
        return data.Player;
      }

      return null;
    }

    /// <summary>
    /// Converts a Quantum PlayerRef to an ActorId (Photon client id).
    /// </summary>
    /// <param name="player">Player reference</param>
    /// <returns>ActorId or null if payer was not found</returns>
    public Int32? PlayerToActorId(PlayerRef player) {
      RuntimePlayerData data;

      if (_playerData.TryFind(player, out data)) {
        return data.ActorId;
      }

      return null;
    }

    /// <summary>
    /// Returns the first player that is using a certain ActorId (Photon client id).
    /// </summary>
    /// <param name="actorId">Actor id</param>
    /// <returns>Player reference or null if actor id was not found</returns>
    /// The first player because multiple players from the same Photon client can join.
    public PlayerRef? ActorIdToFirstPlayer(Int32 actorId) {
      foreach (var kvp in _playerData.Iterator()) {
        if (kvp.Value.ActorId == actorId) {
          return kvp.Key;
        }
      }

      return null;
    }

    /// <summary>
    /// Returns all players with a certain ActorId (Photon client id).
    /// </summary>
    /// <param name="actorId">Actor id</param>
    /// <returns>Array of player references</returns>
    public PlayerRef[] ActorIdToAllPlayers(Int32 actorId) {
      return _playerData.Iterator().Where(x => x.Value.ActorId == actorId).Select(x => (PlayerRef)x.Key).ToArray();
    }

    public void UpdatePlayerData(IDeterministicGame game) {
      InputSetMask set = new InputSetMask();
      InputSetMask isNew = new InputSetMask();
      var hasSetChanged = false;

      if (IsVerified == false) {
        // TODO: revisit when delta compression is available (complete method will likely be moved to verified frames)
        // It can be removed then, after testing.
        return;
      }

      for (Int32 i = 0; i < PlayerCount; ++i) {
        var isPlayerConnected = (GetPlayerInputFlags(i) & DeterministicInputFlags.PlayerNotPresent) == 0;
        if (!isPlayerConnected) {
          if (PlayerLastConnectionState.IsSet(i)) {
            OnPlayerRemoved(this, game.Session, i);
            Signals.OnPlayerRemoved(i);
          }
        }

        var rpc = GetRawRpc(i);
        if (rpc != null && rpc.Length > 0) {
          var flags = GetPlayerInputFlags(i);
          if ((flags & DeterministicInputFlags.Command) != DeterministicInputFlags.Command) {
            var playerDataOriginal = _playerData;

            try {
              // create player data
              RuntimePlayerData data;
              data.ActorId = game.Session.GameMode == DeterministicGameMode.Replay ? 0 : BitConverter.ToInt32(rpc, 0);
              data.PlayerSlot = BitConverter.ToInt32(rpc, 4);
              data.Player = Context.AssetSerializer.PlayerFromByteArray<RuntimePlayer>(rpc, 8, rpc.Length - 8, compressed: true);

              // global player index mapping needs to be injected into the session
              Assert.Check(IsVerified, "Verified frame obligatory when accepting runtime player.");
              OnPlayerAdded(this, game.Session, data.PlayerSlot, data.ActorId, i);

              // set new flag
              if (_playerData.HasKey(i) == false) {
                isNew.Add(i);
              }
              _playerData = _playerData.AddOrSet(i, data);

              // set mask
              set.Add(i);
              hasSetChanged = true;
#if DEBUG
            } catch (Exception e) {
              Log.Exception("## RuntimePlayer Deserialization Threw Exception ##", e);
#else
            } catch {
#endif
              _playerData = playerDataOriginal;
            }
          }
        }
      }

      if (hasSetChanged) {
        for (Int32 i = 0; i < PlayerCount; ++i) {
          if (set.Contains(i)) {
            try {
              Signals.OnPlayerAdded(i, isNew.Contains(i));
            } catch (Exception exn) {
              Log.Exception(exn);
            }
          }
        }
      }
    }

    internal void ResetPhysics() {
      ResetPhysicsCodeGen();
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Core/FrameContextUser.cs

namespace Quantum {
  public partial class FrameContextUser : Core.FrameContext {
    public FrameContextUser(Args args) 
      : base(args) {
      ConstructUser(args);
    }

    public override sealed void Dispose() {
      DisposeUser();
      base.Dispose();
    }

    partial void ConstructUser(Args args);
    partial void DisposeUser();
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Core/FrameEvents.cs

namespace Quantum {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  partial class Frame {
    public partial struct FrameEvents {
      Frame _f;

      public FrameEvents(Frame f) {
        _f = f;
      }
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Core/FrameSignals.cs

namespace Quantum {
  using System;
  using Photon.Deterministic;
  using Quantum.Core;
  public unsafe interface ISignalOnComponentAdded<T> : ISignal where T : unmanaged, IComponent {
    void OnAdded(Frame f, EntityRef entity, T* component);
  }

  public unsafe interface ISignalOnComponentRemoved<T> : ISignal where T : unmanaged, IComponent {
    void OnRemoved(Frame f, EntityRef entity, T* component);
  } 
  
  public unsafe interface ISignalOnMapChanged : ISignal {
    void OnMapChanged(Frame f, AssetRef<Map> previousMap);
  }

  public unsafe interface ISignalOnEntityPrototypeMaterialized : ISignal {
    void OnEntityPrototypeMaterialized(Frame f, EntityRef entity, EntityPrototypeRef prototypeRef);
  }

  public unsafe interface ISignalOnPlayerConnected : ISignal {
    void OnPlayerConnected(Frame f, PlayerRef player);
  }

  public unsafe interface ISignalOnPlayerDisconnected : ISignal {
    void OnPlayerDisconnected(Frame f, PlayerRef player);
  }

  partial class Frame {
    public unsafe partial struct FrameSignals {
      Frame _f;

      public FrameSignals(Frame f) {
        _f = f;
      }

      public void OnPlayerAdded(PlayerRef player, bool firstTime) {
        {
          var array = _f._ISignalOnPlayerAdded;
          for (Int32 i = 0; i < array.Length; ++i) {
            var s = array[i];
            if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
              s.OnPlayerAdded(_f, player, firstTime);
            }
          }
        }

        // Call deprecated signals
        {
          var array = _f._ISignalOnPlayerDataSet;
          for (Int32 i = 0; i < array.Length; ++i) {
            var s = array[i];
            if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
              s.OnPlayerDataSet(_f, player);
            }
          }
        }
      }

      public void OnPlayerRemoved(PlayerRef player) {
        {
          var array = _f._ISignalOnPlayerRemoved;
          for (Int32 i = 0; i < array.Length; ++i) {
            var s = array[i];
            if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
              s.OnPlayerRemoved(_f, player);
            }
          }
        }
      }

      public void OnMapChanged(AssetRef<Map> previousMap) {
        var array = _f._ISignalOnMapChangedSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnMapChanged(_f, previousMap);
          }
        }
      }

      public void OnEntityPrototypeMaterialized(EntityRef entity, EntityPrototypeRef prototypeRef) {
        var array = _f._ISignalOnEntityPrototypeMaterializedSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnEntityPrototypeMaterialized(_f, entity, prototypeRef);
          }
        }
      }

      public void OnPlayerConnected(PlayerRef player) {
        var array = _f._ISignalOnPlayerConnectedSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnPlayerConnected(_f, player);
          }
        }
      }

      public void OnPlayerDisconnected(PlayerRef player) {
        var array = _f._ISignalOnPlayerDisconnectedSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnPlayerDisconnected(_f, player);
          }
        }
      }

      public void OnNavMeshWaypointReached(EntityRef entity, FPVector3 waypoint, Navigation.WaypointFlag waypointFlags, ref bool resetAgent) {
        var array   = _f._ISignalOnNavMeshWaypointReachedSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnNavMeshWaypointReached(_f, entity, waypoint, waypointFlags, ref resetAgent);
          }
        }
      }

      public void OnNavMeshSearchFailed(EntityRef entity, ref bool resetAgent) {
        var array   = _f._ISignalOnNavMeshSearchFailedSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnNavMeshSearchFailed(_f, entity, ref resetAgent);
          }
        }
      }

      public void OnNavMeshMoveAgent(EntityRef entity, FPVector2 desiredDirection) {
        var array = _f._ISignalOnNavMeshMoveAgentSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnNavMeshMoveAgent(_f, entity, desiredDirection);
          }
        }
      }

      public void OnCollision2D(CollisionInfo2D info) {
        var array   = _f._ISignalOnCollision2DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnCollision2D(_f, info);
          }
        }
      }

      public void OnCollisionEnter2D(CollisionInfo2D info) {
        var array   = _f._ISignalOnCollisionEnter2DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnCollisionEnter2D(_f, info);
          }
        }
      }

      public void OnCollisionExit2D(ExitInfo2D info) {
        var array   = _f._ISignalOnCollisionExit2DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnCollisionExit2D(_f, info);
          }
        }
      }
      
      public void OnTrigger2D(TriggerInfo2D info) {
        var array   = _f._ISignalOnTrigger2DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnTrigger2D(_f, info);
          }
        }
      }

      public void OnTriggerEnter2D(TriggerInfo2D info) {
        var array   = _f._ISignalOnTriggerEnter2DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnTriggerEnter2D(_f, info);
          }
        }
      }

      public void OnTriggerExit2D(ExitInfo2D info) {
        var array   = _f._ISignalOnTriggerExit2DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnTriggerExit2D(_f, info);
          }
        }
      }
      
      public void OnCollision3D(CollisionInfo3D info) {
        var array   = _f._ISignalOnCollision3DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnCollision3D(_f, info);
          }
        }
      }

      public void OnCollisionEnter3D(CollisionInfo3D info) {
        var array   = _f._ISignalOnCollisionEnter3DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnCollisionEnter3D(_f, info);
          }
        }
      }

      public void OnCollisionExit3D(ExitInfo3D info) {
        var array   = _f._ISignalOnCollisionExit3DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnCollisionExit3D(_f, info);
          }
        }
      }
      
      public void OnTrigger3D(TriggerInfo3D info) {
        var array   = _f._ISignalOnTrigger3DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnTrigger3D(_f, info);
          }
        }
      }

      public void OnTriggerEnter3D(TriggerInfo3D info) {
        var array   = _f._ISignalOnTriggerEnter3DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnTriggerEnter3D(_f, info);
          }
        }
      }

      public void OnTriggerExit3D(ExitInfo3D info) {
        var array   = _f._ISignalOnTriggerExit3DSystems;
        for (Int32 i = 0; i < array.Length; ++i) {
          var s = array[i];
          if (_f.SystemIsEnabledInHierarchy((SystemBase)s)) {
            s.OnTriggerExit3D(_f, info);
          }
        }
      }
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Core/FrameThreadSafeExtensions.cs

namespace Quantum {
  using System.Runtime.CompilerServices;
  
  public static unsafe class FrameThreadSafeExtensions {
    /// <summary>
    /// A convenience method to get the global state from a frame without having to cast it.
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static _globals_* GetGlobal(this FrameThreadSafe frame) {
      var f = (Frame)frame;
      return f.Global;
    } 
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Core/IAssetSerializerExtensions.cs

namespace Quantum {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using UnityEngine;

  public static class IAssetSerializerLegacyExtensions {
    [Obsolete("Use " + nameof(IAssetSerializerExtensions.AssetsFromByteArray) + " instead")]
    public static IEnumerable<AssetObject> DeserializeAssets(this IAssetSerializer serializer, byte[] data) => serializer.AssetsFromByteArray(data);
    
    [Obsolete("Use " + nameof(IAssetSerializerExtensions.AssetsToByteArray) + " instead")]
    public static byte[] SerializeAssets(this IAssetSerializer serializer, IEnumerable<AssetObject> assets) => serializer.AssetsToByteArray(assets.ToArray());

#if QUANTUM_UNITY
    [Obsolete("Use JsonUtility or any other serialization method instead")]
    public static byte[] SerializeReplay(this IAssetSerializer serializer, QuantumReplayFile file) {
      var json = JsonUtility.ToJson(file);
      return System.Text.Encoding.UTF8.GetBytes(json);
    }

    [Obsolete("Use JsonUtility or any other serialization method instead")]
    public static QuantumReplayFile DeserializeReplay(this IAssetSerializer serializer, byte[] data) {
      var json = System.Text.Encoding.UTF8.GetString(data);
      return JsonUtility.FromJson<QuantumReplayFile>(json);
    }
    
    [Obsolete("Use JsonUtility or any other serialization method instead")]
    public static byte[] SerializeChecksum(this IAssetSerializer serializer, ChecksumFile file) {
      var json = JsonUtility.ToJson(file);
      return System.Text.Encoding.UTF8.GetBytes(json);
    }

    [Obsolete("Use JsonUtility or any other serialization method instead")]
    public static ChecksumFile DeserializeChecksum(this IAssetSerializer serializer, byte[] data) {
      var json = System.Text.Encoding.UTF8.GetString(data);
      return JsonUtility.FromJson<ChecksumFile>(json);
    }
#endif
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Core/ISignalOnPlayerDataSet.cs

namespace Quantum {
  using System;

  [Obsolete("Use new interface ISignalOnPlayerAdded and change signature to OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)")]
  public interface ISignalOnPlayerDataSet : ISignal {
    /// <summary>
    /// Is called when a player and his RuntimePlayer was added to the simulation.
    /// </summary>
    /// <param name="f">Frame</param>
    /// <param name="player">Player</param>
    void OnPlayerDataSet(Frame f, PlayerRef player);
  }

  public interface ISignalOnPlayerAdded : ISignal {
    /// <summary>
    /// Is called when a player and his RuntimePlayer was added to the simulation.
    /// </summary>
    /// <param name="f">Frame</param>
    /// <param name="player">Player</param>
    /// <param name="firstTime">The first time that this player ref was assigned to a player at all. When firstTime is false the player ref is being reused by a different player. See documentation.</param>
    void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime);
  }

  public interface ISignalOnPlayerRemoved : ISignal {
    /// <summary>
    /// Is called when a player was removed from the simulation.
    /// </summary>
    /// <param name="f">Frame</param>
    /// <param name="player">Player</param>
    void OnPlayerRemoved(Frame f, PlayerRef player);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Core/NavMeshSignals.cs

namespace Quantum {
  using Photon.Deterministic;

  /// <summary>
  /// Signal is fired when an agent reaches a waypoint.
  /// </summary>
  /// <remarks>Requires enabled <see cref="Navigation.Config.EnableNavigationCallbacks"/> in <see cref="SimulationConfig.Navigation"/>.</remarks>
  /// \ingroup NavigationApi
  public unsafe interface ISignalOnNavMeshWaypointReached : ISignal {
    /// <param name="f">Current frame object</param>
    /// <param name="entity">The entity the navmesh agent component belongs to</param>
    /// <param name="waypoint">The current waypoint position</param>
    /// <param name="waypointFlags">The current waypoint flags</param>
    /// <param name="resetAgent">If set to true the NavMeshPathfinder component will be cleared and stopped. Set to false if NavMeshPathfinder.SetTarget() was called inside the callback.</param>
    void OnNavMeshWaypointReached(Frame f, EntityRef entity, FPVector3 waypoint, Navigation.WaypointFlag waypointFlags, ref bool resetAgent);
  }

  /// <summary>
  /// Signal is fired when the agent could not find a path in the agent update after using <see cref="NavMeshPathfinder.SetTarget"/>
  /// </summary>
  /// <remarks>Requires enabled <see cref="Navigation.Config.EnableNavigationCallbacks"/> in <see cref="SimulationConfig.Navigation"/>.</remarks>
  /// \ingroup NavigationApi
  public unsafe interface ISignalOnNavMeshSearchFailed: ISignal {
    /// <param name="f">Current frame object</param>
    /// <param name="entity">The entity the navmesh agent component belongs to</param>
    /// <param name="resetAgent">Set this to true if the agent should reset its internal state (default is true).</param>
    void OnNavMeshSearchFailed(Frame f, EntityRef entity, ref bool resetAgent);
  }

  /// <summary>
  /// Signal is called when the agent should move. The desired direction is influence by avoidance.
  /// </summary>
  /// <remarks>The agent velocity should be set in the callback.</remarks>
  /// \ingroup NavigationApi
  public unsafe interface ISignalOnNavMeshMoveAgent: ISignal {
    void OnNavMeshMoveAgent(Frame f, EntityRef entity, FPVector2 desiredDirection);
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Core/RuntimeConfig.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  using System.Text;

  /// <summary>
  /// In contrast to the <see cref="SimulationConfig"/>, which has only static configuration data, the RuntimeConfig holds information that can be different from game to game.
  /// </summary>
  /// By default is defines for example what map to load and the random start seed. It is assembled from scratch each time starting a game.
  /// <para>Developers can add custom data to quantum_code/quantum.state/RuntimeConfig.User.cs (don't forget to fill out the serialization methods).</para>
  /// <para>Like the <see cref="DeterministicSessionConfig"/> this config is distributed to every other client after the first player connected and joined the Quantum plugin.</para>
  [Serializable]
  public partial class RuntimeConfig : IRuntimeConfig {
    /// <summary> 
    /// Seed to initialize the randomization session under <see cref="Frame.RNG"/>. 
    /// </summary>
    public Int32 Seed;
    /// <summary> 
    /// Asset reference of the Quantum map used with the upcoming game session. 
    /// </summary>
    public AssetRef<Map> Map;
    /// <summary> Asset reference to the SimulationConfig used with the upcoming game session. </summary>
    public AssetRef<SimulationConfig> SimulationConfig;
    /// <summary> 
    /// Asset reference to the Quantum systems configuration.
    /// If no config is assigned then a default selection of build-in systems is used (<see cref="DeterministicSystemSetup.CreateSystems(RuntimeConfig, SimulationConfig, SystemsConfig)"/>.
    /// The systems to be used can always be changed by code inside <see cref="DeterministicSystemSetup.AddSystemsUser(System.Collections.Generic.ICollection{SystemBase}, RuntimeConfig, Quantum.SimulationConfig, Quantum.SystemsConfig)"/>.
    /// </summary>
    public AssetRef<SystemsConfig> SystemsConfig;

    /// <summary>
    /// Dump the content into a human readable form.
    /// </summary>
    /// <returns>String representation</returns>
    [Obsolete("No longer used. Convert to string using JsonUtility or IAssetSerializer.")]
    public String Dump() {
      String dump = "";
      DumpUserData(ref dump);

      StringBuilder sb = new StringBuilder();
      sb.Append(dump);
      sb.Append("\n");
      sb.AppendLine("Seed: " + Seed);
      sb.AppendLine($"Map.Guid: {Map.ToString()}");
      sb.AppendLine($"SimulationConfig.Guid: {SimulationConfig.ToString()} ");

      return sb.ToString();
    }

    [Obsolete("No longer used. Convert to string using JsonUtility or IAssetSerializer.")]
    partial void DumpUserData(ref String dump);

    #region Legacy

    [Obsolete("Use IAssetSerializer instead")]
    public void Serialize(BitStream stream) {
    }

    [Obsolete("This method can be deleted.")]
    partial void SerializeUserData(BitStream stream);

    [Obsolete("Use IAssetSerializer instead")]
    public static Byte[] ToByteArray(RuntimeConfig config) {
      throw new NotImplementedException();
    }

    [Obsolete("Use IAssetSerializer instead")]
    public static RuntimeConfig FromByteArray(Byte[] data) {
      throw new NotImplementedException();
    }

    #endregion
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Core/RuntimePlayer.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;

  [Serializable]
  public partial class RuntimePlayer : IRuntimePlayer {
    /// <summary>
    /// This is a proposal how to let players select an avatar prototype using RuntimePlayer. Can be removed.
    /// </summary>
    public Quantum.AssetRef<Quantum.EntityPrototype> PlayerAvatar;
    /// <summary>
    /// This is a proposal how to assign a nickname to players using RuntimePlayer. Can be removed.
    /// </summary>
    public string PlayerNickname;

    #region Legacy

    [Obsolete("Use IAssetSerializer instead")]
    public void Serialize(BitStream stream) {
    }

    [Obsolete("This method can be deleted.")]
    partial void SerializeUserData(BitStream stream);

    [Obsolete("Use IAssetSerializer instead")]
    public static Byte[] ToByteArray(RuntimePlayer config) {
      throw new NotImplementedException();
    }

    [Obsolete("Use IAssetSerializer instead")]
    public static RuntimePlayer FromByteArray(Byte[] data) {
      throw new NotImplementedException();
    }

    #endregion
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Core/Statics.cs

namespace Quantum {
  using UnityEngine;

  partial class Statics {
    static Statics() {
      InitComponentTypeIdGen();
      
      InitStaticDelegatesGen();
      InitStaticDelegatesBotSDK();
      InitStaticDelegatesUser();
    }

#if QUANTUM_UNITY
    [RuntimeInitializeOnLoadMethod]
    [UnityEngine.Scripting.Preserve]
#endif
    public static void Init() {
      // this will invoke the static constructor
    }
    
    public static void RegisterSimulationTypes(TypeRegistry typeRegistry) {
      RegisterSimulationTypesGen(typeRegistry);
      RegisterLegacySimulationTypesGen(typeRegistry);
      RegisterSimulationTypesBotSDK(typeRegistry);
      RegisterSimulationTypesUser(typeRegistry);
    }
    
    static partial void InitComponentTypeIdGen();
    static partial void InitStaticDelegatesGen();
    static partial void InitStaticDelegatesBotSDK();
    static partial void InitStaticDelegatesUser();
    
    static partial void RegisterSimulationTypesGen(TypeRegistry typeRegistry);
    static partial void RegisterLegacySimulationTypesGen(TypeRegistry typeRegistry);
    static partial void RegisterSimulationTypesBotSDK(TypeRegistry typeRegistry);
    static partial void RegisterSimulationTypesUser(TypeRegistry typeRegistry);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Core/SystemSetup.cs

namespace Quantum {
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;

  /// <summary>
  /// Responsible for instantiating Quantum systems on simualtion start.
  /// User systems can be added by adding a <see cref="SystemsConfig"/> to the <see cref="RuntimeConfig"/>.
  /// Or adding them in the user callback <see cref="AddSystemsUser"/>.
  /// </summary>
  public static partial class DeterministicSystemSetup {
    static partial void AddSystemsUser(ICollection<SystemBase> systems, RuntimeConfig gameConfig, SimulationConfig simulationConfig, SystemsConfig systemsConfig);

    public static ICollection<SystemBase> CreateSystems(RuntimeConfig gameConfig, SimulationConfig simulationConfig, SystemsConfig systemsConfig) {
      // Call legacy SystemSettings by reflection and print warning if exists
      var systems = CallLegacySystemSetup(gameConfig, simulationConfig);
      if (systems != null) {
        Log.Warn("Detected SystemSetup.CreateSystems() usage. Please migrate to SystemsConfig.asset or DeterministicSystemSetup.AddSystemsUser() found in SystemSetup.User.cs.");
        return systems;
      }

      if (systemsConfig != null) {
        // Create systems using the systems configuration asset
        systems = SystemsConfig.CreateSystems(systemsConfig);
      } else {
        // Instantiate pre-defined core systems
        systems = new List<SystemBase>() {
          new Core.CullingSystem2D(),
          new Core.CullingSystem3D(),
          new Core.PhysicsSystem2D(),
          new Core.PhysicsSystem3D(),
          new Core.NavigationSystem(),
          new Core.EntityPrototypeSystem(),
          new Core.PlayerConnectedSystem()};
      }

      // Add the debug command system
      var debugSystem = Core.DebugCommand.CreateSystem();
      if (debugSystem != null) {
        systems.Add(debugSystem);
      }

      // Finally call the user method
      AddSystemsUser(systems, gameConfig, simulationConfig, systemsConfig);

      return systems;
    }

    private static ICollection<SystemBase> CallLegacySystemSetup(RuntimeConfig gameConfig, SimulationConfig simulationConfig) {
      // public static SystemBase[] CreateSystems(RuntimeConfig gameConfig, SimulationConfig simulationConfig) {
      var systemSetupType = TypeUtils.FindType("SystemSetup");
      if (systemSetupType == null) {
        return null;
      }
      
      var createSystemsMethodInfo = systemSetupType.GetMethod("CreateSystems", BindingFlags.Public | BindingFlags.Static);
      if (createSystemsMethodInfo == null) {
        return null;
      }

      var systemArray = (SystemBase[])createSystemsMethodInfo.Invoke(null, new object[] { gameConfig, simulationConfig });
      if (systemArray == null || systemArray.Length == 0) {
        return null;
      }

      return systemArray.ToList();
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Game/CallbackDispatcher.cs

namespace Quantum {
  using System;
  using System.Collections.Generic;

  public class CallbackDispatcher : DispatcherBase, Quantum.ICallbackDispatcher {

    protected static Dictionary<Type, Int32> GetBuiltInTypes() {
      return new Dictionary<Type, Int32>() {
        { typeof(CallbackChecksumComputed), CallbackChecksumComputed.ID },
        { typeof(CallbackChecksumError), CallbackChecksumError.ID },
        { typeof(CallbackChecksumErrorFrameDump), CallbackChecksumErrorFrameDump.ID },
        { typeof(CallbackEventCanceled), CallbackEventCanceled.ID },
        { typeof(CallbackEventConfirmed), CallbackEventConfirmed.ID },
        { typeof(CallbackGameDestroyed), CallbackGameDestroyed.ID },
        { typeof(CallbackGameInit), CallbackGameInit.ID },
        { typeof(CallbackGameStarted), CallbackGameStarted.ID },
        { typeof(CallbackGameResynced), CallbackGameResynced.ID },
        { typeof(CallbackInputConfirmed), CallbackInputConfirmed.ID },
        { typeof(CallbackPollInput), CallbackPollInput.ID },
        { typeof(CallbackSimulateFinished), CallbackSimulateFinished.ID },
        { typeof(CallbackUpdateView), CallbackUpdateView.ID },
        { typeof(CallbackPluginDisconnect), CallbackPluginDisconnect.ID },
        { typeof(CallbackLocalPlayerAddConfirmed), CallbackLocalPlayerAddConfirmed.ID },
        { typeof(CallbackLocalPlayerRemoveConfirmed), CallbackLocalPlayerRemoveConfirmed.ID },
        { typeof(CallbackLocalPlayerAddFailed), CallbackLocalPlayerAddFailed.ID },
        { typeof(CallbackLocalPlayerRemoveFailed), CallbackLocalPlayerRemoveFailed.ID },
      };
    }

    public CallbackDispatcher() : base(GetBuiltInTypes()) { }
    protected CallbackDispatcher(Dictionary<Type, Int32> callbackTypes) : base(callbackTypes) { }

    public bool Publish(CallbackBase e) {
      return base.InvokeMeta(e.ID, e);
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Game/ChecksumErrorFrameDumpContext.cs

namespace Quantum {
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using Photon.Deterministic;

  public unsafe partial class ChecksumErrorFrameDumpContext {
    public SimulationConfig SimulationConfig;
    public QTuple<AssetGuid, ulong>[] AssetDBChecksums;

    private ChecksumErrorFrameDumpContext() {}

    public ChecksumErrorFrameDumpContext(QuantumGame game, Frame frame) {
      var options = game.Configurations.Simulation.ChecksumErrorDumpOptions;

      SimulationConfig = game.Configurations.Simulation;

      // write checksums
      if ((options & SimulationConfigChecksumErrorDumpOptions.SendAssetDBChecksums) == SimulationConfigChecksumErrorDumpOptions.SendAssetDBChecksums) {

        List<AssetObject> assets = frame.Context.AssetDB.ResourceManager.LoadAllAssets();
        assets.Sort((a, b) => a.Guid.CompareTo(b.Guid));
        
        AssetDBChecksums = new QTuple<AssetGuid, ulong>[assets.Count];
        
        for (int i = 0; i < assets.Count; ++i) {
          var bytes = frame.Context.AssetSerializer.AssetToByteArray(assets[i]);
          fixed (byte* p = bytes) {
            var crc = CRC64.Calculate(0, p, bytes.Length);
            AssetDBChecksums[i] = QTuple.Create(assets[i].Guid, crc);
          }
        }
      }

      ConstructUser(game, frame);
    }

    partial void ConstructUser(QuantumGame game, Frame frame);
    partial void SerializeUser(QuantumGame game, BinaryWriter writer);
    partial void DeserializeUser(QuantumGame game, BinaryReader reader);

    public void Serialize(QuantumGame game, BinaryWriter writer) {
      writer.Write(AssetDBChecksums?.Length ?? 0);
      if (AssetDBChecksums != null) {
        foreach (var asset in AssetDBChecksums) {
          writer.Write(asset.Item0.Value);
          writer.Write(asset.Item1);
        }
      }

      // write simulation config
      if (SimulationConfig != null) {
        var simConfigBytes = game.AssetSerializer.AssetToByteArray(SimulationConfig);
        writer.Write(simConfigBytes.Length);
        writer.Write(simConfigBytes);
      } else {
        writer.Write(0);
      }

      SerializeUser(game, writer);
    }

    public static ChecksumErrorFrameDumpContext Deserialize(QuantumGame game, BinaryReader reader) {
      var result = new ChecksumErrorFrameDumpContext();

      // read checksums
      {
        int count = reader.ReadInt32();
        if (count > 0) {
          result.AssetDBChecksums = new QTuple<AssetGuid, ulong>[count];

          for (int i = 0; i < count; ++i) {
            var guidRaw = reader.ReadInt64();
            var crc64 = reader.ReadUInt64();
            result.AssetDBChecksums[i] = QTuple.Create(new AssetGuid(guidRaw), crc64);
          }
        }
      }

      // read sim config
      {
        int count = reader.ReadInt32();
        if (count > 0) {
          var configBytes = reader.ReadBytes(count);
          result.SimulationConfig = game.AssetSerializer.AssetFromByteArray<SimulationConfig>(configBytes);
        }
      }

      result.DeserializeUser(game, reader);

      return result;
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Game/EventDispatcher.cs

namespace Quantum {
  using System;
  using System.Collections.Generic;

  public class EventDispatcher : DispatcherBase, IEventDispatcher {

    private static Dictionary<Type, Int32> GetEventTypes() {
      var result = new Dictionary<Type, Int32> {
        { typeof(EventBase), 0 }
      };

      for (int eventID = 0; eventID < Frame.FrameEvents.EVENT_TYPE_COUNT; ++eventID) {
        result.Add(Frame.FrameEvents.GetEventType(eventID), eventID + 1);
      }

      return result;
    }

    public EventDispatcher() : base(GetEventTypes()) { }

    public unsafe bool Publish(EventBase e) {

      int eventDepth = 0;
      for (int id = e.Id; id >= 0; id = Frame.FrameEvents.GetParentEventID(id)) {
        ++eventDepth;
      }

      int* eventIdStack = stackalloc int[eventDepth];
      for (int id = e.Id, i = 0; id >= 0; id = Frame.FrameEvents.GetParentEventID(id), i++) {
        eventIdStack[i] = id;
      }

      bool hadActiveHandlers = false;

      // start with the EventBase
      int metaIndex = 0;

      for (; ; ) {
        hadActiveHandlers |= base.InvokeMeta(metaIndex, e);

        if (--eventDepth >= 0) {
          // choose next event
          metaIndex = eventIdStack[eventDepth] + 1;
        } else {
          break;
        }
      }

      return hadActiveHandlers;
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Game/QuantumGame.cs

namespace Quantum {
  using System;
  using System.IO;
  using System.Collections.Generic;
  using System.Linq;
  using Photon.Deterministic;
  using Quantum.Core;
  using Quantum.Allocator;
  using Quantum.Profiling;

  /// <summary>
  /// QuantumGame acts as an interface to the simulation from the client code's perspective.
  /// </summary>
  /// Access and method to this class is always safe from the clients point of view.
  public unsafe partial class QuantumGame : IDeterministicGame {
    public event Action<ProfilerContextData> ProfilerSampleGenerated;

    /// <summary>
    /// Stores the different frames the simulation uses during one tick.
    /// </summary>
    public class FramesContainer {
      public Frame Verified;
      public Frame Predicted;
      public Frame PredictedPrevious;
      public Frame PreviousUpdatePredicted;
    }

    // Caveat: Only set after the first CreateFrame() call
    public class ConfigurationsContainer {
      public RuntimeConfig Runtime;
      public SimulationConfig Simulation;
    }

    /// <summary> Access the frames of various times available during one tick. </summary>
    public FramesContainer Frames { get; }

    /// <summary> Access the configurations that the simulation is running with. </summary>
    public ConfigurationsContainer Configurations { get; }

    /// <summary> Access the Deterministic session object to query more internals. </summary>
    public DeterministicSession Session { get; private set; }

    /// <summary> Used for position interpolation on the client for smoother interpolation results. </summary>
    public Single InterpolationFactor { get; private set; }

    /// <summary> </summary>
    public InstantReplaySettings InstantReplayConfig { get; private set; }

    /// <summary> </summary>
    public IAssetSerializer AssetSerializer { get; }

    public IResourceManager ResourceManager { get => _resourceManager; }

    /// <summary> Extra heaps to allocate for a session in case you need to create 'auxiliary' frames than actually required for the simulation itself. </summary>
    public int HeapExtraCount { get; }


    Byte[] _inputStreamReadZeroArray;
    IResourceManager _resourceManager;
    ICallbackDispatcher _callbackDispatcher;
    IEventDispatcher _eventDispatcher;

    FrameSerializer _inputSerializerRead;
    FrameSerializer _inputSerializerWrite;

    SystemBase[] _systemsRoot;
    SystemBase[] _systemsAll;

    FrameContext _context;
    TypeRegistry _typeRegistry;
    bool _polledInputInThisSimulation;
    DynamicAssetDB _initialDynamicAssets;
    int _flags;
    bool _gameStartedCalled;

    public QuantumGame(in QuantumGameStartParameters startParams) {
      _typeRegistry  = new TypeRegistry();
      Statics.RegisterSimulationTypes(_typeRegistry);
      
      Frames         = new FramesContainer();
      Configurations = new ConfigurationsContainer();

      _resourceManager      = startParams.ResourceManager;
      AssetSerializer       = startParams.AssetSerializer;
      _callbackDispatcher   = startParams.CallbackDispatcher;
      _eventDispatcher      = startParams.EventDispatcher;
      InstantReplayConfig   = startParams.InstantReplaySettings;
      HeapExtraCount        = startParams.HeapExtraCount;
      _flags                = startParams.GameFlags;
      _gameStartedCalled    = false;

      if (startParams.InitialDynamicAssets != null) {
        _initialDynamicAssets = new DynamicAssetDB();
        _initialDynamicAssets.CopyFrom(startParams.InitialDynamicAssets);
      }

      InitCallbacks();
    }

    /// <summary>
    /// Returns an array that is unique on every client and represents the global player indices that the local client controls in the Quantum simulation.
    /// </summary>
    /// <returns>Array of global players this client controls.</returns>
    public List<PlayerRef> GetLocalPlayers() {
      return Session.LocalPlayers;
    }

    /// <summary>
    /// Returns an array that is unique on every client and represents the player slots that the local client controls in the Quantum simulation.
    /// </summary>
    /// <returns>Array of local player slots  this client controls.</returns>
    public List<Int32> GetLocalPlayerSlots() {
      return Session.LocalPlayerSlots;
    }

    /// <summary>
    ///  Helps to decide if a PlayerRef is associated with the local player.
    /// </summary>
    /// <param name="playerRef">Player reference</param>
    /// <returns>True if the player is the local player</returns>
    public Boolean PlayerIsLocal(PlayerRef playerRef) {
      if (playerRef == PlayerRef.None) {
        return false;
      }

      return Session.IsPlayerLocal(playerRef);
    }

    /// <summary>
    /// Sends a command to the server.
    /// </summary>
    /// <param name="command">Command to send</param>
    /// Commands are similar to input, they drive the simulation, but do not have to be sent regularly.
    /// <example><code>
    /// RemoveUnitCommand command = new RemoveUnitCommand();
    /// command.CellIndex = 42;
    /// QuantumRunner.Default.Game.SendCommand(command);
    /// </code></example>
    public void SendCommand(DeterministicCommand command) {
      Session.SendCommand(0, command);
    }

    /// <summary>
    /// Sends a command to the server.
    /// </summary>
    /// <param name="playerSlot">Specify the local player index when you have multiple players controlled from the same machine.</param>
    /// <param name="command">Command to send</param>
    /// <para>See <see cref="SendCommand(DeterministicCommand)"/></para>
    /// <para>Games that only have one local player can ignore the player index field.</para>
    public void SendCommand(Int32 playerSlot, DeterministicCommand command) {
      Session.SendCommand(playerSlot, command);
    }

    /// <summary>
    /// Send data for the local player to join the online match.
    /// If the client has multiple local players, the data will be sent for the first player set.
    /// </summary>
    /// <param name="data">Player data</param>
    /// After starting, joining the Quantum Game and after the OnGameStart signal has been fired each player needs to call the AddPlayer method to be added as a player in every ones simulation.\n
    /// The reason this needs to be called explicitly is that it greatly simplifies late-joining players.
    public void AddPlayer(RuntimePlayer data) {
      Session.AddPlayer(0, AssetSerializer.PlayerToByteArray(data, compress: true));
    }

    /// <summary>
    /// Send data for one local player to join the online match.
    /// </summary>
    /// <param name="playerSlot">Local player index</param>
    /// <param name="data">Player data</param>
    /// After starting, joining the Quantum Game and after the OnGameStart signal has been fired each player needs to call the AddPlayer method to be added as a player in every ones simulation.\n
    /// The reason this needs to be called explicitly is that it greatly simplifies late-joining players.
    public void AddPlayer(Int32 playerSlot, RuntimePlayer data) {
      Session.AddPlayer(playerSlot, AssetSerializer.PlayerToByteArray(data, compress: true));
    }

    /// <summary>
    /// Remove the player. Assuming there is only one local player that this client controls.
    /// </summary>
    public void RemovePlayer() {
      Session.RemovePlayer(0);
    }

    /// <summary>
    /// Remove a player slot from the game.
    /// </summary>
    /// <param name="playerSlot">Local player</param>
    public void RemovePlayer(Int32 playerSlot) {
      Session.RemovePlayer(playerSlot);
    }

    /// <summary>
    /// Removes all players from the game and acts as a spectator.
    /// </summary>
    public void RemoveAllPlayers() {
      Session.RemovePlayer(-1);
    }

    /// <summary>
    /// <see cref="QuantumGameFlags"/>
    /// </summary>
    public int GameFlags => _flags;

    public void OnDestroy() {
      SnapshotsOnDestroy();
      InvokeOnDestroy();
      CheckTrackedHeapAllocations();
    }

    public Frame CreateFrame() {
      return (Frame)((IDeterministicGame)this).CreateFrame(_context);
    }

    DeterministicFrame IDeterministicGame.CreateFrame(IDisposable context) {
      return new Frame((FrameContextUser)context, _systemsAll, _systemsRoot, Session.SessionConfig, Configurations.Runtime, Configurations.Simulation, Session.DeltaTime);
    }

    DeterministicFrame IDeterministicGame.CreateFrame(IDisposable context, Byte[] data) {
      Frame f = CreateFrame();
      f.Deserialize(data);
      return f;
    }

    public DeterministicFrame GetVerifiedFrame(int tick) {
      if (_checksumSnapshotBuffer != null) {
        var result = _checksumSnapshotBuffer.Find(tick, DeterministicFrameSnapshotBufferFindMode.Equal);
        if (result == null) {
          Log.Warn($"Unable to find verified frame for tick {tick}, increase {nameof(DeterministicSessionConfig.ChecksumInterval)} or increase {nameof(SimulationConfig.ChecksumSnapshotHistoryLengthSeconds)}.");
        }
        return result;
      }
      return null;
    }

    public IDisposable CreateFrameContext() {
      if (_context == null) {
        Assert.Check(_systemsAll == null);
        Assert.Check(_systemsRoot == null);

        // create asset database
        var assetDB = new AssetDB(_resourceManager);

        // de-serialize runtime config, session is the one from the server
        Configurations.Runtime = AssetSerializer.ConfigFromByteArray<RuntimeConfig>(Session.RuntimeConfig, compressed: true);

        // find simulation config
        var simulationConfig = Configurations.Runtime.SimulationConfig;
        if (simulationConfig.Id.IsValid == false) {
          throw new ArgumentException("No SimulationConfig set. Register one with the RuntimeConfig that the simulation is started with.");
        }
        Configurations.Simulation = (SimulationConfig)assetDB.FindAsset(simulationConfig.Id);
        Assert.Always(Configurations.Simulation != null, "RuntimeConfig.SimulationConfig with asset id {0} was not found", simulationConfig.Id);

        // register commands
        Session.CommandSerializer.RegisterFactories(DeterministicCommandSetup.GetCommandFactories(Configurations.Runtime, Configurations.Simulation));

        var systemsConfig = (SystemsConfig)assetDB.FindAsset(Configurations.Runtime.SystemsConfig.Id);

        // initialize systems
        _systemsRoot = DeterministicSystemSetup.CreateSystems(Configurations.Runtime, Configurations.Simulation, systemsConfig).Where(x => x != null).ToArray();
        _systemsAll = _systemsRoot.SelectMany(x => x.Hierarchy).ToArray();

        // the simulator creates at least one frame (Verified)
        Int32 heapCount = 1;

        // additional frame (Predicted) in predicted sessions
        if (Session.IsPredicted) {
          heapCount++;
        }

        // additional frame (Previous) in interpolatable sessions
        if (Session.IsInterpolatable) {
          heapCount++;
        }

        // additional frame (Previous Update Predicted) if the session is both predicted and interpolatable
        if (Session.IsPredicted && Session.IsInterpolatable) {
          heapCount++;
        }

        heapCount += Math.Max(0, Configurations.Simulation.HeapExtraCount);
        heapCount += Math.Max(0, HeapExtraCount);
        heapCount += SnapshotsCreateBuffers(Session.SessionConfig.UpdateFPS,
          Session.IsOnline ? Session.SessionConfig.ChecksumInterval : 0, Configurations.Simulation.ChecksumSnapshotHistoryLengthSeconds,
          InstantReplayConfig.SnapshotsPerSecond == 0 ? 0 : Session.SessionConfig.UpdateFPS / InstantReplayConfig.SnapshotsPerSecond, InstantReplayConfig.LenghtSeconds);

        // set system runtime indices
        for (Int32 i = 0; i < _systemsAll.Length; ++i) {
          _systemsAll[i].RuntimeIndex = i;
        }

        // set core count override
        Session.PlatformInfo.CoreCount = Configurations.Simulation.ThreadCount;

        FrameContext.Args args;
        args.AssetDatabase               = assetDB;
        args.PlatformInfo                = Session.PlatformInfo;
        args.IsServer                    = (_flags & QuantumGameFlags.Server) == QuantumGameFlags.Server;
        args.IsLocalPlayer               = Session.IsLocalPlayer;
        args.HeapConfig                  = new Heap.Config(Configurations.Simulation.HeapPageShift, Configurations.Simulation.HeapPageCount, heapCount);
#if DEBUG  
        args.HeapTrackingMode            = Configurations.Simulation.HeapTrackingMode;
#else
        args.HeapTrackingMode            = HeapTrackingMode.Disabled;
#endif
        args.PhysicsConfig               = Configurations.Simulation.Physics;
        args.NavigationConfig            = Configurations.Simulation.Navigation;
        args.CommandSerializer           = Session.CommandSerializer;
        args.AssetSerializer             = AssetSerializer;
        args.InitialDynamicAssets        = _initialDynamicAssets;
        args.UseSharedChecksumSerialized = (_flags & QuantumGameFlags.DisableSharedChecksumSerializer) != QuantumGameFlags.DisableSharedChecksumSerializer;
        args.IsTaskProfilerEnabled       = (_flags & QuantumGameFlags.EnableTaskProfiler) == QuantumGameFlags.EnableTaskProfiler;

        // toggle various parts of the context code
        args.UsePhysics2D   = _systemsAll.FirstOrDefault(x => x is PhysicsSystem2D) != null;
        args.UsePhysics3D   = _systemsAll.FirstOrDefault(x => x is PhysicsSystem3D) != null;
        args.UseNavigation  = _systemsAll.FirstOrDefault(x => x is NavigationSystem) != null;
        args.UseCullingArea = _systemsAll.FirstOrDefault(x => x is CullingSystem2D) != null || _systemsAll.FirstOrDefault(x => x is CullingSystem3D) != null;

        // create frame context
        _context = new FrameContextUser(args);
      }

      return _context;
    }

    /// <summary>
    /// Set the prediction area.
    /// </summary>
    /// <param name="position">Center of the prediction area</param>
    /// <param name="radius">Radius of the prediction area</param>
    /// <para>The Prediction Culling feature must be explicitly enabled in <see cref="SimulationConfig.UsePredictionArea"/>.</para>
    /// <para>This can be safely called from the main-thread.</para>
    /// <para>Prediction Culling allows developers to save CPU time in games where the player has only a partial view of the game scene.
    /// Quantum prediction and rollbacks, which are time consuming, will only run for important entities that are visible to the local player(s). Leaving anything outside that area to be simulated only once per tick with no rollbacks as soon as the inputs are confirmed from server.
    /// It is safe and simple to activate and, depending on the game, the performance difference can be quite large.Imagine a 30Hz game to constantly rollback ten ticks for every confirmed input (with more players, the predictor eventually misses at least for one of them). This requires the game simulation to be lightweight to be able to run at almost 300Hz(because of the rollbacks). With Prediction Culling enabled the full frames will be simulated at the expected 30Hz all the time while the much smaller prediction area is the only one running within the prediction buffer.</para>
    public void SetPredictionArea(FPVector3 position, FP radius) {
      _context.SetPredictionArea(position, radius);
    }

    /// <summary>
    /// See <see cref="SetPredictionArea(FPVector3, FP)"/>.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    public void SetPredictionArea(FPVector2 position, FP radius) {
      _context.SetPredictionArea(position.XOY, radius);
    }

    public void OnGameEnded() {
      InvokeOnGameEnded();
    }

    public void OnGameStart(DeterministicFrame f) {
      // init event invoker
      InitEventInvoker(Session.RollbackWindow);

      Frames.Predicted = (Frame)f;
      Frames.PredictedPrevious = (Frame)f;
      Frames.Verified = (Frame)f;
      Frames.PreviousUpdatePredicted = (Frame)f;

      _context.OnGameStart(f);

      InvokeOnGameInit(Session.IsPaused);

      // init systems on latest frame
      InitSystems(f);

      if (Session.IsPaused == false) {
        // wait for the start callback when waiting for a snapshot
        TryInvokeOnGameStart(isResync: false);
      }

      // invoke events from OnInit/OnEnabled
      InvokeEvents();
    }

    public void OnGameResync() {
      _checksumSnapshotBuffer?.Clear();
      ReplayToolsOnGameResync();

      // reset physics engines statics
      Frames.Verified.ResetPhysics();

      // events won't get confirmed
      CancelPendingEvents();

      InvokeOnGameResync();

      // OnGameResync() is also used for seeking in replay, only the first call is the actual game start event
      TryInvokeOnGameStart(true);
    }

    private void TryInvokeOnGameStart(bool isResync) {
      if (_gameStartedCalled == false) {
        InvokeOnGameStart(isResync);
        _gameStartedCalled = true;
      }
    }

    public DeterministicFrameInputTemp OnLocalInput(Int32 frame, Int32 playerSlot) {
      var input = default(QTuple<Input, DeterministicInputFlags>);

      // poll input
      try {
        bool isFirst = _polledInputInThisSimulation == false;
        _polledInputInThisSimulation = true;
        input = InvokeOnPollInput(frame, playerSlot, isFirst);
      } catch (Exception exn) {
        Log.Exception("## Input Code Threw Exception ##", exn);
      }

      if (_inputSerializerWrite == null) {
        _inputSerializerWrite = new FrameSerializer(DeterministicFrameSerializeMode.Serialize, null, new Byte[1024]);
      }

      // clear old data
      _inputSerializerWrite.Reset();
      _inputSerializerWrite.Writing = true;
      _inputSerializerWrite.InputMode = true;

      // pack into stream
      Input.Write(_inputSerializerWrite, input.Item0);

      // return temp input
      return DeterministicFrameInputTemp.Predicted(frame, playerSlot, _inputSerializerWrite.Stream.Data, _inputSerializerWrite.Stream.BytesRequired, input.Item1);
    }

    public void OnSerializedInput(byte* encoded, Array result) {
      _inputSerializerWrite.Reset();
      _inputSerializerWrite.Writing = true;
      _inputSerializerWrite.InputMode = true;
      var input = *((Input*)encoded);
      // pack into stream
      Input.Write(_inputSerializerWrite, input);
      // write into shared input buffer
      Buffer.BlockCopy(_inputSerializerWrite.Stream.Data, 0, result, 0, _inputSerializerWrite.Stream.BytesRequired);
    }

    public void OnSimulate(DeterministicFrame state) {
      HostProfiler.Start("QuantumGame.OnSimulate");

      var f = (Frame)state;

      try {
        // reset profiling
        HostProfiler.Start("Init Profiler");
        f.Context.ProfilerContext.Reset();
        var profiler = f.Context.ProfilerContext.GetProfilerForTaskThread(0);
        HostProfiler.End();

        HostProfiler.Start("ApplyInputs");
        //ApplyInputs(f);
        ApplyInputs(f);
        HostProfiler.End();

        HostProfiler.Start("OnSimulateBegin");
        f.Context.OnFrameSimulationBegin(f);
        f.OnFrameSimulateBegin();
        f.Context.TaskContext.BeginFrame(f);
        HostProfiler.End();

        var handle = f.Context.TaskContext.AddRootTask();

        HostProfiler.Start("UpdatePlayerData");
        f.UpdatePlayerData(this);
        HostProfiler.End();

        profiler.Start("Scheduling Tasks #ff9900");
        HostProfiler.Start("Scheduling Tasks");
        
        for (Int32 i = 0; i < _systemsRoot.Length; ++i) {
          if (f.SystemIsEnabledSelf(_systemsRoot[i])) {
            try {
              handle = _systemsRoot[i].OnSchedule(f, handle);
            } catch (Exception exn) {
              LogSimulationException(exn);
            }
          }
        }

        HostProfiler.End();
        profiler.End();

        try {
          f.Context.TaskContext.EndFrame();
          f.OnFrameSimulateEnd();
          f.Context.OnFrameSimulationEnd();
        } catch (Exception exn) {
          Log.Exception(exn);
        }

        if (ProfilerSampleGenerated != null) {
          var data = f.Context.ProfilerContext.CreateReport(f.Number, f.IsVerified);
          ProfilerSampleGenerated(data);
        }

#if PROFILER_FRAME_AVERAGE
      f.Context.ProfilerContext.StoreFrameTime();
      Log.Info("Frame Average: " +  f.Context.ProfilerContext.GetFrameTimeAverage());
#endif
      } catch (Exception exn) {
        LogSimulationException(exn);
      }

      HostProfiler.End();
    }

    public void OnSimulateFinished(DeterministicFrame state) {
      SnapshotsOnSimulateFinished(state);
      InvokeOnSimulateFinished(state);
    }

    public void OnUpdateDone() {
      Frames.Predicted = (Frame)Session.FramePredicted;
      Frames.PredictedPrevious = (Frame)Session.FramePredictedPrevious;
      Frames.Verified = (Frame)Session.FrameVerified;
      Frames.PreviousUpdatePredicted = (Frame)Session.PreviousUpdateFramePredicted;

      if (Session.IsStalling == false) {
        var f = (float)(Session.AccumulatedTime / Frames.Predicted.DeltaTime.AsFloat);
        InterpolationFactor = f < 0.0f ? 0.0f : f > 1.0f ? 1.0f : f; // Clamp01
      }

      InvokeOnUpdateView();
      InvokeEvents();
    }

    public void AssignSession(DeterministicSession session) {
      Session = session;

      DeterministicSessionConfig sessionConfig;
      Session.GetLocalConfigs(out sessionConfig, out _);

      // verify player count is in correct range
      if (sessionConfig.PlayerCount < 1 || sessionConfig.PlayerCount > Quantum.Input.MAX_COUNT) {
        throw new Exception(String.Format("Invalid player count {0} (needs to be in 1-{1} range)", sessionConfig.PlayerCount, Quantum.Input.MAX_COUNT));
      }

      // verify all types
      var verifier = new MemoryLayoutVerifier(MemoryLayoutVerifier.Platform ?? new MemoryLayoutVerifier.DefaultPlatform());
      var result = verifier.Verify(_typeRegistry.Types);
      if (result.Count > 0) {
        throw new Exception("MemoryIntegrity Check Failed: " + System.Environment.NewLine + String.Join(System.Environment.NewLine, result.ToArray()));
      } else {
        Log.Debug("Memory Integrity Verified");
      }
    }

    public void OnChecksumError(DeterministicTickChecksumError error, DeterministicFrame[] frames) {
      InvokeOnChecksumError(error, frames);
    }

    public void OnChecksumComputed(Int32 frame, ulong checksum) {
      InvokeOnChecksumComputed(frame, checksum);
      ReplayToolsOnChecksumComputed(frame, checksum);
    }

    public void OnSimulationEnd() {
      _context.OnSimulationEnd();
    }

    public void OnSimulationBegin() {
      _polledInputInThisSimulation = false;
      _context.OnSimulationBegin();
    }

    public void OnInputConfirmed(DeterministicFrameInputTemp input) {
      InvokeOnInputConfirmed(input);
      ReplayToolsOnInputConfirmed(input);
    }

    public void OnInputSetConfirmed(int tick, int length, byte[] data) {
      ReplayToolsOnInputSetConfirmed(tick, length, data);
    }

    public void OnChecksumErrorFrameDump(int actorId, int frameNumber, DeterministicSessionConfig sessionConfig, byte[] runtimeConfig, byte[] frameData, byte[] extraData) {
      InvokeOnChecksumErrorFrameDump(actorId, frameNumber, sessionConfig, runtimeConfig, frameData, extraData, AssetSerializer);
    }

    public void OnPluginDisconnect(string reason) {
      Log.Error("DISCONNECTED: " + reason);
      InvokeOnPluginDisconnect(reason);
    }

    public void OnLocalPlayerAddConfirmed(DeterministicFrame frame, int playerSlot, PlayerRef player) {
      Log.Debug($"Player Added Locally {player} at frame {frame.Number}");
      InvokeOnLocalPlayerAddConfirmed((Frame)frame, playerSlot, player);
    }

    public void OnLocalPlayerRemoveConfirmed(DeterministicFrame frame, int playerSlot, PlayerRef player) {
      InvokeOnLocalPlayerRemoveConfirmed((Frame)frame, playerSlot, player);
    }

    public void OnLocalPlayerAddFailed(int playerSlot, string message) {
      InvokeOnLocalPlayerAddFailed(playerSlot, message);
    }

    public void OnLocalPlayerRemoveFailed(int playerSlot, string message) {
      InvokeOnLocalPlayerRemoveFailed(playerSlot, message);
    }

    public int GetInputInMemorySize() {
      return sizeof(Input);
    }

    public Int32 GetInputSerializedFixedSize() {
      var stream = new FrameSerializer(DeterministicFrameSerializeMode.Serialize, null, 1024);
      stream.Writing = true;
      stream.InputMode = true;
      Input.Write(stream, new Input());
      return stream.ToArray().Length;
    }

    void InitSystems(DeterministicFrame df) {
      var f = (Frame)df;

      try {
        f.Context.OnFrameSimulationBegin(f);

        // call init on ALL systems
        for (Int32 i = 0; i < _systemsAll.Length; ++i) {
          try {
            _systemsAll[i].OnInit(f);

            if (f.CommitCommandsMode == CommitCommandsModes.InBetweenSystems) {
              f.Unsafe.CommitAllCommands();
            }
          } catch (Exception exn) {
            LogSimulationException(exn);
          }
        }

        // TODO: this seems like a good place to fire OnMapChanged,
        // if we want to do that for the initial map

        // call OnEnabled on all systems which start enabled
        for (Int32 i = 0; i < _systemsRoot.Length; ++i) {
          if (_systemsRoot[i].StartEnabled) {
            try {
              _systemsRoot[i].OnEnabled(f);
              
              if (f.CommitCommandsMode == CommitCommandsModes.InBetweenSystems) {
                f.Unsafe.CommitAllCommands();
              }
            } catch (Exception exn) {
              LogSimulationException(exn);
            }
          }
        }

        f.Context.OnFrameSimulationEnd();
      } catch (Exception e) {
        LogSimulationException(e);
      }
    }

    public void DeserializeInputInto(int player, byte[] data, byte* buffer, bool verified) {
      if (_inputSerializerRead == null) {
        _inputStreamReadZeroArray = new Byte[1024];
        _inputSerializerRead = new FrameSerializer(DeterministicFrameSerializeMode.Serialize, null, new Byte[1024]);
      }

      _inputSerializerRead.Reset();
      _inputSerializerRead.Frame = null;
      _inputSerializerRead.Reading = true;
      _inputSerializerRead.InputMode = true;

      if (data == null || data.Length == 0) {
        _inputSerializerRead.CopyFromArray(_inputStreamReadZeroArray);
      } else {
        _inputSerializerRead.CopyFromArray(data);
      }

      try {
        *(Input*)buffer = Input.Read(_inputSerializerRead);
      } catch (Exception exn) {
        *(Input*)buffer = default;

        // log exception
        Log.Error($"Received invalid input data from player {player}, could not deserialize.");
        Log.Exception(exn);
      }
    }

    void ApplyInputs(Frame f) {
      for (Int32 i = 0; i < Session.PlayerCount; i++) {
        var raw = f.GetRawInput(i);
        if (raw == null) {
          Log.Error($"Got null input for player {i}");
        } else {
          f.SetPlayerInput(i, *(Input*)raw);
        }
      }
    }

    Boolean ReadInputFromStream(out Input input) {
      try {
        input = Input.Read(_inputSerializerRead);
        return true;
      } catch {
        input = default(Input);
        return false;
      }
    }

    void LogSimulationException(Exception exn) {
      Log.Exception("## Simulation Code Threw Exception ##", exn);
    }

    public byte[] GetExtraErrorFrameDumpData(DeterministicFrame frame) {
      using (var stream = new MemoryStream()) {
        using (var writer = new BinaryWriter(stream)) {
          var data = new ChecksumErrorFrameDumpContext(this, (Frame)frame);
          data.Serialize(this, writer);
        }
        return stream.ToArray();
      }
    }

    public void CheckTrackedHeapAllocations() {
      if (_context == null) {
        return;
      }

      switch (_context.HeapTrackingMode) {
        case HeapTrackingMode.Disabled:
          break;

        case HeapTrackingMode.DetectLeaks:
        case HeapTrackingMode.TraceAllocations:
          // verified heap tracker must have been hooked to the context tracker
          Assert.Check(Frames.Verified.Heap.Tracker.Equals(_context.HeapTracker));

          Frames.Verified.Serialize(DeterministicFrameSerializeMode.Serialize, new byte[20 * 1024 * 1024], out var frameSerializer);
          Frames.Verified.Heap.Tracker.CheckAllocationsDebug(frameSerializer.GetSerializedPtrs());
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Game/QuantumGame.EventDispatcher.cs

namespace Quantum {
  using Quantum.Profiling;
  using System;
  using System.Collections.Generic;

  public partial class QuantumGame {

    Dictionary<EventKey, bool> _eventsTriggered; 
    Queue<EventKey> _eventsConfirmationQueue;


    public int EventWaitingForConfirmationCount => _eventsConfirmationQueue.Count;

    void InitEventInvoker(Int32 size) {
      // how many events per frame without a resize
      const int EventsPerTickHeuristic = 50;
      _eventsTriggered = new Dictionary<EventKey, bool>(size * EventsPerTickHeuristic);
      _eventsConfirmationQueue = new Queue<EventKey>(size * EventsPerTickHeuristic);
    }

    void RaiseEvent(EventBase evnt) {
      HostProfiler.Start("QuantumGame.InvokeEvents");
      try {
        evnt.Game = this;
        _eventDispatcher?.Publish(evnt);
      } catch (Exception exn) {
        Log.Exception("## Event Callback Threw Exception ##", exn);
      }
      HostProfiler.End();
    }

    void CancelPendingEvents() {
      while (_eventsConfirmationQueue.Count > 0) {
        var key = _eventsConfirmationQueue.Dequeue();
        _eventsTriggered.Remove(key);
        InvokeOnEvent(key, false);
      }
      _eventsTriggered.Clear();
    }


    void InvokeEvents() {
      HostProfiler.Start("QuantumGame.InvokeEvents");
      while (_context.Events.Count > 0) {
        var head = _context.Events.PopHead();
        try {
          if (head.Synced) {
            if (Session.IsFrameVerified(head.Tick)) {
              RaiseEvent(head);
            }
          } else {
            // calculate hash code
            var key = new EventKey(head.Tick, head.Id, head.GetHashCode());

            // if frame is verified, CONFIRM the event in the temp collection of hashes
            bool confirmed = Session.IsFrameVerified(head.Tick);

            // if this was already raised, do nothing
            if (!_eventsTriggered.TryGetValue(key, out var alreadyConfirmed)) {
              // dont trigger this again
              _eventsTriggered.Add(key, confirmed);
              // trigger event
              RaiseEvent(head);
              // enqueue confirmation
              _eventsConfirmationQueue.Enqueue(key);
            } else if (confirmed && !alreadyConfirmed) {
              // confirm this event is definitive...
              _eventsTriggered[key] = confirmed;
            }
          }
        } finally {
          _context.ReleaseEvent(head);
        }
      }

      // invoke confirmed/canceled event callbacks
      while (_eventsConfirmationQueue.Count > 0) {

        var key = _eventsConfirmationQueue.Peek();

        // need to wait; this will block confirmations from resimulations to maintain order
        if (!Session.IsFrameVerified(key.Tick)) {
          Assert.Check(key.Tick <= Session.FrameVerified.Number + Session.RollbackWindow);
          break;
        }

        var confirmed = _eventsTriggered[key];
        _eventsTriggered.Remove(key);
        _eventsConfirmationQueue.Dequeue();

        InvokeOnEvent(key, confirmed);
      }
      HostProfiler.End();
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Game/QuantumGame.ReplayTools.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  using System.Collections.Generic;
  using System.IO;

  public partial class QuantumGame {

    public InputProvider RecordedInputs { get; private set; }
    public ChecksumFile RecordedChecksums { get; private set; }
    public Stream RecordInputStream { get; set; }
    
    ChecksumFile _checksumsToVerify;

    public Frame GetInstantReplaySnapshot(int frame) {
      if (!_instantReplaySnapshotsRecording) {
        Log.Error("Can't find any recorded snapshots. Use StartRecordingSnapshots to start recording.");
        return null;
      }

      var buffer = (_commonSnapshotInterval > 0 ? _checksumSnapshotBuffer : _instantReplaySnapshotBuffer);
      Assert.Check(buffer != null);

      var result = buffer.Find(frame, DeterministicFrameSnapshotBufferFindMode.ClosestLessThanOrEqual);
      if (result == null) {
        result = buffer.Find(frame, DeterministicFrameSnapshotBufferFindMode.Closest);
        if (result == null) {
          Log.Warn($"Unable to find a replay snapshot for frame {frame}. No snapshots were saved.");
        } else {
          Log.Warn($"Unable to find a replay snapshot for frame {frame} or earlier. The closest match is {result.Number}. Increase the max replay length.");
        }
      }

      return (Frame)result;
    }

    public void GetInstantReplaySnapshots(int startFrame, int endFrame, List<Frame> frames) {
      if (!_instantReplaySnapshotsRecording) {
        Log.Error("Can't find any recorded snapshots. Use StartRecordingSnapshots to start recording.");
        return;
      }

      var buffer = (_commonSnapshotInterval > 0 ? _checksumSnapshotBuffer : _instantReplaySnapshotBuffer);
      Assert.Check(buffer != null);

      var firstFrame = buffer.Find(startFrame, DeterministicFrameSnapshotBufferFindMode.ClosestLessThanOrEqual);
      var minFrameNumber = firstFrame?.Number ?? startFrame;

      foreach (Frame frame in buffer.Data) {
        if (frame == null) {
          continue;
        }

        if (frame.Number >= minFrameNumber && frame.Number <= endFrame)
          frames.Add(frame);
      }
    }

    public QuantumReplayFile CreateSavegame(bool includeDb = false) {
      if (Frames.Verified == null) {
        Log.Error("Cannot create a savegame. Frames verified not found.");
        return null;
      }

      var result = new QuantumReplayFile {
        DeterministicConfig = Frames.Verified.SessionConfig,
        RuntimeConfigData = QuantumJsonFriendlyDataBlob.Encode(AssetSerializer.ConfigToByteArray(Frames.Verified.RuntimeConfig, compress: true), isCompressed: false, asBase64String: true),
        LastTick = Frames.Verified.Number,
        InitialFrameData = Frames.Verified.Serialize(DeterministicFrameSerializeMode.Serialize)
      };

      if (includeDb) {
        using (var stream = new MemoryStream()) {
          AssetSerializer.SerializeAssets(stream, ResourceManager.LoadAllAssets().ToArray());
          result.AssetDatabaseData = QuantumJsonFriendlyDataBlob.Encode(stream.ToArray(), isCompressed: true, asBase64String: true);
        }
      }

      return result;
    }

    public QuantumReplayFile GetRecordedReplay(
      bool includeChecksums = false, 
      bool includeDb = false,
      QuantumJsonFriendlyDataBlob.Encoder customAssetDbSerializer = null,
      QuantumJsonFriendlyDataBlob.Encoder customRuntimeConfigSerializer = null,
      QuantumJsonFriendlyDataBlob.Encoder customInputSerializer = null) {
      if (Frames.Verified == null) {
        Log.Error("Cannot create a replay. Frames current or verified are not valid, yet.");
        return null;
      }

      if (RecordedInputs == null && RecordInputStream == null) {
        Log.Error("Cannot create a replay, because no recorded input was found. Use StartRecordingInput to start recording or setup RecordingFlags.");
        return null;
      }

      // If the recorded input stream is a memory stream convert to ReplayFile in place.
      // Otherwise developer has to follow up on the stream.
      var inputHistoryRaw = default(byte[]);
      if (RecordInputStream != null) {
        if (RecordInputStream is MemoryStream memorySteam) {
          memorySteam.Flush();
          inputHistoryRaw = memorySteam.ToArray();
        }
      }

      var verifiedFrame = Frames.Verified.Number;
      var runtimeConfigBytes = AssetSerializer.ConfigToByteArray(Frames.Verified.RuntimeConfig, compress: true);

      var result = new QuantumReplayFile {
        DeterministicConfig = Frames.Verified.SessionConfig,
        RuntimeConfigData = customRuntimeConfigSerializer?.Invoke(runtimeConfigBytes) ?? QuantumJsonFriendlyDataBlob.Encode(runtimeConfigBytes, isCompressed: false, asBase64String: true),
        InputHistoryDeltaCompressed = customInputSerializer?.Invoke(inputHistoryRaw) ?? QuantumJsonFriendlyDataBlob.Encode(inputHistoryRaw, isCompressed: true, asBase64String: true),
        InputHistoryLegacy = RecordedInputs?.ExportToList(verifiedFrame),
        LastTick = verifiedFrame,
        InitialTick = Session.InitialTick,
        InitialFrameData = Session.IntitialFrameData,
        Checksums = includeChecksums ? RecordedChecksums.Clone() : null
      };

      if (includeDb) {
        using (var stream = new MemoryStream()) {
          AssetSerializer.SerializeAssets(stream, ResourceManager.LoadAllAssets().ToArray());
          var bytes = stream.ToArray();
          result.AssetDatabaseData = customAssetDbSerializer?.Invoke(bytes) ?? QuantumJsonFriendlyDataBlob.Encode(bytes, isCompressed: true, asBase64String: true);
        }
      }

      return result;
    }

    private void ReplayToolsOnInputConfirmed(DeterministicFrameInputTemp input) {
      if (RecordedInputs == null) {
        return;
      }

      RecordedInputs.OnInputConfirmed(this, input);
    }

    private void ReplayToolsOnInputSetConfirmed(int tick, int length, byte[] data) {
      if (RecordInputStream == null) {
        return;
      }

      RecordInputStream.Write(BitConverter.GetBytes(length), 0, 4);
      RecordInputStream.Write(data, 0, length);
    }

    private void ReplayToolsOnGameResync() {
      _instantReplaySnapshotBuffer?.Clear();
      RecordedInputs?.Clear(Frames.Verified.Number);
      RecordedChecksums?.Clear();
    }

    private void ReplayToolsOnChecksumComputed(Int32 frame, ulong checksum) {
      if (RecordedChecksums != null) {
        RecordedChecksums.RecordChecksum(this, frame, checksum);
      }
      if (_checksumsToVerify != null) {
        _checksumsToVerify.VerifyChecksum(this, frame, checksum);
      }
    }

    public void StartRecordingInput(Int32? startFrame = null) {
      if (Session == null) {
        Log.Error("Can't start input recording, because the session is invalid. Wait for the OnGameStart callback.");
        return;
      }

      if (Session.SessionConfig.InputDeltaCompression) {
        if (RecordInputStream == null) {
          // Create memory stream when no input stream was set
          RecordInputStream = new MemoryStream(1024 * 1024);
        }
      } else {
        if (RecordedInputs == null) {
          if (startFrame.HasValue) {
            RecordedInputs = new InputProvider(Session.SessionConfig.PlayerCount, startFrame.Value, 60 * 60, 0);
          } else {
            // start frame is the session RollbackWindow
            RecordedInputs = new InputProvider(Session.SessionConfig);
          }
        }
      }
      Log.Info("QuantumGame.ReplayTools: Input recording started");
    }

    public void StartRecordingChecksums() {
      if (RecordedChecksums == null) {
        RecordedChecksums = new ChecksumFile();
        Log.Info("QuantumGame.ReplayTools: Checksum recording started");
      }
    }

    public void StartVerifyingChecksums(ChecksumFile checksums) {
      if (_checksumsToVerify == null) {
        _checksumsToVerify = checksums;
        Log.Info("QuantumGame.ReplayTools: Checksum verification started");
      }
    }

    public void StartRecordingInstantReplaySnapshots() {
      if (_instantReplaySnapshotsRecording) {
        return;
      }

      if (InstantReplayConfig.LenghtSeconds <= 0 || InstantReplayConfig.SnapshotsPerSecond <= 0) {
        Assert.Check(_instantReplaySnapshotBuffer == null);
        Assert.Check(_commonSnapshotInterval <= 0);
        Log.Error($"Can't start recording replay snapshots with these settings: {InstantReplayConfig}");
        return;
      }

      _instantReplaySnapshotsRecording = true;
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Game/QuantumGame.Snapshots.cs

namespace Quantum {
  using System;
  using Photon.Deterministic;
  using Quantum.Profiling;

  public partial class QuantumGame {

    DeterministicFrameRingBuffer _checksumSnapshotBuffer;
    DeterministicFrameRingBuffer _instantReplaySnapshotBuffer;

    bool _instantReplaySnapshotsRecording;
    Int32 _commonSnapshotInterval;
    Int32 _instantReplaySnapshotInterval;

    void SnapshotsOnDestroy() {
      _checksumSnapshotBuffer?.Clear();
      _checksumSnapshotBuffer = null;
      _instantReplaySnapshotBuffer?.Clear();
      _instantReplaySnapshotBuffer = null;
    }

    void SnapshotsOnSimulateFinished(DeterministicFrame state) {

      if (!state.IsVerified) {
        return;
      }

      HostProfiler.Start("QuantumGame.RecordingSnapshots");

      if (_checksumSnapshotBuffer != null) {
        // in case replay interval is less than checksum interval and replay is not being recorded,
        // there's no need to sample at a common rate
        Int32 interval;
        if (_commonSnapshotInterval > 0 && _instantReplaySnapshotsRecording) {
          Assert.Check(_instantReplaySnapshotBuffer == null);
          interval = _commonSnapshotInterval;
        } else {
          interval = Session.SessionConfig.ChecksumInterval;
        }

        if ((state.Number % interval) == 0) {
          _checksumSnapshotBuffer.PushBack(state, this, _context);
        }
      }

      if (_instantReplaySnapshotsRecording && _instantReplaySnapshotBuffer != null) {
        Assert.Check(_commonSnapshotInterval <= 0);
        if (_instantReplaySnapshotBuffer.Count == 0 || (state.Number % _instantReplaySnapshotInterval) == 0) {
          _instantReplaySnapshotBuffer.PushBack(state, this, _context);
        }
      }

      HostProfiler.End();
    }

    Int32 SnapshotsCreateBuffers(Int32 simulationRate, Int32 checksumInterval, FP checksumTimeWindow, Int32 replayInterval, FP replayTimeWindow) {

      var checksumFrameWindow = FPMath.CeilToInt(simulationRate * checksumTimeWindow);
      var replayFrameWindow   = FPMath.CeilToInt(simulationRate * replayTimeWindow);

      var checksumBufferSize = DeterministicFrameRingBuffer.GetSize(FPMath.CeilToInt(simulationRate * checksumTimeWindow), checksumInterval);
      var replayBufferSize   = DeterministicFrameRingBuffer.GetSize(FPMath.CeilToInt(simulationRate * replayTimeWindow), replayInterval);

      if (checksumInterval > 0 && checksumBufferSize > 0 && replayBufferSize > 0 && replayInterval > 0) {
        if (DeterministicFrameRingBuffer.TryGetCommonSamplingPattern(checksumFrameWindow, checksumInterval, replayFrameWindow, replayInterval, out var commonWindow, out var commonInterval)) {
          _commonSnapshotInterval = commonInterval;
          _checksumSnapshotBuffer = new DeterministicFrameRingBuffer(DeterministicFrameRingBuffer.GetSize(commonWindow, commonInterval));
          Log.Trace($"Snapshots: common buffer created with interval: {_commonSnapshotInterval}, window: {commonWindow}, capacity: {_checksumSnapshotBuffer.Capacity}");
          return _checksumSnapshotBuffer.Capacity;
        } else {
          // shared buffer not possible
          Log.Warn($"Unable to create a shared buffer for checksumed frames and replay snapshots. This is not optimal. Check the documentation for details.");
        }
      }

      if (checksumBufferSize > 0) {
        _checksumSnapshotBuffer = new DeterministicFrameRingBuffer(checksumBufferSize);
        Log.Trace($"Snapshots: checksum buffer created with interval {checksumInterval}, capacity: {checksumBufferSize}");
      }

      if (replayBufferSize > 0) {
        _instantReplaySnapshotInterval = replayInterval;
        _instantReplaySnapshotBuffer = new DeterministicFrameRingBuffer(replayBufferSize);
        Log.Trace($"Snapshots: replay buffer created with interval {replayInterval}, capacity: {replayBufferSize}");
      }

      return checksumBufferSize + replayBufferSize;
    }

    static Int32 SnapshotsGetMinBufferSize(Int32 window, Int32 samplingRate) {
      return samplingRate <= 0 ? 0 : (1 + window / samplingRate);
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Game/QuantumGameCallbacks.cs

namespace Quantum {
  using System;
  using System.IO;
  using System.Text;
  using Photon.Deterministic;
  using Photon.Deterministic.Protocol;
  using Quantum.Profiling;

  public enum CallbackId {
    PollInput,
    GameInit,
    GameStarted,
    GameResynced,
    GameDestroyed,
    UpdateView,
    SimulateFinished,
    EventCanceled,
    EventConfirmed,
    ChecksumError,
    ChecksumErrorFrameDump,
    InputConfirmed,
    ChecksumComputed,
    PluginDisconnect,
    PlayerAddConfirmed,
    PlayerRemoveConfirmed,
    PlayerAddFailed,
    PlayerRemoveFailed,
    UserCallbackIdStart,
  }

  /// <summary>
  /// Callback called when the simulation queries local input.
  /// </summary>
  public sealed class CallbackPollInput : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.PollInput;
    internal CallbackPollInput(QuantumGame game) : base(ID, game) { }

    public Int32 Frame;
    public Int32 PlayerSlot;
    [Obsolete("Renamed to PlayerSlot because it's the local player slot instead of a global player.")]
    public Int32 Player {
      get { return PlayerSlot; }
      set { Frame = PlayerSlot; }
    }

    public void SetInput(Input input, DeterministicInputFlags flags) {
      IsInputSet = true;
      Input = input;
      Flags = flags;
    }

    public void SetInput(QTuple<Input, DeterministicInputFlags> input) {
      SetInput(input.Item0, input.Item1);
    }

    public bool IsFirstInThisUpdate { get; internal set; }
    public bool IsInputSet { get; internal set; }
    public Input Input { get; private set; }
    public DeterministicInputFlags Flags { get; private set; }
  }

  /// <summary>
  /// Callback called when the game has been started.
  /// </summary>
  public sealed class CallbackGameInit: QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.GameInit;
    public bool IsResync;
    internal CallbackGameInit(QuantumGame game) : base(ID, game) { }
  }

  /// <summary>
  /// Callback called when the game has been started.
  /// </summary>
  public sealed class CallbackGameStarted : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.GameStarted;
    public bool IsResync;
    internal CallbackGameStarted(QuantumGame game) : base(ID, game) { }
  }

  /// <summary>
  /// Callback called when the game has been re-synchronized from a snapshot.
  /// </summary>
  public sealed class CallbackGameResynced : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.GameResynced;
    internal CallbackGameResynced(QuantumGame game) : base(ID, game) { }
  }

  /// <summary>
  /// Callback called when the game was destroyed.
  /// </summary>
  public sealed class CallbackGameDestroyed : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.GameDestroyed;
    internal CallbackGameDestroyed(QuantumGame game) : base(ID, game) { }
  }

  /// <summary>
  /// Callback guaranteed to be called every rendered frame.
  /// </summary>
  public sealed class CallbackUpdateView : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.UpdateView;
    internal CallbackUpdateView(QuantumGame game) : base(ID, game) { }
  }

  /// <summary>
  /// Callback called when frame simulation has completed.
  /// </summary>
  public sealed class CallbackSimulateFinished : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.SimulateFinished;
    internal CallbackSimulateFinished(QuantumGame game) : base(ID, game) { }

    public Frame Frame;
  }

  /// <summary>
  /// Callback called when an event raised in a predicted frame was canceled in a verified frame due to a roll-back / missed prediction.
  /// Synchronised events are only raised on verified frames and thus will never be canceled; this is useful to graciously discard non-sync'ed events in the view.
  /// </summary>
  public sealed class CallbackEventCanceled : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.EventCanceled;
    internal CallbackEventCanceled(QuantumGame game) : base(ID, game) { }

    public EventKey EventKey;
  }

  /// <summary>
  /// Callback called when an event was confirmed by a verified frame.
  /// </summary>
  public sealed class CallbackEventConfirmed : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.EventConfirmed;
    internal CallbackEventConfirmed(QuantumGame game) : base(ID, game) { }

    public EventKey EventKey;
  }

  /// <summary>
  /// Callback called on a checksum error.
  /// </summary>
  public sealed class CallbackChecksumError : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.ChecksumError;
    internal CallbackChecksumError(QuantumGame game) : base(ID, game) { }

    public DeterministicTickChecksumError Error;
    internal DeterministicFrame[] _rawFrames;
    internal Frame[] _convertedFrame;

    public int FrameCount => Frames.Length;
    public Frame GetFrame(int index) => (Frame)Frames[index];

    public Frame[] Frames {
      get {
        if (_convertedFrame == null) {
          _convertedFrame = new Frame[_rawFrames.Length];
          for (int i = 0; i < _rawFrames.Length; ++i) {
            _convertedFrame[i] = (Frame)_rawFrames[i];
          }
        }
        return _convertedFrame;
      }
    }
  }

  /// <summary>
  /// Callback called when due to a checksum error a frame is dumped.
  /// </summary>
  public sealed class CallbackChecksumErrorFrameDump : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.ChecksumErrorFrameDump;
    internal CallbackChecksumErrorFrameDump(QuantumGame game) : base(ID, game) { }

    public Int32 ActorId;
    public Int32 FrameNumber;
    public Byte[] FrameData;
    public Byte[] RuntimeConfigBytes;
    public Byte[] ExtraBytes;
    public DeterministicSessionConfig SessionConfig;
    public IAssetSerializer Serializer;

    private Frame _frameToOverride;

    private Byte[] _overridenFrameData;
    private SimulationConfig _overridenSimulationConfig;
    private DeterministicSessionConfig _overridenSessionConfig;
    private RuntimeConfig _overridenRuntimeConfig;
    
    private QTuple<bool, string> _frameDump;
    private QTuple<bool, Frame> _frame;
    private QTuple<bool, RuntimeConfig> _runtimeConfig;
    private QTuple<bool, ChecksumErrorFrameDumpContext> _context;

    internal void Clear() {

      try {
        if (_overridenRuntimeConfig != null) {
          _frameToOverride.RuntimeConfig = _overridenRuntimeConfig;
        }
        if (_overridenSessionConfig != null) {
          _frameToOverride.SessionConfig = _overridenSessionConfig;
        }

        if (_overridenSimulationConfig != null) {
          _frameToOverride.SimulationConfig = _overridenSimulationConfig;
        }

        if (_overridenFrameData != null) {
          _frameToOverride.Deserialize(_overridenFrameData);
        }
      } finally {
        _frameToOverride = null;
        _overridenFrameData = null;
        _overridenSimulationConfig = null;
        _overridenSessionConfig = null;
        _overridenRuntimeConfig = null;

        _runtimeConfig = default;
        _context = default;
        _frame = default;
        _frameDump = default;
        SessionConfig = null;
        Serializer = null;
      }
    }

    public Frame Frame {
      get {
        if (!_frame.Item0) {
          _frame = QTuple.Create(true, (Frame)null);
          if (_frameToOverride != null) {
            var originalFrameData = _frameToOverride.Serialize(DeterministicFrameSerializeMode.Serialize);
            try {
              _frameToOverride.Deserialize(FrameData);
              _frame = QTuple.Create(true, _frameToOverride);
              _overridenFrameData = originalFrameData;
            } catch (System.Exception ex) {
              // revert to the old data
              Log.Warn($"Failed to deserilize dump frame. The snapshot will appear as raw data.\n{ex}");
              _frameToOverride.Deserialize(originalFrameData);
            }

            _overridenRuntimeConfig = _frameToOverride.RuntimeConfig;
            _overridenSessionConfig = _frameToOverride.SessionConfig;
            _overridenSimulationConfig = _frameToOverride.SimulationConfig;
            _frameToOverride.SessionConfig = SessionConfig;

            if (RuntimeConfig != null) {
              _frameToOverride.RuntimeConfig = RuntimeConfig;
            }

            if (SimulationConfig != null) {
              _frameToOverride.SimulationConfig = SimulationConfig;
            }
          }
        }
        return _frame.Item1;
      }
    }

    public string FrameDump {
      get {
        if (!_frameDump.Item0) {
          if (Frame != null) {
            int dumpFlags = Frame.DumpFlag_NoHeap | Frame.DumpFlag_NoIsVerified;
            if (RuntimeConfig == null) {
              dumpFlags |= Frame.DumpFlag_NoRuntimeConfig;
            }
            if (SimulationConfig == null) {
              dumpFlags |= Frame.DumpFlag_NoSimulationConfig;
            }

            var options = Game.Configurations.Simulation.ChecksumErrorDumpOptions;
            if (options.HasFlag(SimulationConfigChecksumErrorDumpOptions.ReadableDynamicDB)) {
              dumpFlags |= Frame.DumpFlag_ReadableDynamicDB;
            }
            if (options.HasFlag(SimulationConfigChecksumErrorDumpOptions.RawFPValues)) {
              dumpFlags |= Frame.DumpFlag_PrintRawValues;
            }
            if (options.HasFlag(SimulationConfigChecksumErrorDumpOptions.ComponentChecksums)) {
              dumpFlags |= Frame.DumpFlag_ComponentChecksums;
            }

            _frameDump = QTuple.Create(true, Frame.DumpFrame(dumpFlags));

            if (Context?.AssetDBChecksums != null) {
              var sb = new StringBuilder();
              sb.Append(_frameDump.Item1);
              sb.AppendLine();
              sb.AppendLine("# RECEIVED ASSETDB CHECKSUMS");
              foreach (var entry in Context.AssetDBChecksums) {
                sb.Append(entry.Item0).Append(": ").Append(entry.Item1).AppendLine();
              }

              _frameDump = QTuple.Create(true, sb.ToString());
            }
          } else {
            unsafe {
              byte[] actualData = FrameData;
              bool wasCompressed = false;
              try {
                actualData = ByteUtils.GZipDecompressBytes(FrameData);
                wasCompressed = true;
              } catch { }

              fixed (byte* p = actualData) {
                var printer = new FramePrinter();
                printer.AddLine($"#### RAW FRAME DUMP (was compressed: {wasCompressed}) ####");
                printer.ScopeBegin();
                UnmanagedUtils.PrintBytesHex(p, FrameData.Length, 32, printer);
                printer.ScopeEnd();
                _frameDump = QTuple.Create(true, printer.ToString());
              }
            }
          }
        }

        return _frameDump.Item1;
      }
    }


    public RuntimeConfig RuntimeConfig {
      get {
        if (!_runtimeConfig.Item0) {
          try {
            _runtimeConfig = QTuple.Create(true, Serializer.ConfigFromByteArray<RuntimeConfig>(RuntimeConfigBytes, compressed: true));
          } catch (Exception ex) {
            Log.Exception(ex);
            _runtimeConfig = QTuple.Create(true, (RuntimeConfig)null);
          }
        }
        return _runtimeConfig.Item1;
      }
    }

    public SimulationConfig SimulationConfig => Context?.SimulationConfig;

    internal void Init(Frame frame) {
      _frameToOverride = frame;
    }

    public ChecksumErrorFrameDumpContext Context {
      get {
        if (!_context.Item0) {
          try {
            using (var reader = new BinaryReader(new MemoryStream(ExtraBytes))) {
              _context = QTuple.Create(true, ChecksumErrorFrameDumpContext.Deserialize(Game, reader));
            }
          } catch (Exception ex) {
            Log.Exception(ex);
            _context = QTuple.Create(true, (ChecksumErrorFrameDumpContext)null);
          }
        }
        return _context.Item1;
      }
    }
  }

  /// <summary>
  /// Callback when local input was confirmed.
  /// </summary>
  public sealed class CallbackInputConfirmed : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.InputConfirmed;
    internal CallbackInputConfirmed(QuantumGame game) : base(ID, game) { }
    public DeterministicFrameInputTemp Input;
  }

  /// <summary>
  /// Callback called when a checksum has been computed.
  /// </summary>
  public sealed class CallbackChecksumComputed : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.ChecksumComputed;
    internal CallbackChecksumComputed(QuantumGame game) : base(ID, game) { }

    public Int32 Frame;
    public UInt64 Checksum;
  }

  /// <summary>
  /// Callback called when the local client is disconnected by the plugin.
  /// </summary>
  public sealed class CallbackPluginDisconnect : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.PluginDisconnect;
    internal CallbackPluginDisconnect(QuantumGame game) : base(ID, game) { }

    public string Reason;
  }

  public sealed class CallbackLocalPlayerAddConfirmed : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.PlayerAddConfirmed;
    internal CallbackLocalPlayerAddConfirmed(QuantumGame game) : base(ID, game) { }

    public Frame Frame;
    public int PlayerSlot;
    public PlayerRef Player;
  }

  public sealed class CallbackLocalPlayerRemoveConfirmed : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.PlayerAddFailed;
    internal CallbackLocalPlayerRemoveConfirmed(QuantumGame game) : base(ID, game) { }

    public Frame Frame;
    public int PlayerSlot;
    public PlayerRef Player;
  }

  public sealed class CallbackLocalPlayerAddFailed : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.PlayerRemoveFailed;
    internal CallbackLocalPlayerAddFailed(QuantumGame game) : base(ID, game) { }

    public int PlayerSlot;
    public string Message;
  }

  public sealed class CallbackLocalPlayerRemoveFailed : QuantumGame.CallbackBase {
    public new const Int32 ID = (int)CallbackId.PlayerRemoveConfirmed;
    internal CallbackLocalPlayerRemoveFailed(QuantumGame game) : base(ID, game) { }

    public int PlayerSlot;
    public string Message;
  }

  partial class QuantumGame {

    public class CallbackBase : Quantum.CallbackBase {
      public new QuantumGame Game {
        get => (QuantumGame)base.Game;
        set => base.Game = value;
      }

      public CallbackBase(int id, QuantumGame game) : base(id, game) {
      }


      public static Type GetCallbackType(CallbackId id) {
        switch (id) {
          case CallbackId.ChecksumComputed: return typeof(CallbackChecksumComputed);
          case CallbackId.ChecksumError: return typeof(CallbackChecksumError);
          case CallbackId.ChecksumErrorFrameDump: return typeof(CallbackChecksumErrorFrameDump);
          case CallbackId.EventCanceled: return typeof(CallbackEventCanceled);
          case CallbackId.EventConfirmed: return typeof(CallbackEventConfirmed);
          case CallbackId.GameDestroyed: return typeof(CallbackGameDestroyed);
          case CallbackId.GameStarted: return typeof(CallbackGameStarted);
          case CallbackId.InputConfirmed: return typeof(CallbackInputConfirmed);
          case CallbackId.PollInput: return typeof(CallbackPollInput);
          case CallbackId.SimulateFinished: return typeof(CallbackSimulateFinished);
          case CallbackId.UpdateView: return typeof(CallbackUpdateView);
          case CallbackId.PluginDisconnect: return typeof(CallbackPluginDisconnect);
          case CallbackId.PlayerAddConfirmed: return typeof(CallbackLocalPlayerAddConfirmed);
          case CallbackId.PlayerRemoveConfirmed: return typeof(CallbackLocalPlayerRemoveConfirmed);
          case CallbackId.PlayerAddFailed: return typeof(CallbackLocalPlayerAddFailed);
          case CallbackId.PlayerRemoveFailed: return typeof(CallbackLocalPlayerRemoveFailed);
          default: throw new ArgumentOutOfRangeException(nameof(id));
        }
      }
    }

    // callback objects
    private CallbackChecksumComputed _callbackChecksumComputed;
    private CallbackChecksumError _callbackChecksumError;
    private CallbackChecksumErrorFrameDump _callbackChecksumErrorFrameDump;
    private CallbackEventCanceled _callbackEventCanceled;
    private CallbackEventConfirmed _callbackEventConfirmed;
    private CallbackGameDestroyed _callbackGameDestroyed;
    private CallbackGameInit _callbackGameInit;
    private CallbackGameStarted _callbackGameStarted;
    private CallbackGameResynced _callbackGameResynced;
    private CallbackInputConfirmed _callbackInputConfirmed;
    private CallbackPollInput _callbackPollInput;
    private CallbackSimulateFinished _callbackSimulateFinished;
    private CallbackUpdateView _callbackUpdateView;
    private CallbackPluginDisconnect _callbackPluginDisconnect;
    private CallbackLocalPlayerAddConfirmed _callbackLocalPlayerAddConfirmed;
    private CallbackLocalPlayerRemoveConfirmed _callbackLocalPlayerRemoveConfirmed;
    private CallbackLocalPlayerAddFailed _callbackLocalPlayerAddFailed;
    private CallbackLocalPlayerRemoveFailed _callbackLocalPlayerRemoveFailed;


    public void InitCallbacks() {
      _callbackChecksumComputed = new CallbackChecksumComputed(this);
      _callbackChecksumError = new CallbackChecksumError(this);
      _callbackChecksumErrorFrameDump = new CallbackChecksumErrorFrameDump(this);
      _callbackEventCanceled = new CallbackEventCanceled(this);
      _callbackEventConfirmed = new CallbackEventConfirmed(this);
      _callbackGameDestroyed = new CallbackGameDestroyed(this);
      _callbackGameInit = new CallbackGameInit(this);
      _callbackGameStarted = new CallbackGameStarted(this);
      _callbackGameResynced = new CallbackGameResynced(this);
      _callbackInputConfirmed = new CallbackInputConfirmed(this);
      _callbackPollInput = new CallbackPollInput(this);
      _callbackSimulateFinished = new CallbackSimulateFinished(this);
      _callbackUpdateView = new CallbackUpdateView(this);
      _callbackPluginDisconnect = new CallbackPluginDisconnect(this);
      _callbackLocalPlayerAddConfirmed = new CallbackLocalPlayerAddConfirmed(this);
      _callbackLocalPlayerRemoveConfirmed = new CallbackLocalPlayerRemoveConfirmed(this);
      _callbackLocalPlayerAddFailed = new CallbackLocalPlayerAddFailed(this);
      _callbackLocalPlayerRemoveFailed = new CallbackLocalPlayerRemoveFailed(this);
    }

    public void InvokeOnGameEnded() {
      // not implemented 
    }

    public void InvokeOnDestroy() {
      try {
        _callbackDispatcher?.Publish(_callbackGameDestroyed);
      } catch (Exception ex) {
        Log.Exception(ex);

      }
    }

    void InvokeOnGameInit(bool isResync) {
      try {
        _callbackGameInit.IsResync = isResync;
        _callbackDispatcher?.Publish(_callbackGameInit);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }

    void InvokeOnGameStart(bool isResync) {
      try {
        _callbackGameStarted.IsResync = isResync;
        _callbackDispatcher?.Publish(_callbackGameStarted);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }

    void InvokeOnGameResync() {
      try {
        _callbackDispatcher?.Publish(_callbackGameResynced);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }


    QTuple<Input, DeterministicInputFlags> InvokeOnPollInput(int frame, int playerSlot, bool isFirstInThisUpdate) {

      try {
        _callbackPollInput.IsInputSet = false;
        _callbackPollInput.Frame = frame;
        _callbackPollInput.PlayerSlot = playerSlot;
        _callbackPollInput.IsFirstInThisUpdate = isFirstInThisUpdate;
        _callbackDispatcher?.Publish(_callbackPollInput);
        if (_callbackPollInput.IsInputSet) {
          return QTuple.Create(_callbackPollInput.Input, _callbackPollInput.Flags);
        }
        return default;
      } catch (Exception ex) {
        Log.Exception(ex);
        return default;
      }
    }

    void InvokeOnUpdateView() {
      HostProfiler.Start("QuantumGame.InvokeOnUpdateView");
      try {
        _callbackDispatcher?.Publish(_callbackUpdateView);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
      HostProfiler.End();
    }

    public void InvokeOnSimulateFinished(DeterministicFrame state) {
      HostProfiler.Start("QuantumGame.InvokeOnSimulateFinished");
      try {
        _callbackSimulateFinished.Frame = (Frame)state;
        _callbackDispatcher?.Publish(_callbackSimulateFinished);
      } catch (Exception ex) {
        Log.Exception(ex);
      }

      _callbackSimulateFinished.Frame = null;
      HostProfiler.End();
    }

    public void InvokeOnChecksumError(DeterministicTickChecksumError error, DeterministicFrame[] frames) {
      try {
        _callbackChecksumError.Error = error;
        _callbackChecksumError._rawFrames = frames;
        _callbackChecksumError._convertedFrame = null;
        try {
          _callbackDispatcher?.Publish(_callbackChecksumError);
        } finally {
          _callbackChecksumError._rawFrames = null;
          _callbackChecksumError._convertedFrame = null;
        }
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }

    public void InvokeOnChecksumComputed(Int32 frame, ulong checksum) {
      try {
        _callbackChecksumComputed.Frame = frame;
        _callbackChecksumComputed.Checksum = checksum;
        _callbackDispatcher?.Publish(_callbackChecksumComputed);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }

    public void InvokeOnInputConfirmed(DeterministicFrameInputTemp input) {
      try {
        _callbackInputConfirmed.Input = input;
        try {
          _callbackDispatcher?.Publish(_callbackInputConfirmed);
        } finally {
          _callbackInputConfirmed.Input = default;
        }
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }

    public void InvokeOnChecksumErrorFrameDump(Int32 actorId, Int32 frameNumber, DeterministicSessionConfig sessionConfig, byte[] runtimeConfig, byte[] frameData, byte[] extraData, IAssetSerializer serializer) {
      HostProfiler.Start("QuantumGame.InvokeOnChecksumErrorFrameDump");
      try {

        // find the frame that's going to be overwritten: 
        Frame frameToOverwrite = null;

        if (_checksumSnapshotBuffer?.Capacity > 0) {
          if (_checksumSnapshotBuffer.Count == 0) {
            _checksumSnapshotBuffer.PushBack(Frames.Verified, this, _context);
          }
          frameToOverwrite = (Frame)_checksumSnapshotBuffer.PeekBack();
        } else {
          // TODO: use replay buffer maybe? or one of predicted?
          Log.Warn("Unable to acquire a frame to decode the snapshot. The snapshot will appear as raw binary data. Increase ChecksumFrameBufferSize.");
        }

        try {
          _callbackChecksumErrorFrameDump.Init(frameToOverwrite);
          _callbackChecksumErrorFrameDump.ActorId = actorId;
          _callbackChecksumErrorFrameDump.FrameNumber = frameNumber;
          _callbackChecksumErrorFrameDump.FrameData = frameData;
          _callbackChecksumErrorFrameDump.SessionConfig = sessionConfig;
          _callbackChecksumErrorFrameDump.Serializer = serializer;
          _callbackChecksumErrorFrameDump.RuntimeConfigBytes = runtimeConfig;
          _callbackChecksumErrorFrameDump.ExtraBytes = extraData;

          _callbackDispatcher?.Publish(_callbackChecksumErrorFrameDump);

        } finally {
          _callbackChecksumErrorFrameDump.Clear();
        }
      } catch (Exception ex) {
        Log.Exception(ex);
      }
      HostProfiler.End();
    }

    private void InvokeOnEvent(EventKey key, bool confirmed) {
      HostProfiler.Start("QuantumGame.InvokeOnEvent");
      try {
        if (confirmed) {
          _callbackEventConfirmed.EventKey = key;
          _callbackDispatcher?.Publish(_callbackEventConfirmed);
        } else {
          // call event cancelation, passing: game (this), frame (f), event hash...
          // also pass the index from eventCollection (trhis is the event type ID);
          _callbackEventCanceled.EventKey = key;
          _callbackDispatcher?.Publish(_callbackEventCanceled);
        }
      } catch (Exception ex) {
        Log.Exception(ex);
      }
      HostProfiler.End();
    }

    public void InvokeOnPluginDisconnect(string reason) {
      try {
        _callbackPluginDisconnect.Reason = reason;
        _callbackDispatcher?.Publish(_callbackPluginDisconnect);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }

    public void InvokeOnLocalPlayerAddConfirmed(Frame frame, int playerSlot, PlayerRef player) {
      try {
        _callbackLocalPlayerAddConfirmed.Frame = frame;
        _callbackLocalPlayerAddConfirmed.PlayerSlot = playerSlot;
        _callbackLocalPlayerAddConfirmed.Player = player;
        _callbackDispatcher?.Publish(_callbackLocalPlayerAddConfirmed);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }

    public void InvokeOnLocalPlayerRemoveConfirmed(Frame frame, int playerSlot, PlayerRef player) {
      try {
        _callbackLocalPlayerRemoveConfirmed.Frame = frame;
        _callbackLocalPlayerRemoveConfirmed.PlayerSlot = playerSlot;
        _callbackLocalPlayerRemoveConfirmed.Player = player;
        _callbackDispatcher?.Publish(_callbackLocalPlayerRemoveConfirmed);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }

    public void InvokeOnLocalPlayerAddFailed(int playerSlot, string message) {
      try {
        _callbackLocalPlayerAddFailed.PlayerSlot = playerSlot;
        _callbackLocalPlayerAddFailed.Message = message;
        _callbackDispatcher?.Publish(_callbackLocalPlayerAddFailed);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }

    public void InvokeOnLocalPlayerRemoveFailed(int playerSlot, string message) {
      try {
        _callbackLocalPlayerRemoveFailed.PlayerSlot = playerSlot;
        _callbackLocalPlayerRemoveFailed.Message = message;
        _callbackDispatcher?.Publish(_callbackLocalPlayerRemoveFailed);
      } catch (Exception ex) {
        Log.Exception(ex);
      }
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Game/QuantumGameFlags.cs

namespace Quantum {
  /// <summary>
  /// This class contains values for flags that will be accessible with <see cref="QuantumGame.GameFlags"/>.
  /// Built-in flags control some aspects of QuantumGame inner workings, without affecting the simulation
  /// outcome.
  /// </summary>
  public partial class QuantumGameFlags {
    /// <summary>
    /// Starts the game in the server mode. 
    /// When this flag is not set, all the events marked with "server" get culled immediatelly.
    /// If this flag is set, all the events marked with "client" get culled immediatelly.
    /// </summary>
    public const int Server = 1 << 0;
    /// <summary>
    /// By default, QuantumGame uses a single shared checksum serializer to reduce allocations. 
    /// The serializer is *not* static - it is only shared between frames comming from the same QuantumGame.
    /// Set this flag if you want to disable this behaviour, for example if you calculate
    /// checksums for multiple frames using multiple threads.
    /// </summary>
    public const int DisableSharedChecksumSerializer = 1 << 1;
    /// <summary>
    /// By default, a Quantum session creates additional frame instances to cache previous states that can
    /// be used for interpolation, notably for transform interpolations on the View.
    /// Set this flag if you want to disable this behaviour (e.g. a server-side or console-only simulation),
    /// reducing memory allocations and the time spent copying states over.
    /// </summary>
    public const int DisableInterpolatableStates = 1 << 2;
    /// <summary>
    /// Set this flag to enables the Quantum task profiler in debug or release configurations.
    /// </summary>
    public const int EnableTaskProfiler = 1 << 3;
    /// <summary>
    /// Custom user flags start from this value. Flags are accessible with <see cref="QuantumGame.GameFlags"/>.
    /// </summary>
    public const int CustomFlagsStart = 1 << 16;
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Legacy/QuantumGame.Legacy.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  
  public partial class QuantumGame {
    [Obsolete("Use QuantumGame(in QuantumGameStartParameters startParams)")]
    public QuantumGame(in StartParameters startParams) : this(in startParams.Parameters) { }

    [Obsolete("Has been replaced by QuantumGame(in QuantumGameStartParameters startParams)")]
    public QuantumGame(IResourceManager manager, IAssetSerializer assetSerializer, ICallbackDispatcher callbackDispatcher, IEventDispatcher eventDispatcher)
      : this(new QuantumGameStartParameters() {
        ResourceManager = manager,
        AssetSerializer = assetSerializer,
        CallbackDispatcher = callbackDispatcher,
        EventDispatcher = eventDispatcher,
      }) { }

    [Obsolete("Use AddPlayer()")]
    public void SendPlayerData(RuntimePlayer data) {
      AddPlayer(data);
    }

    [Obsolete("Use AddPlayer()")]
    public void SendPlayerData(Int32 playerSlot, RuntimePlayer data) {
      AddPlayer(playerSlot, data);
    }

    [Obsolete("Use QuantumGameStartParameters")]
    public struct StartParameters {
      public static implicit operator QuantumGameStartParameters(StartParameters v) {
        return v.Parameters;
      }

      public QuantumGameStartParameters Parameters;

      public IResourceManager ResourceManager {
        get => Parameters.ResourceManager;
        set => Parameters.ResourceManager = value;
      }
      public IAssetSerializer AssetSerializer {
        get => Parameters.AssetSerializer;
        set => Parameters.AssetSerializer = value;
      }
      public ICallbackDispatcher CallbackDispatcher {
        get => Parameters.CallbackDispatcher;
        set => Parameters.CallbackDispatcher = value;
      }
      public IEventDispatcher EventDispatcher {
        get => Parameters.EventDispatcher;
        set => Parameters.EventDispatcher = value;
      }
      public InstantReplaySettings InstantReplaySettings {
        get => Parameters.InstantReplaySettings;
        set => Parameters.InstantReplaySettings = value;
      }
      public int HeapExtraCount {
        get => Parameters.HeapExtraCount;
        set => Parameters.HeapExtraCount = value;
      }
      public DynamicAssetDB InitialDynamicAssets {
        get => Parameters.InitialDynamicAssets;
        set => Parameters.InitialDynamicAssets = value;
      }
      public int GameFlags {
        get => Parameters.GameFlags;
        set => Parameters.GameFlags = value;
      }
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Legacy/SessionContainer.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  [Obsolete("Has been replaced by SessionRunner class.")]
  public class SessionContainer {
    public static Boolean _loadedAllStatics = false;
    public static readonly Object _lock = new Object();

    DeterministicSessionConfig _sessionConfig;
    RuntimeConfig _runtimeConfig;
    QuantumGame _game;
    DeterministicSession _session;
    long _startGameTimeoutInMiliseconds = -1;
    DateTime _startGameTimestamp;

    public QuantumGame QuantumGame => _game;
    public IDeterministicGame Game => _game;
    public DeterministicSession Session => _session;
    public RuntimeConfig RuntimeConfig => _runtimeConfig;
    public DeterministicSessionConfig DeterministicConfig => _sessionConfig;

    /// <summary>
    /// Check this when the container reconnects into a running game and handle accordingly.
    /// </summary>
    public bool HasGameStartTimedOut => _startGameTimeoutInMiliseconds > 0 && Session != null && Session.IsPaused && DateTime.Now > _startGameTimestamp + TimeSpan.FromMilliseconds(_startGameTimeoutInMiliseconds);
    /// <summary>
    /// Default is infinity (-1). Set this when the you expect to connect to a running game and wait for a snapshot.
    /// </summary>
    public long GameStartTimeoutInMiliseconds {
      get { 
        return _startGameTimeoutInMiliseconds; 
      } 
      set {
        _startGameTimeoutInMiliseconds = value;
      }
    }

    public static Native.Allocator CreateNativeAllocator() {
      switch (Environment.OSVersion.Platform) {
        case PlatformID.Unix:
        case PlatformID.MacOSX:
          return new Native.LIBCAllocator();
        default:
          return new Native.MSVCRTAllocator();
      }
    }

    public static Native.Utility CreateNativeUtils() {
      switch (Environment.OSVersion.Platform) {
        case PlatformID.Unix:
        case PlatformID.MacOSX:
          return new Native.LIBCUtility();

        default:
          return new Native.MSVCRTUtility();
      }
    }

    /// <summary>
    /// Start the simulation as a replay by providing an input provider.
    /// </summary>
    /// <param name="startParams">Game start parameters</param>
    /// <param name="provider">Input provider</param>
    /// <param name="clientId">Optional client id</param>
    /// <param name="logInitForConsole">Optionally disable setting up the console as log output (required on the Quantum plugin)</param>
    [Obsolete("Use SessionRunner class")]
    public void StartReplay(QuantumGame.StartParameters startParams, IDeterministicReplayProvider provider, string clientId = "server", bool logInitForConsole = true, IDeterministicPlatformTaskRunner taskRunner = null) {
      DeterministicSessionArgs sessionArgs;
      sessionArgs.Mode = DeterministicGameMode.Replay;
      sessionArgs.Game = null;
      sessionArgs.Replay = provider;
      sessionArgs.Communicator = null;
      sessionArgs.PlatformInfo = null;
      sessionArgs.InitialTick = 0;
      sessionArgs.FrameData = null;
      sessionArgs.SessionConfig = null;
      sessionArgs.RuntimeConfig = null;
      sessionArgs.DisableInterpolatableStates = (startParams.GameFlags & QuantumGameFlags.DisableInterpolatableStates) == QuantumGameFlags.DisableInterpolatableStates;
      Start(startParams, sessionArgs, clientId, logInitForConsole, taskRunner);
    }

    /// <summary>
    /// Start the simulation as a spectator.
    /// </summary>
    /// <param name="startParams">Game start parameters</param>
    /// <param name="networkCommunicator">Quantum network comunicator (has to have a peer that is connected to a room</param>
    /// <param name="frameData">Optionally the frame to start from</param>
    /// <param name="initialTick">The tick that the frame data is based on</param>
    /// <param name="clientId">Optional client id</param>
    /// <param name="logInitForConsole">Optionally disable setting up the console as log output (required on the Quantum plugin)</param>
    [Obsolete("Use SessionRunner class")]
    public void StartSpectator(QuantumGame.StartParameters startParams, ICommunicator networkCommunicator, byte[] frameData = null, int initialTick = 0, string clientId = "observer", bool logInitForConsole = true, IDeterministicPlatformTaskRunner taskRunner = null) {
      DeterministicSessionArgs sessionArgs;
      sessionArgs.Mode = DeterministicGameMode.Multiplayer;
      sessionArgs.Game = null;
      sessionArgs.Replay = null;
      sessionArgs.Communicator = networkCommunicator;
      sessionArgs.PlatformInfo = null;
      sessionArgs.InitialTick = initialTick;
      sessionArgs.FrameData = frameData;
      sessionArgs.SessionConfig = null;
      sessionArgs.RuntimeConfig = null;
      sessionArgs.DisableInterpolatableStates = (startParams.GameFlags & QuantumGameFlags.DisableInterpolatableStates) == QuantumGameFlags.DisableInterpolatableStates;
      Start(startParams, sessionArgs, clientId, logInitForConsole, taskRunner);
    }


    [Obsolete("Use Start signature without playerSlots arguments")]
    public void Start(QuantumGame.StartParameters startParams, DeterministicSessionArgs sessionArgs, int playerSlots, string clientId = "server", bool logInitForConsole = true, IDeterministicPlatformTaskRunner taskRunner = null) {
      Start(startParams, sessionArgs, clientId, logInitForConsole, taskRunner);
    }

    /// <summary>
    /// Start the simulation in a custom way.
    /// </summary>
    /// <param name="startParams">Game start parameters</param>
    /// <param name="sessionArgs">Game session args</param>
    /// <param name="playerSlots">Number of player slots</param>
    /// <param name="clientId">Optional client id</param>
    /// <param name="logInitForConsole">Optionally disable setting up the console as log output (required on the Quantum plugin)</param>
    [Obsolete("Use SessionRunner class")]
    public void Start(QuantumGame.StartParameters startParams, DeterministicSessionArgs sessionArgs, string clientId = "server", bool logInitForConsole = true, IDeterministicPlatformTaskRunner taskRunner = null) {
        if (!_loadedAllStatics) {
        lock (_lock) {
          if (!_loadedAllStatics) {
            // console first
            if (logInitForConsole) {
              Log.InitForConsole();
            }

            // try to figure out platform if not set
            if (Native.Utils == null) {
              Native.Utils = CreateNativeUtils();
            }

            if (MemoryLayoutVerifier.Platform == null) {
              MemoryLayoutVerifier.Platform = new MemoryLayoutVerifier.DefaultPlatform();
            }
          }

          _loadedAllStatics = true;
        }
      }

      _game = new QuantumGame(startParams);

      DeterministicPlatformInfo info;
      info = new DeterministicPlatformInfo();
      info.Allocator = CreateNativeAllocator();
      info.Architecture = DeterministicPlatformInfo.Architectures.x86;
      info.RuntimeHost = DeterministicPlatformInfo.RuntimeHosts.PhotonServer;
      info.Runtime = DeterministicPlatformInfo.Runtimes.NetFramework;
      info.TaskRunner = taskRunner ?? new DotNetTaskRunner();

      switch (Environment.OSVersion.Platform) {
        case PlatformID.Unix:
          info.Platform = DeterministicPlatformInfo.Platforms.Linux;
          break;

        case PlatformID.MacOSX:
          info.Platform = DeterministicPlatformInfo.Platforms.OSX;
          break;

        default:
          info.Platform = DeterministicPlatformInfo.Platforms.Windows;
          break;
      }

      sessionArgs.Game = _game;
      sessionArgs.PlatformInfo = info;
      sessionArgs.SessionConfig = _sessionConfig;
      sessionArgs.RuntimeConfig = startParams.AssetSerializer.ConfigToByteArray(_runtimeConfig, compress: true);

      _session = new DeterministicSession(sessionArgs);
      _session.Join(clientId);

      _startGameTimestamp = DateTime.Now;
    }

    /// <summary>
    /// Update the session.
    /// </summary>
    /// <param name="dt">Optionally provide a custom delta time</param>
    public void Service(double? dt = null) {
      _session.Update(dt);
    }

    /// <summary>
    /// Destroy the session.
    /// </summary>
    public void Destroy() {
      _session?.Destroy();
      _session = null;
    }

    /// <summary>
    /// Use other constructors that provide the session and runtime config.
    /// </summary>
    public SessionContainer() {
      _sessionConfig = null;
      _runtimeConfig = null;
    }

    public SessionContainer(ReplayFile replayFile) {
      _sessionConfig = replayFile.DeterministicConfig;
      _runtimeConfig = replayFile.RuntimeConfig;
    }

    public SessionContainer(DeterministicSessionConfig sessionConfig, RuntimeConfig runtimeConfig) {
      _sessionConfig = sessionConfig;
      _runtimeConfig = runtimeConfig;
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Replay/BitStreamReplayInputProvider.cs

namespace Quantum {
  using Photon.Deterministic;

  public class BitStreamReplayInputProvider : IDeterministicStreamReplayInputProvider {
    private int _maxFrame;
    private BitStream _inputStream;

    public int MaxFrame => _maxFrame;

    public BitStreamReplayInputProvider(BitStream inputStream, int maxFrame) {
      _inputStream = inputStream;
      _maxFrame = maxFrame;
    }

    public bool CanSimulate(int frame) {
      return frame <= _maxFrame;
    }

    public int BeginReadFrame(int frame) {
      return _inputStream.ReadInt();
    }

    public void CompleteReadFrame(int frame, int length, ref byte[] data) {
      _inputStream.ReadByteArray(data, length);
    }

    public DeterministicFrameInputTemp GetInput(int frame, int player) {
      // unused
      return new DeterministicFrameInputTemp();
    }

    public void AddRpc(int player, byte[] data, bool command) {
      // unused
    }

    public QTuple<byte[], bool> GetRpc(int frame, int player) {
      // unused
      return new QTuple<byte[], bool>();
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Replay/ChecksumFile.cs

namespace Quantum {
  using System;

  [Serializable]
  public class ChecksumFile {
    public const int GrowSize = 60 * 60;

    [Serializable]
    public struct ChecksumEntry {
      public int Frame;
      // Unity JSON cannot read the unsigned long data type. 
      // We can convert on this level, keeping the ULong CalculateChecksum() signature and encode the 
      // checksum as a long for serialization. Any other ideas?
      public long ChecksumAsLong;
    }

    public ChecksumEntry[] Checksums;

    private Int32 writeIndex;

    internal void RecordChecksum(QuantumGame game, Int32 frame, ulong checksum) {
      if (Checksums == null) {
        Checksums = new ChecksumEntry[GrowSize];
      }

      if (writeIndex + 1 > Checksums.Length) {
        Array.Resize(ref Checksums, Checksums.Length + GrowSize);
      }

      Checksums[writeIndex].Frame = frame;
      Checksums[writeIndex].ChecksumAsLong = ChecksumFileHelper.UlongToLong(checksum);
      writeIndex++;
    }

    internal void VerifyChecksum(QuantumGame game, Int32 frame, ulong checksum) {
      if (Checksums.Length > 0) {
        var readIndex = (frame - Checksums[0].Frame) / game.Session.SessionConfig.ChecksumInterval;
        if (readIndex < Checksums.Length) {
          Assert.Check(Checksums[readIndex].Frame == frame, "Unexpected checksum frame {0} instead of {1}", Checksums[readIndex].Frame, frame);
          if (Checksums[readIndex].ChecksumAsLong != ChecksumFileHelper.UlongToLong(checksum)) {
            Log.Error($"Checksum mismatch in frame {frame}: {Checksums[readIndex].ChecksumAsLong} != {ChecksumFileHelper.UlongToLong(checksum)}");
          }
        }
      }
    }

    /// <summary>
    /// Clone this object and discard empty checksum entries.
    /// </summary>
    /// <returns>New checksum object to save to a replay for example</returns>
    public ChecksumFile Clone() {
      if (Checksums == null) {
        return new ChecksumFile();
      }

      var result = new ChecksumFile { Checksums = new ChecksumEntry[writeIndex] };

      Array.Copy(Checksums, result.Checksums, writeIndex);

      return result;
    }

    internal void Clear() {
      writeIndex = 0;
      if ( Checksums != null ) {
        for (int i = 0; i < Checksums.Length; ++i) {
          Checksums[i] = default;
        }
      }
    }
  }

  public static class ChecksumFileHelper {
    public static unsafe long UlongToLong(ulong value) {
      return *((long*)&value);
    }

    public static unsafe ulong LongToULong(long value) {
      return *((ulong*)&value);
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Replay/DotNetTaskRunner.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  using System.Threading;

  public class DotNetTaskRunner : IDeterministicPlatformTaskRunner {
    int _length;
    bool[] _done = new bool[128];
    public void Schedule(Action[] delegates) {
      // store how many we're executing
      _length = delegates.Length;
      // clear current state
      Array.Clear(_done, 0, _done.Length);
      // barrier this
      Thread.MemoryBarrier();
      // queue work
      for (int i = 0; i < delegates.Length; ++i) {
        ThreadPool.QueueUserWorkItem(Wrap(i, delegates[i]));
      }
    }
    public void WaitForComplete() {
      throw new NotImplementedException();
    }
    public bool PollForComplete() {
      for (int i = 0; i < _length; ++i) {
        if (Volatile.Read(ref _done[i]) == false) {
          return false;
        }
      }
      return true;
    }
    WaitCallback Wrap(int index, Action callback) {
      return _ => {
        try {
          Assert.Check(Volatile.Read(ref _done[index]) == false);
          callback();
        } catch (Exception exn) {
          Log.Exception(exn);
        } finally {
          Volatile.Write(ref _done[index], true);
        }
      };
    }

    public void Dispose() {
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Replay/InactiveTaskRunner.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;

  public class InactiveTaskRunner : IDeterministicPlatformTaskRunner {
    public void Schedule(Action[] delegates) { }

    public void WaitForComplete() { }

    public bool PollForComplete() {
      return true;
    }

    public void Dispose() { }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Replay/InputProvider.cs

namespace Quantum {
  using System;
  using System.Linq;
  using Photon.Deterministic;

  public class InputProvider : IDeterministicReplayProvider {
    private int                         _playerCount;
    private int                         _growSize;
    private int                         _startFrame;
    private DeterministicTickInputSet[] _inputs;

    private int MaxFrame => _inputs.Length + _startFrame;

    public InputProvider(DeterministicSessionConfig config, int capacity = 60 * 60, int growSize = 0) : this(config.PlayerCount, config.RollbackWindow, capacity, growSize) {
    }

    public InputProvider(DeterministicTickInputSet[] inputList) {
      ImportFromList(inputList);
    }

    public InputProvider(int playerCount, int startFrame, int capacity, int growSize) {
      _playerCount = playerCount;
      _startFrame  = startFrame;
      _growSize    = growSize;

      if (capacity > 0) {
        Allocate(capacity);
      }
    }

    public bool CanSimulate(int frame) {
      var index = ToIndex(frame);

      if (index >= 0 && index < _inputs.Length) {
        return _inputs[index].IsComplete();
      }

      return false;
    }

    public void Clear(int startFrame) {
      _startFrame = startFrame;
      for (int i = 0; i < _inputs.Length; i++) {
        _inputs[i].Tick = i + _startFrame;
        for (int j = 0; j < _playerCount; j++) {
          _inputs[i].Inputs[j].Clear();
        }
      }
    }

    public void ImportFromList(DeterministicTickInputSet[] inputList) {
      _startFrame = inputList.Length == 0 ? 0 : inputList[0].Tick;

      // Use external list as our own
      _inputs = inputList;
      for (int i = 0; i < _inputs.Length; i++) {
        for (int j = 0; j < inputList[i].Inputs.Length; j++) {
          inputList[i].Inputs[j].Sent = true;
        }
      }
    }

    public DeterministicTickInputSet[] ExportToList(int verifiedFrame) {
      var size = _inputs.Length;
      while (size > 0 && _inputs[size - 1].Inputs.Any(x => x.Tick == 0 || x.Tick > verifiedFrame)) {
        // Truncate non-verified and incomplete input from the end
        size--;
      }

      if (size <= 0) {
        return new DeterministicTickInputSet[0];
      }

      var result = new DeterministicTickInputSet[size];
      Array.Copy(_inputs, result, size);

      return result;
    }

    public void OnInputConfirmed(QuantumGame game, DeterministicFrameInputTemp input) {
      if (input.Frame < _startFrame) {
        // if starting to record from a frame following a snapshot,
        // confirmed inputs from previous frames can still arrive
        return;
      }

      if (input.Frame >= MaxFrame) {
        var minSize  = Math.Max(input.Frame - _startFrame, _inputs.Length);
        var growSize = _growSize > 0 ? minSize + _growSize : minSize * 2;
        Allocate(growSize);
      }

      _inputs[ToIndex(input.Frame)].Inputs[input.Player].Set(input);
    }

    public void InjectInput(DeterministicTickInput input, bool localReplay) {
      if (input.Tick >= MaxFrame) {
        var minSize  = Math.Max(input.Tick - _startFrame, _inputs.Length);
        var growSize = _growSize > 0 ? minSize + _growSize : minSize * 2;
        Allocate(growSize);
      }

      _inputs[ToIndex(input.Tick)].Inputs[input.PlayerIndex].CopyFrom(input);

      if (localReplay) {
        _inputs[ToIndex(input.Tick)].Inputs[input.PlayerIndex].Sent = true;
      }
    }

    public void AddRpc(int player, byte[] data, bool command) {
    }

    public QTuple<byte[], bool> GetRpc(int frame, int player) {
      if (frame < MaxFrame) {
        return QTuple.Create(
                             _inputs[ToIndex(frame)].Inputs[player].Rpc,
                             (_inputs[ToIndex(frame)].Inputs[player].Flags & DeterministicInputFlags.Command) == DeterministicInputFlags.Command);
      }

      return default;
    }

    public DeterministicFrameInputTemp GetInput(int frame, int player) {
      if (frame < MaxFrame) {
        var input = _inputs[ToIndex(frame)].Inputs[player];
        return DeterministicFrameInputTemp.Verified(frame, player, null, input.DataArray, input.DataLength, input.Flags);
      }

      return default;
    }

    private int ToIndex(int frame) {
      return frame - _startFrame;
    }

    private void Allocate(int size) {
      var oldSize = 0;
      if (_inputs == null) {
        _inputs = new DeterministicTickInputSet[size];
      } else {
        oldSize = _inputs.Length;
        Array.Resize(ref _inputs, size);
      }

      for (int i = oldSize; i < _inputs.Length; i++) {
        _inputs[i].Tick = i + _startFrame;
        _inputs[i].Inputs = new DeterministicTickInput[_playerCount];
        for (int j = 0; j < _playerCount; j++) {
          _inputs[i].Inputs[j] = new DeterministicTickInput();
        }
      }
    }
  }

  public static class InputProviderExtensions {
    public static void CopyFrom(this DeterministicTickInput input, DeterministicTickInput otherInput) {
      input.Sent        = otherInput.Sent;
      input.Tick        = otherInput.Tick;
      input.PlayerIndex = otherInput.PlayerIndex;
      input.DataLength  = otherInput.DataLength;
      input.Flags       = otherInput.Flags;

      if (otherInput.DataArray != null) {
        input.DataArray = new byte[otherInput.DataArray.Length];
        Array.Copy(otherInput.DataArray, input.DataArray, otherInput.DataArray.Length);
      }

      if (otherInput.Rpc != null) {
        input.Rpc = new byte[otherInput.Rpc.Length];
        Array.Copy(otherInput.Rpc, input.Rpc, otherInput.Rpc.Length);
      }
    }

    public static void Clear(this DeterministicTickInput input) {
      input.Tick        = default;
      input.PlayerIndex = default;
      input.DataArray   = default;
      input.DataLength  = default;
      input.Flags       = default;
      input.Rpc         = default;
    }



    public static void Set(this DeterministicTickInput input, DeterministicFrameInputTemp temp) {
      input.Tick        = temp.Frame;
      input.PlayerIndex = temp.Player;
      input.DataArray   = temp.CloneData();
      input.DataLength  = temp.DataLength;
      input.Flags       = temp.Flags;
      input.Rpc         = temp.Rpc;
    }

    public static void Clear(this DeterministicTickInputSet set) {
      set.Tick = default;

      if (set.Inputs == null) {
        return;
      }

      for (int i = 0; i < set.Inputs.Length; i++) {
        set.Inputs[i].Clear();
      }
    }

    public static bool IsComplete(this DeterministicTickInputSet set) {
      for (int i = 0; i < set.Inputs.Length; i++) {
        if (set.Inputs[i].Tick == 0) {
          return false;
        }
      }

      return true;
    }

    public static bool IsFinished(this DeterministicTickInputSet set) {
      for (int i = 0; i < set.Inputs.Length; i++) {
        if (set.Inputs[i].Tick == 0 ||
            set.Inputs[i].Sent == false) {
          return false;
        }
      }

      return true;
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Replay/QuantumInputHistoryData.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;

  /// <summary>
  /// Input history wrapper to used to store on replay files.
  /// Default way to save is DeltaCompressed in Json with compression to reduce the (Json) file size the most.
  /// </summary>
  [Serializable]
  public class QuantumInputHistoryData : QuantumJsonFriendlyDataBlob {
    /// <summary>
    /// The delta compressed input history stream.
    /// [length as int][data as int array][len][data][len][data]..
    /// </summary>
    public QuantumJsonFriendlyDataBlob DeltaCompressed;
    /// <summary>
    /// The last tick that the delta compressed input is available for, required when <see cref="DeltaCompressed"/> is used.
    /// </summary>
    public int DeltaCompressedLastTick;
    /// <summary>
    /// The full verbose input history. This is replaced by <see cref="DeltaCompressed"/> in Quantum 3.0 but it's still functional.
    /// </summary>
    public DeterministicTickInputSet[] FullLegacy;


  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Replay/QuantumJsonFriendlyDataBlob.cs

﻿namespace Quantum {
  using Photon.Deterministic;
  using System;

  /// <summary>
  /// Wrapper around saving binary data in Json to work around the issue that Unity Json tools serialize byte array only verbose.
  /// </summary>
  [Serializable]
  public class QuantumJsonFriendlyDataBlob {
    /// <summary>
    /// The byte array is saved as is.
    /// </summary>
    public byte[] Binary;
    /// <summary>
    /// The byte array is saved as Base64 text.
    /// </summary>
    public string Base64;
    /// <summary>
    /// Both <see cref="Binary"/> and <see cref="Base64"/> can be GZip compressed.
    /// </summary>
    public bool IsCompressed;

    /// <summary>
    /// Used to customize encoding of this class in interal API.
    /// </summary>
    /// <param name="data">Data to encode</param>
    /// <returns>New instance with encoded data</returns>
    public delegate QuantumJsonFriendlyDataBlob Encoder(byte[] data);

    /// <summary>
    /// Decode the byte[] array.
    /// Based on the configuration will return the <see cref="Binary"/> (unzipped) or the <see cref="Base64"/> (decoded and unzipped).
    /// </summary>
    /// <returns>Decoded data</returns>
    public byte[] Decode() {
      var bytes = Binary;

      if (string.IsNullOrEmpty(Base64) == false) {
        bytes = ByteUtils.Base64Decode(Base64);
      }

      if (bytes?.Length > 0) {
        return IsCompressed ? ByteUtils.GZipDecompressBytes(bytes) : bytes;
      }

      return null;
    }

    /// <summary>
    /// Encode a byte[].
    /// </summary>
    /// <param name="data">The data to encode</param>
    /// <param name="isCompressed">Is the data GZip compressed</param>
    /// <param name="asBase64String">Is the data converted to base64</param>
    /// <returns>Encoded data object</returns>
    public static QuantumJsonFriendlyDataBlob Encode(byte[] data, bool isCompressed, bool asBase64String) {
      if (data?.Length == 0) {
        return null;
      }

      var bytes = data;
      if (isCompressed) {
        bytes = ByteUtils.GZipCompressBytes(data);
      }

      return new QuantumJsonFriendlyDataBlob {
        Base64 = asBase64String ? ByteUtils.Base64Encode(bytes) : null,
        Binary = asBase64String ? null                          : bytes,
        IsCompressed = isCompressed,
      };
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Replay/QuantumReplayFile.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
#if QUANTUM_UNITY
  using UnityEngine.Serialization;
#endif

  /// <summary>
  /// A class that holds all relevant data to run a Quantum replay that can be saved and loaded in JSON.
  /// A replay can contain an AssetDatabase for convenience during development which should be omitted when using replay inside a production environment where replay sizes are a concern.
  /// A replay can contain checksums that are verified when running the replay which is also a development feature.
  /// </summary>
  [Serializable]
  public class QuantumReplayFile {
    /// <summary>
    /// Delta compressed binary input history, this is the same that is send over replay webhooks for example.
    /// </summary>
    public QuantumJsonFriendlyDataBlob InputHistoryDeltaCompressed;
    /// <summary>
    /// Full verbose input used in Quantum 2.1, which is still functional, but has only fringe use cases. 
    /// </summary>
    public DeterministicTickInputSet[] InputHistoryLegacy;
    /// <summary>
    /// Binary serialized RuntimeConfig.
    /// Use AssetSerializer.ConfigToByteArray(runtimeConfig, compress: true)
    /// </summary>
    public QuantumJsonFriendlyDataBlob RuntimeConfigData;
    /// <summary>
    /// The session config.
    /// </summary>
    public DeterministicSessionConfig DeterministicConfig;
    /// <summary>
    /// The last tick of the input.
    /// </summary>
    public int LastTick;
    /// <summary>
    /// The initial tick to start from, requires <see cref="InitialFrameData"/> to be set.
    /// </summary>
#if QUANTUM_UNITY
    [FormerlySerializedAs("InitialFrame")]
#endif
    public int InitialTick;
    /// <summary>
    /// Optional frame data to start the replay with. This is used for savegames for example.
    /// </summary>
    public byte[] InitialFrameData;
    /// <summary>
    /// Optional checksums. Omit this for replays in production enviroments.
    /// </summary>
    public ChecksumFile Checksums;
    /// <summary>
    /// Optional serialized asset database. Omit this for replays in production enviroments.
    /// Use AssetSerializer.SerializeAssets(stream, ResourceManager.LoadAllAssets().ToArray()
    /// </summary>
    public QuantumJsonFriendlyDataBlob AssetDatabaseData;

    /// <summary>
    /// Helper method to create an input provider based on the combination of the saved input history configurations.
    /// </summary>
    /// <returns>Replay innput provider to play back the input. Use this in <see cref="SessionRunner.Arguments.ReplayProvider"/></returns>.
    public IDeterministicReplayProvider CreateInputProvider() {
      if (InputHistoryLegacy?.Length > 0) {
        return new InputProvider(InputHistoryLegacy);
      }

      if (LastTick <= 0) {
        return null;
      }

      var bytes = InputHistoryDeltaCompressed.Decode();
      if (bytes?.Length > 0) {
        var inputStream = new BitStream(bytes);
        return new BitStreamReplayInputProvider(inputStream, LastTick);
      }

      return null;
    }
  }

  #region Legacy 

  [Obsolete("Use QuantumReplayFile")]
  public class ReplayFile : QuantumReplayFile {
    [Obsolete("use InputHistoryLegacy instead")]
    public DeterministicTickInputSet[] InputHistory {
      get => InputHistoryLegacy;
      set => InputHistoryLegacy = value;
    }

    [Obsolete("Use InputHistoryDeltaCompressed instead")]
    public byte[] InputHistoryRaw {
      get => InputHistoryDeltaCompressed.Decode();
      set => InputHistoryDeltaCompressed = QuantumJsonFriendlyDataBlob.Encode(value, isCompressed: true, asBase64String: true); 
    }

    [Obsolete("Use InitialFrameData")]
    public byte[] Frame {
      get => InitialFrameData;
      set => InitialFrameData = value;
    }

    [Obsolete("Use InitialTick")]
    public int InitialFrame {
      get => InitialTick;
      set => InitialTick = value;
    }

    [Obsolete("Use RuntimeConfig.Binary, serialization of RuntimeConfig requires the AssetSerializer: use AssetSerializer.ConfigToByteArray(RuntimeConfig, compress: true)")]
    public RuntimeConfig RuntimeConfig => null;

    [Obsolete("RuntimeConfig.Binary instead")]
    public byte[] RuntimeConfigBinary {
      get => RuntimeConfigData?.Decode();
      set => RuntimeConfigData = QuantumJsonFriendlyDataBlob.Encode(value, isCompressed: false, asBase64String: true);
    }

    [Obsolete("Use AssetDatabaseData instead")]
    public byte[] AssetDatabase {
      get => AssetDatabaseData?.Decode();
      set => AssetDatabaseData = QuantumJsonFriendlyDataBlob.Encode(value, isCompressed: true, asBase64String: true);
    }
  }

  #endregion
}


#endregion


#region Assets/Photon/Quantum/Simulation/Replay/RingBufferInputProvider.cs

namespace Quantum {
  using System;
  using Photon.Deterministic;

  public class RingBufferInputProvider : IDeterministicReplayProvider {
    private readonly DeterministicTickInputSet[] _inputs;

    private readonly int _capacity;
    private int _startFrame;

    public RingBufferInputProvider(DeterministicSessionConfig sessionConfig, int capacity = 256) : this(sessionConfig.PlayerCount, sessionConfig.RollbackWindow, capacity) {
    }

    public bool CanSimulate(int frame) {
      if (frame < _startFrame) {
        return false;
      }

      var index = ToIndex(frame);

      return _inputs[index].Tick == frame && _inputs[index].IsComplete();
    }

    public RingBufferInputProvider(DeterministicTickInputSet[] inputList) {
      _startFrame = inputList.Length == 0 ? 0 : inputList[0].Tick;

      // Use external list as our own
      _inputs = inputList;
      _capacity = inputList?.Length ?? 0;

      for (int i = 0; i < _inputs.Length; i++) {
        for (int j = 0; j < inputList[i].Inputs.Length; j++) {
          inputList[i].Inputs[j].Sent = true;
        }
      }
    }

    public RingBufferInputProvider(int playerCount, int startFrame, int capacity) {
      _startFrame  = startFrame;

      _capacity = Math.Max(0, capacity);
      _inputs = new DeterministicTickInputSet[_capacity];
      
      for (int i = 0; i < _inputs.Length; i++) {
        _inputs[i].Inputs = new DeterministicTickInput[playerCount];

        for (int j = 0; j < playerCount; j++) {
          _inputs[i].Inputs[j] = new DeterministicTickInput();
        }
      }
    }

    public void Clear(int startFrame) {
      _startFrame = startFrame;

      for (int i = 0; i < _inputs.Length; i++) {
        _inputs[i].Clear();
      }
    }

    public void OnInputConfirmed(QuantumGame game, DeterministicFrameInputTemp input) {
      if (TryGetInputSetIndex(input.Frame, out var index) == false) {
        return;
      }

      _inputs[index].Inputs[input.Player].Set(input);
    }

    public void InjectInput(DeterministicTickInput input, bool localReplay) {
      if (TryGetInputSetIndex(input.Tick, out var index) == false) {
        return;
      }

      _inputs[index].Inputs[input.PlayerIndex].CopyFrom(input);

      if (localReplay) {
        _inputs[index].Inputs[input.PlayerIndex].Sent = true;
      }
    }

    public void AddRpc(int player, byte[] data, bool command) {
    }

    public QTuple<byte[], bool> GetRpc(int frame, int player) {
      var index = ToIndex(frame);

      if (frame < _startFrame || _inputs[index].Tick != frame) {
        return default;
      }

      var playerInput = _inputs[index].Inputs[player];

      return QTuple.Create(playerInput.Rpc, (playerInput.Flags & DeterministicInputFlags.Command) == DeterministicInputFlags.Command);
    }

    public DeterministicFrameInputTemp GetInput(int frame, int player) {
      var index = ToIndex(frame);

      if (frame < _startFrame || _inputs[index].Tick != frame) {
        return default;
      }
      
      var playerInput = _inputs[index].Inputs[player];
      return DeterministicFrameInputTemp.Verified(frame, player, null, playerInput.DataArray, playerInput.DataLength, playerInput.Flags);
    }

    private int ToIndex(int frame) {
      return (frame - _startFrame) % _capacity;
    }
    
    private bool TryGetInputSetIndex(int tick, out int index) {
      if (tick < _startFrame) {
        // if starting to record from a frame following a snapshot,
        // confirmed inputs from previous frames can still arrive
        index = -1;
        return false;
      }
      
      index = ToIndex(tick);
      ref var set = ref _inputs[index];

      if (set.Tick != tick) {
        set.Clear();
        set.Tick = tick;
      }

      Assert.Check(set.Tick == tick, set.Tick, tick);

      return true;
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Replay/StreamReplayInputProvider.cs

namespace Quantum {
  using Photon.Deterministic;
  using Quantum.Core;
  using System;
  using System.IO;

  public class StreamReplayInputProvider : IDeterministicStreamReplayInputProvider {
    private int _maxFrame;
    private Stream _inputStream;
    private byte[] _lengthReadBuffer = new byte[4];

    public int MaxFrame => _maxFrame;

    public StreamReplayInputProvider(Stream inputStream, int maxFrame) {
      _inputStream = inputStream;
      _maxFrame = maxFrame;
    }

    public bool CanSimulate(int frame) {
      return frame <= _maxFrame;
    }

    public int BeginReadFrame(int frame) {
      var bytesRead = _inputStream.Read(_lengthReadBuffer, 0, 4);
      Assert.Always(bytesRead == 4, bytesRead);
      return BitConverter.ToInt32(_lengthReadBuffer, 0);
    }

    public void CompleteReadFrame(int frame, int length, ref byte[] data) {
      var bytesRead = _inputStream.Read(data, 0, length);
      Assert.Always(bytesRead == length, bytesRead);
    }

    public DeterministicFrameInputTemp GetInput(int frame, int player) {
      // unused
      return new DeterministicFrameInputTemp();
    }

    public void AddRpc(int player, byte[] data, bool command) {
      // unused
    }

    public QTuple<byte[], bool> GetRpc(int frame, int player) {
      // unused
      return new QTuple<byte[], bool>();
    }

    public static void ForwardToFrame(Stream stream, int frame) {
      // Read from the recorded frame until we find the desired start frame.
      while (true) {
        int dataLength = stream.ReadInt();
        Assert.Always(dataLength > 0, "Failed to read a valid data length from recorded stream.");

        int recordedFrame = stream.ReadInt();
        if (recordedFrame == frame) {
          stream.SeekOrThrow(-8, SeekOrigin.Current);
          break;
        } else {
          stream.SeekOrThrow(dataLength - 4, SeekOrigin.Current);
        }
      }
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Runner/DotNetRunnerFactory.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  using System.Threading.Tasks;

  /// <summary>
  /// Platform dependent information and factory methods for the <see cref="SessionRunner"/>.
  /// This factory is created to be used on the Quantum server plugin.
  /// </summary>
  public class DotNetRunnerFactory : IRunnerFactory {
    public static Boolean _isInitialized = false;
    public static readonly Object _lock = new Object();

    public virtual DeterministicPlatformInfo CreatePlaformInfo => CreatePlatformInfo();

    public virtual TaskFactory CreateTaskFactory => null;
    
    public virtual Action UpdateDB => null;

    public virtual IDeterministicGame CreateGame(QuantumGameStartParameters startParameters) => new QuantumGame(startParameters);

    public virtual void CreateProfiler(string clientId, DeterministicSessionConfig deterministicConfig, DeterministicPlatformInfo platformInfo, IDeterministicGame game) { }

    public virtual SessionRunner CreateRunner(SessionRunner.Arguments arguments) => new SessionRunner();

    public static DeterministicPlatformInfo CreatePlatformInfo() {
      Init();

      DeterministicPlatformInfo info;
      info = new DeterministicPlatformInfo();
      info.Allocator = CreateNativeAllocator();
      info.Architecture = DeterministicPlatformInfo.Architectures.x86;
      info.RuntimeHost = DeterministicPlatformInfo.RuntimeHosts.PhotonServer;
      info.Runtime = DeterministicPlatformInfo.Runtimes.NetFramework;
      info.TaskRunner = new DotNetTaskRunner();

      switch (Environment.OSVersion.Platform) {
        case PlatformID.Unix:
          info.Platform = DeterministicPlatformInfo.Platforms.Linux;
          break;

        case PlatformID.MacOSX:
          info.Platform = DeterministicPlatformInfo.Platforms.OSX;
          break;

        default:
          info.Platform = DeterministicPlatformInfo.Platforms.Windows;
          break;
      }

      return info;
    }

    public static void Init() {
      if (!_isInitialized) {
        lock (_lock) {
          if (!_isInitialized) {
            Native.Utils ??= CreateNativeUtils();
            MemoryLayoutVerifier.Platform ??= new MemoryLayoutVerifier.DefaultPlatform();
          }

          _isInitialized = true;
        }
      }
    }

    public static Native.Allocator CreateNativeAllocator() {
      switch (Environment.OSVersion.Platform) {
        case PlatformID.Unix:
        case PlatformID.MacOSX:
          return new Native.LIBCAllocator();
        default:
          return new Native.MSVCRTAllocator();
      }
    }

    public static Native.Utility CreateNativeUtils() {
      switch (Environment.OSVersion.Platform) {
        case PlatformID.Unix:
        case PlatformID.MacOSX:
          return new Native.LIBCUtility();

        default:
          return new Native.MSVCRTUtility();
      }
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Runner/DotNetSessionRunner.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  using System.IO;

  /// <summary>
  /// This class implements the <see cref="IDeterministicSessionRunner"/> interface and contains code to glue together the Quantum server and Quantum session runner.
  /// This was formerly part of the Quantum Server SDK.
  /// </summary>
  public class DotNetSessionRunner : IDeterministicSessionRunner {
    static IResourceManager _resourceManager;
    static Object _lock = new Object();
    IAssetSerializer _assetSerializer;
    SessionRunner _runner;

    /// <summary>
    /// Get and set the AssetSerializer directly after <see cref="IDeterministicSessionRunner"/> creation, until it is possible to pass it internally.
    /// </summary>
    public IAssetSerializer AssetSerializer {
      get { return _assetSerializer; }
      set { _assetSerializer = value; }
    }

    /// <summary>
    /// Grants access to the Quantum session runner.
    /// </summary>
    public SessionRunner Runner => _runner;

    /// <summary>
    /// Initialized the server simulation. It initilizes static classes like FPLut, Native.Utils and instantiates a static resource manager that is shared over multiple server simulations.
    /// This method throws exceptions on errors.
    /// </summary>
    public void Init(DeterministicSessionRunnerInitArguments args) {
      _assetSerializer ??= (IAssetSerializer)args.AssetSerializer;
      Assert.Always(_assetSerializer != null, "AssetSerializer required");

      lock (_lock) {
        if (!FPLut.IsLoaded) {
          FPLut.Init(args.LutPath);
        }

        if (Native.Utils == null) {
          Native.Utils = DotNetRunnerFactory.CreateNativeUtils();
        }

        // TODO: If resource manager changes during runtime there should be an option to pass an instance here or to to create individual managers for each simulation and make it accessible.
        if (_resourceManager == null) {
          byte[] assetDBData = null;

          // Trying to load the asset db file from disk
          if (string.IsNullOrEmpty(args.AssetDBPath) == false) {
            Assert.Always(File.Exists(args.AssetDBPath), $"AssetDB file not found at {args.AssetDBPath}");
            assetDBData = File.ReadAllBytes(args.AssetDBPath);
          }

          // Trying to load the asset db file from the assembly
          if (assetDBData == null) {
            using (var stream = typeof(QuantumGame).Assembly.GetManifestResourceStream(args.EmbeddedAssetDBName)) {
              Assert.Always(stream != null, $"Failed to find embedded AssetDB {args.EmbeddedAssetDBName}");
              Assert.Always(stream.Length > 0, $"Embedded AssetDB {args.EmbeddedAssetDBName} is empty");
              assetDBData = new byte[stream.Length];
              var bytesRead = stream.Read(assetDBData, 0, (int)stream.Length);
              Assert.Always(bytesRead == stream.Length, $"Failed to read complete embedded AssetDB {args.EmbeddedAssetDBName}: bytesRead {bytesRead} size: {stream.Length}");
            }
          }

          Assert.Always(assetDBData != null, $"No asset database found for path {args.AssetDBPath} or {args.EmbeddedAssetDBName}");

          using (var stream = new MemoryStream(assetDBData)) {
            var assets = _assetSerializer.DeserializeAssets(stream);
            _resourceManager = new ResourceManagerStatic(assets, DotNetRunnerFactory.CreateNativeAllocator());
          }
        }
      }
    }

    /// <summary>
    /// Disposes the Quantum runner.
    /// </summary>
    public void Shutdown() {
      if (_runner == null) {
        return;
      }

      var tempRunner = _runner;
      _runner = null;
      tempRunner.Shutdown(ShutdownCause.SimulationStopped);
    }

    /// <summary>
    /// Implements the start of the Quantum online session. Instantiates a Quantum runner.
    /// </summary>
    /// <param name="args">Start arguments</param>
    public void Start(DeterministicSessionRunnerStartArguments args) {
      var gameFlags = 0;
      // tag the server sim as Server (notably to receive game events that target the server and not receive events that target the client exclusively)
      gameFlags |= QuantumGameFlags.Server;
      // no need to allocate an extra frame for interpolation on the server sim (saves memory and frame copies)
      gameFlags |= QuantumGameFlags.DisableInterpolatableStates;

      _runner = SessionRunner.Start(new SessionRunner.Arguments {
        RunnerFactory = new DotNetRunnerFactory(),
        TaskRunner = new InactiveTaskRunner(), // force the server simulation to run in single thread (scales better)
        AssetSerializer = _assetSerializer,
        ResourceManager = _resourceManager,
        ReplayProvider = args.InputProvider,
        RuntimeConfig = _assetSerializer.ConfigFromByteArray<RuntimeConfig>(args.RuntimeConfig, compressed: true),
        SessionConfig = args.SessionConfig,
        GameMode = DeterministicGameMode.Replay,
        GameFlags = gameFlags,
      });
    }

    /// <summary>
    /// Implements the server update callback.
    /// </summary>
    /// <param name="gameTime">Game time in seconds</param>
    public void Service(double gameTime) {
      if (_runner == null) {
        return;
      }

      // advance the simulation to the latest injected input-set/tick
      if (_runner.Session.FrameVerified != null) {
        // server simulation non synced timed
        var sessionTime = _runner.Session.AccumulatedTime + _runner.Session.FrameVerified.Number * _runner.Session.DeltaTimeDouble;

        // try to update session to catch-up (will still be bound by input)
        _runner.Service((float)(gameTime - sessionTime));
      } else {
        _runner.Service();
      }
    }

    /// <summary>
    /// Implements the server snapshot requested callback. 
    /// This will use the most recent simulated frame on the server to use as a snapshot for late-joining clients.
    /// Which will bypass requesting buddy snapshots from other clients.
    /// </summary>
    /// <param name="tick">The tick of the snapshot.</param>
    /// <param name="data">DeterministicFrame object serialized.</param>
    /// <returns>True, if the snapshot was set.</returns>
    public bool TryCreateSnapshot(ref int tick, ref byte[] data) {
      if (_runner == null || _runner.Session.FramePredicted == null) {
        // Too early for snapshots.
        return false;
      }

      tick = _runner.Session.FramePredicted.Number;
      data = _runner.Session.FramePredicted.Serialize(DeterministicFrameSerializeMode.Serialize);
      return true;
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Runner/IRunnerFactory.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  using System.Threading.Tasks;

  public interface IRunnerFactory {
    DeterministicPlatformInfo CreatePlaformInfo { get; }
    TaskFactory CreateTaskFactory { get; }
    void CreateProfiler(string clientId, DeterministicSessionConfig deterministicConfig, DeterministicPlatformInfo platformInfo, IDeterministicGame game);
    IDeterministicGame CreateGame(QuantumGameStartParameters startParameters);
    Action UpdateDB { get; }
    SessionRunner CreateRunner(SessionRunner.Arguments arguments);
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Runner/SessionRunner.Arguments.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  using System.Threading;

  public partial class SessionRunner {
    /// <summary>
    /// Arguments to start an online or offline Quantum simulation.
    /// </summary>
    public struct Arguments {
      /// <summary>
      /// The default start game timeout. Overwrite by setting explicit StartGameTimeoutInSeconds.
      /// </summary>
      public const float DefaultStartGameTimeoutInSeconds = 10.0f;
      /// <summary>
      /// The Quantum ClientId is a secret between the client and the server and is used when reconnecting into a running simulation to preseve the player index.
      /// </summary>
      public string ClientId;
      /// <summary>
      /// The runtime config the Quantum game should use. Every client needs to set it, the server selects the first one send to it.
      /// </summary>
      public IRuntimeConfig RuntimeConfig;
      /// <summary>
      /// The deterministic config the Quantum game should use. Every client needs to set it, the server selects the first one send to it.
      /// </summary>
      public DeterministicSessionConfig SessionConfig;
      /// <summary>
      /// The replay provider injects recorded inputs and rpcs into the game which is required to run the game as a replay. InputProvider is an implementation of the replay provider. See useages of QuantumGame.RecordedInputs and WuantumRunnerLocalReplay.InputProvider.
      /// </summary>
      public IDeterministicReplayProvider ReplayProvider;
      /// <summary>
      /// The game mode (default is Multiplayer). 
      /// Local mode is for testing only, the simulation is not connected online. It does not go into prediction nor does it perform rollbacks.
      /// Replay mode will also run offline and requires the ReplayProvider to be set to process the input.
      /// Spectating mode will run the simulation without a player and without the ability to input.
      /// </summary>
      public DeterministicGameMode GameMode;
      /// <summary>
      /// The initial tick to start the simulation from as set in FrameData (only set this when FrameData is set as well). The initial frame is also encoded in the data, but required deserilization first.
      /// </summary>
      public Int32 InitialTick;
      /// <summary>
      /// Obsolete: use InitialTick
      /// </summary>
      [Obsolete("Use InitialTick")]
      public Int32 InitialFrame {
        get { return InitialTick; }
        set { InitialTick = value; }
      }
      /// <summary>
      /// Serialized frame to start the simulation from. Requires InitialFrame to be set as well. This can be a reconnect or an instant replay where we already have a frame snapshot locally (QuantumInstantReplay).
      /// </summary>
      public Byte[] FrameData;
      /// <summary>
      /// Optionally name the runner to access it from by id. This is useful when multiple runners are active on the client (for example an instant replay).
      /// </summary>
      public string RunnerId;
      /// <summary>
      /// Setting a player count here will overwrite the <see cref="SessionConfig"/>.PlayerCount during runner creation.
      /// Either SessionConfig.Player or this PlayerCount needs to be > 0.
      /// </summary>
      public Int32 PlayerCount;
      /// <summary>
      /// If set it defines the timeout in seconds to wait for StartGame() to commence the online game. This includes for example sending configrations and waiting for a snapshot.
      /// If not set the default timeout is used defined by <see cref="DefaultStartGameTimeoutInSeconds"/>.
      /// This value is not related to <see cref="DeterministicSessionConfig.SessionStartTimeout"/>, but it's affected by it. The SessionStartTimeout should not be larger than this value.
      /// Use WaitForGameStart() to manually wait for the start. Be sure to Shutdown() the runner in case of exceptions.
      /// </summary>
      public float? StartGameTimeoutInSeconds;
      /// <summary>
      /// The LoadBalancingClient object needs to be connected to game sever (joined a room) when handed to Quantum. Is not required for Replay or Local game modes.
      /// </summary>
      public Action<ShutdownCause, SessionRunner> OnShutdown;
      /// <summary>
      /// Quantum communicator which encapsulates the connection object (from Photon Realtime).
      /// </summary>
      public ICommunicator Communicator;
      /// <summary>
      /// Runner factory to create platform dependent objects.
      /// </summary>
      public IRunnerFactory RunnerFactory;
      /// <summary>
      /// The Quantum internal task runner.
      /// </summary>
      public InactiveTaskRunner TaskRunner;
      /// <summary>
      /// A cancellation token to stop all async tasks (only used during StartAsync()).
      /// </summary>
      public CancellationToken CancellationToken;
      /// <summary>
      /// Encapsulated QuantumGameStartParameters.
      /// </summary>
      public QuantumGameStartParameters GameParameters;
      /// <summary>
      /// Will be used by the Unity runner to update the Quantum simulation with different delta time settings.
      /// </summary>
      public SimulationUpdateTime DeltaTimeType;
      /// <summary>
      /// The recording flags will enable the recording of input and checksums (requires memory and allocations).
      /// When enabled QuantumGame.GetRecordedReplay can be used access the replay data.
      /// </summary>
      public RecordingFlags RecordingFlags;

      /// <summary>
      /// Initializes struct with default values.
      /// </summary>
      public static Arguments CreateDefault() {
        return new Arguments { RunnerId = "Default" };
      }

      /// <summary>
      /// Optionally override the resource manager for example from deserialized Quantum assets (as showcased in QuantumRunnerLocalReplay).
      /// Will set <see cref="GameParameters.ResourceManager"/>
      /// </summary>
      public IResourceManager ResourceManager {
        get { return GameParameters.ResourceManager; }
        set { GameParameters.ResourceManager = value; }
      }

      /// <summary>
      /// Will set <see cref="GameParameters.AssetSerializer "/>
      /// </summary>
      public IAssetSerializer AssetSerializer {
        get { return GameParameters.AssetSerializer; }
        set { GameParameters.AssetSerializer = value; }
      }
      
      /// <summary>
      /// Will set <see cref="GameParameters.CallbackDispatcher"/>
      /// </summary>
      public ICallbackDispatcher CallbackDispatcher {
        get { return GameParameters.CallbackDispatcher; }
        set { GameParameters.CallbackDispatcher = value; }
      }

      /// <summary>
      /// Will set <see cref="GameParameters.EventDispatcher"/>
      /// </summary>
      public IEventDispatcher EventDispatcher {
        get { return GameParameters.EventDispatcher; }
        set { GameParameters.EventDispatcher = value; }
      }

      /// <summary>
      /// The instant replay feature requires this setup data for snapshot recording.
      /// </summary>
      public InstantReplaySettings InstantReplaySettings {
        get { return GameParameters.InstantReplaySettings; }
        set { GameParameters.InstantReplaySettings = value; }
      }

      /// <summary>
      ///  Extra heaps to allocate for a session in case you need to create 'auxiliary' frames than actually required for the simulation itself.
      ///  Will set <see cref="QuantumGameStartParameters.HeapExtraCount"/>
      /// </summary>
      public int HeapExtraCount {
        get { return GameParameters.HeapExtraCount; }
        set { GameParameters.HeapExtraCount = value; }
      }

      /// <summary>
      /// Optionally provide assets to be added to the dynamic asset db. This can be used to introduce procedurally generated assets into the simulation from the start.
      /// Will set <see cref="QuantumGameStartParameters.InitialDynamicAssets"/>
      /// </summary>
      public DynamicAssetDB InitialDynamicAssets {
        get { return GameParameters.InitialDynamicAssets; }
        set { GameParameters.InitialDynamicAssets = value; }
      }

      /// <summary>
      /// GameFlags from <see cref="QuantumGameFlags"/>.
      /// Will set <see cref="QuantumGameStartParameters.GameFlags"/>.
      /// </summary>
      public int GameFlags {
        get { return GameParameters.GameFlags; }
        set { GameParameters.GameFlags = value; }
      }

      public void Validate() {
        if (FrameData?.Length > 0 && (InitialDynamicAssets?.IsEmpty == false)) {
          Log.Warn(
            $"Both {nameof(Arguments.FrameData)} and {nameof(Arguments.InitialDynamicAssets)} are set " +
            $"and not empty. Serialized frames already contain a copy of DynamicAssetDB and that copy will be used " +
            $"instead of {nameof(Arguments.InitialDynamicAssets)}");
        }

        switch (GameMode) {
          case DeterministicGameMode.Multiplayer:
            if (Communicator == null) {
              throw new SessionRunnerException($"Communicator required for game mode {GameMode}");
            }
            if (Communicator.IsConnected == false) {
              throw new SessionRunnerException($"Communicator connection required for game mode {GameMode}");
            }
            break;
        }

        Assert.Always(RunnerFactory != null, "RunnerFactory not set");
        Assert.Always(RuntimeConfig != null, "RuntimeConfig not set");
        Assert.Always(SessionConfig != null, "SessionConfig not set");
        Assert.Always(SessionConfig.PlayerCount > 0 || PlayerCount > 0, "Either PlayerCount or SessionConfig.PlayerCount must be greater than 0");
      }
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Runner/SessionRunner.cs

namespace Quantum {
  using Photon.Deterministic;
  using System;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>
  /// The SessionRunner helps to start, run and shutdown a Quantum simulation.
  /// It should never be reused for multiple simulations of multiple runs of the same game session. Always recrate the runner.
  /// It has an extensive list of starting <see cref="Arguments"/> that make it startable for a variaty of use cases: Local, Multiplayer, Replay, Server etc
  /// It extracts platform dependent code into the <see cref="IRunnerFactory"/> parameter.
  /// It offers asynchronous methods to start and stop the runner but although it uses the TPL syntax for convenience it is not considered to be run in a multi-threaded enviroment. 
  /// Use the non-async versions of the methods or use a <see cref="ConcurrentExclusiveSchedulerPair.ExclusiveScheduler"/> for unit tests and console applications.
  /// Also never use the async methods from the Quantum server plugin, parallelization is done by the Photon-Server.
  /// This class is delivered in source code to enable developers to create custom runner code.
  /// </summary>
  public partial class SessionRunner : IDisposable {
    private Action<ShutdownCause, SessionRunner> _onShutdown;
    private bool _inSessionUpdate;
    private bool _shutdownRequested;
    private TaskCompletionSource<bool> _waitForStartDone;
    private TaskCompletionSource<bool> _waitForShutdownStart;
    private TaskCompletionSource<bool> _waitForShutdownDone;
    private CancellationTokenSource _waitForStartTimeout;
    private CancellationTokenSource _waitForStartCancellation;
    private CancellationToken _outsideCancellationToken;
    private TaskFactory _taskFactory;
    private Action _updateDb;

    /// <summary>
    /// Access the Quantum session. Will be created during the start sequence.
    /// </summary>
    public DeterministicSession Session { get; private set; }
    /// <summary>
    /// Access the Quantum game. Will be created during the start sequence. 
    /// </summary>
    public IDeterministicGame DeterministicGame { get; private set; }
    /// <summary>
    /// Runner id, is set by <see cref="Arguments.RunnerId"/>.
    /// </summary>
    public string Id { get; private set; }
    /// <summary>
    /// Returns if the SessionRunner is running a simulation.
    /// </summary>
    public bool IsRunning => State == SessionState.Running;
    /// <summary>
    /// Access the comunicator object.
    /// </summary>
    public ICommunicator Communicator { get; private set; }
    /// <summary>
    /// Get the current state of the runner.
    /// </summary>
    public SessionState State { get; private set; }
    /// <summary>
    /// Will be used by the Unity runner to update the Quantum simulation with different delta time settings.
    /// </summary>
    public SimulationUpdateTime DeltaTimeType { get; set; }
    /// <summary>
    /// Access the recording flags that the runner was started with.
    /// </summary>
    public RecordingFlags RecordingFlags { get; private set; }

    private TaskFactory TaskFactory {
      get { return _taskFactory ?? System.Threading.Tasks.Task.Factory; }
      set { _taskFactory = value; }
    }

    /// <summary>
    /// Support Unity null checks.
    /// </summary>
    /// <param name="v"></param>
    public static implicit operator bool(SessionRunner v) {
      return v != null;
    }

    /// <summary>
    /// Implements disposeable interface. Calls Shutdown internally. 
    /// This is also called from inside Session.Destroy() to signal shutdown by the simulation.
    /// </summary>
    public void Dispose() {
      Shutdown(ShutdownCause.SimulationStopped);
    }

    /// <summary>
    /// Calls shutdown. Backwards compatibility.
    /// </summary>
    public void Destroy() {
      Shutdown();
    }

    /// <summary>
    /// Can be overridden in a subclass not be notified on shutdowns.
    /// Is called right after tje <see cref="Arguments.OnShutdown"/> callback.
    /// </summary>
    /// <param name="cause">The shutdown cause</param>
    protected virtual void OnShutdown(ShutdownCause cause) { }

    /// <summary>
    /// To update the Quantum simulation this needs to be run from the outside.
    /// From Unity is would be a MonoBehaviour, on the plugin it would be from OnDeterministicUpdate() 
    /// and the spectator has an extra service task to tick this.
    /// </summary>
    /// <param name="deltaTime">If null the internal stopclock is used to update, otherwise pass in the desired delte time to progress the simulation.</param>
    public void Service(double? deltaTime = null) {
      if (Session != null && State == SessionState.Starting) {
        // Waiting for a snapshot 
        if (Session.IsRunning && Session.IsPaused == false) {
          _waitForStartDone?.TrySetResult(true);
          _waitForStartDone = null;
          _waitForStartTimeout?.Dispose();
          _waitForStartTimeout = null;
          _waitForStartCancellation?.Dispose();
          _waitForStartCancellation = null;
          State = SessionState.Running;
        }
      }

      if (Session != null) {
        _inSessionUpdate = true;
        try {
          Session.Update(deltaTime);
          _inSessionUpdate = false;
        } catch (Exception e) {
          _inSessionUpdate = false;
          Shutdown(ShutdownCause.SessionError);
          Log.Exception(e);
          throw;
        }

        // TODO: hopefully gets redundant in 3.0
        _updateDb?.Invoke();
      }

      if (_waitForShutdownStart != null) {
        _waitForShutdownStart.TrySetResult(true);
      }

      if (_shutdownRequested) {
        _shutdownRequested = false;
        Shutdown();
      }
    }

    /// <summary>
    /// Create a runner object and initiates the start procedure.
    /// This method returns right away and will not wait until the actual simulation is started after the start protocol and potentially waiting for a snapshot.
    /// Use <see cref="WaitForStartAsync(CancellationToken)"/> to get notified about the actual local game start.
    /// </summary>
    /// <param name="arguments">Start runner arguments.</param>
    /// <returns>Session runner object</returns>
    /// <exception cref="ArgumentException">Arguments were invalid, check exception message.</exception>
    public static SessionRunner Start(Arguments arguments) {
      try {
        arguments.Validate();
      } catch (Exception e) {
        throw new ArgumentException(e.Message);
      }

      Log.Debug("Starting game");

      return CreateRunnerInternal(arguments);
    }

    /// <summary>
    /// Async version of the start sequence. Will return the runner object once the connection is complete.
    /// <see cref="Arguments.StartGameTimeoutInSeconds"/> must be greater than 0.
    /// Set explicit <see cref="Arguments.TaskRunner"/> or the default Task.Factory is used.
    /// Use <see cref="Arguments.CancellationToken"/> to cancel this task.
    /// Make sure to run this from a Unity "async void" method to not lose the unhandled exceptions.
    /// </summary>
    /// <param name="arguments">Start runner arguments.</param>
    /// <returns>Session runner object</returns>
    /// <exception cref="ArgumentException">Arguments were invalid, check exception message.</exception>
    /// <exception cref="SessionRunnerException">Session failed to start.</exception>
    public static Task<SessionRunner> StartAsync(Arguments arguments) {
      try {
        arguments.Validate();
      } catch (Exception e) {
        throw new ArgumentException(e.Message);
      }

      var startGameTimeoutIsSeconds = arguments.StartGameTimeoutInSeconds.HasValue ? arguments.StartGameTimeoutInSeconds.Value : Arguments.DefaultStartGameTimeoutInSeconds;
      if (startGameTimeoutIsSeconds <= 0) {
        throw new ArgumentException("StartGameTimeoutInSeconds must be greater than 0, or use non-async Start() method.");
      }

      if (arguments.SessionConfig.SessionStartTimeout >= startGameTimeoutIsSeconds) {
        Log.Warn($"Increase the SessionRunner.Arguments.StartGameTimeoutInSeconds ({arguments.SessionConfig.SessionStartTimeout}) or reduce the SessionConfig.SessionStartTimeout ({startGameTimeoutIsSeconds}).");
      }

      Log.Debug("Starting game");

      var taskFactory = arguments.RunnerFactory.CreateTaskFactory ?? System.Threading.Tasks.Task.Factory;

      return taskFactory.StartNew(async () => {
        var runner = CreateRunnerInternal(arguments);

        runner.TaskFactory = taskFactory;
        runner._outsideCancellationToken = arguments.CancellationToken;

        // Wait for game started
        try {
          // TODO: it would be actually nice if this would throw on plugin errors while waiting for the start to complete!
          await runner.WaitForStartAsync(startGameTimeoutIsSeconds);
        } catch (TaskCanceledException) {
          bool isTimedOut = runner._waitForStartTimeout != null && runner._waitForStartTimeout.IsCancellationRequested;
          await runner.ShutdownAsync(ShutdownCause.StartFailed);
          if (isTimedOut) {
            Log.Error($"Session start timed out, the client is still connected but the server did not respond in time to complete the start protocol. SessionRunner.Arguments.StartGameTimeoutInSeconds is a client controlled time out.");
            throw new SessionRunnerException($"Session start timed out");
          } else {
            throw new SessionRunnerException("Session start cancelled");
          }
        }
        return runner;
      }, arguments.CancellationToken).Unwrap();
    }

    /// <summary>
    /// Shutdown the runner. 
    /// Can be called from inside the simulation (during a simulation callback), the shutdown will commence during the next <see cref="Service(double?)"/> call.
    /// </summary>
    /// <param name="cause">Shutdown cause</param>
    public void Shutdown(ShutdownCause cause = ShutdownCause.Ok) {
      switch (State) {
        case SessionState.Shutdown:
        case SessionState.ShuttingDown:
          return;
      }

      if (_inSessionUpdate) {
      // If called from inside session we want to wait until we are back inside the Unity thread. Maybe from Update().
        _shutdownRequested = true;
        Log.Warn("Shutdown requested during session update, postponing execution");
        return;
      }

      State = SessionState.ShuttingDown;

      ShutdownInternal(this, cause);

      Communicator?.OnDestroy();
      Communicator = null;

      State = SessionState.Shutdown;
    }

    /// <summary>
    /// Shutdown the runner asynchronously. 
    /// Can be called from a simualtion callback.
    /// Will also wait for the connection to be properly disconnected.
    /// </summary>
    /// <param name="cause">Shutdown cause</param>
    /// <returns>Once the complete shutdown is completed.</returns>
    /// <exception cref="SessionRunnerException">TaskFactory was never set.</exception>
    public System.Threading.Tasks.Task ShutdownAsync(ShutdownCause cause = ShutdownCause.Ok) {
      if (TaskFactory == null) {
        throw new SessionRunnerException("TaskFactory required");
      }

      switch (State) {
        case SessionState.Shutdown:
          return System.Threading.Tasks.Task.CompletedTask;
        case SessionState.ShuttingDown:
          if (_waitForShutdownStart != null) {
            return _waitForShutdownStart.Task;
          } else {
            Log.Warn("Cannot await shutdown");
            return System.Threading.Tasks.Task.CompletedTask;
          }
      }

      State = SessionState.ShuttingDown;

      // If called from inside session we want to wait until we are back inside the Unity thread. Maybe from Update().
      if (_inSessionUpdate) {
        _waitForShutdownStart = new TaskCompletionSource<bool>();
      }

      var result = default(System.Threading.Tasks.Task);
      if (_waitForShutdownStart != null) {
        // wait for game to complete tick
        result = ((System.Threading.Tasks.Task)_waitForShutdownStart.Task).ContinueWith(t => ShutdownInternal(this, cause), TaskFactory.Scheduler);
      } else {
        // Shut down right away
        result = TaskFactory.StartNew(() => ShutdownInternal(this, cause));
      }

      // disconnect
      // TODO: check if this could delay the shutdown unnecessaryly during connection hickups
      if (Communicator != null) {
        result = result.ContinueWith(t => Communicator.OnDestroyAsync(), TaskFactory.Scheduler);
      }

      // finally set state and signal shutdown completion
      result = result.ContinueWith(t => {
        State = SessionState.Shutdown;
        _waitForShutdownDone?.TrySetResult(true);
      }, TaskFactory.Scheduler);

      return result;
    }

    /// <summary>
    /// When starting the runner synchronously this Task can be used to wait for success, error or timeout.
    /// </summary>
    /// <param name="timeoutInSeconds">Wait for game start timeout.</param>
    /// <returns>Returns when the start has completed or failed.</returns>
    public System.Threading.Tasks.Task WaitForStartAsync(float timeoutInSeconds) {
      _waitForStartTimeout = new CancellationTokenSource((int)(timeoutInSeconds * 1000));
      return WaitForStartAsync(_waitForStartTimeout.Token);
    }

    /// <summary>
    /// When starting the runner synchronously this Task can be used to wait for success, error or timeout.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns when the start has completed or failed.</returns>
    public System.Threading.Tasks.Task WaitForStartAsync(CancellationToken cancellationToken) {
      if (cancellationToken.IsCancellationRequested) {
        return System.Threading.Tasks.Task.FromCanceled(cancellationToken);
      }

      var token = cancellationToken;
      if (_outsideCancellationToken != CancellationToken.None) {
        _waitForStartCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _outsideCancellationToken);
        token = _waitForStartCancellation.Token;
      }

      if (_waitForStartDone == null) {
        _waitForStartDone = new TaskCompletionSource<bool>();
        token.Register(() => _waitForStartDone?.TrySetCanceled(token));
      }

      return _waitForStartDone.Task;
    }

    /// <summary>
    /// Wait for the simulation shutdown is signaled. This is an alternative way to listen for the <see cref="Arguments.OnShutdown"/> callback.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel this task</param>
    /// <returns>After the runner shut down.</returns>
    public System.Threading.Tasks.Task WaitForShutdownAsync(CancellationToken cancellationToken) {
      if (cancellationToken.IsCancellationRequested) {
        return System.Threading.Tasks.Task.FromCanceled(cancellationToken);
      }

      if (_waitForShutdownDone == null) {
        _waitForShutdownDone = new TaskCompletionSource<bool>();
        cancellationToken.Register(() => _waitForShutdownDone.TrySetCanceled(cancellationToken));
      }

      return _waitForShutdownDone.Task;
    }

    /// <summary>
    /// Wait for the simulation shutdown is signaled. This is an alternative way to listen for the <see cref="Arguments.OnShutdown"/> callback.
    /// </summary>
    /// <returns>After the runner shut down.</returns>
    public System.Threading.Tasks.Task WaitForShutdownAsync() {
      if (_waitForShutdownDone == null) {
        _waitForShutdownDone = new TaskCompletionSource<bool>();
      }

      return _waitForShutdownDone.Task;
    }

    protected static SessionRunner CreateRunnerInternal(Arguments arguments) {
      arguments.RunnerId = arguments.RunnerId ?? "Default";
      var runner = arguments.RunnerFactory.CreateRunner(arguments);

      try {
        runner.State = SessionState.Starting;
        runner.Id = arguments.RunnerId;
        runner.Communicator = arguments.Communicator;
        runner._onShutdown = arguments.OnShutdown;
        runner._updateDb = arguments.RunnerFactory.UpdateDB;
        runner.DeltaTimeType = arguments.DeltaTimeType;
        runner.RecordingFlags = arguments.RecordingFlags;
        runner.DeterministicGame = arguments.RunnerFactory.CreateGame(arguments.GameParameters);

        // Make a copy of deterministic config here, because we write to it
        var deterministicConfig = DeterministicSessionConfig.FromByteArray(DeterministicSessionConfig.ToByteArray(arguments.SessionConfig));
        if (arguments.PlayerCount > 0) {
          deterministicConfig.PlayerCount = arguments.PlayerCount;
        }

        var platformInfo = arguments.RunnerFactory.CreatePlaformInfo;
        if (arguments.TaskRunner != null) {
          platformInfo.TaskRunner = arguments.TaskRunner;
        }

        var args = new DeterministicSessionArgs {
          FrameData = arguments.FrameData,
          Game = runner.DeterministicGame,
          InitialTick = arguments.InitialTick,
          Mode = arguments.GameMode,
          PlatformInfo = platformInfo,
          SessionConfig = deterministicConfig,
          Replay = arguments.ReplayProvider,
          DisableInterpolatableStates = (arguments.GameFlags & QuantumGameFlags.DisableInterpolatableStates) == QuantumGameFlags.DisableInterpolatableStates
        };
        
        args.RuntimeConfig = arguments.GameParameters.AssetSerializer.ConfigToByteArray(arguments.RuntimeConfig, compress: true);

        if (arguments.GameMode == DeterministicGameMode.Multiplayer) {
          args.Communicator = arguments.Communicator;
        }

        runner.Session = new DeterministicSession(args) { Runner = runner };
        runner.Session.Join(arguments.ClientId);
        arguments.RunnerFactory.CreateProfiler(arguments.ClientId, args.SessionConfig, args.PlatformInfo, runner.DeterministicGame);
      } catch (Exception) {
        // We need to listen to anything that breaks and cleanup the runner, the calling code does not have the runner, yet.
        runner.Destroy();
        throw;
      }

      return runner;
    }

    protected static void ShutdownInternal(SessionRunner runner, ShutdownCause cause) {
      Log.Info($"Shutting down runner '{runner.Id}'");

      runner._waitForStartDone?.TrySetException(new Exception("Game shut down"));
      runner._waitForStartDone = null;
      runner._waitForStartTimeout?.Dispose();
      runner._waitForStartTimeout = null;
      runner._waitForStartCancellation?.Cancel();
      runner._waitForStartCancellation?.Dispose();
      runner._waitForStartCancellation = null;

      runner.Session?.Destroy();
      runner.Session = null;
      runner.DeterministicGame = null;

      runner._onShutdown?.Invoke(cause, runner);
      runner._onShutdown = null;
      runner.OnShutdown(cause);

      runner._taskFactory = null;
      runner._updateDb = null;
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Runner/SessionRunner.SessionState.cs

namespace Quantum {
  public partial class SessionRunner {
    /// <summary>
    /// The session runner has a state machine.
    /// </summary>
    public enum SessionState {
      /// <summary>
      /// Freshly created state.
      /// </summary>
      NotStarted,
      /// <summary>
      /// The runner is starting and waiting for the start protocol to complete.
      /// </summary>
      Starting,
      /// <summary>
      /// The simulation is running.
      /// </summary>
      Running,
      /// <summary>
      /// The runner is shutting down and waiting to complete shutdown sequence.
      /// </summary>
      ShuttingDown,
      /// <summary>
      /// The runner is shutdown.
      /// </summary>
      Shutdown
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Runner/SessionRunnerException.cs

namespace Quantum {
  using System;

  /// <summary>
  /// Runner specific exceptions.
  /// </summary>
  public class SessionRunnerException : Exception {
    public SessionRunnerException(string message) : base(message) {
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Runner/ShutdownCause.cs

namespace Quantum {
  public enum ShutdownCause {
    /// <summary>
    /// Expected shutdown
    /// </summary>
    Ok,
    /// <summary>
    /// Start timed out or cancelled
    /// </summary>
    StartFailed,
    /// <summary>
    /// The session threw an exception.
    /// </summary>
    SessionError,
    /// <summary>
    /// Not used.
    /// </summary>
    NetworkError,
    /// <summary>
    /// Simulation stopped. Could also be a network error.
    /// </summary>
    SimulationStopped
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Runner/ShutdownConnectionOptions.cs

namespace Quantum {
  /// <summary>
  /// The shutdown connection options define what to do with the client Photon connection during runner shutdown.
  /// </summary>
  public enum ShutdownConnectionOptions {
    /// <summary>
    /// Disconnect
    /// </summary>
    Disconnect,
    /// <summary>
    /// Leave the room and connect to master server.
    /// </summary>
    LeaveRoom,
    /// <summary>
    /// Leave the room and connect to master server, but keep inactive in the room.
    /// </summary>
    LeaveRoomAndBecomeInactive,
    /// <summary>
    /// Do do anything to the connection during runner shutdown.
    /// </summary>
    None
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemArrayComponent.cs

namespace Quantum.Task {
  using System;

  public abstract unsafe class SystemArrayComponent<T> : SystemBase where T : unmanaged, IComponent {
    private TaskDelegateHandle _arrayTaskDelegateHandle;

    // internal max slices
    private const int MAX_SLICES_COUNT = 32;

    public virtual int SlicesCount => MAX_SLICES_COUNT / 2;

    public sealed override void OnInit(Frame f) {
      f.Context.TaskContext.RegisterDelegate(TaskArrayComponent, GetType().Name + ".Update", ref _arrayTaskDelegateHandle);
      OnInitUser(f);
    }

    protected virtual void OnInitUser(Frame f) {

    }

    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      var slicesCount = Math.Max(1, Math.Min(SlicesCount, MAX_SLICES_COUNT));
      return f.Context.TaskContext.AddArrayTask(_arrayTaskDelegateHandle, null, f.ComponentCount<T>(includePendingRemoval: true), taskHandle, slicesCount);
    }

    private void TaskArrayComponent(FrameThreadSafe f, int start, int count, void* arg) {
      var iterator = f.GetComponentBlockIterator<T>(start, count).GetEnumerator();
      while (iterator.MoveNext()) {
        var (entity, component) = iterator.Current;
        Update(f, entity, component);
      }
    }

    public abstract void Update(FrameThreadSafe f, EntityRef entity, T* component);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemArrayFilter.cs

namespace Quantum.Task {
  using System;

  public abstract unsafe class SystemArrayFilter<T> : SystemBase where T : unmanaged {
    private TaskDelegateHandle        _arrayTaskDelegateHandle;
    private ComponentFilterStructMeta _filterMeta;

    // internal max slices
    private const int MAX_SLICES_COUNT = 32;

    public virtual int SlicesCount => MAX_SLICES_COUNT / 2;

    public virtual bool UseCulling => true;

    public virtual ComponentSet Without => default;

    public virtual ComponentSet Any => default;

    public sealed override void OnInit(Frame f) {
      _filterMeta = ComponentFilterStructMeta.Create<T>();
      Assert.Check(_filterMeta.ComponentCount > 0, "Filter Struct '{0}' must have at least one component pointer.", typeof(T));
      
      f.Context.TaskContext.RegisterDelegate(TaskArrayFilter, GetType().Name + ".Update", ref _arrayTaskDelegateHandle);
      OnInitUser(f);
    }

    protected virtual void OnInitUser(Frame f) {

    }

    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      // figure out smallest block iterator
      var taskSize = f.ComponentCount(_filterMeta.ComponentTypes[0], includePendingRemoval: true);

      for (var i = 1; i < _filterMeta.ComponentCount; ++i) {
        var otherCount = f.ComponentCount(_filterMeta.ComponentTypes[i], includePendingRemoval: true);
        if (otherCount < taskSize) {
          taskSize = otherCount;
        }
      }

      var slicesCount = Math.Max(1, Math.Min(SlicesCount, MAX_SLICES_COUNT));
      return f.Context.TaskContext.AddArrayTask(_arrayTaskDelegateHandle, null, taskSize, taskHandle, slicesCount);
    }

    private void TaskArrayFilter(FrameThreadSafe f, int start, int count, void* userData) {
      // grab iterator
      var iterator = f.FilterStruct<T>(Without, Any, start, count);

      // set culling flag
      iterator.UseCulling = UseCulling;

      var filter = default(T);

      // execute filter loop
      while (iterator.Next(&filter)) {
        Update(f, ref filter);
      }
    }

    public abstract void Update(FrameThreadSafe f, ref T filter);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemBase.cs

namespace Quantum {
  using Quantum.Task;
  using System;
  using System.Collections.Generic;

  public abstract partial class SystemBase {
    readonly String _scheduleSample;
    bool _startEnabled;
    Int32? _runtimeIndex;
    SystemBase _parentSystem;

    public Int32 RuntimeIndex {
      get {
        return (Int32)_runtimeIndex;
      }
      set {
        if (_runtimeIndex.HasValue) {
          Log.Error("Can't change systems runtime index after game has started");
        } else {
          _runtimeIndex = value;
        }
      }
    }

    public SystemBase ParentSystem {
      get {
        return _parentSystem;
      } 
      internal set {
        _parentSystem = value;
      }
    }

    public virtual IEnumerable<SystemBase> ChildSystems {
      get {
        return new SystemBase[0];
      }
    }

    public IEnumerable<SystemBase> Hierarchy {
      get {
        yield return this;

        foreach (var child in ChildSystems) {
          foreach (var childHierarchy in child.Hierarchy) {
            if (childHierarchy != null) {
              yield return childHierarchy;
            }
          }
        }
      }
    }

    public virtual Boolean StartEnabled {
      get { return _startEnabled; }
      set { _startEnabled = value; }
    }

    public SystemBase() {
      _scheduleSample = GetType().Name + ".Schedule";
      _startEnabled = true;
    }

    public SystemBase(string scheduleSample) {
      _scheduleSample = scheduleSample;
      _startEnabled = true;
    }

    public virtual void OnInit(Frame f) {
      
    }

    public virtual void OnEnabled(Frame f) {
      
    }

    public virtual void OnDisabled(Frame f) {
      
    }

    public TaskHandle OnSchedule(Frame f, TaskHandle taskHandle) {
#if DEBUG
      var profiler = f.Context.ProfilerContext.GetProfilerForTaskThread(0);
      try {
        profiler.Start(_scheduleSample);
#endif
        
        return Schedule(f, taskHandle);
        
#if DEBUG
      } finally {
        profiler.End();
      }
#endif
    }

    protected abstract TaskHandle Schedule(Frame f, TaskHandle taskHandle);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemGroup.cs

namespace Quantum {
  using System;
  using System.Collections.Generic;
  using Quantum.Task;

  public unsafe class SystemGroup : SystemBase {
    SystemBase[] _children;

    public sealed override IEnumerable<SystemBase> ChildSystems {
      get { return _children; }
    }

    public SystemGroup(String name, params SystemBase[] children) : base(name + ".Schedule") {
      _children = children;

      for (int i = 0; i < _children.Length; i++) {
        _children[i].ParentSystem = this;
      }
    }

    protected sealed override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      if (_children != null) {
        for (var i = 0; i < _children.Length; ++i) {
          if (f.SystemIsEnabledSelf(_children[i])) {
            try {
              taskHandle = _children[i].OnSchedule(f, taskHandle);
            } catch (Exception exn) {
              Log.Exception(exn);
            }
          }
        }
      }

      return taskHandle;
    }

    public override void OnEnabled(Frame f) {
      base.OnEnabled(f);

      for (int i = 0; i < _children.Length; ++i) {
        if (f.SystemIsEnabledSelf(_children[i])) {
          _children[i].OnEnabled(f);
        }
      }
    }

    public override void OnDisabled(Frame f) {
      base.OnDisabled(f);

      for (int i = 0; i < _children.Length; ++i) {
        if (f.SystemIsEnabledSelf(_children[i])) {
          _children[i].OnDisabled(f);
        }
      }
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemMainThread.cs

namespace Quantum {
  using System;
  using Quantum.Core;
  using Quantum.Task;

  public abstract unsafe class SystemMainThread : SystemBase {
    readonly String _update;
    TaskDelegateHandle _updateHandle;

    public SystemMainThread() {
      _update = GetType().Name + ".Update";
    }

    public SystemMainThread(string name) {
      _update = name + ".Update";
    }

    protected TaskHandle ScheduleUpdate(Frame f, TaskHandle taskHandle) {
      if (_updateHandle.IsValid == false) {
        f.Context.TaskContext.RegisterDelegate(TaskCallback, _update, ref _updateHandle);
      }

      return f.Context.TaskContext.AddMainThreadTask(_updateHandle, null, taskHandle);
    }

    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      return ScheduleUpdate(f, taskHandle);
    }

    void TaskCallback(FrameThreadSafe frame, int start, int count, void* arg) {
      Update((Frame)frame);

      if (((FrameBase)frame).CommitCommandsMode == CommitCommandsModes.InBetweenSystems) {
        ((FrameBase)frame).Unsafe.CommitAllCommands();
      }
    }

    public abstract void Update(Frame f);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemMainThreadFilter.cs

namespace Quantum {
  using System.Runtime.CompilerServices;

  public abstract unsafe class SystemMainThreadFilter<T> : SystemMainThread where T : unmanaged {
    public virtual bool UseCulling {
      get { return true; }
    }

    public virtual ComponentSet Without {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => default;
    }
    
    public virtual ComponentSet Any {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => default;
    }

    public sealed override void Update(Frame f) {
      // grab iterator
      var it = f.Unsafe.FilterStruct<T>(Without, Any);

      // set culling flag
      it.UseCulling = UseCulling;

      // execute filter loop
      var filter = default(T);

      while (it.Next(&filter)) {
        Update(f, ref filter);
      }
    }

    public abstract void Update(Frame f, ref T filter);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemMainThreadGroup.cs

namespace Quantum {
  using Quantum.Task;
  using System;
  using System.Collections.Generic;

  public unsafe class SystemMainThreadGroup : SystemMainThread {
    SystemMainThread[] _children;

    public SystemMainThreadGroup(string name, params SystemMainThread[] children) : base(name + ".Schedule") {
      Assert.Check(name != null);
      Assert.Check(children != null);

      _children = children;

      for (int i = 0; i < _children.Length; i++) {
        _children[i].ParentSystem = this;
      }
    }

    public sealed override IEnumerable<SystemBase> ChildSystems {
      get { return _children; }
    }

    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      if (_children != null) {
        for (var i = 0; i < _children.Length; ++i) {
          if (f.SystemIsEnabledSelf(_children[i])) {
            try {
              taskHandle = _children[i].OnSchedule(f, taskHandle);
            } catch (Exception exn) {
              Log.Exception(exn);
            }
          }
        }
      }

      return taskHandle;
    }

    public override void OnEnabled(Frame f) {
      base.OnEnabled(f);

      for (int i = 0; i < _children.Length; ++i) {
        if (f.SystemIsEnabledSelf(_children[i])) {
          _children[i].OnEnabled(f);
        }
      }
    }

    public override void OnDisabled(Frame f) {
      base.OnDisabled(f);

      for (int i = 0; i < _children.Length; ++i) {
        if (f.SystemIsEnabledSelf(_children[i])) {
          _children[i].OnDisabled(f);
        }
      }
    }

    public sealed override void Update(Frame f) {
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemSignalsOnly.cs

namespace Quantum {
  using Quantum.Task;
  public class SystemSignalsOnly : SystemBase {
    protected sealed override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      return taskHandle;
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemThreadedComponent.cs

namespace Quantum.Task {
  using System;
  using System.Threading;

  public abstract unsafe class SystemThreadedComponent<T> : SystemBase where T : unmanaged, IComponent {
    private TaskDelegateHandle _threadedTaskDelegateHandle;
    private int                _sliceIndexer;
    private int                _sliceSize;

    public const int DEFAULT_SLICE_SIZE = 16;

    public virtual int SliceSize => DEFAULT_SLICE_SIZE;

    public sealed override void OnInit(Frame f) {
      f.Context.TaskContext.RegisterDelegate(TaskThreadedComponent, GetType().Name + ".Update", ref _threadedTaskDelegateHandle);
      OnInitUser(f);
    }

    protected virtual void OnInitUser(Frame f) {

    }

    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      // reset indexer
      _sliceIndexer = -1;
      
      // cache slice size safely in main-thread
      _sliceSize = Math.Max(1, SliceSize);
      
      return f.Context.TaskContext.AddThreadedTask(_threadedTaskDelegateHandle, null, taskHandle);
    }

    private void TaskThreadedComponent(FrameThreadSafe f, int start, int count, void* userData) {
      while (true) {
        var sliceIndex = Interlocked.Increment(ref _sliceIndexer);
        var iterator   = f.GetComponentBlockIterator<T>(sliceIndex * _sliceSize, _sliceSize).GetEnumerator();

        if (iterator.MoveNext() == false) {
          // chunk is out of buffer range, we're done
          return;
        }

        do {
          var (entity, component) = iterator.Current;
          Update(f, entity, component);
        } while (iterator.MoveNext());
      }
    }

    public abstract void Update(FrameThreadSafe f, EntityRef entity, T* component);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Base/SystemThreadedFilter.cs

namespace Quantum.Task {
  using System;
  using System.Threading;

  public abstract unsafe class SystemThreadedFilter<T> : SystemBase where T : unmanaged {
    private TaskDelegateHandle _threadedTaskDelegateHandle;
    private int                _sliceIndexer;
    private int                _sliceSize;

    public const int DEFAULT_SLICE_SIZE = 16;

    public virtual int SliceSize => DEFAULT_SLICE_SIZE;

    public virtual bool UseCulling => true;

    public virtual ComponentSet Without => default;

    public virtual ComponentSet Any => default;

    public sealed override void OnInit(Frame f) {
      f.Context.TaskContext.RegisterDelegate(TaskThreadedFilter, GetType().Name + ".Update", ref _threadedTaskDelegateHandle);
      OnInitUser(f);
    }

    protected virtual void OnInitUser(Frame f) {

    }

    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      // reset indexer
      _sliceIndexer = -1;

      // cache slice size safely in main-thread
      _sliceSize = Math.Max(1, SliceSize);

      return f.Context.TaskContext.AddThreadedTask(_threadedTaskDelegateHandle, null, taskHandle);
    }

    private void TaskThreadedFilter(FrameThreadSafe f, int start, int count, void* userData) {
      var iterator = f.FilterStruct<T>(Without, Any);

      // set culling flag
      iterator.UseCulling = UseCulling;

      var filter = default(T);

      while (true) {
        var sliceIndex = Interlocked.Increment(ref _sliceIndexer);

        // reset iterator
        iterator.Reset(sliceIndex * _sliceSize, _sliceSize);

        // execute filter loop
        if (iterator.Next(&filter) == false) {
          // chunk is out of buffer range, we're done
          return;
        }

        do {
          Update(f, ref filter);
        } while (iterator.Next(&filter));
      }
    }

    public abstract void Update(FrameThreadSafe f, ref T filter);
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Core/CullingSystem.cs

namespace Quantum.Core {
  using Quantum.Task;

  /// <summary>
  /// During Predicted frames, culls all <see cref="FrameBase.SetCullable">cullable</see> entities with
  /// <see cref="Transform2D"/> that are positioned out of the
  /// <see cref="FrameContext.SetPredictionArea">prediction area</see>.
  /// </summary>
  /// \ingroup Culling
  public unsafe class CullingSystem2D : SystemBase {
    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      return f.Context.Culling.Schedule2D(f, taskHandle);
    }
  }
  
  /// <summary>
  /// During Predicted frames, culls all <see cref="FrameBase.SetCullable">cullable</see> entities with
  /// <see cref="Transform3D"/> that are positioned out of the
  /// <see cref="FrameContext.SetPredictionArea">prediction area</see>.
  /// </summary>
  /// \ingroup Culling
  public unsafe class CullingSystem3D : SystemBase {
    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      return f.Context.Culling.Schedule3D(f, taskHandle);
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Core/DebugSystem.cs

namespace Quantum.Core {
  using System;
  using System.Linq;
  using Photon.Deterministic;
  using Quantum.Task;

  public static partial class DebugCommandType {
    public const int Create = 0;
    public const int Destroy = 1;
    public const int UserCommandTypeStart = 1000;
  }

  public static partial class DebugCommand {
    
    public static event Action<Payload, Exception> CommandExecuted
#if DEBUG && !QUANTUM_DEBUG_COMMAND_DISABLED
    {
      add => _commandExecuted += value;
      remove => _commandExecuted -= value;
    }
    private static Action<Payload, Exception> _commandExecuted;
#else
    { add { } remove { } }
#endif

    public static bool IsEnabled =>
#if DEBUG && !QUANTUM_DEBUG_COMMAND_DISABLED
      true;
#else
      false;
#endif

    public static void Send(QuantumGame game, params Payload[] payload) {
#if DEBUG && !QUANTUM_DEBUG_COMMAND_DISABLED
      game.SendCommand(new InternalCommand() {
        Data = payload
      });
#else
      Log.Warn("DebugCommand works only in DEBUG builds without QUANTUM_DEBUG_COMMAND_DISABLED define.");
#endif
    }

    public static void Reset() {
#if DEBUG && !QUANTUM_DEBUG_COMMAND_DISABLED
      _commandExecuted = null;
#endif
    }


    public partial struct Payload {
      public long Id;
      public int Type;
      public EntityRef Entity;
      public ComponentSet Components;
      public byte[] Data;
    }


    public static Payload CreateDestroyPayload(EntityRef entityRef) {
      return new Payload() {
        Type = DebugCommandType.Destroy,
        Entity = entityRef
      };
    }

    public static Payload CreateMaterializePayload(EntityRef entityRef, EntityPrototype prototype, IAssetSerializer serializer) {
      ComponentSet componentSet = default;
      foreach (var component in prototype.Container.Components) {
        componentSet.Add(ComponentTypeId.GetComponentIndex(component.ComponentType));
      }
      return new Payload() {
        Type = DebugCommandType.Create,
        Entity = entityRef,
        Data = serializer.AssetToByteArray(prototype),
        Components = componentSet
      };
    }

    public static Payload CreateRemoveComponentPayload(EntityRef entityRef, Type componentType) {
      var components = new ComponentSet();
      components.Add(ComponentTypeId.GetComponentIndex(componentType));

      return new Payload() {
        Type = DebugCommandType.Destroy,
        Entity = entityRef,
        Components = components
      };
    }

#if QUANTUM_DEBUG_COMMAND_DISABLED
    internal static DeterministicCommand CreateCommand() => null;
    internal static SystemBase CreateSystem() => null;
#else
    internal static DeterministicCommand CreateCommand() => new InternalCommand();
    internal static SystemBase CreateSystem() => new InternalSystem();

#if DEBUG
    private static void Execute(Frame f, ref Payload payload) {
      Exception error = null;
      try {
        switch (payload.Type) {
          case DebugCommandType.Create:
            payload.Entity = ExecuteCreate(f, payload.Entity, payload.Data);
            break;
          case DebugCommandType.Destroy:
            ExecuteDestroy(f, payload.Entity, payload.Components);
            break;
          default:
            if (payload.Type >= DebugCommandType.UserCommandTypeStart) {
              ExecuteUser(f, ref payload);
            } else {
              throw new InvalidOperationException($"Unknown command type: {payload.Type}");
            }
            break;
        }
      } catch (Exception ex) {
        error = ex;
      }
      _commandExecuted?.Invoke(payload, error);
    }

    private static void ExecuteDestroy(Frame f, EntityRef entity, ComponentSet components) {
      if (!f.Exists(entity)) {
        Log.Error($"Entity does not exist: {entity}");
      } else if (components.IsEmpty) {
        if (!f.Destroy(entity)) {
          Log.Error($"Failed to destroy entity {entity}");
        }
      } else {
        for (int i = 1; i < ComponentTypeId.Type.Length; ++i) {
          if (!components.IsSet(i)) {
            continue;
          }
          var type = ComponentTypeId.Type[i];
          if (!f.Remove(entity, type)) {
            Log.Error($"Failed to destroy component {type} of entity {entity}");
          }
        }

      }
    }

    private static EntityRef ExecuteCreate(Frame f, EntityRef entity, byte[] data) {
      EntityPrototype prototype = null;
      if (data?.Length > 0) {
        prototype = f.Context.AssetSerializer.AssetFromByteArray<EntityPrototype>(data);
        if (prototype == null) {
          Log.Error("No prototype found");
        }
      }

      if (!entity.IsValid) {
        if (prototype != null) {
          entity = f.Create(prototype);
        } else {
          entity = f.Create();
        }
      } else if (prototype != null) {
        f.Set(entity, prototype, out _);
      }

      return entity;
    }

    static partial void ExecuteUser(Frame f, ref Payload payload);
    static partial void SerializeUser(BitStream stream, ref Payload payload);

    private class InternalCommand : DeterministicCommand {
      public Payload[] Data = { };

      public override void Serialize(BitStream stream) {
        stream.SerializeArrayLength(ref Data);

        for (int i = 0; i < Data.Length; ++i) {
          stream.Serialize(ref Data[i].Id);
          stream.Serialize(ref Data[i].Type);
          stream.Serialize(ref Data[i].Entity);
          stream.Serialize(ref Data[i].Data);
          unsafe {
            var set = Data[i].Components;
            for (int block = 0; block < ComponentSet.BLOCK_COUNT; ++block) {
              stream.Serialize((&set)->_set + block);
            }
            Data[i].Components = set;
          }
          SerializeUser(stream, ref Data[i]);
        }
      }
    }

    private class InternalSystem : SystemMainThread {
      public override void Update(Frame f) {
        for (int p = 0; p < f.PlayerCount; ++p) {
          if (f.GetPlayerCommand(p) is InternalCommand cmd) {
            for (int i = 0; i < cmd.Data.Length; ++i) {
              Execute(f, ref cmd.Data[i]);
            }
          }
        }
      }
    }
#else
    private class InternalCommand : DeterministicCommand {
      public override void Serialize(BitStream stream) {
        throw new NotSupportedException("DebugCommands only work in DEBUG mode");
      }
    }

    private class InternalSystem : SystemBase {
      protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
        return taskHandle;
      }
    }
#endif
#endif
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Core/EntityPrototypeSystem.cs

namespace Quantum.Core {
  public unsafe sealed partial class EntityPrototypeSystem : SystemSignalsOnly, ISignalOnMapChanged {
    public override void OnInit(Frame f) {
      OnMapChanged(f, default);
    }

    public void OnMapChanged(Frame f, AssetRef<Map> previousMap) {
      if (previousMap.Id.IsValid) {
        foreach (var (entity, _) in f.GetComponentIterator<MapEntityLink>()) {
          f.Destroy(entity);
        }
      }

      if (f.Map != null) {
        f.Create(f.Map.MapEntities, f.Map);
      }
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Core/NavigationSystem.cs

namespace Quantum.Core {
  using Quantum.Task;
  using Photon.Deterministic;

  public unsafe class NavigationSystem : SystemBase, INavigationCallbacks {
    Frame _f;

    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      _f = f;
      return f.Navigation.Update(f, f.DeltaTime, this, taskHandle);
    }

    public void OnWaypointReached(EntityRef entity, FPVector3 waypoint, Navigation.WaypointFlag waypointFlags, ref bool resetAgent) {
      _f.Signals.OnNavMeshWaypointReached(entity, waypoint, waypointFlags, ref resetAgent);
    }

    public void OnSearchFailed(EntityRef entity, ref bool resetAgent) {
      _f.Signals.OnNavMeshSearchFailed(entity, ref resetAgent);
    }

    public void OnMoveAgent(EntityRef entity, FPVector2 desiredDirection) {
      _f.Signals.OnNavMeshMoveAgent(entity, desiredDirection);
    }
  }
}

#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Core/PhysicsSystem.cs

namespace Quantum.Core {
  using Quantum.Task;
  public unsafe partial class PhysicsSystem2D : SystemBase, ICollisionCallbacks2D {
    public override void OnInit(Frame f) {
      f.Physics2D.Init();
    }

    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      return f.Physics2D.Update(this, f.DeltaTime, taskHandle);
    }
    
    public void OnCollision2D(FrameBase f, CollisionInfo2D info) {
      ((Frame)f).Signals.OnCollision2D(info);
    }

    public void OnCollisionEnter2D(FrameBase f, CollisionInfo2D info) {
      ((Frame)f).Signals.OnCollisionEnter2D(info);
    }

    public void OnCollisionExit2D(FrameBase f, ExitInfo2D info) {
      ((Frame)f).Signals.OnCollisionExit2D(info);
    }

    public void OnTrigger2D(FrameBase f, TriggerInfo2D info) {
      ((Frame)f).Signals.OnTrigger2D(info);
    }

    public void OnTriggerEnter2D(FrameBase f, TriggerInfo2D info) {
      ((Frame)f).Signals.OnTriggerEnter2D(info);
    }

    public void OnTriggerExit2D(FrameBase f, ExitInfo2D info) {
      ((Frame)f).Signals.OnTriggerExit2D(info);
    }
  }
  
  public unsafe partial class PhysicsSystem3D : SystemBase, ICollisionCallbacks3D {
    public override void OnInit(Frame f) {
      f.Physics3D.Init();
    }
    
    protected override TaskHandle Schedule(Frame f, TaskHandle taskHandle) {
      return f.Physics3D.Update(this, f.DeltaTime, taskHandle);
    }

    public void OnCollision3D(FrameBase f, CollisionInfo3D info) {
      ((Frame)f).Signals.OnCollision3D(info);
    }

    public void OnCollisionEnter3D(FrameBase f, CollisionInfo3D info) {
      ((Frame)f).Signals.OnCollisionEnter3D(info);
    }

    public void OnCollisionExit3D(FrameBase f, ExitInfo3D info) {
      ((Frame)f).Signals.OnCollisionExit3D(info);
    }

    public void OnTrigger3D(FrameBase f, TriggerInfo3D info) {
      ((Frame)f).Signals.OnTrigger3D(info);
    }

    public void OnTriggerEnter3D(FrameBase f, TriggerInfo3D info) {
      ((Frame)f).Signals.OnTriggerEnter3D(info);
    }

    public void OnTriggerExit3D(FrameBase f, ExitInfo3D info) {
      ((Frame)f).Signals.OnTriggerExit3D(info);
    }
  }
}


#endregion


#region Assets/Photon/Quantum/Simulation/Systems/Core/PlayerConnectedSystem.cs

namespace Quantum.Core {
  public unsafe class PlayerConnectedSystem : SystemMainThread {
    public override void Update(Frame f) {
      if (f.IsVerified == false) {
        return;
      }

      for (int p = 0; p < f.PlayerCount; p++) {
        var isPlayerConnected = (f.GetPlayerInputFlags(p) & Photon.Deterministic.DeterministicInputFlags.PlayerNotPresent) == 0;

        var playerLastConnectionStateRef = f.PlayerLastConnectionState;
        
        if (isPlayerConnected != playerLastConnectionStateRef.IsSet(p)) {
          if (isPlayerConnected) {
            f.PlayerConnectedCount++;
            f.Signals.OnPlayerConnected(p);
          } else {
            f.PlayerConnectedCount--;
            f.Signals.OnPlayerDisconnected(p);
          }

          if (isPlayerConnected) {
            playerLastConnectionStateRef.Set(p);
          }
          else {
            playerLastConnectionStateRef.Clear(p);
          }
        }
      }
    }
  }
}



#endregion

#endif
