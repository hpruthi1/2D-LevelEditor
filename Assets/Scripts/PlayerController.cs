using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_animator;
    public float Speed;
    public bool CanJump = false;
    public float WalkSpeed, JumpForce;
    public bool IsFacingRight;
    public bool IsGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        IsFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector2(horizontal * Speed, rb.velocity.y) * Time.deltaTime);
        m_animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Speed = WalkSpeed;
        Flip(horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            CanJump = true;
            rb.AddRelativeForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            m_animator.SetTrigger("Jump");
        }
        else
        {
            CanJump = false;
        }
    }

    private void Flip(float horizontal)
    {
        if(horizontal>0 && !IsFacingRight || horizontal < 0 && IsFacingRight)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }
}
