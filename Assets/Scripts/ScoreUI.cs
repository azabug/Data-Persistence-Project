using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public MainManager Manager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI prompt;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("MainManager").GetComponent<MainManager>();
        scoreText.SetText("You scored "+Manager.m_Points+" Points.");
        prompt.SetText("You played well "+Manager.player+"!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
