using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangentFollower : MonoBehaviour
{

    public Transform target;
    public float hoverDistance = 0.3f;
    Rigidbody rb;
    Vector3 prevBestPos;
    float rotateSpeed = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    

    void FixedUpdate()
    {
        
        RaycastHit hitInfo;
        var down = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, down, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, down * hitInfo.distance, Color.yellow);

            float correction = hoverDistance - hitInfo.distance;
            //transform.position = new Vector3(transform.position.x, transform.position.y + correction, transform.position.z);
            prevBestPos = transform.position;
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            

            rb.MovePosition(transform.position + transform.forward * Time.deltaTime);
            //transform.position = new Vector3(transform.position.x, transform.position.y + correction, transform.position.z);

        }
        else {
            transform.position = prevBestPos;
            transform.Rotate(Vector3.up, Time.deltaTime*rotateSpeed);
        }
        rb.AddTorque(Vector3.Cross(transform.forward, hitInfo.normal) * 1f, ForceMode.Force);


    }
}