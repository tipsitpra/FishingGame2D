using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int noOfFish;
    public GameObject[] prefabFish;
    public float spawnInterval;
}



public class GameManager : MonoBehaviour
{
    public TMP_Text MyScoreText;
    public TMP_Text RequiredScore;
    public static float fishScore;
    public static float fishHP;
    public static float maxfishHP;
    public static bool takeFish;
    public float score;
    public float hp;

    public Wave[] waves;

    [SerializeField]
    private GameObject[] fishPrefab;
    public int xPos;
    public int yPos;

    public Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private int waveFish;

    private bool canSpawn = true;
    private bool canAnime = true;

    public TMP_Text gameOverText;
    public TMP_Text time;
    public int maxScore = 100;

    public Image pointBar;
    public Image backtBar;
    public Image HPBar;
    public Image backHPBar;

    public Animator animator;

    public GameObject gameOver;
    public GameObject gameWin;
    public GameObject ShopUI;
    public GameObject hp_UIBoss;
    public GameObject shootHook;
    public GameObject soundBGM;

    private string targetTag3 = "fish03";
    private string targetTag2 = "fish02";
    private string targetTag4 = "fish04";
    private string targetTag5 = "fish05";
    private bool shop;
    private bool hasBossSpawned1 = false;
    private bool hasBossSpawned2 = false;
    private bool bossFishDead = false;
    private bool chakeBoss = false;

    //public static int hpFish;
    public static float timeRemaing = 0;
    private float lerpTime = 0f;
    private int randomPointSpawn1;
    private float randomPointSpawn2;
    private int setNumberRandom;
    private float lastScoreFinishBoss;
    private bool checkGame = true;
    private static float totalPoint = 100f;



    // Start is called before the first frame update
    void Start()
    {
        timeRemaing = 50;
        UpdateTime(timeRemaing);
        score = 0;
        setNumberRandom = 10;
        randomPointSpawn1 = Random.Range(150 , 300);

        MyScoreText.text = "Score : " + score;  
         //RequiredScore.text = "Required Score : " + 100;
        Time.timeScale = 1;
        //gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        score = fishScore;

        hp = fishHP;
        score = Mathf.Clamp(score, 0, maxScore);
        hp = Mathf.Clamp(hp, 0, maxfishHP);
        Debug.Log(randomPointSpawn1);
        //Debug.Log(score);

        MyScoreText.text = "Score : " + score;
        //RequiredScore.text = "Required Score : " + totalPoint;
        //totalPoint = score*2;
        currentWave = waves[currentWaveNumber];
        UpdateScoreUI();
        UpdateHPBossFishUI();
        StartCoroutine(FishSpawn());
        //GameObject[] totalFish = GameObject.FindGameObjectsWithTag("Fish");
        GameObject bossFish1 = GameObject.Find("Fish 7(Clone)");
        GameObject bossFish2= GameObject.Find("Fish 8(Clone)");

        if (!canSpawn && takeFish)
        {
            currentWave.noOfFish--;
            //currentWaveNumber++;
            canSpawn = true;
            takeFish = false;
            canAnime = true;
        }

        if(fishHP <= 0)
        {
            hp_UIBoss.SetActive(false);
          
        }


        if (hasBossSpawned1)
        {
            bossFishDead = false;

            if (bossFish1 == null)
            {
                bossFishDead = true;
                CheckBossFish();
            }

             canAnime = true;

            if (hasBossSpawned2)
            {
                bossFishDead = false;

                if (bossFish2 == null)
                    bossFishDead = true;

                canAnime = true;
            }
        }
        else
            bossFishDead = true;


        if (score >= 150 && score <= 450 && bossFishDead)
        {
            currentWaveNumber = 1;

            //if(score > 10 && !hasBossSpawned1)
             SpawnBossFish();
        }

        if (score >= 500 && bossFishDead)
        {

            currentWaveNumber = 2;
            //if (score > 30 && !hasBossSpawned2)
                SpawnBossFish();

        }


        if (timeRemaing > 0)
        {
            timeRemaing -= Time.deltaTime;
            UpdateTime(timeRemaing);
        }
        else
        {
            UpdateTime(0);

            GameObject obj = GameObject.Find("Fish(Clone)");
            GameObject obj1 = GameObject.Find("Fish 1(Clone)");
            GameObject obj2 = GameObject.Find("Fish 2(Clone)");


           // if (SceneManager.GetActiveScene().buildIndex == 2)
            //{
                if (score >= 1000 && checkGame)
                {
                    
                    Debug.Log("You Win");
                    GameWin();
                  shootHook.SetActive(false);
                soundBGM.SetActive(false);
                //Time.timeScale = 0;
                //totalPoint = score * 2;
                checkGame = false;
            }
            if (score < 1000 && checkGame)
            {
                    Debug.Log("GAME OVER");
                    GameOver();
                shootHook.SetActive(false);
                soundBGM.SetActive(false);          
                //Time.timeScale = 0;
                 checkGame = false;
            }
           // }

            if (SceneManager.GetActiveScene().buildIndex == 3) 
            {
                
                if (score >= totalPoint )
                {
                    //GameOver();
                    Debug.Log("You Win");
                    GameWin();
                    Time.timeScale = 0;
                   // checkGame = false;
                }
                else
                {
                    Debug.Log("GAME OVER");
                    GameOver();
                    Time.timeScale = 0;
                   // checkGame = false;
                }

            }

            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                if (score >= totalPoint)
                {
                    //GameOver();
                    Debug.Log("You Win");
                    GameWin();
                    Time.timeScale = 0;
                   // checkGame = false;
                }
                else
                {
                    Debug.Log("GAME OVER");
                    GameOver();
                    Time.timeScale = 0;
                   // checkGame = false;
                }

            }
        }

       


    }

    public void GameOver()
    {

      
         gameOver.SetActive(true);
         SoundManager.sndMan.GameOverSound();
        

    }  
    public void GameWin()
    {

        gameWin.SetActive(true);
        SoundManager.sndMan.GameWinSound();
    }

    public void UpdateTime(float timeLeft)
    {
        string formattedString = timeLeft.ToString("F0");
        time.text = "Time : " + formattedString;
    }

    public void EpicFishing()
    {
        if (score >= 150)
        {
            SaveData.fishingEpic = true;

            fishScore -= 150;
        }
    }
    public void LegendaryFishing()
    {
        if (score >= 650)
        {
            SaveData.fishingLegen = true;
            fishScore -= 650;
        }
    }

    IEnumerator FishSpawn()
    {


      

        if (canSpawn && nextSpawnTime < Time.time)
        {
            //xPos = Random.Range(10, -10);
            //yPos = Random.Range(-1, -10);

            //int currentFishCount = FindObjectsOfType(currentWave.prefabFish.GetType()).Length;
            //Debug.Log(currentFishCount);
            if (currentWaveNumber == 0)
            {
                int objectCount3 = 0;
                int objectCount2 = 0;
                GameObject randomEnemy = currentWave.prefabFish[Random.Range(0, currentWave.prefabFish.Length)];

                if (randomEnemy.CompareTag(targetTag3))
                {
                    GameObject[] objects3 = GameObject.FindGameObjectsWithTag(targetTag3);
                    foreach (GameObject obj in objects3)
                    {
                        objectCount3++;
                    }
                }

                if (randomEnemy.CompareTag(targetTag2))
                {
                    GameObject[] objects2 = GameObject.FindGameObjectsWithTag(targetTag2);
                    foreach (GameObject obj in objects2)
                    {
                        objectCount2++;
                    }
                }

                if (objectCount3 < 6 && objectCount2 < 8)
                {
                    Instantiate(/*currentWave.prefabFish[0]*/randomEnemy, new Vector2(xPos, yPos), Quaternion.identity);

                    currentWave.noOfFish++;
                    nextSpawnTime = Time.time + currentWave.spawnInterval;
                    if (currentWave.noOfFish >= 20)
                    {
                        canSpawn = false;
                    }

                    yield return new WaitForSeconds(1.5f);
                }

                Debug.Log(currentWaveNumber);
            }


            if (currentWaveNumber == 1)
            {
                int objectCount2 = 0;
                int objectCount3 = 0;
                int objectCount4 = 0;
                int objectCount5 = 0;
                
                GameObject randomEnemy = currentWave.prefabFish[Random.Range(0, currentWave.prefabFish.Length)];

                if (randomEnemy.CompareTag(targetTag4))
                {
                    GameObject[] objects4 = GameObject.FindGameObjectsWithTag(targetTag4);
                    foreach (GameObject obj in objects4)
                    {
                        objectCount4++;
                    }
                }

                if (randomEnemy.CompareTag(targetTag5))
                {
                    GameObject[] objects5 = GameObject.FindGameObjectsWithTag(targetTag5);
                    foreach (GameObject obj in objects5)
                    {
                        objectCount5++;
                    }
                }

                if (randomEnemy.CompareTag(targetTag3))
                {
                    GameObject[] objects3 = GameObject.FindGameObjectsWithTag(targetTag3);
                    foreach (GameObject obj in objects3)
                    {
                        objectCount3++;
                    }
                }

                if (randomEnemy.CompareTag("fish02"))
                {
                    GameObject[] objects2 = GameObject.FindGameObjectsWithTag("fish02");
                    foreach (GameObject obj in objects2)
                    {
                        objectCount2++;
                    }
                }


                if (objectCount3 < 4 && objectCount4 < 4 && objectCount5 < 5 && objectCount2 < 6)
                {
                    Instantiate(/*currentWave.prefabFish[0]*/randomEnemy, new Vector2(xPos, yPos), Quaternion.identity);

                    currentWave.noOfFish++;
                    nextSpawnTime = Time.time + currentWave.spawnInterval;
                    if (currentWave.noOfFish >= 20)
                    {
                        canSpawn = false;
                    }

                    yield return new WaitForSeconds(3.5f);
                }

                Debug.Log(currentWaveNumber);
            }
            
            if (currentWaveNumber == 2)
            {
                int objectCount1 = 0;
                int objectCount2 = 0;
                int objectCount3 = 0;
                int objectCount4 = 0;
                int objectCount5 = 0;
                int objectCount6 = 0;

                GameObject randomEnemy = currentWave.prefabFish[Random.Range(0, currentWave.prefabFish.Length)];

                if (randomEnemy.CompareTag("fish01"))
                {
                    GameObject[] objects1 = GameObject.FindGameObjectsWithTag("fish01");
                    foreach (GameObject obj in objects1)
                    {
                        objectCount1++;
                    }
                }

                if (randomEnemy.CompareTag("fish06"))
                {
                    GameObject[] objects2 = GameObject.FindGameObjectsWithTag("fish06");
                    foreach (GameObject obj in objects2)
                    {
                        objectCount2++;
                    }
                }

                if (randomEnemy.CompareTag("fish05"))
                {
                    GameObject[] objects3 = GameObject.FindGameObjectsWithTag("fish05");
                    foreach (GameObject obj in objects3)
                    {
                        objectCount3++;
                    }
                }
                if (randomEnemy.CompareTag("fish04"))
                {
                    GameObject[] objects4 = GameObject.FindGameObjectsWithTag("fish04");
                    foreach (GameObject obj in objects4)
                    {
                        objectCount5++;
                    }
                }

                if (randomEnemy.CompareTag("fish03"))
                {
                    GameObject[] objects5 = GameObject.FindGameObjectsWithTag("fish03");
                    foreach (GameObject obj in objects5)
                    {
                        objectCount4++;
                    }
                }

                if (randomEnemy.CompareTag("fish02"))
                {
                    GameObject[] objects5 = GameObject.FindGameObjectsWithTag("fish02");
                    foreach (GameObject obj in objects5)
                    {
                        objectCount6++;
                    }
                }


                if (objectCount1 < 4 && objectCount2 < 4 && objectCount3 < 5 && objectCount4 < 5 && objectCount5 < 5 && objectCount6 < 5)
                {
                    Instantiate(/*currentWave.prefabFish[0]*/randomEnemy, new Vector2(xPos, yPos), Quaternion.identity);

                    currentWave.noOfFish++;
                    nextSpawnTime = Time.time + currentWave.spawnInterval;
                    if (currentWave.noOfFish >= 30)
                    {
                        canSpawn = false;
                    }

                    yield return new WaitForSeconds(3.5f);
                }

                Debug.Log(currentWaveNumber);
            }



        }
    }

    public void CheckBossFish()
    {
        if (!chakeBoss)
        {
            lastScoreFinishBoss = score;
            randomPointSpawn2 = Random.Range(lastScoreFinishBoss + 150 , 800);
            chakeBoss = true;
        }
    }

    public void SpawnBossFish()
    {

        if (score > randomPointSpawn1 && !hasBossSpawned1)
        {

            if (canAnime)
                animator.SetTrigger("Waring");
            Debug.Log(canAnime);
            bossFishDead = false;
            currentWaveNumber = 3;
            Instantiate(currentWave.prefabFish[0], new Vector2(xPos, yPos), Quaternion.identity);
            currentWave.noOfFish++;
            canAnime = false;
            hp_UIBoss.SetActive(true);
            if (currentWave.noOfFish == 1)
            {
                hasBossSpawned1 = true;
                //currentWaveNumber = 0;
                setNumberRandom = setNumberRandom * 5;
                currentWave.noOfFish = 0;

            }
        }
        Debug.Log("randomPointSpawn2"+ randomPointSpawn2);
        if (score > randomPointSpawn2 && !hasBossSpawned2 && chakeBoss )
        {
            if (canAnime)
                animator.SetTrigger("Waring");
            Debug.Log(canAnime);
            bossFishDead = false;
            currentWaveNumber = 4;
            Instantiate(currentWave.prefabFish[0], new Vector2(xPos, yPos), Quaternion.Euler(0f, 0f, -90));
            currentWave.noOfFish++;
            canAnime = false;
            hp_UIBoss.SetActive(true);
            if (currentWave.noOfFish == 1)
            {
                hasBossSpawned2 = true;
                //currentWaveNumber = 0;
                setNumberRandom = setNumberRandom * 5;
                currentWave.noOfFish = 0;

            }
        }





    }


    public void UpdateScoreUI()
    {

        float fillScore = pointBar.fillAmount;
        float fillBack = backtBar.fillAmount;

        float fraction = score / maxScore;

        if (fillScore < fraction)
        {
            lerpTime += Time.deltaTime;
            float percentComplete = lerpTime / 2;
            SoundManager.sndMan.TakeFish();
            pointBar.fillAmount = Mathf.Lerp(fillScore, backtBar.fillAmount, 0.002f);
        }
        //Debug.Log(fillScore);

    } 
    
    public void UpdateHPBossFishUI()
    {

        float fillHP = HPBar.fillAmount;
        float fillBackHP = backHPBar.fillAmount;

        float fraction = hp / maxfishHP;

        if (fillBackHP > fraction)
        {
            HPBar.fillAmount = fraction;
            lerpTime += Time.deltaTime;
            float percentComplete = lerpTime / 2;
            HPBar.fillAmount = Mathf.Lerp(fillHP, fraction, 0.02f);

        }
        if(hp <= 0)
        {
            HPBar.fillAmount = Mathf.Lerp(fillHP, backHPBar.fillAmount, 0.02f);
        }

    }



    public void Restart()
    {

        //SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        fishScore = 0;
        //totalPoint = 100;
        SaveData.fishingEpic = false;
        SaveData.fishingLegen = false;
        Time.timeScale = 1;
        //canSpawn = true;


    }

    public void Shop()
    {

        Cursor.lockState = CursorLockMode.None;
        ShopUI.SetActive(true);

        gameOver.SetActive(false);
        gameWin.SetActive(false);
    }

    public void Back()
    {

        GameObject obj = GameObject.Find("Fish(Clone)");
        if (obj == null )
        {

            GameWin();
            ShopUI.SetActive(false);
        }
        if (obj != null )
        {

            GameOver();
            ShopUI.SetActive(false);
        }
    }

    public void LoadScenceNextLevel()
    {


        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadSceneAsync("Scene 2");
            totalPoint = score * 3;

        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadSceneAsync("Scene 3");
            totalPoint = score * 4;
        }

    }
    
    public void MainMenu()
    {

        SceneManager.LoadScene("MainMenu");

    }


    //public void NextLevel2()
    //{

    //    Update();
    //    //fishScore = 0;
    //    Time.timeScale = 1;
    //    timeRemaing = 20;
    //    canSpawn = true;
    //    currentWaveNumber++;
    //    gameWin.SetActive(false);

    //}





}
