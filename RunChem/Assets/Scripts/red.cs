﻿
using UnityEngine;

public class red : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] const int extraHealth = 5;
    public itemsManager ItemDetails;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -ItemDetails.speed);
    }

    void FixedUpdate()
    {
        if(transform.position.y < (ItemDetails.scrBound.GetScrBound().y * -1) - ItemDetails.offset ) {
            ObjectPooler.Instance.ReturnToPool(this.gameObject, ItemDetails.tag);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            ObjectPooler.Instance.ReturnToPool(this.gameObject, ItemDetails.tag);
        }    
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            healthBar.Instance.addHealth(extraHealth);
        }
    }
}
