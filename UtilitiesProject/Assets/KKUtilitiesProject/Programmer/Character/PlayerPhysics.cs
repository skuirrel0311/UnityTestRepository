using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField]
    Transform parent = null;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - parent.position;
    }

    void FixedUpdate()
    {
        Vector3 movement = (parent.position + offset) - transform.position;
        transform.rotation = Quaternion.identity;
        transform.Translate(movement, Space.World);
    }
}
