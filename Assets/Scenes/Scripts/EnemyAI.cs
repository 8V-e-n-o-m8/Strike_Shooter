using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer enemySprite;
    private float Speed = 0.2f;
    public bool isGrounded = false;
    private const float groundCheckRadius = 0.1f;

    [SerializeField] private AIPath aiPath; // ��������� A* Pathfinding
    [SerializeField] private Transform groundCheck; // ����� �������� �����
    [SerializeField] private LayerMask groundLayer; // ���� �����

    [Header("Player Movement Settings")]
    [Range(0, 15f)] public float jumpForce = 5f;

    [Header("Player Animation Settings")]
    public Animator animator;

    private void Awake()
    {
        enemySprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // �������� ��������� �� ��� �� �����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // �������� ������ � ����������� �� ����������� ��������
        enemySprite.flipX = aiPath.desiredVelocity.x < 0;

        // ���������� ���������� ��������
        animator.SetFloat("Speed", Mathf.Abs(aiPath.desiredVelocity.x));
        animator.SetBool("Jumping", !isGrounded);
    }

    private void FixedUpdate()
    {
        // ��������� �������������� �������� �� A* Pathfinding
        float HorizontalMove = aiPath.desiredVelocity.x * Speed;

        // ��������� ������� ��������
        Vector2 targetVelocity = new Vector2(HorizontalMove, rb.velocity.y);
        rb.velocity = targetVelocity;
    }

    private void OnDrawGizmosSelected()
    {
        // ����������� ����� �������� ����� � ���������
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
