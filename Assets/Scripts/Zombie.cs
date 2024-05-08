using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEditor;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieStates zombieState = ZombieStates.Walking;
    public float Knockback = 10; 
    private Rigidbody RB;
    public AudioSource AS;
    public AudioClip Ow;
    private float Speed;

    private int cureAttempts = 0;
    bool cured = false;
    float stunTimer;
    public float stunTimerFull = 1;

    // Start is called before the first frame update
    void Start()
    {
        
        RB = GetComponent<Rigidbody>();
        Speed = Random.Range(2, 6);
        stunTimer = stunTimerFull;
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.instance.shouldKillAllZombies)
        {
            Destroy(gameObject);
        }

        switch (zombieState)
        {
            case ZombieStates.Walking:
            {
                RB.velocity = transform.right * Speed;
                return;
            }
            case ZombieStates.Cured:
                if (!cured)
                {
                    float gutsReward = (int)(Random.Range(20, 30) + (1.01 * GM.day));
                    GM.instance.guts += gutsReward;
                    GM.instance.gutsThisRnd += gutsReward;
                    GM.instance.zombiesLeft--;
                   
                    cured = true;
                }
                if (cured)
                {
                    Destroy(gameObject);
                }
                //DO NOTHING
                //cured animation sometime
                //Destroy(gameObject);
                return;
            case ZombieStates.Angered:
            {
                if(stunTimer == stunTimerFull)
                    {
                        RB.AddForce(new Vector3(-Knockback, 0), ForceMode.Impulse);
                    }
                if(stunTimer > 0)
                    {
                        
                        stunTimer -= Time.deltaTime;
                    }
                //addstun timer
                if(stunTimer <= 0)
                    {
                        RB.velocity = transform.right * (Speed + 2);
                    }
                
                return;
            }
        }
    }

    public void calculateIfCured()
    {
      
        float curedProbability = (Random.Range(45, 80) + ((GM.instance.medicineStrength * 50) - 25)+ 20*cureAttempts);
        curedProbability = Mathf.Clamp( curedProbability,0, 100);

        Debug.Log("Probability to cure : " + curedProbability);
        cureAttempts++;
        if ( Random.Range(0, 100) <= curedProbability)
        {
            Debug.Log("cured! ");
            GM.totalZombiesCured++;
            zombieState = ZombieStates.Cured;
        }
        else
        {
            AS.PlayOneShot(Ow);
            Debug.Log("Not cured");
            zombieState = ZombieStates.Angered;
            
        }
        
    }

    public void OnCollisionEnter(Collision other)
    {
        PackageProjectile packageScript = other.gameObject.GetComponent<PackageProjectile>();
        if (packageScript!= null)
        {
            
            //Zombie zombieScript = other.gameObject.GetComponent<Zombie>();
            calculateIfCured();
           
        }

    }
}
public enum ZombieStates
{
    Walking, 
    Cured, 
    Angered,
    Attacking
}
