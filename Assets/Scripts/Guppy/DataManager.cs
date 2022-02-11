using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    protected static int currentGachaPoint;
    protected static int currentGachaCount;
    protected static bool tutorialMode;

    public static void SetPoint(int point)
    {
        currentGachaPoint = point;
    }

    public static bool UsePoint(int point)
    {
        if(point > currentGachaPoint)
        {
            return false;
        }

        currentGachaPoint -= point;

        return true;
    }

    public static void AddPoint(int point)
    {
        currentGachaPoint += point;
    }

    public static int GetPoint()
    {
        return currentGachaPoint;
    }

    public static void SetGachaCount(int count)
    {
        currentGachaCount = count;
    }

    public static void AddGachaCount(int count)
    {
        currentGachaCount += count;
    }

    public static int GetGachaCount()
    {
        return currentGachaCount;
    }

    public static void SetTutorialMode(bool state)
    {
        tutorialMode = state;
    }

    public static bool IsTutorialMode()
    {
        return tutorialMode;
    }
}
