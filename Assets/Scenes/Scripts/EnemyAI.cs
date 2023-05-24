using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target; // ���� ��� �������������
    public float moveSpeed = 5f; // �������� ����������� �����
    public float jumpForce = 5f; // ���� ������ �����
    public float maxJumpDistance = 2f; // ������������ �������������� ��������� ��� ������

    private Rigidbody2D rb;
    private bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target != null)
        {
            // ������������� �� �����������
            float direction = target.position.x - transform.position.x;

            if (direction < 0)
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            else if (direction > 0)
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            // ������
            if (isGrounded && Mathf.Abs(direction) <= maxJumpDistance)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        // ����������� �����
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
