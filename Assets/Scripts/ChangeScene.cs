using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    string selectedScene = "Level1";
    public void ChangeSceneByName(string sceneName)
    {
        Debug.Log("Scene changed");
        //connect loading scenes in Unity Engine using their name
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeSceneByName()
    {
        Debug.Log("Scene changed");
        //connect loading scenes in Unity Engine using their name
        SceneManager.LoadScene(selectedScene);
    }
    public void SceneIndexDrop(int drop)
    {
        string sceneName = "";
        switch (drop)
        {
            case 0:
                sceneName = "Level1";
                break;
            case 1:
                sceneName = "Level2";
                break;
            case 2:
                sceneName = "Level3";
                break;
            default:
                sceneName = "Level1";
                break;
        }
        selectedScene = sceneName;

    }
    //public void LoadSaveYes()
    //{
    //    SaveAndLoad.loadActive = true;
    //}

    //public void ReturnToLastScene(string lastSceneName)
    //{
    //    lastSceneName = SceneManager.GetActiveScene().name;

    //    SceneManager.LoadScene(lastSceneName);
    //}

    public void QuitToDesktop()
    {
        //end play mode when using Unity Engine form
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
#else
        //otherwise quit application when using application form
        Application.Quit();
#endif
    }
}
