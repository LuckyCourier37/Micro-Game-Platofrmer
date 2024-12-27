using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamage : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    private SecondPlayerSc player;

        void Start() 
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SecondPlayerSc>();
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            
            collision.rigidbody.AddForce(Vector3.up * 550f * Time.deltaTime, ForceMode.Impulse);
            player.scoreHealth1 -= 10;
            player.UpdateScore();
           
        }

    }
}
