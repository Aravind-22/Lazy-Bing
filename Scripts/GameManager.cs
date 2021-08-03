using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameScreePanel;
    public GameObject pauseScreenPanel;
    public GameObject Quit;
    public GameObject Controls;
    public GameObject SavedProgressPanel;
    public static GameManager instance;
    public bool onPause = false;
    public ParticleSystem rain;
    public ParticleSystem fog;
    public AudioClip raining;
    public AudioClip breeze;
    public AudioClip thunder;
    public AudioSource atmos;
    bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        instance = this;
        atmos = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRunning)
            StartCoroutine(callAtmosphere());
        if (GameManagerStart.instance.isLoading)
            SavedProgress();
    }

    public void onCancelPause()
    {
        onPause = false;
        Time.timeScale = 1;
        gameScreePanel.SetActive(true);
        pauseScreenPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerMovement.instance.playerWalk.UnPause();
    }
    public void onEscape()
    {
        onPause = true;
        Time.timeScale = 0;
        gameScreePanel.SetActive(false);
        pauseScreenPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerMovement.instance.playerWalk.Pause();
    }
    public void OnPauseQuit()
    {
        Quit.SetActive(true);
    }
    public void OnHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void onPauseControls()
    {
        pauseScreenPanel.SetActive(false);
        Controls.SetActive(true);
    }
    public void OnQuitYes()
    {
        Application.Quit();
    }
    public void OnQuitNo()
    {
        pauseScreenPanel.SetActive(true);
        Quit.SetActive(false);
    }
    public void OnClickCancelControls()
    {
        Controls.SetActive(false);
        pauseScreenPanel.SetActive(true);
    }
    public void Mute()
    {
        CameraMovement.instance.MuteGame();
    }
    public void UnMute()
    {
        CameraMovement.instance.UnMuteGame();
    }
    public void SaveGame()
    {
       TaskManager.instance.Save();
       //PlayerMovement.instance.Save();
       //TimeManager.instance.Save();
    }
    public void LoadGame()
    {
        TaskManager.instance.Load();
        onCancelPause();
        TimeManager.instance.RestartPanel.SetActive(false);
        //TimeManager.instance.timer = data.timer;

        //Vector3 pos = PlayerMovement.instance.transform.position;
        //pos.x = data.position[0];
        //pos.y = data.position[1];
        //pos.z = data.position[2];
        //PlayerMovement.instance.transform.position = pos;
    }

    public void SavedProgress()
    {
        SavedProgressPanel.SetActive(true);
        onPause = true;
        Time.timeScale = 0;
        gameScreePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerMovement.instance.playerWalk.Pause();
    }
    public void LoadProgress()
    {
        GameManagerStart.instance.isLoading = false;
        SavedProgressPanel.SetActive(false);
        onCancelPause();
        TaskManager.instance.Load();        
        
    }
    IEnumerator callAtmosphere()
    {
        isRunning = true;
        yield return new WaitForSeconds(300);//wait for 5 min
        atmos.PlayOneShot(thunder);//thunder sound
        yield return new WaitForSeconds(10);//wait for 5 sec
        atmos.PlayOneShot(raining);//play raining sound
        rain.Play();//rain particle
        yield return new WaitForSeconds(65);
        rain.Stop();
        atmos.Stop();
        atmos.PlayOneShot(breeze);//play breeze sound
        fog.Play();//fog particle
        yield return new WaitForSeconds(45);
        fog.Stop();
        atmos.Stop();
        yield return new WaitForSeconds(300);//wait for 5 min
        isRunning = false;
    }
}
