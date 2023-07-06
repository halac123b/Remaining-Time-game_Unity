using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
  [SerializeField] private PlayerItem playerItem;
  [SerializeField] private Image image;
  [SerializeField] private TextMeshProUGUI numberText;

  private void Start()
  {
    image.sprite = playerItem.GetCurrentItem().item.GetSprite();
    numberText.text = playerItem.GetCurrentItem().number.ToString();
    playerItem.OnChangeItem += ChangeItem;
  }

  private void ChangeItem(object sender, EventArgs e)
  {
    image.sprite = playerItem.GetCurrentItem().item.GetSprite();
    numberText.text = playerItem.GetCurrentItem().number.ToString();
  }
}
