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
        if(Manager.m_Points < 10)
        {
            prompt.SetText("You need to work on your game " + Manager.player + "!");
        }
        else if(Manager.m_Points > 10 && Manager.m_Points < 30)
        {
            prompt.SetText("You played well, I know you can do better " + Manager.player + "!");
        }
        else if (Manager.m_Points > 30 && Manager.m_Points < 90)
        {
            prompt.SetText("You played really well, you're getting the hang of this " + Manager.player + "!");
        }
        else if (Manager.m_Points > 90)
        {
            prompt.SetText("You played so incredibly well " + Manager.player + "! WOW!");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
