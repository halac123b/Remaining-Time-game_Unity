using Unity.Netcode;

using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
  [SerializeField]
  private PlayerDataSO[] m_PlayerDatas;

  [SerializeField]
  private SceneName nextScene = SceneName.LobbySelection;

  private IEnumerator Start()
  {
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

    ClearAllCharacterData();

    // Wait for the network Scene Manager to start
    yield return new WaitUntil(() => NetworkManager.Singleton.SceneManager != null);

    // Set the events on the loading manager
    // Doing this because every time the network session ends the loading manager stops
    // detecting the events
    LoadingSceneManager.Instance.Init();
  }

  private void ClearAllCharacterData()
  {
    // Clean the all the data of the characters so we can start with a clean slate
    foreach (PlayerDataSO data in m_PlayerDatas)
    {
      data.EmptyData();
    }
  }

  public void OnClickStart()
  {
    LoadingSceneManager.Instance.LoadScene(nextScene, false);
  }
}
