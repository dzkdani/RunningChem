
using UnityEngine;

public class red : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] readonly static string itemTag = "red";
    const int extraHealth = 5;
    public itemsManager ItemDetails;

    void OnEnable()
    {
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
            healthBar.Instance.addHealth(extraHealth);
        }
    }
}
