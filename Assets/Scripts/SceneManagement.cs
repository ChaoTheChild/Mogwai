using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{   
    public static SceneManagement instance;
    public delegate void OnSceneChange();
    public static event OnSceneChange GameSceneLoad;


    void Awake(){
        if(instance == null){
            instance = this;
             DontDestroyOnLoad(this);
        }else{
            Destroy(this);
        }

    }
    public void SceneTransition(){
        SceneManager.LoadScene("GameScene");
        GameSceneLoad();
    }

    public static string GetCurrentSceneName(){
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        return sceneName;

    }
}
