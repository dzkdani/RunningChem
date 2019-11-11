using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    string itemTag;
    [SerializeField] readonly static int enemyDmg = 2;
    public itemsManager ItemDetails;
    public List<Sprite> enemyImageList = new List<Sprite>(); 

    void OnEnable()
    {
        itemTag = ItemDetails.tag;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -ItemDetails.spd);
        
        sr = GetComponent<SpriteRenderer>();     
        sr.sprite = enemyImageList[Random.Range(0, enemyImageList.Count)];
    }

    void FixedUpdate()
    {
        if(transform.position.y < ItemDetails.minY ) {
            ObjectPooler.Instance.ReturnToPool(this.gameObject, itemTag);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            ObjectPooler.Instance.ReturnToPool(this.gameObject, itemTag);
        }    
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            healthBar.Instance.takeDamage(enemyDmg);
        }
    }
}
