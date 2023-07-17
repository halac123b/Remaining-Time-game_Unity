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

  [SerializeField] protected Animator animator;

  protected PlayerStatus playerStatus;

  protected PlayerInput playerInput;
  protected PlayerMovement playerMovement;

  protected virtual void Awake()
  {
    playerInput = GetComponentInParent<PlayerInput>();
    playerMovement = GetComponentInParent<PlayerMovement>();
    playerStatus = FindObjectOfType<PlayerStatus>();

    playerStatus.OnDeadTrigger += OnDeadAnimation;

    //PlayerInput
    playerInput.playerInputActions.Player.Attack.performed += TriggerAttackPerformed;
    playerInput.playerInputActions.Player.Attack.canceled += TriggerAttackCanceled;

    


  }

    protected virtual void TriggerAttackCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
      animator.SetTrigger(ATTACK_CANCEL);
    }

   protected virtual void TriggerAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
      animator.SetTrigger(ATTACK);
    }

  protected virtual void Update()
  {
    if (!IsOwner) return;

    animator.SetFloat(SPEED, playerMovement.MoveVector().magnitude);
    animator.SetInteger(TYPE_MOVE, playerMovement.GetTypeMove());

    float x = playerMovement.MoveVector().x;
    float y = playerMovement.MoveVector().y;

    SetVERNHOR(animator, x, y);
  }

  protected virtual void SetVERNHOR(Animator anim, float x, float y)
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

  private void OnDeadAnimation(object sender, EventArgs e)
  {
    animator.SetTrigger("isDeath");
    playerMovement.enabled = false;
  }
}
