using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipStore : MonoBehaviour
{
  [SerializeField] Button exitBtn;

  [SerializeField] private List<ItemSO> itemList = new List<ItemSO>();
  private EquipmentInStore equipmentStock;

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

  PlayerEquip playerEquip;
  PlayerStatus playerStatus;
  PlayerItem playerItem;

  [SerializeField] GameObject weaponDescription;
  [SerializeField] TextMeshProUGUI itemDescription;

  [SerializeField] Button[] changeModeBtn;

  public enum Mode : byte
  {
    Weapon,
    Item
  };

  private Mode storeMode = Mode.Weapon;

  private int currentSlot;
  private int lastActiveEquip = 0;

  private void Awake()
  {
    equipmentStock = FindObjectOfType<EquipmentInStore>();
    playerEquip = FindObjectOfType<PlayerEquip>();
    playerItem = FindObjectOfType<PlayerItem>();
    playerStatus = FindObjectOfType<PlayerStatus>();
  }
  private void Start()
  {
    exitBtn.onClick.AddListener(Exit);
    navigationBtn[0].onClick.AddListener(NavigateLeft);
    navigationBtn[1].onClick.AddListener(NavigateRight);

    changeModeBtn[0].onClick.AddListener(delegate { ChangeMode(0); });
    changeModeBtn[1].onClick.AddListener(delegate { ChangeMode(1); });

    itemSlotBtn[0].onClick.AddListener(delegate { UpdateInfo(0); });
    itemSlotBtn[1].onClick.AddListener(delegate { UpdateInfo(1); });
    itemSlotBtn[2].onClick.AddListener(delegate { UpdateInfo(2); });
    itemSlotBtn[3].onClick.AddListener(delegate { UpdateInfo(3); });

    buyBtn[0].onClick.AddListener(delegate { BuyEquip(0); });
    buyBtn[1].onClick.AddListener(delegate { BuyEquip(1); });
    buyBtn[2].onClick.AddListener(delegate { BuyEquip(2); });
    buyBtn[3].onClick.AddListener(delegate { BuyEquip(3); });
  }

  private void ChangeMode(int mode)
  {
    if (mode == 0)
    {
      storeMode = Mode.Weapon;
      weaponDescription.gameObject.SetActive(true);
      itemDescription.gameObject.SetActive(false);
      ChangeColorMode(0);
    }
    else if (mode == 1)
    {
      storeMode = Mode.Item;
      weaponDescription.gameObject.SetActive(false);
      itemDescription.gameObject.SetActive(true);
      ChangeColorMode(1);
    }

    UpdateSlot();
    UpdateInfo(0);
    currentSlot = 0;
  }

  private void ChangeColorMode(int index)
  {
    if (index == 0)
    {
      changeModeBtn[0].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
      changeModeBtn[1].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }
    else if (index == 1)
    {
      changeModeBtn[1].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
      changeModeBtn[0].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }
  }

  private void OnEnable()
  {
    storeMode = Mode.Weapon;
    currentSlot = 0;
    UpdateSlot();
    UpdateInfo(0);
    pointText.text = playerStatus.GetPoint().ToString() + "$";
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

    if ((storeMode == Mode.Weapon && currentSlot + 4 <= equipmentStock.equipmentList.Count) || (storeMode == Mode.Item && currentSlot + 4 <= itemList.Count))
    {
      navigationBtn[1].gameObject.SetActive(true);
    }
    else
    {
      navigationBtn[1].gameObject.SetActive(false);
    }

    for (int i = 0; i < 4; i++)
    {
      if ((storeMode == Mode.Weapon && i + currentSlot >= equipmentStock.equipmentList.Count) || (storeMode == Mode.Item && i + currentSlot >= itemList.Count))
      {
        sprite[i].sprite = alpha;
        price[i].text = "";
      }
      else
      {
        if (storeMode == Mode.Weapon)
        {
          sprite[i].sprite = equipmentStock.equipmentList[currentSlot + i].GetSprite();
          price[i].text = equipmentStock.equipmentList[currentSlot + i].GetPrice().ToString() + "$";
        }

        else if (storeMode == Mode.Item)
        {
          sprite[i].sprite = itemList[currentSlot + i].GetSprite();
          price[i].text = itemList[currentSlot + i].price.ToString() + "$";
        }
      }
    }

    UpdateInfo(lastActiveEquip);
  }

  private void UpdateInfo(int index)
  {
    // Update color
    if ((storeMode == Mode.Weapon && index + currentSlot >= equipmentStock.equipmentList.Count) || (storeMode == Mode.Item && index + currentSlot >= itemList.Count))
    {
      return;
    }
    Color greenColor;
    ColorUtility.TryParseHtmlString("#69E5B2", out greenColor);
    if (lastActiveEquip != -1)
    {
      itemSlotBtn[lastActiveEquip].gameObject.GetComponent<Image>().color = greenColor;
    }

    itemSlotBtn[index].gameObject.GetComponent<Image>().color = Color.red;

    lastActiveEquip = index;

    if (storeMode == Mode.Weapon)
    {
      EquipmentSO equip = equipmentStock.equipmentList[currentSlot + index];
      nameEquip.text = equip.GetName();

      detailSlider[0].value = equip.damage;
      detailText[0].text = equip.damage.ToString();

      detailSlider[1].value = equip.speed;
      detailText[1].text = equip.speed.ToString();

      detailSlider[2].value = equip.range;
      detailText[2].text = equip.range.ToString();
    }

    else if (storeMode == Mode.Item)
    {
      ItemSO item = itemList[currentSlot + index];
      nameEquip.text = item.GetName();

      itemDescription.text = item.description;
    }
  }

  private void Exit()
  {
    playerStatus.canattack = true;
    playerStatus.canMove = true;
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

  private void BuyEquip(int index)
  {
    if ((storeMode == Mode.Weapon && currentSlot + index > equipmentStock.equipmentList.Count) || (storeMode == Mode.Item && currentSlot + index > itemList.Count))
    {
      return;
    }

    if ((storeMode == Mode.Weapon && equipmentStock.equipmentList[currentSlot + index].GetPrice() > playerStatus.GetPoint()) || (storeMode == Mode.Item && itemList[currentSlot + index].price > playerStatus.GetPoint()))
    {
      return;
    }

    if (storeMode == Mode.Weapon)
    {
      playerStatus.SetPoint(playerStatus.GetPoint() - equipmentStock.equipmentList[currentSlot + index].GetPrice());
      playerEquip.AddEquip(equipmentStock.equipmentList[currentSlot + index]);
      equipmentStock.equipmentList.RemoveAt(currentSlot + index);
      pointText.text = playerStatus.GetPoint().ToString();
      UpdateSlot();
    }
    else if (storeMode == Mode.Item)
    {
      playerStatus.SetPoint(playerStatus.GetPoint() - itemList[currentSlot + index].price);
      bool already = false;
      for (int i = 0; i < playerItem.itemList.Count; i++)
      {
        if (playerItem.itemList[i].item.name == itemList[currentSlot + index].name)
        {
          playerItem.itemList[i].number++;
          pointText.text = playerStatus.GetPoint().ToString();
          already = true;
          break;
        }
      }
      if (already == false)
      {
        PlayerItem.ItemSlot newItem = new PlayerItem.ItemSlot
        {
          item = itemList[currentSlot + index],
          number = 1
        };
        playerItem.itemList.Add(newItem);
        pointText.text = playerStatus.GetPoint().ToString();
      }
    }
  }
}
