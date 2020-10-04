using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cell : MonoBehaviour
{
    public enum Wall { Front, Right, Back, Left }

    public Transform genericWall;
    public float wallWidth;
    public float wallHeight;
    public PlayerHealth ph;

    private readonly Transform[] walls = new Transform[4];

    [HideInInspector] public bool triggered = false;

    public void Create(Vector2 size)
    {
        if (tag == "Checkpoint")
        {
            ph.OnPlayerDeath += Cell_OnPlayerDeath;
        }


        transform.localScale = new Vector3(size.x, transform.localScale.y, size.y);

        //Walls
        //Front - z .5 scale x 1
        //Right - x .5 scale z 1
        //Back - z -.5 scale x 1
        //Left - x -.5 scale z 1

        float width = wallWidth / transform.localScale.z;
        float height = wallHeight / transform.localScale.y;

        Transform frontWall = Instantiate(genericWall, transform);
        frontWall.localScale = new Vector3(1.1f, height, width);
        frontWall.localPosition = new Vector3(0f, height / 2f, 0.5f);
        walls[0] = frontWall;

        Transform rightWall = Instantiate(genericWall, transform);
        rightWall.localScale = new Vector3(width, height, 1.1f);
        rightWall.localPosition = new Vector3(0.5f, height / 2f, 0f);
        walls[1] = rightWall;

        Transform backWall = Instantiate(genericWall, transform);
        backWall.localScale = new Vector3(1.1f, height, width);
        backWall.localPosition = new Vector3(0f, height / 2f, -0.5f);
        walls[2] = backWall;

        Transform leftWall = Instantiate(genericWall, transform);
        leftWall.localScale = new Vector3(width, height, 1.1f);
        leftWall.localPosition = new Vector3(-0.5f, height / 2f, 0f);
        walls[3] = leftWall;
    }

    public void Create(float size)
    {
        Create(new Vector2(size, size));
    }

    private void Cell_OnPlayerDeath()
    {
        RemoveWall(Wall.Front);
    }
    
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Create(Random.Range(1f, 7f));
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        RemoveWall(Wall.Front);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        RemoveWall(Wall.Right);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        RemoveWall(Wall.Back);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        RemoveWall(Wall.Left);
    //    }

    //}

    /// <summary>
    /// Removes the given wall from the cell
    /// </summary>
    /// <param name="i">Which wall to remove</param>
    public void RemoveWall(Wall i)
    {
        if (walls[(int)i] != null)
        {
            walls[(int)i].gameObject.SetActive(false);
        }
    }

    public void AddWall(Wall i)
    {
        if (walls[(int)i] != null)
        {
            walls[(int)i].gameObject.SetActive(true);
        }
    }

    public bool Visited
    {
        get; set;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
