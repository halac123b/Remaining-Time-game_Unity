using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class SummonSubMonster : StateMachineBehaviour
{
    [SerializeField] GameObject submonter;
    HealthLazer healthLazer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        healthLazer = FindAnyObjectByType<HealthLazer>();
        if(healthLazer){
            healthLazer.spawn = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MonsterAnimator monsterAnimator = animator.GetComponent<MonsterAnimator>();

        monsterAnimator.UpdataMousePos();
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MonsterAnimator monsterAnimator = animator.GetComponent<MonsterAnimator>();
        NavMeshHit hit;
        if (stateInfo.normalizedTime >= 1f && monsterAnimator.Is_Server() && NavMesh.SamplePosition(monsterAnimator.GetMousePos(), out hit, 1.0f, NavMesh.AllAreas))
        {
            
            // monsterAnimator.UpdataMousePos();
            // Instantiate(submonter, new Vector3(animator.transform.position.x + 1, animator.transform.position.y + 2f, 0), Quaternion.identity);
            NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(submonter,hit.position, 0);
            // NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(submonter, new Vector3(animator.transform.position.x - 1, animator.transform.position.y + 2f, 0), animator.GetComponent<MonsterAnimator>().GetPlayerData().Id);
            // NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(submonter, new Vector3(animator.transform.position.x, animator.transform.position.y + 1 + 2f, 0), animator.GetComponent<MonsterAnimator>().GetPlayerData().Id);
            // NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(submonter, new Vector3(animator.transform.position.x, animator.transform.position.y - 1 + 2f, 0), animator.GetComponent<MonsterAnimator>().GetPlayerData().Id);
        }
        if(healthLazer){
            healthLazer.spawn = false;
        }

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
