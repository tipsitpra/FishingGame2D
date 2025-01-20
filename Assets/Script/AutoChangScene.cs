using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoChangScene : MonoBehaviour
{
    public string scenename;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeSceneTest", 4);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeSceneTest()
    {
        SceneManager.LoadScene(scenename);
    }
}
