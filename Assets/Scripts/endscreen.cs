using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endscreen : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp.text = "You cured " + GM.totalZombiesCured + " zombies!";
    }

    public void restart()
    {
        GM.totalZombiesCured = 0;
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
