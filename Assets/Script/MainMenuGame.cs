using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
