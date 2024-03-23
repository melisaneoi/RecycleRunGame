using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button level1Button; 
    public Button level2Button; 

    void Start()
    {
        
        level1Button.interactable = true;

        level2Button.interactable = true;
    }

    public void ToggleSound()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

    public void BackToOpeningScene()
    {
        SceneManager.LoadScene("OpeningScene");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
}
