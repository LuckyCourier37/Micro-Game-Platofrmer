using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtSecond : MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;
    private SpawnManager spawnManager;
    private SecondPlayerSc player;
     [SerializeField] private GameObject GameControls;
    [SerializeField] CameraScript Maincamera;
    [SerializeField] private GameObject buttonExit;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject Revert;
    void Start()
    {
        button = GetComponent<Button>();
       // spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        player = GameObject.Find("Player").GetComponent<SecondPlayerSc>();

       // button.onClick.AddListener(SetDifficulty2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDifficulty2()
    {
        Debug.Log(button.name + " Was clicked");
        player.StartGame();
       
        GameObject.Find("Title").SetActive(false);
        GameControls.SetActive(true);

        buttonExit.SetActive(false);
        Destroy(Revert.gameObject);
        

        Maincamera.startInititaion();
        Maincamera.KeepTrack();
        
        titleScreen.gameObject.SetActive(false);

        //  spawnManager.StartGame();

    }
}
