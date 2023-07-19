using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterAnimator : AnimatorController
{
    // Start is called before the first frame update
    
    protected override void Awake(){
        base.Awake();
        playerInput.playerInputActions.Player.Attack.started += TriggerAttack01Started;
        playerInput.playerInputActions.Player.Attack02.started += TriggerAttack02Started;
        playerInput.playerInputActions.Player.Attack03.started += TriggerAttack03Started;


    }

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
    protected override void Update()
    {
        base.Update();
        if (!IsOwner) return;
    }
}
