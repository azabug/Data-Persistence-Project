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
    public string highScorePlayers;
    public TextMeshProUGUI highscore;
    public string player;
    public TextMeshProUGUI playerName;
    public string[] playerList;
    public int[] playerPointsList;
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
            m_Scene = 1;
        }
        if(level == 2)
        {
            highscore = GameObject.Find("hscore").GetComponent<TextMeshProUGUI>();
            m_Scene = 2;
            CompileScores();
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
        public string playerOne;
        public string playerTwo;
        public string playerThree;
        public string playerFour;
        public string playerFive;

        public int m_pointsOne;
        public int m_pointsTwo;
        public int m_pointsThree;
        public int m_pointsFour;
        public int m_pointsFive;
    }
    public void CompileScores()
    {
        int[] pointsList = {0, 0, 0, 0, 0};
        string[] playList = {"", "", "", "", ""};
        bool isHighScore = false;

        if (playerPointsList[0] < m_Points)
        {
            pointsList[0] = m_Points;
            pointsList[1] = playerPointsList[0];
            pointsList[2] = playerPointsList[1];
            pointsList[3] = playerPointsList[2];
            pointsList[4] = playerPointsList[3];
            playList[0] = player;
            playList[1] = playerList[0];
            playList[2] = playerList[1];
            playList[3] = playerList[2];
            playList[4] = playerList[3];
            isHighScore = true;
        }
        else if (playerPointsList[0] > m_Points && playerPointsList[1] < m_Points)
        {
            pointsList[0] = playerPointsList[0];
            pointsList[1] = m_Points;
            pointsList[2] = playerPointsList[1];
            pointsList[3] = playerPointsList[2];
            pointsList[4] = playerPointsList[3];
            playList[0] = playerList[0];
            playList[1] = player;
            playList[2] = playerList[1];
            playList[3] = playerList[2];
            playList[4] = playerList[3];
            isHighScore = true;
        }
        else if (playerPointsList[1] > m_Points && playerPointsList[2] < m_Points)
        {
            pointsList[0] = playerPointsList[0];
            pointsList[1] = playerPointsList[1];
            pointsList[2] = m_Points;
            pointsList[3] = playerPointsList[2];
            pointsList[4] = playerPointsList[3];
            playList[0] = playerList[0];
            playList[1] = playerList[1];
            playList[2] = player;
            playList[3] = playerList[2];
            playList[4] = playerList[3];
            isHighScore = true;
        }
        else if (playerPointsList[2] > m_Points && playerPointsList[3] < m_Points)
        {
            pointsList[0] = playerPointsList[0];
            pointsList[1] = playerPointsList[1];
            pointsList[2] = playerPointsList[2];
            pointsList[3] = m_Points;
            pointsList[4] = playerPointsList[3];
            playList[0] = playerList[0];
            playList[1] = playerList[1];
            playList[2] = playerList[2];
            playList[3] = player;
            playList[4] = playerList[3];
            isHighScore = true;
        }
        else if (playerPointsList[3] > m_Points && playerPointsList[4] < m_Points)
        {
            pointsList[0] = playerPointsList[0];
            pointsList[1] = playerPointsList[1];
            pointsList[2] = playerPointsList[2];
            pointsList[3] = playerPointsList[3];
            pointsList[4] = m_Points;
            playList[0] = playerList[0];
            playList[1] = playerList[1];
            playList[2] = playerList[2];
            playList[3] = playerList[3];
            playList[4] = player;
            isHighScore = true;
        }
        else
        {
            isHighScore = false;
        }
        if(isHighScore)
        {
            playerList = playList;
            playerPointsList = pointsList;
            SaveHighScore();
        }
        
    }
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        
        data.playerOne = playerList[0];
        data.playerTwo = playerList[1];
        data.playerThree = playerList[2];
        data.playerFour = playerList[3];
        data.playerFive = playerList[4];

        data.m_pointsOne = playerPointsList[0];
        data.m_pointsTwo = playerPointsList[1];
        data.m_pointsThree = playerPointsList[2];
        data.m_pointsFour = playerPointsList[3];
        data.m_pointsFive = playerPointsList[4];

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
            playerList[0] = data.playerOne;
            playerList[1] = data.playerTwo;
            playerList[2] = data.playerThree;
            playerList[3] = data.playerFour;
            playerList[4] = data.playerFive;
            playerPointsList[0] = data.m_pointsOne;
            playerPointsList[1] = data.m_pointsTwo;
            playerPointsList[2] = data.m_pointsThree;
            playerPointsList[3] = data.m_pointsFour;
            playerPointsList[4] = data.m_pointsFive;

            highScorePlayers = playerList[0] + " : " + playerPointsList[0] + "\n" + playerList[1] + " : " + playerPointsList[1] + "\n" + playerList[2] + " : " + playerPointsList[2] + "\n" + playerList[3] + " : " + playerPointsList[3] + "\n" + playerList[4] + " : " + playerPointsList[4] + "\n";
            highscore.SetText(highScorePlayers);
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
