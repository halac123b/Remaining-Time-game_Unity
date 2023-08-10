using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLazer : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lazer;
    private MonsterAnimator Monster;
    [SerializeField] GameObject power;
    public bool relive = false;
    public bool spawn = false;

    void Start()
    {
        lazer = GetComponent<LineRenderer>();
        lazer.SetPosition(0, transform.position);
        lazer.SetPosition(1, transform.position);   
        Monster = FindAnyObjectByType<MonsterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(relive && Monster){
            lazer.SetPosition(0, new Vector3 (Monster.transform.position.x,Monster.transform.position.y+2f));   
        }else lazer.SetPosition(0, transform.position);

        if(spawn && Monster){
             lazer.SetPosition(0, Monster.GetMousePos());   
        }else lazer.SetPosition(0, transform.position);
        
        if (spawn || relive) power.SetActive(true);
        else power.SetActive(false);

    }
}
