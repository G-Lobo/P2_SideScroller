using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Collider2D playerCollider;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private Transform groundCheck;   // O Empty que criamos
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Gizmo")]
    [SerializeField] private bool showGizmo = true;

    private bool isGrounded;

    void Update()
    {
        CheckGround();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);  // Zera o Y antes de pular (opcional mas recomendado)
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Kill player se cair muito
        if (playerRb.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null && showGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}