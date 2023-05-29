using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer Enemy;
    private float HorizontalMove = 0f;
    [SerializeField] private AIPath aIPath;

    [Header("Player Movement Settings")]
    [Range(0, 10f)] public float speed = 0.2f;

    [Header("Player Animation Settings")]
    public Animator animator;

    private void Awake()
    {
        Enemy = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Enemy.flipX = aIPath.desiredVelocity.x <= 0.01f;

        animator.SetFloat("HorizontalMove", Mathf.Abs(HorizontalMove));
    }

    private void FixedUpdate()
    {
        HorizontalMove = Input.GetAxis("Horizontal") * speed;

        Vector2 targetVelocity = new Vector2(HorizontalMove * 10f, rb.velocity.y);

        rb.velocity = targetVelocity;
    }
}
