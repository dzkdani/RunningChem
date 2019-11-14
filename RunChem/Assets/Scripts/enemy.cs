using UnityEngine;

public class enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    string itemTag;
    [SerializeField] readonly static int enemyDmg = 1;
    public itemsManager ItemDetails;

    void OnEnable()
    {
        itemTag = ItemDetails.tag;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -ItemDetails.spd);

        anim = GetComponent<Animator>();
    }
    void Start() {
        anim.SetFloat("randAnim",Random.Range(0.0f, 2.0f));
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
