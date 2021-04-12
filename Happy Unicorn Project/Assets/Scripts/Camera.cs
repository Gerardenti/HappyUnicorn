using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform unicorn;
    public Vector3 offset;

    void Update()
    {
        transform.position = new Vector3(unicorn.position.x + offset.x, unicorn.position.y + offset.y, offset.z);
    }
}
