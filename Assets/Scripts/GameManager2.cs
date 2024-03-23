using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    private int paperScore = 0;
    private int glassScore = 0;
    private int canScore = 0;
    private int lives = 5;

    public TextMeshProUGUI paperScoreText;
    public TextMeshProUGUI glassScoreText;
    public TextMeshProUGUI canScoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameStatusText;

    public GameObject gameCanvas;
    public Button retryButton;
    public Button exitButton;
    public Button menuButton;
    public Image successImage;
    public Image failureImage;

    private void Start()
    {
        retryButton.onClick.AddListener(RetryGame);
        exitButton.onClick.AddListener(ExitGame);
        menuButton.onClick.AddListener(GoToMenu);

        gameCanvas.SetActive(false);
        successImage.gameObject.SetActive(false);
        failureImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaperWaste"))
        {
            paperScore++;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("GlassWaste"))
        {
            glassScore++;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("KonserveAtik"))
        {
            canScore++;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("engel"))
        {
            lives--;
            Destroy(other.gameObject);
        }

        UpdateScoreAndLivesText();

        if (other.CompareTag("container") && lives > 0)
        {
            CheckGameCompletion();
        }
    }

    public void UpdateScoreAndLivesText()
    {
        paperScoreText.text = "" + paperScore;
        glassScoreText.text = "" + glassScore;
        canScoreText.text = "" + canScore;
        livesText.text = "" + lives;
    }

    public void CheckGameCompletion()
    {
        gameCanvas.SetActive(true);
        Time.timeScale = 0f;

        bool isSuccessful = paperScore >= 10 && glassScore >= 10 && canScore >= 10;

        if (isSuccessful)
        {
            gameStatusText.text = "OYUN BİTTİ\nFARKINDALIĞIN VE ÇABAN SAYESİNDE SOKAKLARIMIZ ÇOK DAHA TEMİZ VE KAYNAKLARIMIZI ÇOK DAHA ETKİN KULLANIYORUZ!\nTEŞEKKÜRLER";
            successImage.gameObject.SetActive(true);
            failureImage.gameObject.SetActive(false);
        }
        else
        {
            gameStatusText.text = "Maalesef görevi tamamlayamadın. Tekrar dene!";
            successImage.gameObject.SetActive(false);
            failureImage.gameObject.SetActive(true);
        }
    }
    public void ChangePaperScore(int amount)
    {
        paperScore += amount;
        UpdateScoreAndLivesText();
    }

    public void ChangeGlassScore(int amount)
    {
        glassScore += amount;
        UpdateScoreAndLivesText();
    }

    public void ChangeCanScore(int amount)
    {
        canScore += amount;
        UpdateScoreAndLivesText();
    }
    public void ChangeHealth(int amount)
    {
        lives += amount;
        lives = Mathf.Max(lives, 0);
        UpdateScoreAndLivesText();

        if (lives <= 0)
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        // Oyunun sonlanma koşullarını kontrol et.
        bool allWasteCollected = paperScore >= 10 && glassScore >= 10 && canScore >= 10;
        bool gameWon = allWasteCollected && lives > 0;

        // Oyun sonu ekranını göster.
        gameCanvas.SetActive(true);
        Time.timeScale = 0f;

        // Butonları ve görselleri duruma göre ayarla.
        retryButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);

        if (gameWon)
        {
            // Oyunu kazanma durumu.
            gameStatusText.text = "BAŞARDIN\nFARKINDALIĞIN VE ÇABAN SAYESİNDE SOKAKLARIMIZ ÇOK DAHA TEMİZ VE KAYNAKLARIMIZI ETKİN KULLANIYORUZ!\nTEŞEKKÜRLER";
            successImage.gameObject.SetActive(true);
            failureImage.gameObject.SetActive(false);
        }
        else
        {
            // Oyunu kaybetme durumu.
            gameStatusText.text = "Maalesef görevi tamamlayamadın. Tekrar dene!";
            successImage.gameObject.SetActive(false);
            failureImage.gameObject.SetActive(true);
        }
    }


    void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }
}

