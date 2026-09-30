using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class BallMove : MonoBehaviour
{
    Rigidbody2D myBody;
    InputAction jump;
    public float launchForce, wallForce, bumperForce, pinForce;
    public GameObject ball;
    public Vector2 spawnPos;
    public static int score = 0;
    public static int life = 3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Life " + life + "," + "Score " + score);
        myBody = GetComponent<Rigidbody2D>();
        //jump = InputSystem.actions.FindAction("Jump");
        //myBody.AddForceY(1000f);
        //myBody.AddForce(new Vector2(3000, 5000));

    }

    // Update is called once per frame  
    void Update()
    {
        /*if (jump.IsPressed())
        {
            myBody.AddForceY(500f);
        }*/
        
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //do array stuff matching so no need copy pasta    
        if (other.gameObject.CompareTag("Bumper"))
        {
            ///Debug.Log(-other.GetContact(0).normal * 1000f);
            Vector2 contactPosition = other.GetContact(0).normal;
            //print(contactPosition);
            
            myBody.linearVelocity = contactPosition * bumperForce;
            //myBody.AddForce(-other.GetContact(0).normal * transform.up * 10000f, ForceMode2D.Force);
            //what is get contact
        }
        if (other.gameObject.CompareTag("Pins"))
        {
            ///Debug.Log(-other.GetContact(0).normal * 1000f);
            Vector2 contactPosition = other.GetContact(0).normal;
            //print(contactPosition);
            
            myBody.linearVelocity = contactPosition * pinForce;
            //myBody.AddForce(-other.GetContact(0).normal * transform.up * 10000f, ForceMode2D.Force);
            //what is get contact
        }
        
        /*else if (other.gameObject.CompareTag("Reset"))
        {
            myBody.linearVelocity = Vector2.zero;
            transform.position = resetPosition;
            hasLaunched = false;
        } */else if (other.gameObject.CompareTag("Walls"))
        {
            Vector2 contactPosition = other.GetContact(0).normal;
            myBody.linearVelocity = contactPosition * wallForce;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            //Debug.Log("Landed");
            Instantiate(ball, spawnPos, Quaternion.identity);
            GoalScore goalScript = other.gameObject.GetComponent<GoalScore>();
            if (goalScript != null)
            {
                score += goalScript.getScore();
                Debug.Log(score);
                FindFirstObjectByType<GameRestart>().ChangeScore(score);
                life--;
                if(life <= 0){
                    FindFirstObjectByType<GameRestart>().TriggerGameOver();
                }
            }
        }
    }
}
