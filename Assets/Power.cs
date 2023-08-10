using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LineRenderer lineRenderer;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = lineRenderer.GetPosition(0);
    }
}
