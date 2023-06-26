using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float HorizontalMove = 0f;
    private float speed = 0.1f;
    private bool FacingRight = true;

    [Header("Player Movement Settings")]
    [Range(0, 10f)] public float walkSpeed = 0.1f;
    [Range(0, 10f)] public float runSpeed = 0.2f;
    [Range(0, 15f)] public float jumpForce = 5f;

    [Header("Player Animation Settings")]
    public Animator animator;

    public bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        animator.SetFloat("HorizontalMove", Mathf.Abs(HorizontalMove));

        if (isGrounded == false)
        {
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Нажата клавиша Shift, переключаемся на состояние "Running"
            animator.SetBool("Running", true);
            speed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // Отпущена клавиша Shift, переключаемся на состояние "Walking"
            animator.SetBool("Running", false);
            speed = walkSpeed;
        }
    }

    private void FixedUpdate()
    {
        HorizontalMove = Input.GetAxis("Horizontal") * speed;

        if (HorizontalMove < 0 && FacingRight)
        {
            Flip();
        }
        else if (HorizontalMove > 0 && !FacingRight)
        {
            Flip();
        }

        Vector2 targetVelocity = new Vector2(HorizontalMove * 10f, rb.velocity.y);

        rb.velocity = targetVelocity;
    }

    private void Flip()
    {
        FacingRight = !FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;

        transform.localScale = theScale;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
