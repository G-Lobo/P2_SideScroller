using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Collider2D playerCollider;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private Transform groundCheck;  
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Gizmo")]
    [SerializeField] private bool showGizmo = true;

    private bool isGrounded;
    private ScoreManager scoreManager;
    private GameObject currentPlatform;



    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    
    void Update()
    {
        CheckGround();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);  
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        
        if (playerRb.position.y < -6)
        {
            Destroy(gameObject);

            // SceneManager.LoadScene(0);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            Collider2D platformCollider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (platformCollider != null)
            {
                GameObject platformObject = platformCollider.gameObject;

                if (platformObject != currentPlatform)
                {
                    currentPlatform = platformObject;

                    PlatformMover platformScore = platformObject.GetComponent<PlatformMover>();
                    if (platformScore != null && !platformScore.hasScored)
                    {
                        scoreManager.AddPoint();
                        platformScore.hasScored = true;
                    }
                }
            }
        }
        else
        {
            currentPlatform = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null && showGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 🔥 Quebra plataforma se ela for quebrável
        BreakablePlatform platform = collision.gameObject.GetComponent<BreakablePlatform>();
        if (platform != null && platform.canBreak)
        {
            Destroy(collision.gameObject, 0.5f);
        }

        // // 🔥 Sistema de Score
        // PlatformMover platformScore = collision.gameObject.GetComponent<PlatformMover>();
        // if (platformScore != null && !platformScore.hasScored)
        // {
        //     scoreManager.AddPoint();
        //     platformScore.hasScored = true;
        // }
    }
    
}