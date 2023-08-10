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

  [SerializeField] int increaseAmount = 500;

  [SerializeField] private SceneName nextScene = SceneName.ShoppingPhase;

  [SerializeField] Sprite[] mapSprite;

  [SerializeField] Image mapImage;

  private void Start()
  {
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
    // LoadingSceneManager.Instance.nextMap = random.Next(2);
    LoadingSceneManager.Instance.nextMap = 0;

    PointManager.Instance.IncreasePointAll(increaseAmount);

    int numPlayer = LoadingSceneManager.Instance.GetNumPlayer();

    PlayerPoint playerData = PointManager.Instance.playerPoint[0];

    UpdateUIClientRpc(0, playerData);

    if (numPlayer >= 2)
    {
      PlayerPoint data = PointManager.Instance.playerPoint[1];
      UpdateUIClientRpc(1, data);
    }

    if (numPlayer >= 3)
    {
      PlayerPoint data = PointManager.Instance.playerPoint[2];
      UpdateUIClientRpc(2, data);
    }
  }

  [ClientRpc]
  private void UpdateUIClientRpc(int index, PlayerPoint data)
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
    if (data.rank == 1)
    {
      playerPoint[index].color = Color.red;
    }

    roundSlider.value = LoadingSceneManager.Instance.GetRound();
    roundText.text = roundSlider.value.ToString() + "/6";

    mapImage.sprite = mapSprite[LoadingSceneManager.Instance.nextMap];
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


