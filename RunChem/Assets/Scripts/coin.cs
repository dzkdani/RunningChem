using UnityEngine;

public class coin : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private int currentCoin = 0;
    private const int minCoin = 0;
    public itemsManager ItemDetails;

    void coinCollect(int extraCoin) { currentCoin += extraCoin; }
    
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            soalManager.Instance.addToCount(1);
            ObjectPooler.Instance.ReturnToPool(this.gameObject, ItemDetails.tag);
        }
    }
}

