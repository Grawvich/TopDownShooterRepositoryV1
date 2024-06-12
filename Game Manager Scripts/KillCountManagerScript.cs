using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountManagerScript : MonoBehaviour
{

    public static int killCount;

    Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text> ();
        killCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "KillCount: " + killCount;
    }
}
