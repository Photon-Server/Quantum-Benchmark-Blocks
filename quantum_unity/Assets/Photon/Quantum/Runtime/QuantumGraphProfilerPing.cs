namespace Quantum.Profiling
{
  using Photon.Client;

  public sealed class QuantumGraphProfilerPing : QuantumGraphProfilerValueSeries
  {
    protected override void OnUpdate()
    {
      long ping = 0;

      PhotonPeer peer = QuantumGraphProfilersUtility.GetNetworkPeer();
      if (peer != null)
      {
        ping = peer.Stats.RoundtripTime;
        if (ping > 9999)
        {
          ping = default;
        }
      }

      AddValue(ping);
    }
  }
}
