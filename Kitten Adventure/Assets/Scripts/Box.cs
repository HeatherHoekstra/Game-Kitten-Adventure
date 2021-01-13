using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Sprite newSprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if(gameObject.tag == "Untagged")
        {
        spriteRenderer.sprite = newSprite;
        }
        
        
    }

}
