using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed= 3;
    [SerializeField]
    private GameObject target;
    public Vector2 movementDirection;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
    }
    void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            //transform.Translate(Vector3.right * speed * Time.deltaTime);
            //transform.position += transform.up * (Time.deltaTime * speed);
            //transform.Translate(new Vector3(movementDirection.x, movementDirection.y, 0) * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " collision with " + collision.gameObject.name);
        if(collision.gameObject.tag == "AreaEdge")
        {
            ChangeDirection(collision.gameObject.name);
        }
    }

    public void ChangeDirection(string collidedWith)
    {
        if (collidedWith == "LefteEdge")
        {
            movementDirection = new Vector2(Random.Range(0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        }
        else if (collidedWith == "RightEdge")
        {
            movementDirection = new Vector2(Random.Range(-1.0f, 0f), Random.Range(-1.0f, 1.0f)).normalized;
        }
        else if (collidedWith == "TopEdge")
        {
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0f)).normalized;
        }
        else if (collidedWith == "BottomEdge")
        {
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0f, 1.0f)).normalized;
        }
        else
        {
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            rb.AddForce(movementDirection * speed);
        }
    }
    public void GameOver()  
    {
        // STOP MOVEMENT
    }
}
