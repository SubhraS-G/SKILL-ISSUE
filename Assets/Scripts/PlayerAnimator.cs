using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Speed parameter
        animator.SetFloat("Speed",
            Mathf.Abs(rb.linearVelocity.x));

        // Grounded parameter
        animator.SetBool("isGrounded",
            playerController.isGrounded);

        // Falling parameter
        animator.SetBool("isFalling",
            rb.linearVelocity.y < -0.1f &&
            !playerController.isGrounded);
    }
}