using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    private ThrowBall ballThrow;
    private BrickManager brickMan;
    public float throwForce = 2.0f;
    public string highScorePlayerName;
    public int highScorePlayerScore;
    public TextMeshProUGUI highscore;
    public string player;
    public TextMeshProUGUI playerName;
    public Button start;
    private bool m_Started = false;
    public int m_Points;   
    private bool m_GameOver = false;
    public int m_Scene = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }
    public void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            playerName = GameObject.FindGameObjectWithTag("PlayerNameInput").GetComponent<TextMeshProUGUI>();
            highscore = GameObject.Find("hscore").GetComponent<TextMeshProUGUI>();
            start = GameObject.Find("startGameButton").GetComponent<Button>();
            start.onClick.AddListener(SetPlayerName);
            m_Scene = 0;
            LoadHighScore();
        }
        if(level == 1)
        {
            m_Started = false;
            ballThrow = GameObject.Find("ThrowBall").GetComponent<ThrowBall>();
            brickMan = GameObject.Find("BrickManager").GetComponent<BrickManager>();
            //playerName = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
            //gameOverText = GameObject.Find("GameoverText");
            m_Scene = 1;
        }
        if(level == 2)
        {
            highscore = GameObject.Find("hscore").GetComponent<TextMeshProUGUI>();
            m_Scene = 2;
            if(highScorePlayerScore < m_Points)
            {
                SaveHighScore();
            }
            LoadHighScore();
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space) && m_Scene == 1)
            {
                m_Started = true;
                ballThrow.StartBall(throwForce);
                brickMan.HidePrompt();
            }
        }
    }
    
    [System.Serializable]
    class SaveData
    {
        public string player;
        public int m_Points;
    }
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.player = player;
        data.m_Points = m_Points;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savedfile.json", json);
    }
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savedfile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScorePlayerName = data.player;
            highScorePlayerScore = data.m_Points;
            highscore.SetText(highScorePlayerName+" : "+highScorePlayerScore);
        }
        else
        {
            highscore.SetText("No high score yet.");
        }
    }
    public void ResetHighscore()
    {
        File.Delete(Application.persistentDataPath + "/savedfile.json");
    }
    public void UpdatePoints(int points)
    {
        m_Points = points;
    }
    public void SetPlayerName()
    {
        player = playerName.text;
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
