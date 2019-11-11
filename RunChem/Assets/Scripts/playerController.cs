using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] bool isGUI = false;
    const float offset = 2.2f;
    const float translateValueX = 1.1f;
    public enum moveDirection
    {
        Left,
        Right
    }
    moveDirection dir;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { moveCharacter(moveDirection.Left); }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { moveCharacter(moveDirection.Right); }
        clampCharacter();
    }

    void clampCharacter()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -offset, offset),
                                          transform.position.y, transform.position.z);
    }

    void moveCharacter(moveDirection dir)
    {
       switch (dir)
       {
           case moveDirection.Left : transform.position = new Vector3(transform.position.x - translateValueX, 
                    transform.position.y, transform.position.z);
            break;
           case moveDirection.Right : transform.position = new Vector3(transform.position.x + translateValueX, 
                    transform.position.y, transform.position.z);         
            break; 
       }
    }
}
