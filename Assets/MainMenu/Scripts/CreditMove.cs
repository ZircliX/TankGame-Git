using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMove : MonoBehaviour
{
    public Vector3 target;
    public float yValue;
    public bool canMove;

    void Update()
    {
        if (canMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 225 * Time.deltaTime, transform.position.z);
        }

        if (transform.position.y > target.y)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
    }

    public void SetMove(bool b)
    {
        canMove = b;
        ResetPosition();
    }
}