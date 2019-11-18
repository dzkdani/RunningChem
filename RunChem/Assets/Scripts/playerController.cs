using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    const float offset = 2.2f;
    const float translateValueX = 1.1f;
    public enum moveDirection
    {
        Left,
        Right
    }
    moveDirection dir;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        mobileController();

        if (Input.GetKeyDown(KeyCode.LeftArrow)) { moveCharacter(moveDirection.Left); }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { moveCharacter(moveDirection.Right); }
    }

    void LateUpdate() {
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
           case moveDirection.Left : rb.transform.position = new Vector3(transform.position.x - translateValueX, 
                    transform.position.y, transform.position.z);
            break;
           case moveDirection.Right : rb.transform.position = new Vector3(transform.position.x + translateValueX, 
                    transform.position.y, transform.position.z);         
            break; 
       }
    }

    void mobileController()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0f;

            if (touchPos.x < transform.position.x) { moveCharacter(moveDirection.Left); }
            if (touchPos.x > transform.position.x) { moveCharacter(moveDirection.Right); }

        }
    }
}
