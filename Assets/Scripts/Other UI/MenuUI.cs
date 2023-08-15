using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
  private Button startBtn;
  [SerializeField] MenuController menuController;

  private void Start()
  {
    startBtn = GetComponent<Button>();
    startBtn.onClick.AddListener(menuController.OnClickStart);

  }
}