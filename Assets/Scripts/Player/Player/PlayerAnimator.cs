using UnityEngine;
using System;
using Unity.Netcode;

public class PlayerAnimator : NetworkBehaviour
{
  private const string IS_MoveING = "IsMoveing";
  private const string HORIZONTAL = "Horizontal";
  private const string VERTICAL = "Vertical";
  private const string IS_DEAD = "IsDead";

  private Animator animator;
  

  private PlayerMovement playerMovement;
  private PlayerStatus playerStatus;
  private SpriteRenderer sprite;

  private NetworkVariable<bool> flipX = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  private NetworkVariable<PlayerData> playerData = new NetworkVariable<PlayerData>(
    new PlayerData 
    {
        Id = "",
        color = Color.white,
        playerName = ""
    },NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  private void Awake()
  {
    animator = GetComponent<Animator>();
    playerMovement = GetComponentInParent<PlayerMovement>();
    playerStatus = FindObjectOfType<PlayerStatus>();
    playerStatus.OnDeadTrigger += OnDeadAnimation;

    sprite = GetComponent<SpriteRenderer>();

    flipX.OnValueChanged += OnFlipXChanged;
    playerData.OnValueChanged += OnPlayerDataChanged;
    
  }

    private void OnPlayerDataChanged(PlayerData previousValue, PlayerData newValue)
    {
      sprite.material.color = playerData.Value.color;
    }

    private void Start()
  {
    if(IsOwner) playerData.Value = playerStatus.GetPlayerData();
  }

  private void Update()
  {
    sprite.material.color = playerData.Value.color;
    if (!IsOwner)
    {
      return;
    }

    animator.SetFloat("speed",playerMovement.MoveVector().magnitude );
    animator.SetInteger("typemove", playerMovement.GetTypeMove());

   float x = playerMovement.MoveVector().x;
   float y = playerMovement.MoveVector().y;
    if (y > 0.01) {
      y = 1f;
    }
    else if (y < -0.01){
      y = -1f;
    }
    
    if (x > 0.01) {
      x =1f;
      y = 0f;
    }
    else if (x < -0.01) {
      x= -1f;
      y=  0f;
    }

    animator.SetFloat(VERTICAL, y);
    animator.SetFloat(HORIZONTAL, x);
  




      flipX.Value = Mathf.Sign(playerMovement.MoveVector().x) >= 0;
  }

  private void OnDeadAnimation(object sender, EventArgs e)
  {
    animator.SetTrigger("isDeath");
    playerMovement.enabled = false;
  }

  private void OnFlipXChanged(bool oldValue, bool newValue)
  {
    sprite.flipX = newValue;
  }

  public void UpdatePlayerColor(Color color){
    sprite.material.color = color;
  }

}
