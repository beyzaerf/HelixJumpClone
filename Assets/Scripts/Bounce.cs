using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Makes the ball bounce and break platforms. (Attached to ball)
public class Bounce : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] private int speed;
    private Rigidbody rb;
    private bool isPowerUp; //is true when ball has passed 3 or more platforms.
    public int perfectPass; //passing count
    private int score;
    [SerializeField] private bool isBouncing; //for the camera 
    private GameManager gameManager;

    public bool IsBouncing { get => isBouncing; set => isBouncing = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
        rb.isKinematic = true;
    }
    private void Update()
    {
        if (perfectPass >= 3 && !isPowerUp) //if ball passed more than 3 platforms it gets power up
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
            rb.velocity = transform.up * speed;

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
            if (!isPowerUp)
            {
                //game over 
                rb.isKinematic = true;
                gameManager.GameFail();
                gameManager.GameState = GameState.GameOver;
            }
            else //hitting red with power up
            {
                rb.velocity = transform.up * speed;
                IsBouncing = false;
                Destroy(collision.transform.parent.gameObject);
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
        perfectPass++;
        Destroy(other.gameObject);
        AddScore();
        IsBouncing = false;
    }
    public void AddScore()
    {
        score += 2;
    }
}
