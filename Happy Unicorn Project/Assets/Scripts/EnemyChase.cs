using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float visionRadius;
    public float speed;

    GameObject player;

    Camera main;

    Vector3 initialPosition;

    [Tooltip("Puntos de vida")]
    public int maxHp = 3;
    [Tooltip("vida actual")]
    public int hp;

    private bool faceR = true;

    private Animator anim;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Unicorn");

        initialPosition = transform.position;

        /*main = (Camera)GameObject.FindGameObjectWithTag("MainCamera") as Camera;*/

        hp = maxHp;

        rigidBody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        anim.SetFloat("speed", Mathf.Abs(rigidBody.velocity.y));
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        Vector3 target = initialPosition;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < visionRadius) target = player.transform.position;

        float fixedSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

        Debug.DrawLine(transform.position, target, Color.green);
    }

    private void FixedUpdate()
    {
        if (faceR == false && transform.position.x == speed)
        {
            Face();
        }
        else if (faceR == true && transform.position.x == -speed)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }

    public void Attacked()
    {
        if (--hp <= 0) Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("damage TAKE!");
    }
}
