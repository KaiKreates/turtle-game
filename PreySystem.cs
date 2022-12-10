using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreySystem
{
    public Rigidbody rigidbody;
    public GameObject preys;
    public float followSpeed;

    public PreySystem(Rigidbody rigidbody, GameObject prey, float followSpeed)
    {
        rigidbody.velocity = (prey.transform.position - rigidbody.position).normalized * followSpeed;
    }
}
