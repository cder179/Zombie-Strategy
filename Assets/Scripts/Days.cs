using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Days : MonoBehaviour
{
    
    public static int daysNum =7;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void daysButton(int value)
    {
        daysNum = value;
        SceneManager.LoadScene("Main");
    }
}
