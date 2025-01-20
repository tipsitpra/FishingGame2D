using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public bool fishingEpic;
    public  bool fishingLegen;

    public void LoadScenceNextLevel()
    {
        //SaveData.fishingEpic = fishingEpic;
        //SaveData.fishingLegen = fishingLegen;
        SceneManager.LoadSceneAsync("Scene 2");
    }
}
