using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{
    public GameObject storyInfo;

    // Start is called before the first frame update
    void Start()
    {
        storyInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenStory()
    {
        storyInfo.SetActive(true);
    }

    public void CloseStory()
    {
        storyInfo.SetActive(false);
    }
}
