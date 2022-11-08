using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        GameScene,
        Loading,
        MainMenu,
        GameOver,
    }
    private static Action onLoaderCallback;
    public static void Load(Scene scene)
    {
        Debug.Log(scene.ToString());  
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };
        SceneManager.LoadScene(Scene.Loading.ToString());
    }
    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
