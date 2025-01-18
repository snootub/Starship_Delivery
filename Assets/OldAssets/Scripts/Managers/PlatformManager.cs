using System;
using UnityEngine;

//Monobehavior script for storing different potential properties of platforms. All collision-based events are handled on the playermovement end.
public class PlatformManager : MonoBehaviour
{

    [Header("Bouncy Platforms")]
    public float bounciness;

    [Header("Moving Platforms")]
    public float moveRangeX;
    public float moveSpeed;
    private int backtracking = 1;
    private float offset = 0;

    [Header("Hazards")]
    public bool lethal;

    private void FixedUpdate()
    {
        if (moveRangeX > 0 && moveSpeed > 0)
        {
            platformMove();
        }
    }


    private void platformMove()
    {
        transform.Translate(backtracking * moveSpeed * Time.deltaTime,0,0);
        offset += backtracking * moveSpeed * Time.deltaTime;
        if (offset > moveRangeX / 2)
        {
            backtracking = -1;
        }
        if (offset < -moveRangeX / 2)
        {
            backtracking = 1;
        }

    }
}
