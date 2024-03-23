using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Ses açma/kapama için ses butonu fonksiyonu
    public void ToggleSound()
    {
        // Ses durumunu kontrol et ve ona göre ayarla
        // Burada PlayerPrefs kullanıyoruz ancak ses yönetimi için daha gelişmiş bir sistem tercih edilebilir
        bool isSoundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundEnabled", isSoundOn ? 1 : 0);

        // Ses ayarını uygula
        AudioListener.volume = isSoundOn ? 1.0f : 0.0f;
    }

    // Çıkış butonu fonksiyonu
    public void QuitGame()
    {
        // Oyunu kapat
        Application.Quit();
    }

    // Oyna butonu fonksiyonu
    public void PlayGame()
    {
        // Seviye seçim ekranına geçiş
        // "LevelSelect" sahne ismi örnek olarak kullanılmıştır, gerçek sahne isminizle değiştirin
        SceneManager.LoadScene("LevelSelect");
    }
}
