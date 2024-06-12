using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject sharePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void Settings()
    {
        settingsPanel.GetComponent<Animator>().SetTrigger("Pop");
    }

    public void Share()
    {
        sharePanel.GetComponent<Animator>().SetTrigger("SharePop");
    }

    //used for SHOP button
    public void OpenSite()
    {
        Application.OpenURL("...");
    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/");
    }

    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/");
    }

    public void OpenFacebook()
    {
        Application.OpenURL("https://www.facebook.com/");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
