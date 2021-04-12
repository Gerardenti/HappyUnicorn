using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum Direction { NONE, UP, DOWN, LEFT, RIGHT };

    public Direction uniDirection = Direction.NONE;

    public float baseSpeed = 0.3f;
    private float currentSpeedV = 0.0f;
    private float currentSpeedH = 0.0f;
    private Rigidbody2D rigidBody;

    //public bool onFloor;
    public int numCharge;

    private KeyCode upButton = KeyCode.W;
    private KeyCode leftButton = KeyCode.A;
    private KeyCode downButton = KeyCode.S;
    private KeyCode rightButton = KeyCode.D;

    private KeyCode hornButton = KeyCode.P;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        uniDirection = Direction.NONE;

        if (Input.GetKey(upButton))
        {
            uniDirection = Direction.UP;
        }
        else if (Input.GetKey(downButton))
        {
            uniDirection = Direction.DOWN;
        }

        if (Input.GetKey(leftButton))
        {
            uniDirection = Direction.LEFT;
        }
        else if (Input.GetKey(rightButton))
        {
            uniDirection = Direction.RIGHT;
        }
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;

        currentSpeedV = 0;
        currentSpeedH = 0;

        switch (uniDirection)
        {
            default: break;
            case Direction.UP:
                currentSpeedV = baseSpeed;
                break;
            case Direction.DOWN:
                currentSpeedV = -baseSpeed;
                break;
            case Direction.LEFT:
                currentSpeedH = -baseSpeed;
                break;
            case Direction.RIGHT:
                currentSpeedH = baseSpeed;
                break;
        }
        rigidBody.velocity = new Vector2(currentSpeedH, currentSpeedV) * delta;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pixie"){
            numCharge++;
            other.gameObject.SetActive(false);
        }
    }
}
//