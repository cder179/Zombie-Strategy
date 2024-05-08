using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimateText : MonoBehaviour
{
    //List<int> numbers = new List<int>();
    public Animator[] textList;
    public float timer = 0;
    public int animatorNum = 0;
    public Animation opacitySlide;
    public bool isActive = false;

    public GameObject start1;
    public GameObject start2;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)

        {
            showText();

        }
    }

    void showText()
    {
        if(timer>= 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if(animatorNum + 1 <= textList.Length){
                    textList[animatorNum].Play("OpacitySlide");
                    animatorNum++;
                    timer = 0.5f;
                } else
                {
                    
                    if(SceneManager.GetActiveScene().name == "Main")
                    {
                        animatorNum = 0;
                        GM.instance.nextDay();
                    }
                    else
                    {
                        //start1.SetActive(false);
                        //start2.SetActive(true);
                        SceneManager.LoadScene("Main");
                        //Enable bucket
                    }
                    
                }
              
            }
        }
        
    }
}
