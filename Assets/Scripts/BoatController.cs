using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    public float TargetHeight;
    public float TargetSpeed;

    public float Speed;

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
        if (Input.GetKey(KeyCode.W))
        {
            Speed += Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Speed -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up);
        }

        transform.position += transform.forward * Speed * Time.deltaTime;

        TargetHeight = Mathf.Sin(Time.time);
    }

    private void FixedUpdate()
    {
        ChildHeight.transform.position = Vector3.Lerp(ChildHeight.transform.position, new Vector3(0, TargetHeight, 0) + transform.position, Time.fixedDeltaTime);

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

        Gizmos.color = Color.white;
        if (rb)
        {
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity);
        }
    }
}
