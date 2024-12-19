using UnityEngine;

public class Enemy_Skeleton : Entity
{
    private bool isAttacking = false;

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;

    [Header("Move Info")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.linearVelocity = new Vector2(moveSpeed * 2.0f * facingDirection, rb.linearVelocity.y);
                isAttacking = false;
                Debug.Log("You cannot hide!");
            }
            else
            {
                isAttacking = true;
                Debug.Log("ATTACK!" + isPlayerDetected.collider.gameObject.name);
            }
        }
        else
        {
            Movement();
        }
        if (!isGrounded || isWallDetected)
            Flip();
    }

    private void Movement()
    {
        if (!isAttacking)
            rb.linearVelocity = new Vector2(moveSpeed * facingDirection, rb.linearVelocity.y);
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDirection, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDirection, transform.position.y));
    }
}
