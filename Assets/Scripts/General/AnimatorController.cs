using UnityEngine;
using System;
using Unity.Netcode;

public class AnimatorController : NetworkBehaviour
{
  protected const string HORIZONTAL = "Horizontal";
  protected const string VERTICAL = "Vertical";
  protected const string IS_PROCESSING = "isprocessing";
  protected const string SPEED = "speed";
  protected const string TYPE_MOVE = "typemove";
  protected const string TYPE_ATTACK = "typeattack";
  protected const string ATTACK = "attack";
  protected const string ATTACK_CANCEL = "attackcancel";

  [SerializeField] public Animator animator;

  protected PlayerStatus playerStatus;

  protected PlayerInput playerInput;
  protected PlayerMovement playerMovement;
  protected NetworkVariable<PlayerData> playerData = new NetworkVariable<PlayerData>(
    new PlayerData
    {
      Id = 0,
      color = Color.red,
      playerName = "",
      playerWeapon = 0,
    }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


  public PlayerData GetPlayerData()
  {
    return playerData.Value;
  }

  protected virtual void Start()
  {
    if (IsOwner)
    {
      playerData.Value = PointManager.Instance.GetPlayerData(Convert.ToInt32(NetworkManager.Singleton.LocalClientId));
    }
  }

  protected virtual void Awake()
  {
    playerInput = GetComponentInParent<PlayerInput>();
    playerMovement = GetComponentInParent<PlayerMovement>();
    playerStatus = FindObjectOfType<PlayerStatus>();

    //PlayerInput
    // if (playerInput.playerInputActions != null) Debug.LogError("playerInput!");
    playerInput.playerInputActions.Player.Attack.started += TriggerAttackStarted;
    playerInput.playerInputActions.Player.Attack.canceled += TriggerAttackCanceled;
  }

  protected virtual void TriggerAttackCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
  {
    if (!IsOwner || animator == null || !playerStatus.canattack) return;
    animator.SetTrigger(ATTACK_CANCEL);
  }

  protected virtual void TriggerAttackStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
  {
    if (!IsOwner || animator == null || !playerStatus.canattack) return;
    animator.SetTrigger(ATTACK);
  }

  protected virtual void Update()
  {
    if (!IsOwner) return;

    animator.SetFloat(SPEED, playerMovement.MoveVector().magnitude);

    float x = playerMovement.MoveVector().x;
    float y = playerMovement.MoveVector().y;

    Set_VERTICAL_HORIZONTAL(animator, x, y);
  }


  public virtual void Set_VERTICAL_HORIZONTAL(float x, float y)
  {
    if (!IsOwner) return;
    Set_VERTICAL_HORIZONTAL(animator, x, y);
    // Set_VERTICAL_HORIZONTAL(cover_animator, x, y);
    // Set_VERTICAL_HORIZONTAL(weapon_animator, x, y);
  }
  public virtual void Set_VERTICAL_HORIZONTAL(Animator anim, float x, float y)
  {
    if (y > 0.01f)
    {
      anim.SetFloat(VERTICAL, 1f);
    }
    else if (y < -0.01f)
    {
      anim.SetFloat(VERTICAL, -1f);
    }

    if (x > 0.01f)
    {
      anim.SetFloat(VERTICAL, 0f);
      anim.SetFloat(HORIZONTAL, 1f);
    }
    else if (x < -0.01f)
    {
      anim.SetFloat(VERTICAL, 0f);
      anim.SetFloat(HORIZONTAL, -1f);
    }
  }


}
