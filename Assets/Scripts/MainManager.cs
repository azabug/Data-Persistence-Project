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
    public GameObject settingsPanel;
    public int hits = 0;
    public int gameLevel = 1;
    public float throwForce = 2.0f;
    public TextMeshProUGUI throwForceText;
    public float paddleSize = 1.0f;
    public GameObject paddle;
    public GameObject paddleSizeImage;
    private Vector3 scaleChange;
    public string highScorePlayers;
    public TextMeshProUGUI highscore;
    public string player = "Player One";
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerSelectedName;
    public TextMeshProUGUI prompt;
    public TextMeshProUGUI prompt2;
    public string[] playerList;
    public int[] playerPointsList;
    public Button enterName;
    public Button speedPlusButton;
    public Button speedMinusButton;
    public Button paddlePlusButton;
    public Button paddleMinusButton;
    private bool m_Started = false;
    public int m_Points;   
    //private bool m_GameOver = false;
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
            prompt = GameObject.Find("Prompt").GetComponent<TextMeshProUGUI>();
            prompt.SetText("Enter a new player name");
            prompt2 = GameObject.Find("Prompt2").GetComponent<TextMeshProUGUI>();
            prompt2.SetText("Or keep playing.");
            playerSelectedName = GameObject.Find("SelectedName").GetComponent<TextMeshProUGUI>();
            if(player != "" && player != null)
            {
                playerSelectedName.text = player.ToString();
            }
            highscore = GameObject.Find("hscore").GetComponent<TextMeshProUGUI>();
            enterName = GameObject.Find("InputNameButton").GetComponent<Button>();
            //playerName.SetText(player.ToString());
            enterName.onClick.AddListener(SetPlayerName);
            
            m_Scene = 0;
            m_Points = 0;
            gameLevel = 1;
            LoadHighScore();
        }
        if(level == 1)
        {
            m_Started = false;
            hits = 0;
            ballThrow = GameObject.Find("ThrowBall").GetComponent<ThrowBall>();
            brickMan = GameObject.Find("BrickManager").GetComponent<BrickManager>();
            highscore = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
            highscore.SetText("High Score - "+playerList[0]+" : "+playerPointsList[0]);
            CheckHighestScore(false);
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
    public void CheckHighestScore(bool isHit)
    {
        if (isHit) { hits += 1; }
        if (m_Points > playerPointsList[0])
        {
            highscore.SetText("High Score - "+player+" : "+m_Points);
        }
        if (hits == 36)
        {
            gameLevel += 1;
            SceneManager.LoadScene(1);
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
        else if (playerPointsList[0] > m_Points && playerPointsList[1] < m_Points || playerPointsList[0] == m_Points)
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
        else if (playerPointsList[1] > m_Points && playerPointsList[2] < m_Points || playerPointsList[1] == m_Points)
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
        else if (playerPointsList[2] > m_Points && playerPointsList[3] < m_Points || playerPointsList[2] == m_Points)
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
        else if (playerPointsList[3] > m_Points && playerPointsList[4] < m_Points || playerPointsList[3] == m_Points)
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
        prompt2.SetText("New player selected.");
        playerSelectedName.SetText("Player : "+player);
    }
    public void LoadLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }
    public void OpenSettings()
    {
        Vector3 imageScale = new Vector3(paddleSize, 1, 1);
        throwForceText = GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>();
        throwForceText.SetText(throwForce.ToString());
        paddleSizeImage = GameObject.Find("PaddleImg");
        paddleSizeImage.transform.localScale = imageScale;
        speedPlusButton = GameObject.Find("SpeedPlusButton").GetComponent<Button>();
        speedMinusButton = GameObject.Find("SpeedMinusButton").GetComponent<Button>();
        paddlePlusButton = GameObject.Find("PaddlePlusButton").GetComponent<Button>();
        paddleMinusButton = GameObject.Find("PaddleMinusButton").GetComponent<Button>();
        speedPlusButton.onClick.AddListener(AdjustSpeedPlus);
        speedMinusButton.onClick.AddListener(AdjustSpeedMinus);
        paddlePlusButton.onClick.AddListener(AdjustPaddleSizePlus);
        paddleMinusButton.onClick.AddListener(AdjustPaddleSizeMinus);
    }
    public void AdjustSpeedPlus()
    {
        if(throwForce < 5.0f)
        {
            throwForce += 0.1f;
            throwForceText.SetText(throwForce.ToString());
        }
    }
    public void AdjustSpeedMinus()
    {
        if (throwForce > 2.0f)
        {
            throwForce -= 0.1f;
            throwForceText.SetText(throwForce.ToString());
        }
    }
    public void AdjustPaddleSizePlus()
    {
        if (paddleSize < 1.0f)
        {
            paddleSize += 0.1f;
            scaleChange = new Vector3(0.1f, 0.0f, 0.0f);
            paddleSizeImage.transform.localScale += scaleChange;
        }
    }
    public void AdjustPaddleSizeMinus()
    {
        if (paddleSize > 0.4f)
        {
            paddleSize -= 0.1f;
            scaleChange = new Vector3(0.1f, 0.0f, 0.0f);
            paddleSizeImage.transform.localScale -= scaleChange;
        }
    }
    public void GamePause()
    {

    }
    public void GameOver()
    {
        hits = 0;
        //m_GameOver = true;
        m_Started = false;
        SceneManager.LoadScene(2);
    }
}
