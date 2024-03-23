using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager2 : MonoBehaviour
{
    public void ToggleSound()
    {        
        bool isSoundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundEnabled", isSoundOn ? 1 : 0);
        
        AudioListener.volume = isSoundOn ? 1.0f : 0.0f;
    }
    
    public void QuitGame()
    {        
        Application.Quit();
    }
}
