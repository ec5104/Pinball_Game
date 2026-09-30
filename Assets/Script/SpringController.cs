using System;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpringController : MonoBehaviour
{
    Rigidbody2D myBody;
    InputAction jump;
    bool rebound;
    [SerializeField] float force;
    [SerializeField] float reboundForce;
    //[SerializeField] GameObject ball;
    GameObject ball;
    bool isTouchingBall;
    float pressTime;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        jump = InputSystem.actions.FindAction("Jump");
        rebound = false;
        isTouchingBall = false;
        FindFirstObjectByType<GameRestart>().ChangeScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (jump.IsPressed())
        {
            
            //myBody.AddForceY(-force * Time.deltaTime);
            if (transform.position.y > -49)
            {
                pressTime += Time.deltaTime;
                //Debug.Log("launching" + pressTime);
                transform.Translate(Vector2.up * -force * Time.deltaTime);
            }

            // transform go down.
            // if this y position is beyond what i want
            // stop moving the thing.
            
            // if im not pressing space, automatically go up.
            // stop moving if beynod. 
        }

        if (jump.WasReleasedThisFrame())
        {
            rebound = true;
        }

        
    }

    void FixedUpdate()
    {
        if (rebound)
        {
            if (transform.position.y < -30) {
                transform.Translate(Vector2.up * force * reboundForce*Time.deltaTime);
                if (isTouchingBall)
                {
                    //Debug.Log(pressTime);
                    ball.GetComponent<Rigidbody2D>().AddForceY(500f * pressTime);
                    
                }
            }
            else
            {
                rebound = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isTouchingBall = true;
            ball = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isTouchingBall = false;
        }
    }
}

