using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{

    public GameObject spawnObject;
    private bool number;
    // Start is called before the first frame update
    void Start()
    {
        number = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&& number == false)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = new Vector3(0, 0, 10);
            
                Instantiate(spawnObject, pos + offset, Quaternion.identity);
                number = true;
            
        }
    }
}
