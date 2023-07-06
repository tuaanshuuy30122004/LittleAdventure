using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager GameManager;
    public Slider HealthSlider;
    public GameObject PausePanel;
    public GameObject GameoverPanel;
    public GameObject FinishPanel;

    private void Start()
    {
        PausePanel.SetActive(false);
        GameoverPanel.SetActive(false);
        FinishPanel.SetActive(false);
    }
    void Update()
    {
        HealthSlider.value = GameManager.player.GetComponent<Health>().healthPercent;
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PausePanel.SetActive(false );
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
