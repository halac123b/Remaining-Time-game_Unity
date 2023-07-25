using UnityEngine;
using UnityEngine.UI;

public class ChangeItemBtn : MonoBehaviour
{
  private Button button;
  private PlayerItem playerItem;

  private void Start()
  {
    playerItem = FindObjectOfType<PlayerItem>();
    button = GetComponent<Button>();
    // button.onClick.AddListener(playerItem.ChangeItem);
  }
}
