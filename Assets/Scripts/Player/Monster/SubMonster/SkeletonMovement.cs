using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMovement : NetworkBehaviour
{
    private NavMeshAgent agent;
    public bool canmove = true;
    public NetworkVariable<float> HP =  new NetworkVariable<float>(100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        // Tìm tất cả các vật thể trong bán kính 10 đơn vị xung quanh vật thể của bạn
        agent.enabled = true;
        Transform nearest = NearestPlayer();
         if(canmove && HP.Value > 0 && nearest) {
            // Debug.LogError(agent.isOnNavMesh);
            if (agent.isOnNavMesh){
                // Debug.LogWarning(nearest.position);
                agent.SetDestination(new Vector3(nearest.position.x,nearest.position.y,0));
            }
         } 
        
    }
    /////////////////////Support//////////////////////////

    public Vector3 Getdirection(){

        // Lọc ra vật thể gần nhất có layer là "Player"
        Transform nearestPlayer = NearestPlayer();

        // Nếu có vật thể "Player" gần nhất, di chuyển vật thể của bạn đến gần vật thể đó
        if (nearestPlayer != null)
        {
            Vector3 direction = (nearestPlayer.position - transform.position + new Vector3(0,0.5f,0)).normalized;
            return direction;
        }
        return new Vector3();
    }
    public Transform NearestPlayer(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);

        // Lọc ra vật thể gần nhất có layer là "Player"
        Transform nearestPlayer = null;
        float nearestDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestPlayer = collider.transform;
                    nearestDistance = distance;
                }
            }
        }
        return nearestPlayer;
    }
}
