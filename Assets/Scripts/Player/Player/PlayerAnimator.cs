using UnityEngine;
using System;
using Unity.Netcode;

public class PlayerAnimator : NetworkBehaviour
{
  private const string HORIZONTAL = "Horizontal";
  private const string VERTICAL = "Vertical";
  private const string IS_PROCESSING = "isprocessing";
  private const string SPEED = "speed";
  private const string TYPE_MOVE = "typemove";

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
    }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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
    if (IsOwner) playerData.Value = playerStatus.GetPlayerData();
  }

  private void Update()
  {
    sprite.material.color = playerData.Value.color;
    if (!IsOwner)
    {
      return;
    }

    animator.SetFloat(SPEED, playerMovement.MoveVector().magnitude);
    animator.SetInteger(TYPE_MOVE, playerMovement.GetTypeMove());

    float x = playerMovement.MoveVector().x;
    float y = playerMovement.MoveVector().y;
    
    if (x <= -0.01f){
            flipX.Value = false;
        }else if (x >= 0.01f){
            flipX.Value = true;
        }
    
    if (y > 0.01f)
    {
      animator.SetFloat(VERTICAL, 1f);
    }
    else if (y < -0.01f)
    {
      animator.SetFloat(VERTICAL, -1f);
    }

    if (x > 0.01f)
    {
      animator.SetFloat(VERTICAL, 0f);
      animator.SetFloat(HORIZONTAL, 1f);
    }
    else if (x < -0.01f)
    {
      animator.SetFloat(VERTICAL, 0f);
      animator.SetFloat(HORIZONTAL, -1f);
    }

    animator.SetBool(IS_PROCESSING, playerMovement.GetProcessStatus());
    
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

  public void UpdatePlayerColor(Color color)
  {
    sprite.material.color = color;
  }

}
