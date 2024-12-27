using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SecondPlayerSc : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    private float devider = 7f;
   [SerializeField] private bool isGrounded = true;
    public float jumpForce = 12.5f;
    private float valueBounce = 5.5f;
    private float boundaryBounce = 6f;
    private int jumpCount = 0;
    private float verticalInput;
    private float horizontalInput;
    private CubeFacePosition shape;
    [SerializeField] private List<Vector3> shapeList;
    [SerializeField] private GameObject MainCamera;
    private CameraScript CamScript;
    private bool canDoubleJump = false;

    public int countKills = 0;
    public int countPickup = 0;
    public TextMeshProUGUI scoreHealth;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI congratulations;
    public int scoreHealth1 = 100;
    public bool isGameActive;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button Exit;
    public bool vanquishGame = false;

    private Rigidbody playerRb;
    private bool jumpPermision = true;
    private bool startscript = false;

    private bool permissionloose = true;
    public Vector3 Display1;
    private Stopwatch swJump;


    void Start()
    {
        CamScript = MainCamera.GetComponent<CameraScript>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

       
        if (startscript)
        {
            MovePlayer();
            ConstrainPlayerPosition(shapeList);


            Display1 = Vector3.Normalize(playerRb.velocity);

            ConstrainYPosition();
            if (scoreHealth1 < 1 && permissionloose)
            {
                GameOver();
                permissionloose = false;
            }

        }

    }
    
    private void ConstrainYPosition()
    {
        if (gameObject.transform.position.y < -3)
        {
            gameObject.transform.position = new Vector3(16.35f, 5.45f, 47.25f);
            playerRb.velocity = new Vector3(0, 0, 0);
            playerRb.angularVelocity = new Vector3(0, 0, 0);
            CamScript.ResetCameraPosition();
        }
    }

    private void Update()
    {
        DoubleJump();
    }

    private void SingleJump()
    {
        
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPermision = false;
        
    }

    private void DoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                SingleJump();
                jumpCount = 1; // Первый прыжок
                isGrounded = false;
            }
            else if (jumpCount == 1 && canDoubleJump)
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
                SingleJump();
                jumpCount = 2; // Второй прыжок
            }
        }

    }



    void MovePlayer()
    {
        if(isGameActive)
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

    }

    void ConstrainPlayerPosition(List<Vector3> parametar)
    {
        if(shapeList != null && shapeList.Count != 0)
        {
            if (transform.position.z > parametar[0].z + 0.75f * parametar[1].z && swJump.Elapsed.TotalSeconds > 0.25)
            {
                playerRb.AddForce(Vector3.back * boundaryBounce, ForceMode.Impulse);
                swJump.Reset();
                swJump.Start();

            }
            else if (transform.position.z < parametar[0].z - 0.75f * parametar[1].z && swJump.Elapsed.TotalSeconds > 0.25)
            {

                playerRb.AddForce(Vector3.forward * boundaryBounce, ForceMode.Impulse);
                swJump.Reset();
                swJump.Start();
            }
        }
       
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpPermision = true;
            isGrounded = true;
            jumpCount = 0;

            shape = collision.gameObject.GetComponent<CubeFacePosition>();
            if (shape != null)
            {
              shapeList =  shape.GetCollection();
            }
           
        }
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {


            playerRb.AddForce(Vector3.up * valueBounce, ForceMode.Impulse);
            countKills++;

        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            canDoubleJump = true;
            
            countPickup++;
            scoreHealth1 += 40;
            UpdateScore();
        }

    }


    public void UpdateScore()
    {
        scoreHealth.text = "Health:" + scoreHealth1 ;

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
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        Exit.gameObject.SetActive(true);
    }

    IEnumerator DestroyCrates()
    {

        GameObject[] array = GameObject.FindGameObjectsWithTag("WallofBox");
        if(array.Length > 0 && array != null)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i].gameObject.GetComponentInChildren<DeleteAssetCopy>().permissionAnim = true;

            }
            yield return new WaitForSeconds(0.9f);
            for (int i = 0; i < array.Length; i++)
            {
                Destroy(array[i]);

            }
        }

    }
}
