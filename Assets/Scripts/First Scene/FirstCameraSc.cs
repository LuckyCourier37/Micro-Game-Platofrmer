using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCameraSc : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 startPosition;
    [SerializeField] private GameObject ManagentControl;
    [SerializeField] private GameObject titleControl;
    [SerializeField] private GameObject Health;
    [SerializeField] private GameObject countEnemy;
    [SerializeField] private PlayerController player;

    private bool ChangerPosition = false;
    void Start()
    {
        startPosition = transform.position;

        var cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        Management();
    }
    private void Management()
    {
        if(player.GetStartVariable() && player.isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.X) && !ChangerPosition)
            {
                transform.position = transform.position + new Vector3(0f, 0f, 100f);
                ManagentControl.SetActive(true);
                ChangerPosition = true;
                titleControl.SetActive(false);
                Health.SetActive(false);
                countEnemy.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.X) && ChangerPosition)
            {
                ManagentControl.SetActive(false);
                ChangerPosition = false;
                transform.position = startPosition;
                titleControl.SetActive(true);
                Health.SetActive(true);
                countEnemy.SetActive(true);
            }
        }
        
    }

  
}
