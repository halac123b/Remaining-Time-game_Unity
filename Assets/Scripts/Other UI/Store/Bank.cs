using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Bank : MonoBehaviour
{
  [SerializeField] Button exitBtn;

  [SerializeField] private int minBid = 30;
  [SerializeField] private int step = 10;

  PlayerStatus playerStatus;

  private int bidCurrent;

  [SerializeField] TextMeshProUGUI bid;
  [SerializeField] TextMeshProUGUI currentPoint;

  [SerializeField] Button[] stepArrow;

  [SerializeField] private ShoppingManager manager;

  private void Awake()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();
  }
  private void Start()
  {
    manager.OnUpdatePoint += FirstUpdate;

    exitBtn.onClick.AddListener(Exit);

    stepArrow[0].onClick.AddListener(delegate { UpdateBid(step); });
    stepArrow[1].onClick.AddListener(delegate { UpdateBid(-step); });
    stepArrow[2].onClick.AddListener(delegate { UpdateBid(1); });
    stepArrow[3].onClick.AddListener(delegate { UpdateBid(-1); });

    gameObject.SetActive(false);
  }

  private void FirstUpdate(object sender, EventArgs e)
  {
    playerStatus.bid = minBid;
    playerStatus.SetPoint(playerStatus.GetPoint() - minBid);
    bid.text = playerStatus.bid.ToString();
    DisplayStep();
    currentPoint.text = playerStatus.GetPoint().ToString();
  }

  private void OnEnable()
  {
    if (gameObject.transform.localScale == new Vector3(0, 0, 0))
    {
      gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }
    currentPoint.text = playerStatus.GetPoint().ToString();
  }

  private void DisplayStep()
  {
    if (playerStatus.GetPoint() < step)
    {
      stepArrow[0].gameObject.SetActive(false);
    }
    else
    {
      stepArrow[0].gameObject.SetActive(true);
    }

    if (playerStatus.GetPoint() < 1)
    {
      stepArrow[2].gameObject.SetActive(false);
    }
    else
    {
      stepArrow[2].gameObject.SetActive(true);
    }

    if (playerStatus.bid - step >= minBid)
    {
      stepArrow[1].gameObject.SetActive(true);
    }
    else
    {
      stepArrow[1].gameObject.SetActive(false);
    }

    if (playerStatus.bid - 1 >= minBid)
    {
      stepArrow[3].gameObject.SetActive(true);
    }
    else
    {
      stepArrow[3].gameObject.SetActive(false);
    }
  }

  private void UpdateBid(int amount)
  {
    playerStatus.bid += amount;
    bid.text = playerStatus.bid.ToString();
    playerStatus.SetPoint(playerStatus.GetPoint() - amount);
    DisplayStep();
    currentPoint.text = playerStatus.GetPoint().ToString();
  }

  private void Exit()
  {
    playerStatus.canMove = true;
    gameObject.SetActive(false);
  }
}
