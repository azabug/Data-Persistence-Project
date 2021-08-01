using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickManager : MonoBehaviour
{
    public MainManager Manager;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Text ScoreText;
    public Text LevelText;
    public GameObject PlayerPrompt;
    private int m_Points;

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("MainManager").GetComponent<MainManager>();
        ScoreText.text = $"Score : {Manager.m_Points}";
        LevelText.text = $"Level : {Manager.gameLevel}";
        SetUpBricks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }
    void AddPoint(int point)
    {
        m_Points = Manager.m_Points;
        m_Points += point;
        Manager.UpdatePoints(m_Points);
        ScoreText.text = $"Score : {Manager.m_Points}";
        LevelText.text = $"Level : {Manager.gameLevel}";
    }
    public void ShowPrompt()
    {
        PlayerPrompt.SetActive(true);
    }
    public void HidePrompt()
    {
        PlayerPrompt.SetActive(false);
    }
}
