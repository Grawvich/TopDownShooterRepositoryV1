﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    //public Transform mapSelectPanel;
    private int currentMap;

    private void Awake()
    {
        SelectMap(0);
    }

    public void SelectMap(int _index)
    {
        previousButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount - 1);

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i ==_index);
        }
    }

    public void ChangeMap(int _change)
    {
        currentMap += _change;
        SelectMap(currentMap);
    }
}