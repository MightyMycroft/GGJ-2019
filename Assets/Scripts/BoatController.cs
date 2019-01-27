using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    public bool DrawGizmos;

    [Space(10)]
    public float OffsetHeight;
    public float TargetHeight;

    [Tooltip("How fast the object will float to the Target Height")]
    public float FloatingMultiplier;

    public float Mass = 100;
    public float MaxAngularVelocity = 2;

    public float FinalHeight
    {
        get
        {
            return OffsetHeight + TargetHeight;
        }
    }

    public float NormalizedSteering
    {
        get
        {
            return CurrentTorque / MaxTorque;
        }
    }

    public float NormalizedSpeed
    {
        get
        {
            return CurrentSpeed / MaxForce;
        }
    }

    [Space(5)]
    [Header("Rotation")]
    public float MaxTorque = 10;

    [Space(5)]
    [Header("Speed")]
    public float MaxForce = 10;
    
    [Space(5)]
    [Header("Monitors")]
    public float CurrentSpeed;
    public float CurrentTorque;

    Transform ChildHeight;
    Transform ChildRotation;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = MaxAngularVelocity;
        rb.mass = Mass;
        ChildHeight = transform.GetChild(0);
        ChildRotation = ChildHeight.GetChild(0);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * MaxForce, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(-transform.up * MaxTorque, ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * MaxTorque, ForceMode.Force);
        }

        var position2D = new Vector2(transform.position.x, transform.position.z);
        var normal = Water.GetNormal(position2D);
        var direction = new Vector2(transform.forward.x, transform.forward.z);
        var tangent = Water.GetTangent(position2D, direction);
        TargetHeight = Water.GetHeightAt(position2D) + OffsetHeight;
        ChildRotation.transform.rotation = Quaternion.LookRotation(tangent, normal);

        ChildHeight.transform.position = Vector3.Lerp(ChildHeight.transform.position, new Vector3(0, TargetHeight, 0) + transform.position, Time.fixedDeltaTime * FloatingMultiplier);

        CurrentSpeed = Vector3.Dot(transform.forward, rb.velocity);
        CurrentTorque = rb.angularVelocity.y;
    }


    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.color = Color.blue;

            // Height gizmos
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, TargetHeight, 0));
            Gizmos.DrawSphere(transform.position + new Vector3(0, TargetHeight, 0), 0.25f);

            // Direction gizmos
            Gizmos.color = Color.yellow * 0.66f;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);

            // Speed gizmos
            Gizmos.color = Color.white;
            if (rb)
            {
                Gizmos.DrawLine(transform.position, transform.position + rb.velocity);
            }
        }
    }
}
