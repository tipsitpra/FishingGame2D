using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishBossHp : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().sortingOrder = 32767;
        if (Input.GetMouseButtonDown(0)) // ��Ǩ�ͺ����ա�ä�ԡ��������������
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.CompareTag("Damage")) // ��Ǩ�ͺ����ѵ�ط��١��ԡ�� Tag ����ͧ����������
            {

                GameManager.fishHP -= 1;
                SoundManager.sndMan.DamageFishBoss();
                Debug.Log(GameManager.fishHP);
            }

        }
    }

}
