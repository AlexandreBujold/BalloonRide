using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public bool changeSprites = false;
    public TabSprites tabSprites;

    public bool changeColors = true;
    public TabColors tabColors;

    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;

    public PanelGroup panelGroup;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (null == selectedTab || button != selectedTab)
        {
            button.background.sprite = changeSprites == true ? tabSprites.tabHover : button.background.sprite;
            button.background.color = changeColors == true ? tabColors.tabHover : button.background.color;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if (null != selectedTab)
        {
            selectedTab.Deselect();
        }
        selectedTab = button;
        selectedTab.Select();

        ResetTabs();

        button.background.sprite = changeSprites == true ? tabSprites.tabActive : button.background.sprite;
        button.background.color = changeColors == true ? tabColors.tabActive : button.background.color;

        int index = button.transform.GetSiblingIndex();
        //for(int i = 0; i < objectsToSwap.Count; i++)
        //{
        //    objectsToSwap[i].SetActive(index == i ? true : false);
        //}

        if (null != panelGroup)
        {
            panelGroup.SetPageIndex(index);
        }
    }

    public void ResetTabs()
    {
        if (tabButtons == null)
        {
            return;
        }

        foreach(TabButton button in tabButtons)
        {
            if (null != selectedTab && button == selectedTab) { continue; }
            button.background.sprite = changeSprites == true ? tabSprites.tabIdle : button.background.sprite;
            button.background.color = changeColors == true ? tabColors.tabIdle : button.background.color;
        }
    }

    [System.Serializable]
    public struct TabSprites
    {
        public Sprite tabIdle;
        public Sprite tabHover;
        public Sprite tabActive;
    }

    [System.Serializable]
    public struct TabColors
    {
        public Color tabIdle;
        public Color tabHover;
        public Color tabActive;
    }

}
