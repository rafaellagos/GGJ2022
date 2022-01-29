using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public bool gameIsPaused;

    [SerializeField] private Main_Controller main_Controller;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject[] ObjectsToDisable;
    [SerializeField] private Toggle hasMusicToggle; 
    [SerializeField] private Slider bgmSlider, fxSlider;
    [SerializeField] private AudioSource bgmSource,fxSource;
    [SerializeField] private GameObject firstSelected;
    [SerializeField] private AudioClip selectButton;


    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(gameIsPaused);
        if (PlayerPrefs.HasKey("hasMusic") == true)
        {
            if (PlayerPrefs.GetInt("hasMusic") == 1)
            {
                UI_MusicToggle(true);
                hasMusicToggle.isOn = true;
            }
            else
            {
                UI_MusicToggle(false);
                hasMusicToggle.isOn = false;
            }
        }
        else
        {    
            UI_MusicToggle(true);
            hasMusicToggle.isOn = true;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")  && main_Controller.GameStarted() == true && main_Controller.GameEnded() == false)
        {
            UI_PauseGame();
        }
    }

    public void UI_PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        pausePanel.SetActive(gameIsPaused);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected);

        if (gameIsPaused == true)
        {         
            Time.timeScale = 0;
        }
        else
        {    
            Time.timeScale = 1;
        }
        if (ObjectsToDisable.Length != 0 )
        {
            for (int i = 0; i < ObjectsToDisable.Length; i++)
            {
                ObjectsToDisable[i].SetActive(!gameIsPaused);
            }
        }
        
        PlaySelectButton();
    }

    public void UI_QuitGame() 
    {
        PlaySelectButton();
        Application.Quit();
    }

    public void UI_MusicToggle(bool newValue) 
    {
        if (newValue == true)
        {
            UI_FXVolumeChange(PlayerPrefs.GetFloat("fxSource"));
            UI_BGMVolumeChange(PlayerPrefs.GetFloat("bgmSource"));
            PlayerPrefs.SetInt("hasMusic", 1);
            fxSlider.interactable = true;
            bgmSlider.interactable = true;           
        }
        else
        {
            fxSource.volume = 0;
            bgmSource.volume = 0;
            PlayerPrefs.SetInt("hasMusic", 0);
            fxSlider.interactable = false;
            bgmSlider.interactable = false;
        }
        PlaySelectButton();

    }

    public void UI_FXVolumeChange(float newValue) 
    {
        fxSource.volume = newValue;
        fxSlider.value = newValue;
        PlayerPrefs.SetFloat("fxSource", newValue);
        PlaySelectButton();
    }

    public void UI_BGMVolumeChange(float newValue)
    {
        bgmSource.volume = newValue;
        bgmSlider.value = newValue;
        PlayerPrefs.SetFloat("bgmSource", newValue);
        PlaySelectButton();
    }

    public void PlaySelectButton() 
    {
        fxSource.PlayOneShot(selectButton);
    }
}
