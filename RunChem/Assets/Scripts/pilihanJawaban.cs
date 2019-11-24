using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pilihanJawaban : MonoBehaviour
{
    [SerializeField] List<Sprite> pilSpriteList = new List<Sprite>();
    Rigidbody2D rb;
    SpriteRenderer sr;
    string itemTag;
    public itemsManager ItemDetails;
    int tempJwbn;

    void OnEnable()
    {
        itemTag = ItemDetails.tag;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -ItemDetails.spd);

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = pilSpriteList[ObjectSpawner.Instance.getIteration()];
    }

    void FixedUpdate()
    {
        if(transform.position.y < ItemDetails.minY) {
            ObjectPooler.Instance.ReturnToPool(this.gameObject, itemTag);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < pilSpriteList.Count; i++)
            {
                ObjectPooler.Instance.ReturnToPool(GameObject.FindGameObjectWithTag("opsi"), itemTag);        
            }
        }    
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < pilSpriteList.Count; i++)
            {
                if (sr.sprite == pilSpriteList[i])
                {
                    tempJwbn = i+1;
                }
            }
            soalManager.Instance.chekJawaban(tempJwbn);
        }
    }


}
