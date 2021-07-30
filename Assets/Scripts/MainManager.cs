using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    //public Brick BrickPrefab;
    //public int LineCount = 6;
    //public Rigidbody Ball;
    private ThrowBall ballThrow;
    public float throwForce = 2.0f;
    public string player = "";
    public TextMeshProUGUI playerName;
    public Button start;

    //public Text ScoreText;
    //private GameObject gameOverText;

    private bool m_Started = false;
    public int m_Points;
    
    private bool m_GameOver = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //LoadColor();
        
    }
    public void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            playerName = GameObject.FindGameObjectWithTag("PlayerNameInput").GetComponent<TextMeshProUGUI>();
            start = GameObject.Find("startGameButton").GetComponent<Button>();
            start.onClick.AddListener(SetPlayerName);
            
        }
        if(level == 1)
        {
            m_Started = false;
            ballThrow = GameObject.Find("ThrowBall").GetComponent<ThrowBall>();
            //playerName = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
            //gameOverText = GameObject.Find("GameoverText");
        }
        if(level == 2)
        {

        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                
                ballThrow.StartBall(throwForce);
                //float randomDirection = Random.Range(-1.0f, 1.0f);
                //Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                //forceDir.Normalize();

                //Ball.transform.SetParent(null);
                //Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        /*
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
        */
    }

    //void AddPoint(int point)
    //{
    //    m_Points += point;
    //    ScoreText.text = $"Score : {m_Points}";
    //}

    //public void GameStart()
    //{
    //    const float step = 0.6f;
    //    int perLine = Mathf.FloorToInt(4.0f / step);

    //    int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
    //    for (int i = 0; i < LineCount; ++i)
    //    {
    //        for (int x = 0; x < perLine; ++x)
    //        {
    //            Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
    //            var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
    //            brick.PointValue = pointCountArray[i];
    //            brick.onDestroyed.AddListener(AddPoint);
    //        }
    //    }
    //}
    public void UpdatePoints(int points)
    {
        m_Points = points;
    }
    public void SetPlayerName()
    {
        player = playerName.text.ToString();
        Debug.Log(player);
    }
    public void GamePause()
    {

    }
    public void GameOver()
    {
        m_GameOver = true;
        SceneManager.LoadScene(2);
    }
}
