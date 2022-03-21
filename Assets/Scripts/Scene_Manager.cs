using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    Scene scene;
    string[] ScenesNames = { "CutScene", "Route-1", "Sample_1", "ShedInside" };
    private string loadSceneName;
    private string PreviousScene;
    void Start()
    {
        PreviousScene = SceneGetter();
    }

    public void SceneChanger(string loadSceneName)
    {
       
        if (loadSceneName != SceneGetter())
        {
            SceneManager.LoadScene(loadSceneName);
        }
        else
        {
            SceneManager.LoadScene(PreviousScene);
        }
       
    }

    public string SceneGetter()
    {
       scene= SceneManager.GetActiveScene();

        return scene.name;
    }
}
