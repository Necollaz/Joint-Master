using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Swing : MonoBehaviour
{
    [SerializeField] private float _swingForce = 1000f;
    [SerializeField] private float _velocity = 50f;

    private HingeJoint _hingeJoint;
    private KeyCode _actionKey = KeyCode.Space;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        StartMoving();
    }

    private void StartMoving()
    {
        if (Input.GetKey(_actionKey))
        {
            JointMotor motor = _hingeJoint.motor;
            motor.force = _swingForce;
            motor.targetVelocity = _velocity;
            _hingeJoint.motor = motor;
            _hingeJoint.useMotor = true;
        }
        else
        {
            _hingeJoint.useMotor = false;
        }
    }
}
