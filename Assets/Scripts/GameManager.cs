using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int can = 5;

    public GameObject gameCanvas;
    public TextMeshProUGUI gameStatusText;
    public Button retryButton;
    public Button levelSelectButton;
    public Button nextLevelButton;
    public Text scoreText;
    public Text canText;
    public Image successImage;
    public Image failureImage;

    private void Start()
    {
        retryButton.onClick.AddListener(RetryGame);
        levelSelectButton.onClick.AddListener(GoToLevelSelect);
        nextLevelButton.onClick.AddListener(GoToNextLevel);

        gameCanvas.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        successImage.gameObject.SetActive(false);
        failureImage.gameObject.SetActive(false);

        UpdateScoreAndCanText();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.name);
        if (other.tag == "OrganicWaste")
        {
            score++;
            Destroy(other.gameObject);
        }
        else if (other.tag == "engel")
        {
            can--;
            Destroy(other.gameObject);
        }
        if (other.tag == "container")
        {
            gameCanvas.SetActive(true);
            Time.timeScale = 0f; 

            if (score >= 30)
            {
                gameStatusText.text = "TEBRİKLER \n ARTIK SOKAKLARIMIZ DAHA TEMİZ \n 2. SEVİYEYE GEÇEBİLİRSİN.";
                successImage.gameObject.SetActive(true);
                failureImage.gameObject.SetActive(false);
                PlayerPrefs.SetInt("Level1Completed", 1);
                PlayerPrefs.Save();
                nextLevelButton.gameObject.SetActive(true);
            }
            else
            {
                gameStatusText.text = "MALESEF SOKAĞIMIZ YETERİNCE TEMİZ DEĞİL. \n TEKRAR DENE.";
                successImage.gameObject.SetActive(false);
                failureImage.gameObject.SetActive(true); 
            }
        }

        if (can <= 0)
        {
            gameStatusText.text = "BAŞARAMADIN. \n YETERİNCE DİKKATLİ DEĞİLDİN.";
            failureImage.gameObject.SetActive(true);
        }

        UpdateScoreAndCanText();
    }

    public void UpdateScoreAndCanText()
    {
        scoreText.text = ""+ score;
        canText.text = "" + can;

    }
    public void ChangeScore(int amount)
    {
        score += amount;
        UpdateScoreAndCanText();
    }

    public void ChangeHealth(int amount)
    {
        can += amount;
        can = Mathf.Max(can, 0);
        UpdateScoreAndCanText();

        if (can <= 0)
        {
            EndGame();
        }
    }


    void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GoToLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }

    void GoToNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }
    public void EndGame()
    {
        gameCanvas.SetActive(true);
        Time.timeScale = 0f;

        bool hasWon = score >= 30;
        bool hasLost = can <= 0;

        if (hasWon)
        {
            gameStatusText.text = "TEBRİKLER \n ARTIK SOKAKLARIMIZ DAHA TEMİZ \n 2. SEVİYEYE GEÇEBİLİRSİN.";
            successImage.gameObject.SetActive(true);
            failureImage.gameObject.SetActive(false);
            nextLevelButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            levelSelectButton.gameObject.SetActive(true);
        }
        else if (hasLost && !hasWon)
        {
            gameStatusText.text = "BAŞARAMADIN. \n YETERİNCE DİKKATLİ DEĞİLDİN.";
            successImage.gameObject.SetActive(false);
            failureImage.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
            levelSelectButton.gameObject.SetActive(true);
        }
        else
        {
            gameStatusText.text = "MALESEF SOKAĞIMIZ YETERİNCE TEMİZ DEĞİL. \n TEKRAR DENE.";
            successImage.gameObject.SetActive(false);
            failureImage.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
            levelSelectButton.gameObject.SetActive(true);
        }
    }
}
