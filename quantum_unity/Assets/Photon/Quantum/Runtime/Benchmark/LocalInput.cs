using System;
using Photon.Deterministic;
using Quantum;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public unsafe class LocalInput : MonoBehaviour {
    

  public int numberOfSamples;
  public int grownScale;
  public int max;
  public bool running;

  [SerializeField] InputField samplesInput;
  [SerializeField] InputField grownInput;
  [SerializeField] InputField maxInput;
  [SerializeField] InputField fileNameInput;
  [SerializeField] InputField pathInput;
  [SerializeField] Text logText;
  [SerializeField] string filePath;
  [SerializeField] string fileName;

  public void Start() {
    fileNameInput.text = fileName;
    if (Application.isEditor) {
      pathInput.text = Application.dataPath;
    } else if(Application.isMobilePlatform){
      pathInput.text = Application.persistentDataPath;  
    } else {
      pathInput.text = Directory.GetCurrentDirectory();
    }
    
  }

  public void StartRunning() {
    running = true;
    //TODO: a command is better
    var singleton = QuantumRunner.Default.Game.Frames.Verified.Unsafe.GetPointerSingleton<BenchmarkSingleton>();
    singleton->filename = fileNameInput.text;
    singleton->path = pathInput.text;
  }

  public void StopRunning() {
    running = false;
    logText.text = "";
  }

  public void SetNumberOfSamples() {
    this.numberOfSamples = int.Parse(samplesInput.text);
    this.grownScale = int.Parse(grownInput.text);
    this.max = int.Parse(maxInput.text);
  }

  private void OnEnable() {
    QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    QuantumEvent.Subscribe(listener: this, handler: (EventLog e) => LogData(e));
    QuantumEvent.Subscribe(listener: this, handler: (EventWriteLog e) => WriteLogData(e));
  }

  public void LogData(EventLog e) {
    logText.text += e.logText;
  }

  public void WriteLogData(EventWriteLog e) {
    Debug.Log("Document saved at :" + e.logText);
    logText.text += e.logText;
  }

  public void PollInput(CallbackPollInput callback) {
    Quantum.Input i = new Quantum.Input(); 
    i.samples = numberOfSamples;
    i.grown = grownScale;
    i.max = max;
    i.runnig = running;
    callback.SetInput(i, DeterministicInputFlags.Repeatable);
    
  }
}
