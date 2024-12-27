using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;  // Ссылка на шарик
    private Vector3 offset = Vector3.zero;    // Смещение камеры относительно игрока
    private float smoothSpeed = 0.03f;  // Скорость плавного перемещения
    private Vector2 boundarySize = new Vector2(1f, 2f);  // Размер невидимой рамки
    private Vector3 startPosition;
    [SerializeField] private GameObject ManagentControl;
    [SerializeField] private GameObject titleControl;
    [SerializeField] private GameObject ScoreHealth;
    [SerializeField] private SecondPlayerSc sphere;
    private bool ChangerPosition = true;
    

    private Camera settings;
    private CameraClearFlags fillFlags;

    private void Start()
    {
       settings = GetComponent<Camera>();
        fillFlags = settings.clearFlags;

        settings.clearFlags = CameraClearFlags.SolidColor;
        settings.backgroundColor = Color.black;

    }
    void LateUpdate()
    {
       
        if (!ChangerPosition) 
        {
            ControlCamera();
        }

        Management();
    }

    

    public void ResetCameraPosition()
    {
        if(!ChangerPosition) 
        {
            transform.position = new Vector3(player.position.x, player.position.y + 2f, startPosition.z);
        }
        // Задаем камере позицию прямо на игроке с учетом смещения
        
    }

    private void ControlCamera()
    {
        Vector3 targetPosition = transform.position;

        // Проверка границ по оси X
        if (Mathf.Abs(player.position.x - transform.position.x) > boundarySize.x)
        {
            targetPosition.x = player.position.x;
        }

        // Проверка границ по оси Y (если хотите ограничивать также по высоте)
        if (Mathf.Abs(player.position.y - transform.position.y) > boundarySize.y)
        {
            targetPosition.y = player.position.y;
        }

        // Плавное перемещение камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition + offset, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void Management()
    {
        if(sphere.isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.X) && !ChangerPosition)
            {
                transform.position = transform.position + new Vector3(0f, 0f, 100f);
                ManagentControl.SetActive(true);
                ChangerPosition = true;
                titleControl.SetActive(false);
                ScoreHealth.SetActive(false);
                fillBackground(ChangerPosition);
            }
            else if (Input.GetKeyDown(KeyCode.X) && ChangerPosition)
            {
                ManagentControl.SetActive(false);
                ChangerPosition = false;
                transform.position = transform.position + new Vector3(0f, 0f, -100f);
                titleControl.SetActive(true);
                ScoreHealth.SetActive(true);
                fillBackground(ChangerPosition);
            }
        }
        
    }

    public void  startInititaion() 
    {
            startPosition = new Vector3(15f, 7f, 37f);
            transform.position = startPosition;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            settings.clearFlags = fillFlags;
    }

    public void KeepTrack()
    {
        ChangerPosition = false;
        
    }

   

    private void fillBackground( bool color)
    {
        if (color)
        {
            settings.clearFlags = CameraClearFlags.SolidColor;
            settings.backgroundColor = Color.black;

        }
        else
        {
            settings.clearFlags = fillFlags;
        }

    }
}
