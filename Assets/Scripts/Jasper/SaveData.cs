using UnityEngine;
using System.IO;
using System;

[Serializable]
public struct Name
{
    public string fileName;
}

// save slot data
public class SaveData : MonoBehaviour
{
    private string saveSlotPath;
    private Name slotName;
    void Awake()
    {
        saveSlotPath = $"{Application.streamingAssetsPath}/SaveSlotName.json";
        //CreateSaveSlotFile();
    }

    //void CreateSaveSlotFile()
    //{
    //    if (!File.Exists(saveSlotPath))
    //    {
    //        File.WriteAllText(saveSlotPath, "Slot1");
    //    }
    //}

    public void SaveChosenSlot(string sN)
    {
        //if theres a file already at the save slots path
        if (File.Exists(saveSlotPath))
        {
            //
            slotName.fileName = sN;
            //
            string lineToSave = JsonUtility.ToJson(slotName);
            //write the save slot path and the json?????????
            File.WriteAllText(saveSlotPath, lineToSave);
        }
    }
}

#region Pseudocode
// if slot 1 is clicked
    // create file name = slot1
    // set last loaded save

#endregion