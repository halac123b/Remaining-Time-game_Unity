using Unity.Netcode;

using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
  [SerializeField] private SceneName nextScene = SceneName.LobbySelection;

  private void Start()
  {

    AudioManager.Instance.SetAndPlay(0);
    // -- To test with latency on development builds --
    // To set the latency, jitter and packet-loss percentage values for develop builds we need
    // the following code to execute before NetworkManager attempts to connect (changing the
    // values of the parameters as desired).
    //
    // If you'd like to test without the simulated latency, just set all parameters below to zero(0).
    //
    // More information here:
    // https://docs-multiplayer.unity3d.com/netcode/current/tutorials/testing/testing_with_artificial_conditions#debug-builds
#if DEVELOPMENT_BUILD && !UNITY_EDITOR
        NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().
            SetDebugSimulatorParameters(
                packetDelay: 50,
                packetJitter: 5,
                dropRate: 3);
#endif

    //ClearAllCharacterData();
  }

  public void OnClickStart()
  {
    LoadingSceneManager.Instance.LoadScene(nextScene, false);
  }
}
