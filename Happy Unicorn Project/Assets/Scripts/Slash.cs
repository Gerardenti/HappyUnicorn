using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [Tooltip("Esperar X segundos antes de destruir el objeto")]
    public float waitBeforeDestroy;

    [HideInInspector]
    public Vector2 Mov;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Mov.x, Mov.y, 0) * speed * Time.deltaTime;
    }

    IEnumerator OnTriggerEnter2D (Collider col)
    {
        if (col.tag == "Object") 
        {
            yield return new WaitForSeconds(waitBeforeDestroy);
            Destroy(gameObject);
        }else if (col.tag != "Unicorn" && col.tag != "Attack")
        {
            if (col.tag == "Enemy") col.SendMessage("Attacked");
            Destroy(gameObject);
        }

    }
}
