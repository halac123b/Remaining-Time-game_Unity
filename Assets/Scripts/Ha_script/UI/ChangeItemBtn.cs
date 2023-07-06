using UnityEngine;
using UnityEngine.UI;

public class ChangeItemBtn : MonoBehaviour
{
  private Button button;
  [SerializeField] private PlayerItem playerItem;

  void Start()
  {
    button = GetComponent<Button>();
    button.onClick.AddListener(playerItem.ChangeItem);
  }
}
