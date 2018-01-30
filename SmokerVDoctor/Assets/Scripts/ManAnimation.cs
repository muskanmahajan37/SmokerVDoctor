using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAnimation : MonoBehaviour {

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        var directionState = 0;
        if (Input.GetKey("w"))
        {
            directionState = 3;
        }
        else if (Input.GetKey("s"))
        {
            directionState = 1;
        }
        else if (Input.GetKey("d"))
        {
            directionState = 4;
            spriteRenderer.flipX = true;
            
        }
        else if (Input.GetKey("a"))
        {
            directionState = 2;
            spriteRenderer.flipX = false;
        }
        animator.SetInteger("directionState", directionState);
    }
}
