using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenThread : MonoBehaviour
{
    public PlayerMovement pm;
    public PlayerHealth ph;
    public float height;

    private LineRenderer rend;
    private bool locked = false;
    private bool allowPath = true;

    private void OnValidate()
    {
        GetLineRenderer();
    }

    private void Start()
    {
        GetLineRenderer();

        pm.OnPlayerEnterCell += Thread_OnPlayerEnterCell;
        pm.OnPlayerEnterCheckpoint += Thread_OnPlayerEnterCheckpoint;

        ph.OnPlayerDeath += Thread_OnPlayerDeath;
    }

    private void Thread_OnPlayerEnterCell(object sender, PlayerMovement.OnPlayerEnterCellEventArgs pos)
    {
        //Check if already visited (it'll be the second to last on the list)

        if (allowPath)
        {
            if (rend.positionCount > 1 && Vector3XZEquals(rend.GetPosition(rend.positionCount - 2), pos.position))
            {
                rend.positionCount--;
            }
            else
            {
                Vector3[] allPoints = new Vector3[rend.positionCount];
                rend.GetPositions(allPoints);
                if (rend.positionCount > 0 && Array.Find(allPoints, x => Vector3XZEquals(x, pos.position)) != Vector3.zero)
                {
                    Debug.Log("Found a repeat, did not add");
                    return;
                }

                rend.positionCount++;
                rend.SetPosition(rend.positionCount - 1, pos.position + (Vector3.up * height));
            }
        }
        else
        {
            if (rend.positionCount > 0 && Vector3XZEquals(rend.GetPosition(rend.positionCount - 1), pos.position))
            {
                allowPath = true;
            }
        }
    }

    private void Thread_OnPlayerEnterCheckpoint(object senter, PlayerMovement.OnPlayerEnterCellEventArgs pos)
    {
        locked = true;

        //First, once we're on a checkpoint we don't want to do any of the other stuff
        pm.OnPlayerEnterCell -= Thread_OnPlayerEnterCell;

        //Then, we'll make a new thread for the next segment, and that *will* be subscribed
        GoldenThread newThread = Instantiate(this, transform.parent);
        newThread.GetLineRenderer();
        newThread.rend.positionCount = 1;
        newThread.rend.SetPosition(0, pos.position);

        //And then we won't need to do any further stuff
        pm.OnPlayerEnterCheckpoint -= Thread_OnPlayerEnterCheckpoint;
    }

    private bool Vector3XZEquals(Vector3 v1, Vector3 v2)
    {
        return (Mathf.Abs(v1.x - v2.x) < 0.01f) &&(Mathf.Abs(v1.z - v2.z) < 0.01f);
    }

    private void Thread_OnPlayerDeath()
    {
        if (!locked)
        {
            rend.positionCount = 1;
            allowPath = false;
        }
    }

    private bool Locked
    {
        get
        {
            return locked;
        }
    }

    private void GetLineRenderer()
    {
        if (rend == null)
        {
            rend = GetComponent<LineRenderer>();
        }
    }
}
