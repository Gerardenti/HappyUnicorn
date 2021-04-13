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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Unicorn");

        initialPosition = transform.position;

        /*main = (Camera)GameObject.FindGameObjectWithTag("MainCamera") as Camera;*/

        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = initialPosition;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < visionRadius) target = player.transform.position;

        float fixedSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

        Debug.DrawLine(transform.position, target, Color.green);
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

    /*
    void OnGUI()
    {
        Vector2 pos = Camera.main.WorlsToScreenPoint(transform.position);

        GUI.Box(
            new Rect(
                pos.x - 20,
                Screen.height - pos.y + 60,
                40,
                24
                ), hp + "/" + maxHp
            );
    }
    */
}
