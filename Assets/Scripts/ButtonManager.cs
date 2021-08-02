
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public MainManager mainMan;

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    //settings//
    public void OpenSettings()
    {
        mainMan = GameObject.Find("MainManager").GetComponent<MainManager>();
        settingsPanel.SetActive(true);
        mainMan.OpenSettings();
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void ExitApp()
    {
        mainMan = GameObject.Find("MainManager").GetComponent<MainManager>();
        mainMan.Exit();
    }
}
