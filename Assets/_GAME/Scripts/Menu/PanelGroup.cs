﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour
{

    public GameObject[] panels;

    public TabGroup tabGroup;

    public int panelIndex;

    private void Awake()
    {
        ShowCurrentPanel();
    }

    public void ShowCurrentPanel()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == panelIndex ? true : false);
        }
    }

    public void SetPageIndex(int index)
    {
        panelIndex = index;
        ShowCurrentPanel();
    }
}