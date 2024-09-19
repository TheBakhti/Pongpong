using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    public float initialSpeed = 10f;
    public float speedIncrease = 0.2f;
    public Text playerText;
    public Text opponentText;
    private int hitCounter;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease*hitCounter));
    }

    private void StartBall(){
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease*hitCounter);
    }

    private void RestartBall(){
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(transform.position.x > 0){
            RestartBall();
            playerText.text = (int.Parse(playerText.text)+1).ToString();
        }else if(transform.position.x < 0){
            RestartBall();
            opponentText.text = (int.Parse(opponentText.text)+1).ToString();
        }
    }

// Make the ball move not only straight from the beginning
private void PlayerBounce(Transform obj) {
    hitCounter++;

    Vector2 ballPosition = transform.position;
    Vector2 playerPosition = obj.position;

    float xDirection;
    float yDirection;

    // We want to flip the direction once it hits the edges
    if (transform.position.x > 0) {
        xDirection = -1;
    } else {
        xDirection = 1;
    }

    // However, we want to make sure that it is not exactly 0, if it is then the ball will get stuck
    yDirection = (ballPosition.y - playerPosition.y) / obj.GetComponent<Collider2D>().bounds.size.y;

    if (yDirection == 0) {
        yDirection = 0.25f;
    }

    // Apply the newly calculated directions to the ball
    rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
}

// Now we need to apply the above function, so let's create the method for that
 private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.name == "PaddleA" || other.gameObject.name == "PaddleB") {
        PlayerBounce(other.transform);
    }
}}