using UnityEngine;

public class coin : MonoBehaviour
{
    Rigidbody2D rb;
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            ObjectPooler.Instance.ReturnToPool(this.gameObject, ItemDetails.tag);
        }
    }
}

