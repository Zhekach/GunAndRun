using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    public void LoadScene(string sceneName)
    {
        if(string.IsNullOrWhiteSpace(sceneName))
            throw new ArgumentException("Scene name can't be null or empty.", nameof(sceneName));
        
        if(Application.CanStreamedLevelBeLoaded(sceneName) == false)
            throw new ArgumentException($"Scene '{sceneName}' is not included in Build Settings.", nameof(sceneName));
        
        SceneManager.LoadScene(sceneName);
    }
}
