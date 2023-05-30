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

    [SerializeField] private AIPath aiPath; // Компонент A* Pathfinding
    [SerializeField] private Transform groundCheck; // Точка проверки земли
    [SerializeField] private LayerMask groundLayer; // Слой земли

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
        // Проверка находится ли бот на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Отразить спрайт в зависимости от направления движения
        enemySprite.flipX = aiPath.desiredVelocity.x < 0;

        // Обновление параметров анимации
        animator.SetFloat("Speed", Mathf.Abs(aiPath.desiredVelocity.x));
        animator.SetBool("Jumping", !isGrounded);
    }

    private void FixedUpdate()
    {
        // Получение горизонтальной скорости от A* Pathfinding
        float HorizontalMove = aiPath.desiredVelocity.x * Speed;

        // Установка целевой скорости
        Vector2 targetVelocity = new Vector2(HorizontalMove, rb.velocity.y);
        rb.velocity = targetVelocity;
    }

    private void OnDrawGizmosSelected()
    {
        // Отображение точки проверки земли в редакторе
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
