using UnityEngine;

public class blue : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] readonly static string itemTag = "blue";
    public itemsManager ItemDetails;

    void OnEnable()
    {
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
            ObjectPooler.Instance.ReturnToPool(this.gameObject, itemTag);
            Debug.Log("Dhuarr Soal");
            soalManager.Instance.popUpSoal();
            Time.timeScale = 0;
        }
    }
}
