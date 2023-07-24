using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipStore : MonoBehaviour
{
  [SerializeField] Button exitBtn;

  [SerializeField] private List<EquipmentSO> equipmentList = new List<EquipmentSO>();

  [SerializeField] Image[] sprite;
  [SerializeField] TextMeshProUGUI nameEquip;
  [SerializeField] TextMeshProUGUI[] price;
  [SerializeField] Sprite alpha;

  [SerializeField] Button[] navigationBtn;
  [SerializeField] Button[] itemSlotBtn;

  [SerializeField] Slider[] detailSlider;
  [SerializeField] TextMeshProUGUI[] detailText;

  [SerializeField] Button[] buyBtn;

  [SerializeField] TextMeshProUGUI pointText;

  [SerializeField] PlayerEquip playerEquip;
  [SerializeField] PlayerStatus playerStatus;


  private int currentSlot;
  private int lastActiveEquip = 0;

  private void Start()
  {
    pointText.text = playerStatus.GetPoint().ToString();

    exitBtn.onClick.AddListener(Exit);
    navigationBtn[0].onClick.AddListener(NavigateLeft);
    navigationBtn[1].onClick.AddListener(NavigateRight);

    itemSlotBtn[0].onClick.AddListener(delegate { UpdateInfo(0); });
    itemSlotBtn[1].onClick.AddListener(delegate { UpdateInfo(1); });
    itemSlotBtn[2].onClick.AddListener(delegate { UpdateInfo(2); });
    itemSlotBtn[3].onClick.AddListener(delegate { UpdateInfo(3); });

    buyBtn[0].onClick.AddListener(delegate { BuyEquip(0); });
    buyBtn[1].onClick.AddListener(delegate { BuyEquip(1); });
    buyBtn[2].onClick.AddListener(delegate { BuyEquip(2); });
    buyBtn[3].onClick.AddListener(delegate { BuyEquip(3); });
  }

  private void OnEnable()
  {
    currentSlot = 0;
    UpdateSlot();
    UpdateInfo(0);
    pointText.text = playerStatus.GetPoint().ToString();
  }

  private void UpdateSlot()
  {
    if (currentSlot > 0)
    {
      navigationBtn[0].gameObject.SetActive(true);
    }
    else
    {
      navigationBtn[0].gameObject.SetActive(false);
    }

    if (currentSlot + 4 <= equipmentList.Count)
    {
      navigationBtn[1].gameObject.SetActive(true);
    }
    else
    {
      navigationBtn[1].gameObject.SetActive(false);
    }

    for (int i = 0; i < 4; i++)
    {
      if (i + currentSlot >= equipmentList.Count)
      {
        sprite[i].sprite = alpha;
        price[i].text = "";
      }
      else
      {
        sprite[i].sprite = equipmentList[currentSlot + i].GetSprite();
        price[i].text = equipmentList[currentSlot + i].GetPrice().ToString() + "$";
      }
    }

    UpdateInfo(lastActiveEquip);
  }

  private void UpdateInfo(int index)
  {
    // Update color
    Color greenColor;
    ColorUtility.TryParseHtmlString("#69E5B2", out greenColor);
    if (lastActiveEquip != -1)
    {
      itemSlotBtn[lastActiveEquip].gameObject.GetComponent<Image>().color = greenColor;
    }

    itemSlotBtn[index].gameObject.GetComponent<Image>().color = Color.red;

    lastActiveEquip = index;

    EquipmentSO equip = equipmentList[currentSlot + index];
    nameEquip.text = equip.GetName();

    detailSlider[0].value = equip.damage;
    detailText[0].text = equip.damage.ToString();

    detailSlider[1].value = equip.speed;
    detailText[1].text = equip.speed.ToString();

    detailSlider[2].value = equip.range;
    detailText[2].text = equip.range.ToString();
  }

  private void Exit()
  {
    gameObject.SetActive(false);
  }

  private void NavigateLeft()
  {
    currentSlot -= 4;
    UpdateSlot();
  }

  private void NavigateRight()
  {
    currentSlot += 4;
    UpdateSlot();
  }

  private void BuyItem(int index)
  {
    EquipmentSO equip = equipmentList[currentSlot + index];

  }

  private void BuyEquip(int index)
  {
    if (currentSlot + index > equipmentList.Count)
    {
      return;
    }

    if (equipmentList[currentSlot + index].GetPrice() > playerStatus.GetPoint())
    {
      return;
    }

    playerStatus.SetPoint(playerStatus.GetPoint() - equipmentList[currentSlot + index].GetPrice());
    playerEquip.AddEquip(equipmentList[currentSlot + index]);
    equipmentList.RemoveAt(currentSlot + index);
    pointText.text = playerStatus.GetPoint().ToString();
    UpdateSlot();
  }
}
