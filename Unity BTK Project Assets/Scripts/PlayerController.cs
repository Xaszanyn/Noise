using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D playerRB;
    Animator playerAnimator;
    public float moveSpeed = 1f;
    public float jumpSpeed = 1f;

    bool facingRight = true;

    public bool isGrounded = false;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;


    public float jumpFreq, nextJumpTime;



    void Awake()
    {
    }

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // print(Input.GetAxis("Horizontal"));

        HorizontalMove();
        OnGroundCheck();

        if(playerRB.velocity.x  < 0 && facingRight)
            flipFace();
        else if (playerRB.velocity.x > 0 && !facingRight) 
            flipFace();


        if(Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad)) {

            nextJumpTime = Time.timeSinceLevelLoad + jumpFreq;
            Jump();
        }
    }

    void FixedUpdate()
    {

    }


    void HorizontalMove() {
        playerRB.velocity = new Vector2( Input.GetAxis("Horizontal") * moveSpeed , playerRB.velocity.y );
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
    }

    void flipFace() {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;

        transform.localScale = tempLocalScale;
    }

    void Jump() {
        playerRB.AddForce(new Vector2(0f, jumpSpeed));
    }

    void OnGroundCheck() {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("iga", isGrounded);

    }
}
