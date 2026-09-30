using UnityEngine;

public class BumperControl : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this if your Collider 2D "Is Trigger" is UNCHECKED (Physical Bounce)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            TriggerBumper();
        }
    }
    
    private void TriggerBumper()
    {
        // Fire the Animator trigger
        animator.SetTrigger("IsHit");
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        // Optional: Add sound effects or score updates here
    }
}