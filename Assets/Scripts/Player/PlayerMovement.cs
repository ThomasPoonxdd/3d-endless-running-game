using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
 {
    bool alive = true;

    public float forwardSpeed = 15;
    [SerializeField] Rigidbody rb;

    //float horizontalInput;
    [SerializeField] float horizontalMultiplier = 10.5f;
    [SerializeField] float leftRightPosition = 3.3f;
    [SerializeField] float error_Lane = 0.1f;

    // Lane1 | Lane2 | Lane3
    private float desiredLane = 0;

    public float speedIncreasePerPoint = 0.1f;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask obstacleMask;

    public void IncrementSpeed()
    {
        forwardSpeed += speedIncreasePerPoint;
    }

    //private void FixedUpdate() {
    //   Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
    //   Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
    //   rb.MovePosition(rb.position + forwardMove + horizontalMove);
    //}

    void Start() 
    {
        rb.MovePosition(new Vector3(0, 3, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameStarted()) 
        {
            return;
        }

        if (!alive) return;

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }

        #region ChangeLane
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 2)
                desiredLane = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -2)
                desiredLane = -1;
        }
        #endregion

        #region Movement
        float radius = GetComponent<Collider>().bounds.size.z;
        bool isHitByObstacle_z = Physics.Raycast(transform.position, transform.forward, radius-0.5f, obstacleMask);
        bool isHitByObstacle_xR = Physics.Raycast(transform.position, transform.right, radius + 0.1f, obstacleMask);
        bool isHitByObstacle_xL = Physics.Raycast(transform.position, -transform.right, radius + 0.1f, obstacleMask);

        Vector3 forwardMove = transform.forward * forwardSpeed * Time.deltaTime;
        // Move toward z direction 

        
        Vector3 horizontalMove = new Vector3(0, 0, 0);

        if (desiredLane == 1 && rb.position.x != desiredLane * leftRightPosition)
        {
            //Player.position += new Vector3(0, 0, 10.5f * Time.deltaTime);
            if (Math.Abs(rb.position.x - leftRightPosition) < error_Lane)
            {
                horizontalMove = new Vector3(- rb.position.x + leftRightPosition, 0, 0);
            }
            else 
            {
                horizontalMove = new Vector3(Time.deltaTime * horizontalMultiplier, 0, 0);
            }
            
        }
        else if (desiredLane == -1 && rb.position.x != desiredLane * leftRightPosition)
        {
            //Player.position += new Vector3(0, 0, -10.5f * Time.deltaTime);

            if (Math.Abs(rb.position.x + leftRightPosition) < error_Lane)
            {
                horizontalMove = new Vector3(-rb.position.x - leftRightPosition, 0, 0);
            }
            else 
            {
                horizontalMove = new Vector3(-Time.deltaTime * horizontalMultiplier, 0, 0);
            }
           
        }
        else if (desiredLane == 0 && rb.position.x != desiredLane * leftRightPosition)
        {
            //Player.position += new Vector3(0, 0, 10.5f * Time.deltaTime);
            if (Math.Abs(rb.position.x) < error_Lane)
            {
                horizontalMove = new Vector3(-rb.position.x, 0, 0);
            }
            else {
                horizontalMove = new Vector3(-Math.Sign(rb.position.x) * Time.deltaTime * horizontalMultiplier, 0, 0);
            }
            
        }

        if (isHitByObstacle_z)
        {
            forwardMove = new Vector3(0, 0, 0);
        }
        if (isHitByObstacle_xR & horizontalMove.y > 0)
        {
            horizontalMove = new Vector3(0, 0, 0);
            
        }
        if (isHitByObstacle_xL & horizontalMove.y < 0)
        {
            horizontalMove = new Vector3(0, 0, 0);

        }

        rb.MovePosition(rb.position + forwardMove + horizontalMove);
        #endregion



        if (transform.position.y < -5) 
        {
            Die();
        }
    }

    public bool IsAlive()
    {
        return alive;
    }

    public void Die() 
    {
        alive = false;
    }

    void Jump()
    {
        //Check whether we are currently grounded
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGround = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f,groundMask);

        //If we are, jump
        if (isGround) 
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
        
    }
}
