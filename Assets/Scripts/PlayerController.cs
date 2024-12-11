using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 5f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    float hAxis;
    Vector2 direction;

    [SerializeField]
    float speed = 3;
    [SerializeField]
    float jumpPower = 5;

    Rigidbody2D rb;

    [SerializeField]
    bool onGround = false;

    Animator animator;

    [SerializeField]
    AudioClip[] audioClips;
    AudioSource audioSource;

    [SerializeField]
    Transform BG;

    [SerializeField]
    Lives livesScript;
    [SerializeField] private TrailRenderer tr;



    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }




    
    void Update()
    {
        Movement();
        Jump();
        Facing();
        Animations();
    }

    void Movement()
    {
        //Monitor horizontal keypresses and apply movement to player object
        hAxis = Input.GetAxis("Horizontal");
        direction = new Vector2 (hAxis, 0);
       

        transform.Translate(direction * Time.deltaTime * speed);
    }

    void Jump()
    {
        if (isDashing)
        {
            return;
        }

        //If spacebar pressed then apply velocity to rb on yaxis
        if (Input.GetKeyDown(KeyCode.UpArrow) && onGround == true )
        {
            rb.velocity = new Vector2(0, 1) * jumpPower;

            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }
    void Facing()
    {
        //if player is moving left scale = -1
        if (hAxis < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            BG.localScale = new Vector3(1.5f, 1.5f, 1.5f);
           
           
        }

        //if player is moving right scale = 1
        if (hAxis > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            BG.localScale = new Vector3(-1.5f, 1.5f, 1.5f);

           
          
        }
    }

    void Animations()
    {
        //if player is moving then play running animation
        animator.SetFloat("Moving", Mathf.Abs(hAxis));
        animator.SetBool("OnGround", onGround);
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        //if trigger enter object with tag "ground" then onGround = true
        if (col.tag == "ground")
        {
            onGround = true;
        }

        if (col.tag == "enemy")
        {
           livesScript.ReduceLives();
        }

        if (col.tag == "collectible")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
         //if trigger enter object with tag "ground" then onGround = false
        if (col.tag == "ground")
        {
            onGround = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}