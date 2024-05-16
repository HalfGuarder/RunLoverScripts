using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static Rigidbody2D playerRB;

    PlayerControls controls;
    float direction = 0;
 
    public float speed = 500f;

    public float rayPower;

    public float jumpForce = 60;
    bool isGrounded;
    int numberOfJumps = 0;
    public Transform groundCheck;
    public LayerMask groundLayer;

    Animator anim;

    RaycastHit2D rayHit;

    private void Awake()
    {
        if(playerRB == null)
        {
            new Rigidbody2D();
            playerRB = GetComponent<Rigidbody2D>();

            Debug.Log("GetPlayerRB");
        }

        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

        if(playerRB == null)
        {
            new Rigidbody2D();
            playerRB = GetComponent<Rigidbody2D>();

            Debug.Log("GetPlayerRB");
        }
        anim = GetComponent<Animator>();

        controls = new PlayerControls();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx => Jump();
        controls.Land.Move.performed += ctx => Run();
        controls.Land.Move.canceled += ctx => Walk();
    }

    void FixedUpdate()
    {
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
        }
        if(playerRB.velocity.y < 0f)
        {
            Debug.DrawRay(playerRB.position, Vector3.down * 4f, new Color(0, 1, 0));
            /*RaycastHit2D*/ rayHit = Physics2D.Raycast(playerRB.position, Vector3.down, rayPower, LayerMask.GetMask("Ground"));
            if(rayHit.collider != null)
            {
                //Debug.Log(rayHit.collider.name);
                anim.SetBool("isJumping", false);
                numberOfJumps = 0;           
            }
            else if(rayHit.collider == null)
            {
            anim.SetBool("isJumping", true);
            }
        }
    }

    void Jump()
    {
        if(rayHit.collider != null)
        {
            Debug.Log($"numberOfJumps={numberOfJumps}");
            Debug.Log("Jump:0");
            playerRB.velocity = new Vector2(playerRB.velocity.x, (jumpForce - (6 * numberOfJumps)));
            numberOfJumps++;
            Debug.Log($"numberOfJumps={numberOfJumps}");
            Debug.Log("Jump:0");

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Jump);
        }
        else if(rayHit.collider == null)
        {
            if(numberOfJumps == 1)
            {
                Debug.Log($"numberOfJumps={numberOfJumps}");
                Debug.Log("Jump:1");
                playerRB.velocity = new Vector2(playerRB.velocity.x, (jumpForce - (6 * numberOfJumps)));
                numberOfJumps++;
                Debug.Log($"numberOfJumps={numberOfJumps}");
                Debug.Log("Jump:1");

                AudioManager.instance.PlaySfx(AudioManager.Sfx.Jump);
            }
            else if(numberOfJumps > 1)
            {
                Debug.Log($"numberOfJumps={numberOfJumps}");
                Debug.Log("Jump:2");
                return;
            }
            else
            {
                return;
            }
        }
    }

    void Run()
    {
        speed = 500f;
    }

    void Walk()
    {
        speed = 0f;
    }
}
