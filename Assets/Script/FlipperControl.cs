using UnityEngine;
using UnityEngine.InputSystem; // 1. Add this namespace

public class FlipperController : MonoBehaviour
{
    private HingeJoint2D hinge;
    private JointMotor2D motor;

    // 2. Expose an InputAction so you can bind any key directly in the Inspector
    [SerializeField] private InputAction flipAction;
    [SerializeField] private float flipSpeed = -1500f;

    void OnEnable()
    {
        flipAction.Enable(); // 3. Enable the action when the script starts
    }

    void OnDisable()
    {
        flipAction.Disable(); // 4. Clean up when disabled
    }

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        motor = hinge.motor;
    }

    void Update()
    {
        // 5. Check if the bound key is currently being pressed down
        if (flipAction.IsPressed())
        {
            motor.motorSpeed = flipSpeed;
        }
        else
        {
            motor.motorSpeed = -flipSpeed;
        }

        hinge.motor = motor;
    }
}