using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMatching : MonoBehaviour
{
    public GameObject[] hiddenObject;
    public bool taskCompleted=false;
    public static ObjectMatching instance;
    public AudioClip correctMatch;
    int i,counter=0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
       if(ObjectSelector.instance.LMBClicked==true)
        IsObjectActive();
    }

    public void IsObjectActive()
    {
        for(i = 0;i<5; ++i)
        {
            if(DisplayGameObjects.instance.gameObjectNames[i].GetComponent<Text>().text == ObjectSelector.instance.name)
            {
                GameManager.instance.atmos.PlayOneShot(correctMatch);
                TaskManager.instance.cashCount += 2;
                hiddenObject[i].SetActive(false);
                TakeHint.instance.hintText[i].gameObject.SetActive(false);
                TakeHint.instance.isCashReduced = false;
                TakeHint.instance.isChoiceTaken = false;
                TakeHint.instance.hintDistancePanel.SetActive(false);
                TakeHint.instance.hintTextPanel.SetActive(true);
                DisplayGameObjects.instance.tempGameObjects[i].GetComponent<BoxCollider>().enabled = false;
                ObjectSelector.instance.LMBClicked = false;
            }
        }
        ++counter;
        if (counter == 5) {
            taskCompleted = true;
            for(byte i=0;i<5; ++i)
                hiddenObject[i].SetActive(true);
        }
            
        if (taskCompleted)
            counter = 0;
    }
}
