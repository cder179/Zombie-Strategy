using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GM : MonoBehaviour
{
    // Start is called before the first frame update
    public static GM instance;
    public static int day = 0;
    public static int totalZombiesCured = 0;
    public FPController Player;
    public AnimateText animateText;
    public Days days;
    public int totalDays;
    private Vector3 playerStartPos;
    //OTHER STUFF????
    public int packagesLeft = 0;
    public int zombiesLeft;
    public int zombiesTotal;
    public float profitThisRnd;
    public float gutsThisRnd = 0;
    public float gutsSpent;
    public bool shouldKillAllZombies = false;
    //public float previousGutCount;

    public float zombiesLeftToSpawn;
    float zombieSpawnTimer;

    public GameStates GameStates = GameStates.None;

    [Header("LemonadeStand")]
    public int medicinePackages = 0;
    public float medicineStrength = 0.5f;
    public float guts = 100;
    public int lures = 0;
    public int luresBoughtToday = 0;
    public float gutsCost;
    public int humanInterference;
    [Header("End Screen Vars")]


    [Header("UI")]
    [SerializeField] private TextMeshProUGUI PackagesLeftText;
    [SerializeField] private GameObject resourceBucket;
    [SerializeField] private GameObject shootingBucket;
    [SerializeField] private GameObject endDayBucket;
    [SerializeField] private TextMeshProUGUI packagesText;
    [SerializeField] private TextMeshProUGUI luresText;
    [SerializeField] private TextMeshProUGUI gutsText;
    [SerializeField] private TextMeshProUGUI gutsText2;
    [SerializeField] private TextMeshProUGUI startButtonText;
    [Header("End Screen UI")]
    [SerializeField] private TextMeshProUGUI zombiesPresent;
    [SerializeField] private TextMeshProUGUI cureRate;
    [SerializeField] private TextMeshProUGUI LeftoverPackages;
    [SerializeField] private TextMeshProUGUI totalProfit;
    [SerializeField] private TextMeshProUGUI dayNum;
    [SerializeField] private TextMeshProUGUI endReason;
    [SerializeField] private TextMeshProUGUI zombiesCuredTxt;

    [Header("Prefabs")]
    [SerializeField] private Zombie zombiePrefab;
    // Start is called before the first frame update
    void Start()
    {
        totalDays = Days.daysNum;
        GameStates = GameStates.BeginDay;
        resourceBucket.SetActive(true);
        shootingBucket.SetActive(false);
        endDayBucket.SetActive(false);
        instance = this;
        playerStartPos = Player.transform.position;
        //spawnZombies(lures);
    }

  
    //int medicinePackages, int medicineStrength, int guts, int humanInterferenceChance, 
    float spawnZombies(int privlures)
    {
        //calculate number of zombies :)
        int zombieBaseCount = (Random.Range(5, 8));
        float ZombieCount = zombieBaseCount + ((zombieBaseCount * 0.5f)* privlures);
        Debug.Log( Mathf.Round(ZombieCount));
        //for (int i = 0; i < Mathf.Round(ZombieCount); i++)
        //{   
        //    Instantiate(zombiePrefab, new Vector3(-10f, 0, Random.Range(-20f, 20f)), Quaternion.identity);
        //}
        
        zombiesLeft = (int)ZombieCount;
        zombiesTotal = zombiesLeft;
        zombieSpawnTimer = 1;
        return (int)ZombieCount;
    }

    void actuallySpawnZombies(float ZombiesToSpawn)
    {
        if (ZombiesToSpawn > 0)
        {
            if (zombieSpawnTimer > 0)
            {
                zombieSpawnTimer -= Time.deltaTime;
            }
            else
            {
                Instantiate(zombiePrefab, new Vector3(-10f, 0, Random.Range(-20f, 20f)), Quaternion.identity);
                zombieSpawnTimer = 1;
                zombiesLeftToSpawn--;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (GameStates)
        {
            case GameStates.Shooting:
            {
                    actuallySpawnZombies(zombiesLeftToSpawn);
                PackagesLeftText.text = packagesLeft.ToString();
                    gutsText.text = guts.ToString();
                    gutsText2.text = guts.ToString();
                    zombiesCuredTxt.text = "Zombies Cured: " + totalZombiesCured;
                    if (zombiesLeft == 0 || packagesLeft <= 0)
                    {
                       
                            StartCoroutine(wait());
                       
                        //wait
                        //TRANSITION TO END DAY SCREEN
        
                    }

                    return;
            }

            case GameStates.BeginDay:
            {
                packagesLeft = medicinePackages;
                updateText();
                PackagesLeftText.text = "";
                //resourceBucket.SetActive(true);
                return;
            }

            case GameStates.EndDay:
                {

                    shouldKillAllZombies = false;
                    totalProfit.text = "Total Profit... " + profitThisRnd;
                    //Debug.Log("Profit: " + profitThisRnd);
                    LeftoverPackages.text = "Leftover Packages... " + packagesLeft;
                    //Debug.Log("Leftover Packages: "+ packagesLeft);
                    zombiesPresent.text = "# of Zombies Present... " + zombiesTotal;
                    //Debug.Log("Zombies Present: " + zombiesTotal);
                    cureRate.text = "Cure Rate... " + calculateAvgCureRate() + "%";
                    //Debug.Log("Cure Rate: " + calculateAvgCureRate()+ "%" );
                    dayNum.text = "Day "+ day;
                    return;
                }
        }
       
    }

    void resetVals()
    {
    medicinePackages = 0;
    medicineStrength = 0.5f;
    luresBoughtToday = 0;
    }

   public void startDay()
    {
        if (gutsCost > guts)
        {
            Debug.Log("NOT ENOUGH GUTS!!!!");
            
        }
        else
        {
            lures = lures + luresBoughtToday;
            gutsThisRnd = 0;
            profitThisRnd = 0;
            day++;
            gutsSpent = gutsCost;
            //previousGutCount = guts;
            guts = guts - gutsCost;
            GameStates = GameStates.Shooting;
            zombiesLeftToSpawn = spawnZombies(lures);
            resourceBucket.SetActive(false);
            shootingBucket.SetActive(true);
            lockMouse(true);
           
        }
        
    }

    public void nextDay()
    {
        luresBoughtToday = 0;
        animateText.isActive = false;
        GameStates = GameStates.BeginDay;
        profitThisRnd = 0;
        endDayBucket.SetActive(false);
        resourceBucket.SetActive(true);
        lockMouse(false);
    }

    public void updateText()
    {
        packagesText.text = medicinePackages.ToString();
        float t = lures + luresBoughtToday;
        luresText.text = t.ToString();
        gutsText.text =guts.ToString();
        gutsText2.text = guts.ToString();
        float tempgutsCost = (luresBoughtToday * 25) +
            //((medicinePackages * (medicineStrength)+1)* 20);
            (medicinePackages * (medicineStrength + 0.5f )* 10);
        gutsCost = Mathf.Round(tempgutsCost);
        startButtonText.text = "Start Day!\nCost: " + gutsCost + " Guts";
    }

    public void setPackages(int value)
    {
        medicinePackages = (medicinePackages <= 0 && value <= 0) ? medicinePackages : medicinePackages + value;
    }
    
    public void setLures(int value)
    {
        //if lures bought today are at zero and going down, keep the same
        luresBoughtToday = (luresBoughtToday <= 0 && value<=0) ? luresBoughtToday : luresBoughtToday + value;
        //lures = (lures <= 0 && value<=0) ? lures : lures + value;
    }
   public void changeSlider(float value)
   {
       medicineStrength = value;
   }

    public int calculateAvgCureRate()
    {
        /**
         * THIS IS BEING CALCULATED ASSUMING CURE RATE RANDOM STAYS AT 45 - 80. 
         * FUTURE ME PLEASE REMEMBER TO CHANGE THIS W/ A VARIABLE PLEASE PLEASE PLEASE PLEASE!!!!
         **/
        float curedProbability = 62.5f + ((medicineStrength * 50) - 25);
        curedProbability = Mathf.Clamp(curedProbability, 0, 100);
        return (int)curedProbability;
    }
    
    public void lockMouse(bool isLocked)
    {

        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }
    IEnumerator wait()
    {
        Debug.Log("inside");
        int waitTime = packagesLeft <= 0 ? 2: 0;
        yield return new WaitForSeconds(waitTime);
        if (day >= totalDays)
        {
            SceneManager.LoadScene("EndScreen");
        }
        if (guts <= 0)
        {
            //change laterr
            SceneManager.LoadScene("EndScreen");
        }
        animateText.isActive = true;
        endReason.text = (zombiesLeft == 0) ? "You cured all the zombies!" : "You ran out of packages.";
        shouldKillAllZombies = true;
        profitThisRnd = gutsThisRnd - gutsSpent;
        Debug.Log("Ending");
        Player.transform.position = playerStartPos;
        GameStates = GameStates.EndDay;
        shootingBucket.SetActive(false);
        lockMouse(false);
        endDayBucket.SetActive(true);
    }
}

public enum GameStates
{
    None,
    BeginDay,
    Shooting,
    EndDay
}
