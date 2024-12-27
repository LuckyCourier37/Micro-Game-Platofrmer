using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    
    private float speed = 750f;
    private float devider = 5f;
    
    private float jumpForce = 12.5f;
    private float valueBounce = 5.5f;
    private float boundaryBounce = 6f;
    private float verticalInput;
    private float horizontalInput;

    public int countKills = 0;
    public int countPickup = 0;
    public TextMeshProUGUI scoreHealth;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI congratulations;
    [SerializeField] private Button NextLevel;
    [SerializeField] private Button Exit;
    public int scoreHealth1 =  100;
    public bool isGameActive;
    public Button restartButton;
    public bool vanquishGame = false;

    private Rigidbody playerRb;
    private bool jumpPermision = true;
    private bool startscript = false;

    private bool permissionloose = true;
    public Vector3 Display1;
    private Stopwatch swJump;
   

    void Start()
    {
        

    }

    // Update is called once per frame
    void  FixedUpdate()
    {

        if (startscript)
        {
            MovePlayer();
            ConstrainPlayerPosition();


            Display1 = Vector3.Normalize(playerRb.velocity);

            if (gameObject.transform.position.y < -3)
            {
                gameObject.transform.position = new Vector3(1.35f, 1f, 0.25f);

            }
            if (scoreHealth1 < 1 && permissionloose)
            {
                GameOver();
                permissionloose = false;
            }

        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpPermision)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPermision = false;

        }
    }

    void MovePlayer()
    {
        
         verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");


        switch (jumpPermision)
        {
            case true:
                playerRb.AddForce(Vector3.forward * speed * Time.deltaTime * verticalInput);
                playerRb.AddForce(Vector3.right * speed * Time.deltaTime * horizontalInput);
                break;
            case false:
                playerRb.AddForce(Vector3.forward * speed / devider * Time.deltaTime * verticalInput);
                playerRb.AddForce(Vector3.right * speed / devider * Time.deltaTime * horizontalInput);
                break;

        }


      

    }

    void ConstrainPlayerPosition()
    {

        if (transform.position.z > 11f && swJump.Elapsed.TotalSeconds > 0.25)
        {
            playerRb.AddForce(Vector3.back * boundaryBounce, ForceMode.Impulse);
            swJump.Reset();
            swJump.Start();

        }
        else if (transform.position.z < -11 && swJump.Elapsed.TotalSeconds > 0.25)
        {

            playerRb.AddForce(Vector3.forward * boundaryBounce, ForceMode.Impulse);
            swJump.Reset();
            swJump.Start();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpPermision = true;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") )
        {
            
            
            playerRb.AddForce(Vector3.up * valueBounce, ForceMode.Impulse);
            countKills++;
            
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {

            Destroy(other.gameObject);
            countPickup++;
            scoreHealth1 += 40;
            UpdateScore();
        }

    }


    public void UpdateScore()
    {
        scoreHealth.text = "Health:" + scoreHealth1;

    }

    public void StartGame()
    {
        isGameActive = true;
        playerRb = GetComponent<Rigidbody>();

        swJump = Stopwatch.StartNew();

        scoreHealth.gameObject.SetActive(true);
        scoreHealth.text = "Health:" + scoreHealth1;
        startscript = true;

    }

    public void GameOver()
    {

        gameOver.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        StartCoroutine(DestroyCrates());
        Exit.gameObject.SetActive(true);
    }

    public void FinishGame()
    {
        isGameActive = false;
        vanquishGame = true;
        restartButton.gameObject.SetActive(true);
        StartCoroutine(DestroyCrates());
        congratulations.gameObject.SetActive(true);
        NextLevel.gameObject.SetActive(true);
    }

    IEnumerator DestroyCrates()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("WallofBox");
        for (int i = 0; i < array.Length; i++)
        {
            array[i].gameObject.GetComponentInChildren<DeleteAsset>().permissionAnim = true;
            
        }
        yield return new WaitForSeconds(0.9f) ;
        for (int i = 0; i < array.Length; i++)
        {
            Destroy(array[i]);

        }
    }

    public void LoadSecondLevel ()
    {
        SceneManager.LoadScene(1);
    } 
    public bool GetStartVariable()
    {
        return startscript;
    }    
        
}

