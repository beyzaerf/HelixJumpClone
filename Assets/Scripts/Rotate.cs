using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotates the platform with the input from the user. (Attached to cylinder)
public class Rotate : MonoBehaviour
{
    Vector3 lastPosition;

    private void Update() // should stop when game over
    {
        if (Input.GetMouseButtonUp(0))
        {
            lastPosition = Vector3.zero;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Input.mousePosition;

            if (lastPosition == Vector3.zero)
                lastPosition = currentPosition;

            float delta = lastPosition.x - currentPosition.x;
            lastPosition = currentPosition;
            transform.Rotate(Vector3.up * delta);
        }
    }
}
