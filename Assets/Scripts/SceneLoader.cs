using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Music music;

    public void LoadByIndex(int scene_index)
    {
        SceneManager.LoadScene(scene_index);
    }

    public void LoadGame()
    {
        music.StartGame();
        LoadByIndex(1);
    }
}
