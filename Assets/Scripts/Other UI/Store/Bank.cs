using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
  [SerializeField] Button exitBtn;

  [SerializeField] private int minBid = 30;
  [SerializeField] private int step = 10;

  [SerializeField] PlayerStatus playerStatus;

  private int bidCurrent;

  [SerializeField] TextMeshProUGUI bid;
  [SerializeField] TextMeshProUGUI currentPoint;

  [SerializeField] Button[] stepArrow;

  private void Start()
  {
    exitBtn.onClick.AddListener(Exit);

    playerStatus.bid = minBid;
    playerStatus.SetPoint(playerStatus.GetPoint() - minBid);
    bid.text = playerStatus.bid.ToString();
    DisplayStep();
    currentPoint.text = playerStatus.GetPoint().ToString();

    stepArrow[0].onClick.AddListener(delegate { UpdateBid(step); });
    stepArrow[1].onClick.AddListener(delegate { UpdateBid(-step); });

    gameObject.SetActive(false);
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

    if (playerStatus.bid - step >= minBid)
    {
      stepArrow[1].gameObject.SetActive(true);
    }
    else
    {
      stepArrow[1].gameObject.SetActive(false);
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
    gameObject.SetActive(false);
  }
}
