using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float spd;
    [SerializeField] boundaries scrBound;    
    private Rigidbody2D rb;
    private float objWidth, objHeight;
    private float move;
    private readonly string horizontal = "Horizontal";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update() {
        move = Input.GetAxis(horizontal) * Time.fixedDeltaTime * spd;
    }

    void FixedUpdate()
    {
        rb.MovePosition( (Vector2)rb.position + Vector2.right * move);
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -scrBound.GetScrBound().x + objWidth, scrBound.GetScrBound().x - objWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -scrBound.GetScrBound().y + objHeight, scrBound.GetScrBound().y - objHeight);
        transform.position = viewPos;
    }
}
