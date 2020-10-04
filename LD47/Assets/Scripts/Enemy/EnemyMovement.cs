using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minDistance;

    private bool allowMovement = false;
    private bool allowRotation = false;

    private void Update()
    {
        if (allowMovement)
        {
            if (Vector3.Distance(transform.position, target.position) > minDistance)
            {
                Vector3 newPos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
                newPos = new Vector3(newPos.x, 0f, newPos.z);
                transform.position = newPos;
            }
        }

        if (allowRotation)
        {
            transform.LookAt(target);
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
        }
    }

    //public void ToggleMovement(bool move)
    //{
    //    allowMovement = move;
    //}

    public void ToggleMovement(int move)
    {
        allowMovement = move != 0;
    }

    //public void ToggleRotation(bool rot)
    //{
    //    allowRotation = rot;
    //}

    public void ToggleRotation(int rot)
    {
        allowRotation = rot != 0;
    }
}
