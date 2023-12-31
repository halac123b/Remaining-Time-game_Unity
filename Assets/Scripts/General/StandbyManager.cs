using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;

public class StandbyManager : SingletonNetwork<StandbyManager>
{
  [SerializeField] CountDown countDown;
  private int numberConnected = 0;

  [SerializeField] TextMeshProUGUI[] playerName;
  [SerializeField] Image[] playerAvatar;
  [SerializeField] TextMeshProUGUI[] playerPoint;
  [SerializeField] Slider roundSlider;
  [SerializeField] TextMeshProUGUI roundText;

  [SerializeField] Sprite[] characterSprite;

  private PlayerData playerData;

  int increaseAmount = 60;

  [SerializeField] private SceneName nextScene = SceneName.ShoppingPhase;

  [SerializeField] Sprite[] mapSprite;

  [SerializeField] Image mapImage;

  private void Start()
  {
    AudioManager.Instance.SetAndPlay(2);
    if (!IsHost)
    {
      return;
    }
    countDown.OnTimeOut += LoadNextScene;

    if (LoadingSceneManager.Instance.GetRound() != 1)
    {
      PointManager.Instance.RollPlayerIndex();
    }

    // Random next map
    System.Random random = new System.Random();
    LoadingSceneManager.Instance.nextMap = random.Next(3);
    // LoadingSceneManager.Instance.nextMap = 1;

    PointManager.Instance.IncreasePointAll(increaseAmount);

    int numPlayer = LoadingSceneManager.Instance.GetNumPlayer();

    PlayerPoint playerData = PointManager.Instance.playerPoint[0];

    int nextMap = LoadingSceneManager.Instance.nextMap;
    int round = LoadingSceneManager.Instance.GetRound();

    UpdateUIClientRpc(0, playerData, nextMap, round);

    if (numPlayer >= 2)
    {
      PlayerPoint data = PointManager.Instance.playerPoint[1];
      UpdateUIClientRpc(1, data, nextMap, round);
    }

    if (numPlayer >= 3)
    {
      PlayerPoint data = PointManager.Instance.playerPoint[2];
      UpdateUIClientRpc(2, data, nextMap, round);
    }
  }


  [ClientRpc]
  private void UpdateUIClientRpc(int index, PlayerPoint data, int nextMap, int round)
  {
    playerName[index].text = data.playerData.playerName;

    if (index == Convert.ToInt32(NetworkManager.Singleton.LocalClientId))
    {
      playerName[index].color = Color.yellow;
    }

    if (Convert.ToInt16(NetworkManager.Singleton.LocalClientId) == index)
    {
      PointManager.Instance.playerPoint[index] = data;
    }

    playerAvatar[index].sprite = characterSprite[Mathf.Clamp(data.playerIndex, 0, 1)];

    playerPoint[index].text = data.point.ToString() + " $";
    if (data.rank == 0)
    {
      playerPoint[index].color = Color.red;
    }
    else
    {
      playerPoint[index].color = Color.white;
    }

    roundSlider.value = round;
    roundText.text = roundSlider.value.ToString() + "/6";

    mapImage.sprite = mapSprite[nextMap];
  }

  private void LoadNextScene(object sender, EventArgs e)
  {
    LoadingSceneManager.Instance.LoadScene(nextScene);
  }

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
}


