using UnityEngine;
using System.IO;

public class LoadData : MonoBehaviour
{
    public Name saveSlot;
    public string saveSlotPath;

    void Start()
    {
        Debug.Log(ReadSaveSlotName());
    }

    public string ReadSaveSlotName()
    {
        saveSlotPath = $"{Application.streamingAssetsPath}/SaveSlotName.json";

        if (File.Exists(saveSlotPath))
        {
            string loadedData = File.ReadAllText(saveSlotPath);
            saveSlot = JsonUtility.FromJson<Name>(loadedData);
            return saveSlot.fileName;
        }
        return "";
    }
}
