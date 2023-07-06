using UnityEngine;
using System;

public class PlayerAnimator : MonoBehaviour
{
  private const string IS_WALKING = "IsWalking";
  private const string HORIZONTAL = "Horizontal";
  private const string VERTICAL = "Vertical";
  private const string IS_DEAD = "IsDead";

  private Animator animator;

  [SerializeField] private PlayerMovement player;
  [SerializeField] private PlayerStatus playerStatus;

  private void Awake()
  {
    animator = GetComponent<Animator>();
    playerStatus.OnDeadTrigger += OnDeadAnimation;
  }

  private void Update()
  {
    animator.SetBool(IS_WALKING, player.WalkVector() != Vector2.zero);
    animator.SetFloat(HORIZONTAL, player.WalkVector().x);
    animator.SetFloat(VERTICAL, player.WalkVector().y);
    player.transform.localScale = new Vector2(-Mathf.Sign(player.WalkVector().x), 1f);
  }

  private void OnDeadAnimation(object sender, EventArgs e)
  {
    animator.SetTrigger(IS_DEAD);
    player.enabled = false;
  }
}
