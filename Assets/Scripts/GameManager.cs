using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public Image H1, H2, H3;
    public TextMeshProUGUI Timer, Score;
    public GameObject obst;
    public GameObject coin;

    

    [Header("Obstacles")]
    public float TimeToSpwan = 1;
    public float SpwnnerTime;

    [Header("Coin")]
    public float timeToSpwanCoin = 2;
    public float CoinSpwnner;

    [HideInInspector] public int maxHealth = 3;
    [HideInInspector] public int currentHealth = 3;
    [HideInInspector] public int ScorePoint = 0;
    [HideInInspector] public bool gameOver = false;

    [Header("SpwnBoounds")]
    [SerializeField] private Vector2 Y_axis;
    [SerializeField] private Vector2 X_axis;

    [Header("Game Over")]
    public GameObject GameOverPanal;
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI BestTime;

    private float startTime;
    void Start()
    {
        manager = this;
        currentHealth = maxHealth;
        startTime = Time.time;
        GameOverPanal.SetActive(false);
    }

    void Update()
    {
        HealthIndicator();
        timer();
        ObsSpwanner();
        CoinSpwanner();
        GameOver();
        Score.text = ScorePoint.ToString();
    }
    void timer()
    {
        float t = Time.time - startTime;
        string min = ((int)t / 60).ToString();
        string sec = (t % 60).ToString("f2");
        Timer.text = min + " : " + sec;
    }

    void HealthIndicator()
    {
        if (currentHealth == 3)
        {
            H1.gameObject.SetActive(true);
            H2.gameObject.SetActive(true);
            H3.gameObject.SetActive(true);
        }
        if (currentHealth == 2)
        {
            H1.gameObject.SetActive(true);
            H2.gameObject.SetActive(true);
            H3.gameObject.SetActive(false);
        }
        if (currentHealth == 1)
        {
            H1.gameObject.SetActive(true);
            H2.gameObject.SetActive(false);
        }
        if (currentHealth == 0)
        {
            H1.gameObject.SetActive(false);
        }
    }

    public void ObsSpwanner()
    {
        if (!gameOver)
        {
            SpwnnerTime -= Time.deltaTime;

            float Y = Random.Range(Y_axis.x, Y_axis.y);
            float X = Random.Range(X_axis.x, X_axis.y);

            if (SpwnnerTime <= 0)
            {
                SpwnnerTime = TimeToSpwan;
                Instantiate(obst, new Vector2(X, Y), Quaternion.identity).transform.parent = transform;
            }
        }
       
    }
    public void CoinSpwanner()
    {
        if (!gameOver)
        {
            CoinSpwnner -= Time.deltaTime;

            float Y = Random.Range(Y_axis.x, Y_axis.y);
            float X = Random.Range(X_axis.x, X_axis.y);

            if (CoinSpwnner <= 0)
            {
                CoinSpwnner = timeToSpwanCoin;
                Instantiate(coin, new Vector2(X, Y), Quaternion.identity).transform.parent = transform;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void GameOver()
    {
        if (currentHealth <= 0)
        {
            if (!gameOver)
            {
                HighScore.text = "Best Score is : "+ScorePoint.ToString();
                BestTime.text = "Best Time is : " + Timer.text;
            }
            gameOver = true;

            GameOverPanal.SetActive(true);
            Timer.gameObject.SetActive(false);
            Score.gameObject.SetActive(false);
            FindObjectOfType<BackgroundMovement>().enabled = false;
            FindObjectOfType<playerMovement>().anime.SetBool("die", true);
            FindObjectOfType<playerMovement>().upForce = 0;
            FindObjectOfType<playerMovement>().gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1.4f, 1.4f);
            FindObjectOfType<playerMovement>().gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.2f, -0.3f);
        }
    }
}
