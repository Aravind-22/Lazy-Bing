using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerStart : MonoBehaviour
{
    public GameObject HomeScreenPanel;
    public GameObject NewGamePanel;
    public GameObject OptionsPanel;
    public GameObject QuitPanel;
    public GameObject ResetPanel;
    public GameObject InstructionsText;
    public GameObject ControlsText;
    public GameObject OptionsMenu;
    public GameObject InstructionsTextInOptions;
    public GameObject ControlsTextInOptions;
    public GameObject LoadingAnim;
    public bool isLoading = false;
    public static GameManagerStart instance;

    private void Start()
    {
        instance = this;
    }

    public void NewGame()
    {
        HomeScreenPanel.SetActive(false);
        NewGamePanel.SetActive(true);
    }
    public void Options()
    {
        HomeScreenPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }
    public void QuitGame()
    {
        HomeScreenPanel.SetActive(false);
        QuitPanel.SetActive(true);
    }
    public void OnQuitYes()
    {
        Application.Quit();
    }
    public void OnQuitNo()
    {
        HomeScreenPanel.SetActive(true);
        QuitPanel.SetActive(false);
    }
    public void CancelOption()
    {
        HomeScreenPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }
    public void CancelInstruction()
    {
        NewGamePanel.SetActive(false);
        HomeScreenPanel.SetActive(true);
    }
    public void NextButton()
    {
        InstructionsText.SetActive(false);
        ControlsText.SetActive(true);
    }
    public void BackButton()
    {
        InstructionsText.SetActive(true);
        ControlsText.SetActive(false);
    }
    public void Play()
    {
        LoadingAnim.SetActive(true);
        StartCoroutine(LoadGameScene());
    }
    public void OnClickControls()
    {
        ControlsTextInOptions.SetActive(true);
        OptionsMenu.SetActive(false);
    }
    public void OnClickCancelControls()
    {
        ControlsTextInOptions.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void OnClickInstructions()
    {
        InstructionsTextInOptions.SetActive(true);
        OptionsMenu.SetActive(false);
    }
    public void OnClickCancelInstructions()
    {
        InstructionsTextInOptions.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void OnReset()
    {
        ResetPanel.SetActive(true);
    }
    public void onResetYes()
    {
        PlayerPrefs.DeleteAll();
        SaveSystem.Delete();
        ResetPanel.SetActive(false);
    }
    public void onResetNo()
    {
        ResetPanel.SetActive(false);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isLoading = true;
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LoadingAnim.SetActive(false);
    }

}
