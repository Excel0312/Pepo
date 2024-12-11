using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    TextMeshProUGUI livesText;

    [SerializeField]
    bool initLive = false;

    [SerializeField]
    GameObject GameOver;

    void Start()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    void ResetLives()
    {
        PlayerPrefs.SetInt("Lives", 3);
    }

    public void ReduceLives()
    {
        int lives = PlayerPrefs.GetInt("Lives");
        lives = lives -1;
        PlayerPrefs.SetInt("Lives", lives);

        if(lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else 
        {
            //Game over Screen
            GameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void AddLives(){
        int lives = PlayerPrefs.GetInt("Lives");
        if(lives > 0){
            lives = lives +1;
            PlayerPrefs.SetInt ("Lives", lives);
        }
    }

    public void Retry()
    {
        Time.timeScale = 1;
        ResetLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
         Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    void Update()
    {
        if (initLive == true)
        {
            ResetLives();
            initLive = false;
        }

        livesText.text = "Lives = " + PlayerPrefs.GetInt("Lives");
    }
}
