using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float spd;
    private Rigidbody2D rb;
    private float move;
    private readonly string horizontal = "Horizontal";  

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        move = Input.GetAxis(horizontal) * Time.fixedDeltaTime * spd;
    }

    void FixedUpdate()
    {
        rb.MovePosition( (Vector2)rb.position + Vector2.right * move);
        transform.position = new Vector3 (Mathf.Clamp(transform.position.x, -2f, 2f), 
                                          transform.position.y, transform.position.z);
    }
}
