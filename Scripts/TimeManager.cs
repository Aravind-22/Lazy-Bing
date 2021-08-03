using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text time;
    public GameObject energyText;
    int sec, min;
    public GameObject restingChair;
    public GameObject RestartPanel;
    public float timer;
    public float restingTimer, blinkTimer=0;
    public static TimeManager instance;
    public bool resetTimer=false;
    public bool isActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        restingChair = GameObject.FindGameObjectWithTag("Chair");
        timer = 300;
        restingTimer = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            if(!PlayerMovement.instance.isKeyPressed)
                timer -= Time.deltaTime;
            if (timer <= 1)
            {
                RestartTask();
            }                
            if (!resetTimer)
                StopWatch();
        }
        if (resetTimer)
        {          
            timer = 300;
            restingTimer = 15;
            resetTimer = false;
            isActivated = false;
        }
        if (timer <= 30 && timer>0)
        {
           // "Energy not sufficient. Please take rest";
            restingChair.GetComponent<BoxCollider>().enabled = true;
            energyText.GetComponent<Text>().text = "Energy  not  sufficient. Please  take  rest.";
            isActivated = true;
            //Debug.Log("resting timer: "+restingTimer);
        }
        if (isActivated)
        {
            //energyText.SetActive(true);
            blinkTimer = blinkTimer + Time.deltaTime;
            if (blinkTimer >= 1)
            {
                energyText.SetActive(true);
            }
            if (blinkTimer >= 2)
            {
                energyText.SetActive(false);
                blinkTimer = 0;
            }
        }
        if (!isActivated)
        {
            energyText.SetActive(false);
        }
    }
    void StopWatch()
    {
        min = Mathf.FloorToInt(timer / 60F);
        sec = Mathf.FloorToInt(timer - min * 60);
        string niceTime = string.Format("{0:0}:{1:00}", min, sec);
        time.text = niceTime;
    }
    //public void Save()
    //{
    //    SaveSystem.SaveGame(null, null, this);
    //}
    void RestartTask()
    {
        RestartPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.instance.onPause = true;
        resetTimer = true;
        PlayerMovement.instance.playerWalk.Pause();
    }
}
