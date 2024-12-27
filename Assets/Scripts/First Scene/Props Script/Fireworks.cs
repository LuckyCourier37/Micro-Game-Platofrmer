using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    // Start is called before the first frame update
   public List<GameObject> fireworks = new List<GameObject>();
    private PlayerController player;
    private Vector3 location;
    private bool oneIteration = true;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.vanquishGame && oneIteration)
        {
            
            StartCoroutine(SpawnFireworks());
            oneIteration = false;

        }
    }

    IEnumerator SpawnFireworks()
    {
        for (int i = 0; i < fireworks.Count; i++)
        {
            switch (i)
            {
                case 0: location = new Vector3(-11f, 1, -8.5f); break;
                case 1: location = new Vector3(10.5f, 1, -8.5f); break;
                case 2: location = new Vector3(12f, 1, 5.5f); break;
                case 3: location = new Vector3(-12f, 1, 5.5f); break;
                case 4: location = new Vector3(1.5f, 1, 0.15f); break;

            }
            Instantiate(fireworks[i], location, fireworks[i].transform.rotation);
        }

        yield return null;

    }

}


