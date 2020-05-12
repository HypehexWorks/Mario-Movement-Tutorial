using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Settings")]
    [Range(0, 25)]
    public float moveSpeed;
    [Range(0, 25)]
    public float jumpForce;
    [Header("Other")]
    public float startTimeBtwJumps = .2f;
    private float timeBtwJumps;
    public float startTimeBtwGrounds = .2f;
    private float timeBtwGrounds;
    public float cutJumpTime;
    [Space]
    public Transform groundPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float movement;


    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);

        if (isGrounded) 
        {
            timeBtwGrounds = startTimeBtwGrounds;
        }

        timeBtwJumps -= Time.deltaTime;
        timeBtwGrounds -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            timeBtwJumps = startTimeBtwJumps;
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutJumpTime);
        }

        if (timeBtwGrounds > 0 && timeBtwJumps > 0)
        {
            timeBtwGrounds = 0;
            timeBtwJumps = 0;

            rb.velocity = Vector2.up * jumpForce;
        }

        if (movement > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (movement < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

}
