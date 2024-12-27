using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class risePlatform : MonoBehaviour
{

    [SerializeField]private List<float[] > Height = new List<float[]>();
    private float[] heights = new float[2];
    private bool tumbler = false;
    private Stopwatch shortDelay;
   [SerializeField] private bool PermissionforStopMove;
  [SerializeField]  private bool PermissionToRotate = false;
   [SerializeField] private int direction = 0;
    [SerializeField] private int way = 0;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (gameObject.name == "Bridge_B" || gameObject.name == "Bridge_C")
        {
            ManagementControl();
        }

    }
     void Update()
    {
        if (gameObject.name == "RotatingPlat")
        {
            ManagementControlCopy();


        }
    }

    private void Initialize()
    {
        float[] mass = new float[2];
        mass[0] = 5.0f; mass[1] = 17.5f;
        Height.Add(mass);

        float[] mass2 = new float[2];
        mass2[0] = 16.5f; mass2[1] = 27.5f;
        Height.Add(mass2);

        float[] mass3 = new float[2];
        mass3[0] = 25f; mass3[1] = 45f;
        Height.Add(mass3);

        switch (gameObject.name)
        {
            case "Bridge_B": heights = Height[0]; break;
            case "Bridge_C": heights = Height[1]; break;
            case "RotatingPlat": heights = Height[2]; break;

        }
        PermissionforStopMove = false;
    }

    private void ManagementControl()
    {
            if (transform.position.y < heights[1] && !tumbler && !PermissionforStopMove)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 3f, Space.World);
                if (transform.position.y > heights[1])
                {
                    tumbler = true;
                    PermissionforStopMove = true;
                    shortDelay = Stopwatch.StartNew();
                   
                }
            }
            if (PermissionforStopMove)
            {

                if (shortDelay.Elapsed.TotalSeconds > 0.75f)
                {
                    PermissionforStopMove = false;
                    shortDelay.Reset();
                   
                }
            }

            if (tumbler && !PermissionforStopMove)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 3f, Space.World);
                if (transform.position.y < heights[0])
                {
                    tumbler = false;
                    PermissionforStopMove = true;
                    shortDelay = Stopwatch.StartNew();
                    
                }
            }

    }

    private void ManagementControlCopy()
    {
        if (transform.position.y < heights[1] && !tumbler && !PermissionforStopMove )
        {
            transform.Translate(Vector3.up * Time.deltaTime * 3f, Space.World);
            if (transform.position.y > heights[1])
            {
                tumbler = true;
                PermissionforStopMove = true;
                shortDelay = Stopwatch.StartNew();
                
                way++;
            }
        }
        if (PermissionforStopMove)
        {

            if (shortDelay.Elapsed.TotalSeconds > 1.25f)
            {
                
                shortDelay.Reset();
                if (way < 2) PermissionforStopMove = false;
                else PermissionToRotate = true;

            }
            RotatingPlatform();
        }

        if (tumbler && !PermissionforStopMove  )
        {
            transform.Translate(Vector3.down * Time.deltaTime * 3f, Space.World);
            if (transform.position.y < heights[0])
            {
                tumbler = false;
                PermissionforStopMove = true;
                shortDelay = Stopwatch.StartNew();
                

                way++;
            }
        }
    }

    private void RotatingPlatform()
    {
        if ( PermissionToRotate)
        {
               if(direction == 0) transform.Rotate(Vector3.forward * 80f * Time.deltaTime, Space.Self);

                if (transform.rotation.eulerAngles.z > 180f && direction == 0)
                {
                    PermissionforStopMove = false;
                    PermissionToRotate = false;
                    way = 0;
                direction = 1;
                }
            
            else if (direction == 1)
            {
                transform.Rotate(Vector3.forward * 80f * Time.deltaTime, Space.Self);

                if (transform.rotation.eulerAngles.z > 358f)
                {
                    PermissionforStopMove = false;
                    PermissionToRotate = false;
                    transform.rotation = Quaternion.Euler(0, 0, 0f);

                    way = 0;
                    direction = 0;
                }
            }

        }
    }
}
