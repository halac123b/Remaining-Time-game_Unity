using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class MonsterAnimator : AnimatorController
{
    // Start is called before the first frame update
    [SerializeField] public Transform AimBar;
    private NetworkVariable<Vector2> mouse = new NetworkVariable<Vector2>(new Vector2(0, 0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    protected override void Awake(){
        base.Awake();
        playerInput.playerInputActions.Player.Attack.started += TriggerAttack01Started;
        playerInput.playerInputActions.Player.Attack02.started += TriggerAttack02Started;
        playerInput.playerInputActions.Player.Attack03.started += TriggerAttack03Started;
        playerInput.playerInputActions.Player.E_Btn.started += TriggerSummonGrunt;
        playerInput.playerInputActions.Player.Q_Btn.started += TriggerSummonHunter;


    }

    private void TriggerSummonHunter(InputAction.CallbackContext context)
    {
        if(IsOwner)
        animator.SetTrigger("summonHunter");
    }

    private void TriggerSummonGrunt(InputAction.CallbackContext context)
    {
        if(IsOwner)
        animator.SetTrigger("summonGrunt");

    }

    protected override void Update()
    {
        base.Update();
        if (!IsOwner) return;
    }

/////////////////////Support////////////////////////////////
     public Vector2 GetMousePos()
    {
        return mouse.Value;
    }
    public void UpdataMousePos()
    {
        if (!IsOwner) return;
        // Update mouse position
        Vector2 mouse_pos = Input.mousePosition;
        mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
        mouse.Value = mouse_pos;
    }

/////////////////////////////Handle Event////////////////////////////// 

    private void TriggerAttack01Started(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        
        animator.SetInteger(TYPE_ATTACK,1);
        animator.SetTrigger(ATTACK);
    }

    private void TriggerAttack02Started(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;

        animator.SetInteger(TYPE_ATTACK,2);
        animator.SetTrigger(ATTACK);
    }
    private void TriggerAttack03Started(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;

        animator.SetInteger(TYPE_ATTACK,3);
        animator.SetTrigger(ATTACK);
    }

    // Update is called once per frame
}
