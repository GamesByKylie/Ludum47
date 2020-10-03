using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenThread : MonoBehaviour
{
    public PlayerMovement pm;
    public float height;

    private LineRenderer rend;
    private bool locked = false;

    private void OnValidate()
    {
        rend = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        if (rend == null)
        {
            rend = GetComponent<LineRenderer>();
        }

        pm.OnPlayerEnterCell += Thread_OnPlayerEnterCell;
        pm.OnPlayerEnterCheckpoint += Thread_OnPlayerEnterCheckpoint;
    }

    private void Thread_OnPlayerEnterCell(object sender, PlayerMovement.OnPlayerEnterCellEventArgs pos)
    {
        //Check if already visited (it'll be the second to last on the list)

        if (rend.positionCount > 1)
        {
            Vector3 v = rend.GetPosition(rend.positionCount - 2);
            Debug.Log($"Comparing ({v.x}, {v.y}, {v.z}) to ({pos.position.x}, {pos.position.y}, {pos.position.z})");
        }

        if (rend.positionCount > 1 && Vector3XZEquals(rend.GetPosition(rend.positionCount - 2), pos.position))
        {
            rend.positionCount--;
        }
        else
        {
            rend.positionCount++;
            rend.SetPosition(rend.positionCount - 1, pos.position + (Vector3.up * height));
        }
    }

    private void Thread_OnPlayerEnterCheckpoint(object senter, PlayerMovement.OnPlayerEnterCellEventArgs pos)
    {
        locked = true;

        //First, once we're on a checkpoint we don't want to do any of the other stuff
        pm.OnPlayerEnterCell -= Thread_OnPlayerEnterCell;

        //Then, we'll make a new thread for the next segment, and that *will* be subscribed
        Instantiate(this, transform.parent);

        //And then we won't need to do any further stuff
        pm.OnPlayerEnterCheckpoint -= Thread_OnPlayerEnterCheckpoint;
    }

    private bool Vector3XZEquals(Vector3 v1, Vector3 v2)
    {
        return (Mathf.Abs(v1.x - v2.x) < 0.01f) &&(Mathf.Abs(v1.z - v2.z) < 0.01f);
    }

    private bool Locked
    {
        get
        {
            return locked;
        }
    }
}
