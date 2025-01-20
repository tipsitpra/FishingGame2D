using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PodMove : MonoBehaviour
{
   
    public enum PodState
    {
        ROTATION,
        SHOOT,
        REWIND,
        REWINDFORBOSS,
    }
    [SerializeField]
    private int _rotateSpeed = 5;
    PodState podState = PodState.ROTATION;
    private int _angle = 0;
    private float speed = 2;
    public Transform caster, collidedWith;
    private Vector2 _Origin;
    private Transform _Rod;
    
    private bool _flagRod;
    private int _slowDown;
    private int _point;
    private int _time;
    private float _hp;
    private float oldSpeed;
    public string[] tagsToCheck;
    public GameObject podFollow;
    public GameObject pullBoss;
    public bool fishingEpic;
    public bool fishingLegendary;
    public bool checkSound;

    void Awake()
    {
        _Origin = transform.position;
        fishingEpic = SaveData.fishingEpic;
        fishingLegendary = SaveData.fishingLegen;
    }

    // Update is called once per frame
    void Update()
    {


        if (fishingEpic == true)
        {
            speed = 5;
            Debug.Log("Fishing Epic");
        }
        if (fishingLegendary == true)
        {
            speed = 8;
            Debug.Log("Fishing Legendary");
        }


        switch (podState)
        {
            case PodState.ROTATION:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    podState = PodState.SHOOT;
                _angle += _rotateSpeed;
                if (_angle > 80 || _angle < -80)
                    _rotateSpeed = -_rotateSpeed;
                transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
                checkSound = true;
                break;
            case PodState.SHOOT:
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                if (Mathf.Abs(transform.position.x) > 8 || transform.position.y < -4)
                    podState = PodState.REWIND;
                    Sound();
                break;
            case PodState.REWIND:
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, caster.position) < 0.5f)
                {
                   // _slowDown = 0;
                    _flagRod = false;
                    //Sound();
                    transform.position = _Origin;
                    GameManager.fishScore += _point;
                    GameManager.timeRemaing += _time;
                    Destroy(gameObject);
                }    
                    break;
            case PodState.REWINDFORBOSS:
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, caster.position) < 0.5f)
                {
                    // _slowDown = 0;
                    _flagRod = false;
                    transform.position = _Origin;
                    GameManager.fishScore += _point;
                    GameManager.timeRemaing += _time;

                }
                break;



        }


    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (tagsToCheck.Contains(col.tag))
        {
            if (_flagRod) return;
            _flagRod = true;
            Transform previousParent = col.transform;
            _Rod = col.transform;
            _Rod.SetParent(transform);
            
            if (col.CompareTag("fish01"))
            {
                _Rod.GetComponent<FishEasy>().speed = 0;
                _point = _Rod.GetComponent<FishEasy>().point;
                _time = _Rod.GetComponent<FishEasy>().time;
            }

            if (col.CompareTag("fish02"))
            {
                _Rod.GetComponent<FishNormal>().speed = 0;
                _point = _Rod.GetComponent<FishNormal>().point;
                _time = _Rod.GetComponent<FishNormal>().time;
            }

            if (col.CompareTag("fish03"))
            {
                _Rod.GetComponent<FishHard>().speed = 0;
                _point = _Rod.GetComponent<FishHard>().point;
                _time = _Rod.GetComponent<FishHard>().time;
            }

            if (col.CompareTag("fish04"))
            {
                _Rod.GetComponent<FishHard>().speed = 0;
                _point = _Rod.GetComponent<FishHard>().point;
                _time = _Rod.GetComponent<FishHard>().time;
            }

            if (col.CompareTag("fish05"))
            {
                _Rod.GetComponent<FishNormal>().speed = 0;
                _point = _Rod.GetComponent<FishNormal>().point;
                _time = _Rod.GetComponent<FishNormal>().time;
            }

            if (col.CompareTag("fish06"))
            {
                _Rod.GetComponent<FishHard>().speed = 0;
                _point = _Rod.GetComponent<FishHard>().point;
                _time = _Rod.GetComponent<FishHard>().time;
            }

            if (col.CompareTag("fish07"))
            {
                _Rod.GetComponent<FishEasy>().speed = 0;
                _point = _Rod.GetComponent<FishEasy>().point;
                _time = _Rod.GetComponent<FishEasy>().time;
            }
            if (col.CompareTag("bossfish01"))
            {
                oldSpeed = _Rod.GetComponent<FishAI>().speed;
                _Rod.GetComponent<FishAI>().speed = 0;
                speed = _Rod.GetComponent<FishAI>().slowDown;
                _hp = _Rod.GetComponent<FishAI>().hp;
                Instantiate(pullBoss,new Vector2(7,0.2f), Quaternion.identity);
                Debug.Log(_hp);
                Invoke("BossFishTake", 3f);
               //_time = _Rod.GetComponent<FishAI>().time;
            }
            if (col.CompareTag("bossfish02"))
            {
                oldSpeed = _Rod.GetComponent<FishAI>().speed;
                _Rod.GetComponent<FishAI>().speed = 0;
                speed = _Rod.GetComponent<FishAI>().slowDown;
                _hp = _Rod.GetComponent<FishAI>().hp;
              
                Instantiate(pullBoss,new Vector2(7,0.2f), Quaternion.identity);
                Debug.Log(_hp);
                Invoke("BossFishTake", 3f);
               //_time = _Rod.GetComponent<FishAI>().time;
            }

            //Debug.Log(previousParent);
            GameManager.takeFish = true;
                podState = PodState.REWIND;
            
        }

    }


    public void Sound()
    {
        if (checkSound && podState == PodState.SHOOT)
        {
            SoundManager.sndMan.SoundHook();
            checkSound = false;
        }

    }
    public void BossFishTake()
    {
        GameObject bossObject = GameObject.Find("Pull(Clone)");
        _hp = _Rod.GetComponent<FishAI>().hp;
        if (_hp > 0)
        {
            _Rod.GetComponent<FishAI>().speed = oldSpeed;
            speed = 2;
            _Rod.SetParent(null);
            
            if (bossObject != null)
            {
                Destroy(bossObject);
            }
        }
        if(_hp <= 0)
        {
            GameManager.takeFish = true;
            podState = PodState.REWIND;
            _point = _Rod.GetComponent<FishAI>().point;
            _Rod.GetComponent<FishAI>().speed = 0;
             speed = 2;
            _time = _Rod.GetComponent<FishAI>().time;
             Destroy(bossObject);
        }
    }





}
