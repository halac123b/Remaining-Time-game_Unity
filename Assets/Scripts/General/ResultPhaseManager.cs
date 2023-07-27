using UnityEngine;
using Unity.Netcode;
using TMPro;
using System;
using UnityEngine.UI;

public class ResultPhaseManager : SingletonNetwork<ResultPhaseManager>
{
  private int numberConnected = 0;

  [SerializeField] CountDown countDown;

  [SerializeField] private SceneName nextScene = SceneName.StandbyPhase;

  [SerializeField] private TextMeshProUGUI sceneName;

  [SerializeField] Image[] playerAvatar;
  [SerializeField] Sprite[] characterSprite;

  [SerializeField] TextMeshProUGUI[] playerName;

  [SerializeField] TextMeshProUGUI[] playerPoint;

  [SerializeField] TextMeshProUGUI[] resultText;

  public void ServerSceneInit()
  {
    numberConnected++;

    // Check if is the last client
    if (numberConnected != LoadingSceneManager.Instance.GetNumPlayer())
      return;

    StartCountClientRpc();
  }

  [ClientRpc]
  private void StartCountClientRpc()
  {
    countDown.SetStartCounting();
  }

  private void Start()
  {
    if (!IsHost)
    {
      return;
    }
    countDown.OnTimeOut += LoadNextScene;

    PlayerPoint playerData;
    for (int i = 0; i < 3; i++)
    {

      playerData = PointManager.Instance.playerPoint[i];
      Debug.Log("vvv" + playerData.roundRank);
      UpdateUIClientRpc(playerData.roundRank, playerData);
    }
  }

  private void LoadNextScene(object sender, EventArgs e)
  {
    LoadingSceneManager.Instance.LoadScene(nextScene);
    LoadingSceneManager.Instance.GoNextRound();
  }

  [ClientRpc]
  private void UpdateUIClientRpc(int index, PlayerPoint data)
  {
    sceneName.text = LoadingSceneManager.Instance.GetRound().ToString();

    playerName[index].text = data.playerData.playerName;

    if (data.playerData.Id == NetworkManager.Singleton.LocalClientId)
    {
      playerName[index].color = Color.yellow;
    }

    playerAvatar[index].sprite = characterSprite[Mathf.Clamp(data.playerIndex, 0, 1)];

    playerPoint[index].text = data.point.ToString() + " $";

    if (data.rank == 1)
    {
      playerPoint[index].color = Color.red;
    }

    if (index == 0)
    {
      resultText[0].text = "+" + data.bidAmount.ToString() + " $";
    }
    else if (index == 2)
    {
      resultText[1].text = "-" + data.bidAmount.ToString() + " $";
    }
  }
}
