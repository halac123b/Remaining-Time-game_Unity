using UnityEngine;

[CreateAssetMenu(fileName = "Player Data SO", menuName = "Need4Time/Player Data", order = 2)]
public class PlayerDataSO : ScriptableObject
{
  [Header("Data")]
  public Sprite playerSprite;          // CharacterSprite for the character selection scene
  public Sprite characterSprite;      // The character ship sprite for the character selection scene
  public Sprite iconSprite;               // Sprite use on the player UI on gameplay scene
  public Sprite iconDeathSprite;          // Sprite use on the player UI on gameplay scene for his death
  public string playerName;            // Character name
  public GameObject spaceshipPrefab;      // Prefab of the spaceship to use on gameplay scene
  public GameObject spaceshipScorePrefab; // Sprite for the ship on the endgame scene UI
  public Color color;                     // The color that identifies this character, use for coloring sprites (laser)
  public Color darkColor;                 // Dark color user for the gameplay UI to set the player name color

  [Header("Client Info")]
  public ulong clientId;                  // The clientId who selected this character
  public int playerId;                    // With player is [1,2,3,4] -> more in case more player can play
  public bool isSelected;                 // Use for locking this character on the character selection scene

  [Header("Score")]
  public int enemiesDestroyed;            // The enemies defeat by the player for the final score
  public int powerUpsUsed;                // The power ups used by the player for the final score

  void OnEnable()
  {
    EmptyData();
  }

  public void EmptyData()
  {
    isSelected = false;
    clientId = 0;
    playerId = -1;
    enemiesDestroyed = 0;
    powerUpsUsed = 0;
  }
}