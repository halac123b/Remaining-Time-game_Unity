using System.Collections;
using UnityEngine;

/*
    Script to go to the Menu scene after the loading manager is load.
*/
public class GoToMenu : MonoBehaviour
{
  [SerializeField] private GameObject logoImg;

  private IEnumerator Start()
  {
    // Wait for the loading scene manager to start
    yield return new WaitUntil(() => LoadingSceneManager.Instance != null);
    yield return new WaitForSeconds(1.5f);

    // Load the menu
    LoadingSceneManager.Instance.LoadScene(SceneName.Menu, false);
    Destroy(logoImg);
  }
}