using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScollBackGround : MonoBehaviour
{
    
    private SpriteRenderer quadRenderer;

    float scrollSpeed = 0.5f;

    void Start()
    {
        quadRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 textureOffset = new Vector2(Time.time*scrollSpeed,0);
        quadRenderer.material.mainTextureOffset = textureOffset;
    }
}
