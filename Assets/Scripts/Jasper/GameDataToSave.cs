using System;
using UnityEngine;
using System.IO;

public class GameDataToSave : MonoBehaviour
{
    public DataToSave gameDataToSave;
    private LoadData _loadSaveSlotName;
    public string _saveSlotPath;

    void OnEnable()
    {
        _loadSaveSlotName = GetComponent<LoadData>();        
    }
    void Start()
    {
        _saveSlotPath = $"{Application.streamingAssetsPath}/{_loadSaveSlotName.saveSlot.fileName}.json";

        Debug.Log(LoadData().currency);
       // SaveGameDataToSaveSlot(gameDataToSave, _saveSlotPath);
    }
    public void SaveGameDataToSaveSlot(DataToSave dataToSave, string pathToSaveTo)
    {
        if (!File.Exists(_saveSlotPath))
        {
            string contentToSave = JsonUtility.ToJson(dataToSave);
            File.WriteAllText(pathToSaveTo, contentToSave);

        }
    }

    DataToSave LoadData()
    {
        string loadedDataAsJson = File.ReadAllText(_saveSlotPath);
        return JsonUtility.FromJson<DataToSave>(loadedDataAsJson);
    }
}
        
[Serializable]
public class DataToSave
{
    public int level;
    public int currency;
    public float completion;
}

