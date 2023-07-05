using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
  private const string IS_WALKING = "IsWalking";
  private const string HORIZONTAL = "Horizontal";
  private const string VERTICAL = "Vertical";

  private Animator animator;

  [SerializeField] private PlayerMovement player;

  private void Awake()
  {
    animator = GetComponent<Animator>();
  }

  private void Update()
  {
    animator.SetBool(IS_WALKING, player.WalkVector() != Vector2.zero);
    animator.SetFloat(HORIZONTAL, player.WalkVector().x);
    animator.SetFloat(VERTICAL, player.WalkVector().y);
    player.transform.localScale = new Vector2(-Mathf.Sign(player.WalkVector().x), 1f);
  }
}
