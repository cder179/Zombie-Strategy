using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageProjectile : MonoBehaviour
{
    public Rigidbody RB;
    public float Speed = 30;
    bool shouldDestroy;
    public float Knockback = 10;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        //When I spawn, I fly straight forwards at my Speed
        RB.velocity = transform.forward * Speed;
    }

    private void FixedUpdate()
    {
        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //If I hit something with a rigidbody. . .
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null && !other.gameObject.CompareTag("Player") )
        {
            //I push them in the direction I'm flying with a power equal to my Knockback stat
            //rb.AddForce(RB.velocity.normalized * Knockback,ForceMode.Impulse);
            //rb.AddForce(-Knockback, 0, 0);

            //If I hit anything, I despawn
            shouldDestroy = true;
            
        }
;
      
    }
}
