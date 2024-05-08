using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class FPController : MonoBehaviour
{
    public Rigidbody RB;
    [SerializeField] private float Speed;
    [SerializeField] private float mouseSpeed = 3;
    [SerializeField] private Camera Eyes;
    public PackageProjectile PackagePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
        //
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GM.instance.GameStates)
        {
            case GameStates.Shooting:
            {
                Shooting();
                return;
            }
        }
    }

    void Shooting()
    {
        float xMouse = Input.GetAxis("Mouse X") * mouseSpeed;
        transform.Rotate(0, xMouse, 0);
        
        float yMouse = Input.GetAxis("Mouse Y");
        Eyes.transform.Rotate(-yMouse, 0, 0);
        
        Vector3 vel = Vector3.zero;
        vel.y = RB.velocity.y;
        if (Input.GetKey(KeyCode.W))
        {
            vel += transform.forward * Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vel += transform.forward * -Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vel += transform.right * Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vel += transform.right * -Speed;
        }

        RB.velocity = vel;
        
        //shooting
        if (Input.GetMouseButtonDown(0) && GM.instance.packagesLeft > 0)
        {
            //Spawn a projectile right in front of my eyes
            Instantiate(PackagePrefab, Eyes.transform.position + Eyes.transform.forward,
                Eyes.transform.rotation);
            GM.instance.packagesLeft--;
        }
    }
}
