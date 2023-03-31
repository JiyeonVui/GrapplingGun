using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class DataObject
{
    public float percent;
    public int ID;
    public bool IsCompleted = false;

    public DataObject(float percent, bool IsCompleted,int ID)
    {
        this.percent = percent;
        this.IsCompleted = IsCompleted;
        this.ID = ID;
    }
    public bool Completed()
    {
        return this.IsCompleted;
    }
    public void Percent(float percent)
    {
        this.percent = percent;
        if(percent >= 1)
        {
            IsCompleted = true;
        }
    }
    public int getID()
    {
        return this.ID;
    }
    public float getPercent() { return this.percent; }

}

public class Data
{
    public int currentLevel;
    public bool isTutorials = true;
    public List<string> DataObjectList;

    public Data()
    {
        this.currentLevel = 0;
        this.isTutorials = true;
        this.DataObjectList = new List<string>();
    }
}
