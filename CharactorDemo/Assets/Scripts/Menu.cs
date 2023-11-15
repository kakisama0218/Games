using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    private bool isPaused=false;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&!isPaused)
        {
            Debug.Log(Input.GetKeyDown(KeyCode.Escape) && !isPaused);
            PauseGame();
            isPaused = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
            isPaused = false;
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseLoad()
    {
        pauseMenu.SetActive(true);
        Invoke("PauseGame", 0.5f);
    }
    public void PauseGame()
    {
        //pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
    public void BackMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
}
