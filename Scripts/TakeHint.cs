using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeHint : MonoBehaviour
{
    int choice, index;
    float distance;
    public Text[] hintText;
    public GameObject hintTextPanel, displayTextPanel, hintDistancePanel;
    public Image[] hintColors;
    public bool isHintTaken = false,isChoiceTaken=false, isCashReduced= false;
    Color alpha, beta;
    int colorIndex = 0;
    public static TakeHint instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        index = 0;
        choice = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (index = 0; index < 5; ++index)
        {
            hintText[index].text = (index+1)+". "+ DisplayGameObjects.instance.gameObjectNames[index].GetComponent<Text>().text;
        }
        if (!isHintTaken && !isCashReduced)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                isHintTaken = true;
                isCashReduced = true;
                Debug.Log("Hint taken");
            }
        }
        if (isHintTaken)
        {
            hintTextPanel.SetActive(false);
            displayTextPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                choice = 1;
                isChoiceTaken = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                choice = 2;
                isChoiceTaken = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                choice = 3;
                isChoiceTaken = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                choice = 4;
                isChoiceTaken = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                choice = 5;
                isChoiceTaken = true;
            }
        }
        if (isChoiceTaken)
        {
            hintDistancePanel.SetActive(true);
            displayTextPanel.SetActive(false);
            Vector3 playerTransform = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 objectTransform = DisplayGameObjects.instance.tempGameObjects[choice-1].transform.position;
            hintText[choice - 1].gameObject.SetActive(false);
            distance = Vector3.Distance(playerTransform, objectTransform);
            Indicator(distance);
        }
    }
    public void Indicator(float dist)
    {

        if (dist <= 5)
        {
            colorIndex = 3;
            Color3();
        }
        else if(dist>5 && dist <= 15)
        {
            colorIndex = 2;
            Color2();
        }
        else if (dist > 15 && dist <= 40)
        {
            colorIndex = 1;
            Color1();
        }
        else if (dist >40)
        {
            colorIndex = 0;
            Color0();
        }
        alpha = hintColors[colorIndex].color;
        alpha.a = 1;
        hintColors[colorIndex].color = alpha;
    }
    void Color0()
    {
        beta = hintColors[1].color;
        beta.a = 0.155f;
        hintColors[1].color = beta;
        beta = hintColors[2].color;
        beta.a = 0.155f;
        hintColors[2].color = beta;
        beta = hintColors[3].color;
        beta.a = 0.155f;
        hintColors[3].color = beta;
    }
    void Color1()
    {
        beta = hintColors[0].color;
        beta.a = 0.155f;
        hintColors[0].color = beta;
        beta = hintColors[2].color;
        beta.a = 0.155f;
        hintColors[2].color = beta;
        beta = hintColors[3].color;
        beta.a = 0.155f;
        hintColors[3].color = beta;
    }
    void Color2()
    {
        beta = hintColors[0].color;
        beta.a = 0.155f;
        hintColors[0].color = beta;
        beta = hintColors[1].color;
        beta.a = 0.155f;
        hintColors[1].color = beta;
        beta = hintColors[3].color;
        beta.a = 0.155f;
        hintColors[3].color = beta;
    }
    void Color3()
    {
        beta = hintColors[0].color;
        beta.a = 0.155f;
        hintColors[0].color = beta;
        beta = hintColors[1].color;
        beta.a = 0.155f;
        hintColors[1].color = beta;
        beta = hintColors[2].color;
        beta.a = 0.155f;
        hintColors[2].color = beta;
    }
}
