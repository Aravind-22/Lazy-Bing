using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Text taskText;
    public Text cashText;
    public GameObject taskPanel;
    public GameObject WinScreenPanel;
    public GameObject LoseScreenPanel;
    public int taskCount = 1;
    public int cashCount=200;
    public float timer;
    public Vector3 pos;
    public bool[] names;
    bool completed = false;
    bool hintTaken = false;
    public static TaskManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        names = new bool[5];
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectMatching.instance.taskCompleted == true)
        {
            //Debug.Log(ObjectMatching.instance.taskCompleted);
            completed = true;
            if (completed)
            {
                if (taskCount == 15 && completed)
                {
                    if (cashCount >= 1000)
                    {
                        GameOver();
                    }
                    else 
                    {
                        RestartGame();
                    }
                    taskPanel.SetActive(false);
                }
                taskPanel.SetActive(true);
                TimeManager.instance.resetTimer = true;
                NextTask();
                completed = false;
                ++taskCount;
                cashCount += 50;
                for(int i = 0; i <= 4; ++i)
                {
                   TakeHint.instance.hintText[i].gameObject.SetActive(true);
                }
                //Debug.Log(taskCount);
            }
        }
        taskText.GetComponent<Text>().text = taskCount.ToString();
        if (TakeHint.instance.isHintTaken && TakeHint.instance.isChoiceTaken)
            hintTaken = true;
        if (hintTaken)
        {
            cashCount -= 3;
            TakeHint.instance.isHintTaken = false;
            hintTaken = false;
        }
        cashText.GetComponent<Text>().text = cashCount.ToString();
        timer = TimeManager.instance.timer;
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        pos = transform.position;
        for(int i = 0; i < 5; ++i)
        {
            names[i] = ObjectMatching.instance.hiddenObject[i].activeSelf;
        }
    }
    void NextTask()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.instance.onPause = true;
        GameManager.instance.gameScreePanel.SetActive(false);
    }

    public void onclickNextTask()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.instance.onPause = false;
        GameManager.instance.gameScreePanel.SetActive(true);
        taskPanel.SetActive(false);
        Save();
    }

    void GameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.instance.onPause = true;
        taskPanel.SetActive(false);
        WinScreenPanel.SetActive(true);
    }
    void RestartGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.instance.onPause = true;
        taskPanel.SetActive(false);
        LoseScreenPanel.SetActive(true);
    }
    public void Save()
    {
        SaveSystem.SaveGame(this);
    }
    public void Load()
    {
        PlayerData data = SaveSystem.LoadGame();
        taskCount = data.task;
        cashCount = data.cash;
        timer = data.timer;

        pos.x = data.position[0];
        pos.y = data.position[1];
        pos.z = data.position[2];

        names[0] = data.objectNames[0];
        names[1] = data.objectNames[1];
        names[2] = data.objectNames[2];
        names[3] = data.objectNames[3];
        names[4] = data.objectNames[4];

        TimeManager.instance.timer = timer;
        GameObject.FindGameObjectWithTag("Player").transform.position = pos;
        for(int i = 0; i < 5; ++i)
        {
            ObjectMatching.instance.hiddenObject[i].SetActive(names[i]);
        }        
    }
}
