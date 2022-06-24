using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Makes the ball bounce and break platforms. (Attached to ball)
public class Bounce : MonoBehaviour
{
    [SerializeField] Text text;
    private int speed = 5;
    private Rigidbody rb;
    public bool isPowerUp; //is true when ball has passed 3 or more platforms.
    public int perfectPass; //passing count
    private int score;
    private bool isBouncing; //for the camera 
    private GameManager gameManager;
    [SerializeField] private GameObject splashPrefab;

    public bool IsBouncing { get => isBouncing; set => isBouncing = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
        rb.isKinematic = true;
    }
    private void Update()
    {
        if (perfectPass >= 5 && !isPowerUp) //if ball passed more than 3 platforms it gets power up
        {
            IsBouncing = false;
            isPowerUp = true;
            rb.AddForce(Vector3.up);
            perfectPass = 0; //resets the count
        }
        if (gameManager.GameState == GameState.GameRunning)
            rb.isKinematic = false;
        text.text = score.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bounce"))
        {

            Vector3 vector = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            rb.velocity = transform.up * speed;
            GameObject instSplash = Instantiate(splashPrefab, vector, Quaternion.Euler(90,0,0));
            instSplash.transform.parent = collision.transform;
            IsBouncing = true;
            if (isPowerUp)
            {
                rb.AddForce(Vector3.down, ForceMode.Impulse);
                Destroy(collision.transform.parent.gameObject);
                isPowerUp = false;
                IsBouncing = false;
            }
        }
        else if (collision.transform.CompareTag("RedBounce"))
        {
            IsBouncing = false;
            if (!isPowerUp)   //game over
            { 
                rb.isKinematic = true;
                gameManager.GameFail();
                gameManager.GameState = GameState.GameOver;
            }
            else //hitting red with power up
            {
                rb.velocity = transform.up * speed;
                IsBouncing = false;
                collision.transform.parent.parent = null;

                foreach (Rigidbody rb in collision.transform.parent.GetComponentsInChildren<Rigidbody>())
                {
                    rb.isKinematic = false;
                }
            }
        }
        else if (collision.transform.CompareTag("Goal"))
        {
            //game win
            gameManager.GameWin();
            gameManager.GameState = GameState.GameWon;
        }

    }
    private void OnTriggerEnter(Collider other) //passing without touching anything
    {
        other.transform.parent = null;
        //other.isTrigger = false;
        perfectPass++;
        foreach (Rigidbody rb in other.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.back * 1000);
            DOVirtual.DelayedCall(1, () =>
            {
                rb.GetComponent<MeshCollider>().enabled = false;
            });
            DOVirtual.DelayedCall(2, () =>
            {
                Destroy(rb.gameObject);
            });
        }
        AddScore();
        IsBouncing = false;
    }
    public void AddScore()
    {
        score += 2;
    }
}
