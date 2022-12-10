using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public GameObject prey;
    public Transform eyes;
    private Rigidbody rb;
    public LayerMask whatIsFollow;
    private Quaternion targetRot;
    public int followOrAway;
    public float lookRadius;
    private float turnCooldown = 0;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        /*Collider[] detectPrey = Physics.OverlapSphere(eyes.position, lookRadius, whatIsFollow);
        for (int i = 0; i < detectPrey.Length; i++)
        {
            if (detectPrey[i])
                ChasePlayer(followOrAway);
            else FreeRoam();
        }*/
      
    }

    private void FreeRoam()
    {
        if (turnCooldown < Time.time)
        {
             targetRot = new Quaternion(0, Random.Range(0, 360), 0, 0);
             rb.angularVelocity = Vector3.up * Random.Range(0.2f, 1f);
             turnCooldown = Time.time + Random.Range(4, 10);
        }

        rb.velocity = transform.forward * speed;
    }

    private void ChasePlayer(int follow)
    {
        Vector3 dir = prey.transform.position - transform.position;
        rb.velocity = dir.normalized * follow * speed * 1.5f;
        Vector3 rot = dir * follow;
        transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
        if(Physics.Raycast(eyes.position, transform.forward, 10, whatIsFollow))
        {
            FreeRoam();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(eyes.position, lookRadius);
    }
}