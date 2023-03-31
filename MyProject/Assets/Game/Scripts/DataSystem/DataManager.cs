using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct DataContent
{
    public float percent;
    public bool isCompleted;
}


public class DataManager : MonoBehaviour
{

    Data m_data;
    string dataString = "DATA";
    public Dictionary<int, DataContent> LevelList = new Dictionary<int, DataContent>();


    public void CreateDataOnClick()
    {
        m_data = new Data();
        for(int i = 0; i < 8; i++)
        {
            DataObject dataObject = new DataObject(0f, false, i);
            string jsonItem = JsonUtility.ToJson(dataObject);
            m_data.DataObjectList.Add(jsonItem);
        }
        string json = JsonUtility.ToJson(m_data);
        Debug.Log(json);
        PlayerPrefs.SetString(dataString, json);
        PlayerPrefs.Save();
    }
    public void ChangeData()
    {
        if (PlayerPrefs.HasKey(dataString))
        {
            string json = PlayerPrefs.GetString(dataString);
            m_data = JsonUtility.FromJson<Data>(json);
        }
        for(int i = 0; i < 8; i++)
        {
            if(i%2 == 0)
            {
                string temp = m_data.DataObjectList[i];
                DataObject m_object = JsonUtility.FromJson<DataObject>(temp);
                m_object.Percent(0.5f);
                string json = JsonUtility.ToJson(m_object);
                m_data.DataObjectList[i] = json;
            }
            if(i % 3 == 0)
            {
                string temp = m_data.DataObjectList[i];
                DataObject m_object = JsonUtility.FromJson<DataObject>(temp);
                m_object.Percent(1f);
                string json = JsonUtility.ToJson(m_object);
                m_data.DataObjectList[i] = json;
            }
        }
        string m_json = JsonUtility.ToJson(m_data);
        PlayerPrefs.SetString(dataString, m_json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(dataString))
        {
            string json = PlayerPrefs.GetString(dataString);
            m_data = JsonUtility.FromJson<Data>(json);

            for (int i = 0; i < 8; i++)
            {
                string m_dataString = m_data.DataObjectList[i];
                DataObject dataObject = JsonUtility.FromJson<DataObject>(m_dataString);

                Debug.Log("ID: " + dataObject.getID() + "  " +
                          "Completed: " + dataObject.Completed() + "  " +
                          "Percent:" + dataObject.getPercent().ToString()
                    );
            }
        }
        else
        {
            Debug.Log("NULL");
        }


    }
}
