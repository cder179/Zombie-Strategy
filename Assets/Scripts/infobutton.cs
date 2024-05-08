using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class infobutton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      infoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
