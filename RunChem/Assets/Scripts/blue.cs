using UnityEngine;

public class blue : MonoBehaviour
{
    Rigidbody2D rb;
    string itemTag;
    public itemsManager ItemDetails;

    void OnEnable()
    {
        itemTag = ItemDetails.tag;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -ItemDetails.spd);
    }

    void FixedUpdate()
    {
        if(transform.position.y < ItemDetails.minY) {
            ObjectPooler.Instance.ReturnToPool(this.gameObject, itemTag);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            audioManager.Instance.PlayAudio("elenmeyer");
            ObjectPooler.Instance.ReturnToPool(this.gameObject, itemTag);
            soalManager.Instance.popUpSoal();
        }
    }
}
