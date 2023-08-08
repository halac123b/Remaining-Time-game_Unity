using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckReady : MonoBehaviour
{
  private Button readyBtn;

  private TextMeshProUGUI btnText;

  private string readyState = "no";

  private void Start()
  {
    readyBtn = GetComponent<Button>();
    btnText = GetComponentInChildren<TextMeshProUGUI>();

    readyBtn.onClick.AddListener(UpdateReadyState);
    btnText.text = "Ready";
  }

  private void UpdateReadyState()
  {
    if (readyState == "yes")
    {
      readyState = "no";
      btnText.text = "Ready";
    }
    else
    {
      readyState = "yes";
      btnText.text = "Nope";
    }

    LobbyManager.Instance.UpdatePlayerReady(readyState);
  }
}
