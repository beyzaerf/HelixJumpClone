using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes the ball bounce and break platforms. (Attached to ball)
public class Bounce : MonoBehaviour
{
    [SerializeField] private int speed;
    private Rigidbody rb;
    private bool isPowerUp; //is true when ball has passed 3 or more platforms.
    public int perfectPass;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(perfectPass >= 3 && !isPowerUp)
        {
            isPowerUp = true;
            rb.AddForce(Vector3.up);
            perfectPass = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.transform.CompareTag("Bounce"))
        {
            rb.velocity = transform.up * speed;
            if (isPowerUp)
            {
                Destroy(collision.transform.parent.gameObject);
                isPowerUp = false;
            }
        }
        else if(collision.transform.CompareTag("RedBounce"))
        {
            //game over 
            rb.isKinematic = true;
        }
        
    }
    private void OnTriggerEnter(Collider other) //passing without touching anything ?
    {
        perfectPass++;
    }
}
