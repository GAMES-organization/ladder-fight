using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] Animator playerAnimator;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private bool isJumping = false;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        SetPlayerRotation();
    }

    void MovePlayer()
    {
        float horizontalMovement = joystick.Horizontal;
        print(horizontalMovement);
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        if (horizontalMovement!=0)
        {
            playerAnimator.SetBool("Run", true);
            playerAnimator.SetBool("Idle", false);
        }
        else
        {
            playerAnimator.SetBool("Run", false);
            playerAnimator.SetBool("Idle", true);
        }
        /*if (joystick.Vertical > 0.5f && !isJumping)
        {
            
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
            isJumping = true;
        }
        else
        {
            
        }*/
    }

    void SetPlayerRotation()
    {
        float horizontalMovement = joystick.Horizontal;
        float verticalMovement = joystick.Vertical;

        // Rotate the player towards the movement direction
        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(horizontalMovement, 0f, verticalMovement));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }*/
}
