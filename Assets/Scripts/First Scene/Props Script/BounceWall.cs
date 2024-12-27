using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWall : MonoBehaviour
{


    public bool work = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Player") && collision.gameObject.transform.position.x > 16)
        {
            collision.rigidbody.AddForce(Vector3.left * 10, ForceMode.Impulse);

        } else if (collision.gameObject.CompareTag("Player") && collision.gameObject.transform.position.x < -16)
        {
            collision.rigidbody.AddForce(Vector3.right * 10, ForceMode.Impulse);
        }
    }

    
}
