using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.SceneManagement;
using TMPro;


public class SpawnManager : MonoBehaviour
{

    private bool startscript = false;
    private PlayerController countEnemys;
    public GameObject[] gameObjects;
    public GameObject powerUp;
    private GameObject playerPos;
    
    private Vector3 spawnPosition;
    private List<Vector3> arrayPos = new List<Vector3>();
    private GameObject[] particles;
    private List<GameObject> particles2 = new List<GameObject>();
    private GameObject[] spawnPos;
    public float count;
    private int countEnemyEnd = 0;
    public TextMeshProUGUI countEnemy;
    bool permissionWin = true;

    private float xleftRange = -10, xrightRange = +13;
    private float zUpRange = 8.5f, zDownRange = -8.6f;
    private float yRange = 0.2f;
    private int countPowerUp = 0;

    private Stopwatch timerForRemove;
    private Stopwatch timerForAdd;

    // Start is called before the first frame update
    void Start()
    {
       


    }

    void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        if (startscript)
        {

            if (countEnemys.countKills > 0)
            {

                StartCoroutine(Cycle());
                countEnemyEnd++;
                countEnemys.countKills -= 1;
                countPowerUp++;

                if (countEnemyEnd > 19 && permissionWin)
                {
                    countEnemys.FinishGame();
                    permissionWin = false;

                }

                countEnemy.text = "The remaining enemies:" + (20 - countEnemyEnd);
            }

            if (countEnemys.countPickup > 0 && countPowerUp > 3)
            {
                SpawnPowerUp();
                countEnemys.countPickup = 0;
                countPowerUp = 0;

            }

            if (timerForRemove.Elapsed.TotalSeconds > 9)
            {
                particles = GameObject.FindGameObjectsWithTag("Explosive");
                for (int i = 0; i < particles.Length; i++)
                {
                    particles2.Add(particles[i]);
                    Destroy(particles[i]);
                }

                timerForRemove.Reset();
                timerForRemove.Start();
                particles2.Clear();


            }
            if (timerForAdd.Elapsed.TotalSeconds > 5)
            {

                timerForAdd.Reset();
                timerForAdd.Start();
                if (GameObject.FindGameObjectsWithTag("WallofBox").Length < 4) StartCoroutine(Cycle());


            }


        }

      


    }

     

   

    void SpawnPowerUp()
    {
        spawnPosition = new Vector3(Random.Range(xleftRange, xrightRange), yRange, Random.Range(zDownRange, zUpRange));
        GameObject gameObject = powerUp;
        Instantiate(gameObject, spawnPosition, gameObject.transform.rotation);
    }

    IEnumerator Cycle()
    {
        if (countEnemys.isGameActive) {
            spawnPos = GameObject.FindGameObjectsWithTag("WallofBox");
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            arrayPos.Clear();
            if (spawnPos != null)
            {
                foreach (GameObject vect in spawnPos)
                {
                    arrayPos.Add(vect.transform.position);

                }
                arrayPos.Add(player.transform.position);
            }

            SpawnObstacle();
        }
        yield return null;


    }

    void SpawnObstacle()
    {
        for (int i = 0; ; i++)
        {
            spawnPosition = new Vector3(Random.Range(xleftRange, xrightRange), yRange, Random.Range(zDownRange, zUpRange));
            // arrayPos.Add(spawnPosition);
            if (ComparePos(spawnPosition))
            {
                GameObject gameObject = gameObjects[Random.Range(0, gameObjects.Length)];
                Instantiate(gameObject, spawnPosition, gameObject.transform.rotation);

                break;
            }
            count++;
        }


    }

    private bool ComparePos(Vector3 vector)
    {
        bool permission = false;

        if(arrayPos.Count > 0)
        {
            foreach (Vector3 vect in arrayPos) {
                if (vector.x < vect.x - 1.5f || vector.x > vect.x + 1.5f)
                {
                    permission = true;

                }
                else permission = false;

                if ((vector.z < vect.z - 1.5f || vector.z > vect.z + 1.5f) && permission)
                {
                    permission = true;

                }
                else permission = false;

                if (permission == false) break;
            }

        }
        else return permission = true;

        return permission;

    }

    private void CycleFunc ()
    {
        arrayPos.Clear();
        if (spawnPos != null)
        {
            foreach (GameObject vect in spawnPos)
            {
                arrayPos.Add(vect.transform.position);

            }
            arrayPos.Add(playerPos.transform.position);

        }
        

    }

    public void StartGame()
    {
        countEnemys = GameObject.Find("Player").GetComponent<PlayerController>();

        for (int i = 0; i < 4; i++)
        {
            SpawnObstacle();
            spawnPos = GameObject.FindGameObjectsWithTag("WallofBox");
             playerPos = GameObject.FindGameObjectWithTag("Player");
            CycleFunc();
        }
        SpawnPowerUp();


        timerForRemove = new Stopwatch();
        timerForRemove.Start();
        timerForAdd = new Stopwatch();
        timerForAdd.Start();

        countEnemy.gameObject.SetActive(true);
        countEnemy.text = "The remaining enemies:" + (20 - countEnemyEnd);
        startscript = true;
        

    }



    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
