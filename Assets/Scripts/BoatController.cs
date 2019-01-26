using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    public float TargetHeight;

    [Space(5)]
    [Header("Rotation")]
    public float CurrentTorque = 0;
    public float MaxTorque = 10;

    [Space(5)]
    [Header("Speed")]
    public float CurrentSpeed;
    public float MaxForce = 10;

    [Space(10)]

    Transform ChildHeight;
    Transform ChildRotation;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        ChildHeight = transform.GetChild(0);
        ChildRotation = ChildHeight.GetChild(0);
    }

    void Start()
    {

    }

    void Update()
    {
        var position2D = new Vector2(transform.position.x, transform.position.z);
        TargetHeight = Water.GetHeightAt(position2D)+1;
        var normal = Water.GetNormal(position2D);
        var direction = new Vector2(transform.forward.x, transform.forward.z);
        var tangent = Water.GetTangent(position2D, direction);
        ChildRotation.transform.rotation = Quaternion.LookRotation(tangent, normal);
    }

    private void FixedUpdate()
    {
        ChildHeight.transform.position = Vector3.Lerp(ChildHeight.transform.position, new Vector3(0, TargetHeight, 0) + transform.position, Time.fixedDeltaTime);

        CurrentSpeed = Vector3.Dot(transform.forward, rb.velocity);
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.forward * MaxForce, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(-transform.up * MaxTorque, ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * MaxTorque, ForceMode.Force);
        }
    }


    private void OnDrawGizmos()
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
