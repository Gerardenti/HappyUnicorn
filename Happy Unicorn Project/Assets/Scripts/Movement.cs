using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public enum Direction { NONE, UP, DOWN, LEFT, RIGHT };

    public Direction uniDirection = Direction.NONE;

    public float baseSpeed = 0.3f;
    private float currentSpeedV = 0.0f;
    private float currentSpeedH = 0.0f;
    private Rigidbody2D rigidBody;

    public SpriteRenderer sprite;

    public ChargePixie chargePixie;

    //public bool onFloor;
    public int numCharge;

    public static Movement Instance{get; private set;}

    private bool faceR = true;

    private KeyCode upButton = KeyCode.W;
    private KeyCode leftButton = KeyCode.A;
    private KeyCode downButton = KeyCode.S;
    private KeyCode rightButton = KeyCode.D;

    private KeyCode quitButton = KeyCode.Escape;

    private KeyCode hornButton = KeyCode.P;

    public int chargeNumber;

    bool runningTimer = false;

    bool invulTimer = false;

    public float timer = 0.0f;
    public float iTimer = 0.0f;

    public int uniMaxHP = 5;
    public int currentHP;

    private Rigidbody2D rb2d;
    private Animator anim;



    private void Awake()
    {
        chargeNumber = 0;
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        currentHP = uniMaxHP;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.y));

        if (currentHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }   

        float delta = Time.deltaTime * 1000;
        
        if (runningTimer == true)
        {
            timer += Time.deltaTime;
            if (timer < 2.0f)
            {
                baseSpeed = 0.7f;
                sprite.color = new Color(1, 0.9f, 0, 1);
            }
            else if (timer >= 2.0f)
            {
                baseSpeed = 0.3f;
                sprite.color = new Color(1, 1, 1, 1);
                runningTimer = false;
            }
        } else
        {
            timer = 0;
        }

        /*if (invulTimer == true)
        {
            iTimer += Time.deltaTime;
            if (iTimer > 0.4f && iTimer < 1.0f)
            {
                sprite.color = new Color(1, 1, 1, 1);
            }
            else if (iTimer >= 1.0f)
            {
                iTimer = 0;
                //invulTimer = false;
                DamagedByGnome();
            }
        }
        else
        {
            iTimer = 0;
        }*/

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

        if (Input.GetKey(hornButton) && numCharge > 0)
        {
            numCharge--;
            runningTimer = true;
        }

        if (Input.GetKey(quitButton))
        {
            Application.Quit();
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

        if (faceR == false && currentSpeedH == baseSpeed)
        {
            Face();
        } else if (faceR == true && currentSpeedH == -baseSpeed)
        {
            Face();
        }
    }
    void Face()
    {
        faceR = !faceR;
        Vector3 Flip = transform.localScale;
        Flip.x *= -1;
        transform.localScale = Flip;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        float delta = Time.fixedDeltaTime * 1000;

        if (other.tag == "Pixie"){
            numCharge++;
            chargePixie.chargeNumber++;
            other.gameObject.SetActive(false);
        }
        if (other.tag == "Gnome") {
            DamagedByGnome();
            invulTimer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Gnome")
        {
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (invulTimer == true && collision.CompareTag("Gnome")) {
            iTimer += Time.deltaTime;
            if (iTimer > 0.4f && iTimer < 1.0f) {
                sprite.color = new Color(1, 1, 1, 1);
            } else if (iTimer >= 1.0f) {
                //iTimer = 0;
                invulTimer = false;
                //DamagedByGnome();
            }
        } else if (!collision.CompareTag("Gnome")) {
            invulTimer = false;
            iTimer = 0;
        }
    }

    void DamagedByGnome()
    {
        sprite.color = new Color(1, 0, 0, 1);
        Hurt(1);
        /*timer += Time.deltaTime;
        if (timer == 0)
        {
            currentHP--;
        }
        else if (timer >= 1)
        {
            timer = 0;
        }*/
    }

    public void Hurt(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            
        }
    }
}