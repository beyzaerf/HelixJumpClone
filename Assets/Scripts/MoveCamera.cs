using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform ballTransform;
    public Bounce bounce;
    private void FixedUpdate()
    {
        if (!bounce.IsBouncing)
            transform.position = Vector3.Lerp(transform.position, ballTransform.position + new Vector3(-0.1f, 1, 3.5f), 0.5f);
            //transform.position = ballTransform.position + new Vector3(-0.1f, 1, 3.5f);
    }
}
