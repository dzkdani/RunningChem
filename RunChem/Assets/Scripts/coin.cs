using UnityEngine;

public class coin : MonoBehaviour
{
    Rigidbody2D rb;
    string itemTag;
    private const int minCoin = 0;
    int currentCoin = 0;
    public itemsManager ItemDetails;

    void coinCollect(int extraCoin) { currentCoin += extraCoin; }
    
    void OnEnable()
    {
        itemTag = ItemDetails.tag;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -ItemDetails.spd);
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
            //nambah coin
            soalManager.Instance.addToCount(1);
        }
    }
}

