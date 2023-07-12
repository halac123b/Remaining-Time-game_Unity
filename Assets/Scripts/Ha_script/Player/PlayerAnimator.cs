using UnityEngine;
using System;
using Unity.Netcode;

public class PlayerAnimator : NetworkBehaviour
{
  private const string IS_WALKING = "IsWalking";
  private const string HORIZONTAL = "Horizontal";
  private const string VERTICAL = "Vertical";
  private const string IS_DEAD = "IsDead";

  private Animator animator;

  private PlayerMovement playerMovement;
  private PlayerStatus playerStatus;

  private SpriteRenderer sprite;

  private NetworkVariable<bool> flipX = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  private void Awake()
  {
    animator = GetComponent<Animator>();
    playerMovement = GetComponentInParent<PlayerMovement>();
    playerStatus = GetComponentInParent<PlayerStatus>();
    playerStatus.OnDeadTrigger += OnDeadAnimation;

    sprite = GetComponent<SpriteRenderer>();

    flipX.OnValueChanged += OnFlipXChanged;
  }

  private void Update()
  {
    if (!IsOwner)
    {
      return;
    }

    animator.SetBool(IS_WALKING, playerMovement.WalkVector() != Vector2.zero);

    if (playerMovement.WalkVector().y != 0 || playerMovement.WalkVector().x != 0)
    {
      animator.SetFloat(VERTICAL, playerMovement.WalkVector().y);
      animator.SetFloat(HORIZONTAL, playerMovement.WalkVector().x);
      flipX.Value = Mathf.Sign(playerMovement.WalkVector().x) >= 0;
    }
    //playerMovement.transform.localScale = new Vector2(-Mathf.Sign(playerMovement.WalkVector().x), 1f);
  }

  private void OnDeadAnimation(object sender, EventArgs e)
  {
    animator.SetTrigger(IS_DEAD);
    playerMovement.enabled = false;
  }

  private void OnFlipXChanged(bool oldValue, bool newValue)
  {
    sprite.flipX = newValue;
  }
}
