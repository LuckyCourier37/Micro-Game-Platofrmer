using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCrate : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator playerAnim;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetBool("IsTalking" , true);

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAnim.SetFloat("movespeed", 1);

        }


    }
}
