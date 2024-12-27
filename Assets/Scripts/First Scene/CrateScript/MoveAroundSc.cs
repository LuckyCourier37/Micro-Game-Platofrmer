using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MoveAroundSc : MonoBehaviour
{
    private Rigidbody BoxRb;
    private List<ContactPoint> contactPoints = new List<ContactPoint>();
    private PlayerController player;

    private float zDownRange = -10.5f, zUpRange = 10;
    private float xLeftRange = -15.3f, xRightRange = 16;
    private float strengthBounce = 8.5f;


    private bool permission = true;
    public int range = 9;
    private float time;
    public float timeToOverWrite;
    public float speedCube;

    public bool permissionChange1 = false;
    public bool permissionChange2 = false;

   // public bool flag1 = false;
  //  public bool flag2 = false;
    public float velocity;
    Stopwatch sw;

    // Start is called before the first frame update
    void Start()
    {
        BoxRb = GetComponent<Rigidbody>();
        
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        while (permission)
        {
            range = Random.Range(0, 4);
            permission = false;
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        movingAround(ref time, ref range);

    }

    private void Update()
    {
        time += Time.deltaTime;
        timeToOverWrite += Time.deltaTime;
        if (timeToOverWrite > 0.25f)
        {
            velocity = BoxRb.velocity.sqrMagnitude;

            timeToOverWrite = 0;
        }
        if (permissionChange1 || permissionChange2)
        {
            StartCoroutine(ExampleCoroutine());

        }

    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                contactPoints.Add(collision.contacts[i]);
            }

            player.scoreHealth1 -= 10;
            player.UpdateScore();

            // Print the normal of the first point in the collision.
            UnityEngine.Debug.Log("Normal of the first point: " + contactPoints[0].normal);

            collision.rigidbody.AddForce(Vector3.Reflect(Vector3.Normalize(collision.rigidbody.velocity), -contactPoints[0].normal) * strengthBounce, ForceMode.Impulse);
            // Debug.DrawRay(contactPoints[0].point, -contactPoints[0].normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);

            contactPoints.Clear();
        }

        if(collision.gameObject.CompareTag("WallofBox") )
        {


            BoxRb.velocity = Vector3.zero;
            BoxRb.angularVelocity = Vector3.zero;
            time = 0;

            if (collision.gameObject.GetComponent<MoveAroundSc>().permissionChange1 == false && collision.gameObject.GetComponent<MoveAroundSc>().range == range && 
                collision.gameObject.GetComponent<MoveAroundSc>().permissionChange2 == false)
            {
                permissionChange1 = true;
               


                if (collision.gameObject.GetComponent<MoveAroundSc>().velocity < velocity)
                {

                    range = ChangeDirection(range); 
                }
                else { 
                   
                    collision.gameObject.GetComponent<MoveAroundSc>().range = ChangeDirection(collision.gameObject.GetComponent<MoveAroundSc>().range);
                   

                }


                
            }


            if (collision.gameObject.GetComponent<MoveAroundSc>().permissionChange1 == false && permissionChange1 == false)
            {
                permissionChange2 = true;
               
                range = ChangeDirection(range);
               
            }

            
        }

    }


    private void movingAround(ref float time, ref int range)
    {

        if (time > 5)
        {
             range = Random.Range(0, 4);
             time = 0;
            BoxRb.velocity = Vector3.zero;
            BoxRb.angularVelocity = Vector3.zero;
        } 
        switch (range)
        {
            case 0: { 

                    BoxRb.AddForce(Vector3.forward * speedCube * Time.deltaTime, ForceMode.Impulse); 
                
                     }  
             break;
            case 1: { BoxRb.AddForce(Vector3.back * speedCube * Time.deltaTime, ForceMode.Impulse); }  
                
            break;

            case 2: BoxRb.AddForce(Vector3.right * speedCube * Time.deltaTime, ForceMode.Impulse); break;

            case 3: BoxRb.AddForce(Vector3.left * speedCube * Time.deltaTime, ForceMode.Impulse); break;
        }
        ResetPosition(ref range);


    }

    private void ResetPosition(ref int range)
    {
       

        if(gameObject.transform.position.z < zDownRange || gameObject.transform.position.z > zUpRange)
        {
            BoxRb.velocity = Vector3.zero;
            BoxRb.angularVelocity = Vector3.zero;
            if (gameObject.transform.position.z > 0) range = 1;
            else range = 0;
            time = 0;

            //  transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(-7.0f, 7f));
        }
        if (gameObject.transform.position.x < xLeftRange || gameObject.transform.position.x > xRightRange)
        {

            BoxRb.velocity = Vector3.zero;
            BoxRb.angularVelocity = Vector3.zero;
            if (gameObject.transform.position.x > 0) range = 3;
            else range = 2;
            time = 0;

            // transform.position = new Vector3(Random.Range(-10f, 10f), transform.position.y,transform.position.z);
        }


    }

    private int ChangeDirection(int direction)
    {
        int count = direction;
        switch (count)
        {
            case 0: count = 1; break;
                case 1: count = 0; break;
                case 2: count = 3; break;
                case 3: count = 2; break;
        }
        return count;
    }

    IEnumerator ExampleCoroutine()
    {

        yield return new WaitForSeconds(0.2f);
        permissionChange1 = false;
        permissionChange2 = false;
    }




}

