﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Student : MonoBehaviour {

    // later prob sign a name to it.
    public string name { get; protected set; }
    // public Vector3 pos { get; protected set; }

    int tuitionAmount = 1;

    float probeRange = 1f;
    float timePerChange = 3f;
    float angleForTarget;
    float changeSpeed = 10f;
    float timeForMovement = 3f;
    float timeForRest = 3f;

    Vector3 targetDir;
    Transform selfTransform = null;

	// Use this for initialization
	void Start () {
        selfTransform = transform;
        
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMovement();
        PayForTuition();
	}

    public void Die()
    {
       
    }

    void PayForTuition()
    {
        GameController.instance._PlayerMoney += Time.deltaTime;
    }

    void UpdateMovement()
    {

        // the time per change is equal to the gap time + run time;
        if (timePerChange <= timeForMovement)    // this is the run time. so gap time should be timePerChange - this.
        {
            if (timePerChange <= 0f)
            {
                probeRange = Random.Range(0f, 1.5f);
                FindDirection();
            }

            // check if the new targetDir is good to go!
            // -------------------------------------------
            Tile tileUnderTargetDir = MouseController.instance.GetTileAtWorldCoord(targetDir + selfTransform.position);

            if (tileUnderTargetDir != null)
            {
                if (tileUnderTargetDir.Type != TileType.PlayGround)
                    ReFindDirection();
            }
            else
            {
                ReFindDirection();
            }

            //--------------------------------------------

            // if all the conditions are satisfied, we are ready to go!
            selfTransform.position += Vector3.Slerp(Vector3.zero, targetDir, Time.deltaTime / 7f);

        }

        timePerChange -= Time.deltaTime;
    }

    void FindDirection()
    {
        timePerChange = timeForMovement + timeForRest;

        timeForRest = Random.Range(3f, 20f);
        timeForMovement = Random.Range(3f, 20f);
        timePerChange = Random.Range(3f, 20f);

        angleForTarget = Random.Range(0, 2 * Mathf.PI);

        targetDir = new Vector3(Mathf.Cos(angleForTarget) * probeRange, Mathf.Sin(angleForTarget) * probeRange, 0f);
    }

    void ReFindDirection()
    {
        timePerChange = timeForMovement + timeForRest;

        timeForRest = 0.1f;
        timeForMovement = 0.1f;
        timePerChange = 0.1f;

        angleForTarget = Random.Range(0, 2 * Mathf.PI);

        targetDir = new Vector3(Mathf.Cos(angleForTarget) * probeRange, Mathf.Sin(angleForTarget) * probeRange, 0f);
    }
}