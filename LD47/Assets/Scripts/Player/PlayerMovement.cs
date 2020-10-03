using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerMovement : MonoBehaviour
{

    public class OnPlayerEnterCellEventArgs : EventArgs
    {
        public Vector3 position;
    }
    public float speed;
    public float rotSpeed;
    public Vector2 rotLimits;

    private Rigidbody rb;
    private Camera cam;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (cam == null)
        {
            cam = GetComponentInChildren<Camera>();
        }
    }


    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 fwd = transform.forward * vert;
        Vector3 side = transform.right * horiz;

        Vector3 movement = Vector3.Normalize(fwd + side) * speed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + movement);

        float lookHoriz = Input.GetAxis("Mouse X");
        float lookVert = Input.GetAxis("Mouse Y");

        Vector3 rotation = new Vector3(0f, lookHoriz, 0f) * rotSpeed;

        transform.localEulerAngles += rotation;
        
        cam.transform.localEulerAngles += new Vector3(lookVert, 0f, 0f) * rotSpeed;
        float eulerX = cam.transform.localEulerAngles.x;

        if (eulerX < 360 && eulerX > 180)
        {
            eulerX = Mathf.Clamp(eulerX, rotLimits.x + 360, 360);
        }
        else
        {
            eulerX = Mathf.Clamp(eulerX, 0, rotLimits.y);
        }

        cam.transform.localEulerAngles = new Vector3(eulerX, 0f, 0f);

    }

    public event EventHandler<OnPlayerEnterCellEventArgs> OnPlayerEnterCell;
    public event EventHandler<OnPlayerEnterCellEventArgs> OnPlayerEnterCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            OnPlayerEnterCell?.Invoke(this, new OnPlayerEnterCellEventArgs { position = other.transform.position });
        }
        else if (other.CompareTag("Checkpoint"))
        {
            OnPlayerEnterCheckpoint?.Invoke(this, new OnPlayerEnterCellEventArgs { position = other.transform.position });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Checkpoint Passed"))
        {
            other.GetComponentInParent<Cell>().AddWall(Cell.Wall.Front);
        }
    }
}
