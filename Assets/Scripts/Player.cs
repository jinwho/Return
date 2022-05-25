using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    BoxCollider2D box;
    Animator anim;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    public AudioClip dieSound;
    public AudioClip jumpSound;


    public float moveSpeed = 5f;
    public float jumpSpeed = 15f;

    bool isGrounded = true;
    bool isJumping = false;
    public LayerMask groundLayer;
    bool lastFrameGrounded;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandling();

        GroundCheck();

        AnimationUpdate();
    }

    void InputHandling()
    {
        float x_input = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(x_input * moveSpeed, rb.velocity.y);

        switch (x_input)
        {
            case -1:
                spriteRenderer.flipX = true;
                break;
            case 1:
                spriteRenderer.flipX = false;
                break;
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            audioSource.PlayOneShot(jumpSound, 0.3f);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isJumping = true;
        }

        // restart level
        if (Input.GetKeyDown(KeyCode.R))
        {
            gm.RestartGame();
        }
    }

    void GroundCheck()
    {
        
        // check

        Vector2 boxPos = new Vector2(box.offset.x + transform.position.x, box.offset.y + transform.position.y - box.size.y / 2);
        Vector2 boxSize = new Vector2(box.size.x - (0.03f * 2), 0.1f);

        Collider2D collider = Physics2D.OverlapBox(boxPos, boxSize, 0f, groundLayer);

        isGrounded = (collider != null);
    }

    void AnimationUpdate()
    {
        anim.SetFloat("X_SPEED", Mathf.Abs(rb.velocity.x));

        // Fall Animation Handling
        if (isGrounded && !lastFrameGrounded && rb.velocity.y < 0.1f)
        {
            anim.SetTrigger("FALL");
            isJumping=false;
        }

        lastFrameGrounded = isGrounded;
    }

    public void PlayerDead()
    {
        audioSource.PlayOneShot(dieSound);
        gm.SwitchTilemap();
    }

    public void PlayerWin()
    {
        if (gm.HaveNextStage())
        {
            Invoke("NextStage", 3f);
        }
        else
        {
            Invoke("QuitGame", 3f);
        }
    }

    void NextStage()
    {
        gm.NextStage();
    }

    void QuitGame()
    {
        gm.QuitGame();
    }
}
