using UnityEngine;
using System.IO;

public class DataToSave
{
    public int level;
    public int currency;
    public float completion;
}

public struct Name
{
    public string fileName;
}

public class SaveData : MonoBehaviour
{
    private string saveSlotPath;
    private Name slotName;
    void Awake()
    {
        saveSlotPath = $"{Application.streamingAssetsPath}/SaveSlotName.json";
        CreateSaveSlotFile();
    }

    void CreateSaveSlotFile()
    {
        if (!File.Exists(saveSlotPath))
        {
            File.WriteAllText(saveSlotPath, "Slot1");
        }
    }

    public void SaveChosenSlot(string sN)
    {
        if (File.Exists(saveSlotPath))
        {
            slotName.fileName = sN;
            string lineToSave = JsonUtility.ToJson(slotName);
            File.WriteAllText(saveSlotPath, lineToSave);
        }
    }
    
}
