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
    [SerializeField] GameObject ball;
    bool isTouchingBall;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        jump = InputSystem.actions.FindAction("Jump");
        rebound = false;
        isTouchingBall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jump.IsPressed())
        {
            rebound = false;
            //myBody.AddForceY(-force * Time.deltaTime);
            if (transform.position.y > -49)
            {
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

        if (rebound)
        {
            if (transform.position.y < -30) {
                transform.Translate(Vector2.up * force * reboundForce*Time.deltaTime);
                if(isTouchingBall)
                ball.GetComponent<Rigidbody2D>().AddForceY(100f);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == ball)
        {
            isTouchingBall = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == ball)
        {
            isTouchingBall = false;
        }
    }
}

