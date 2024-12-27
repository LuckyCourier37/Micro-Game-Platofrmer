using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAsset : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boxless;
    public bool permissionAnim = false;
    private Collider side;
    private Collider colliderParent;
    private MoveAroundSc scriptParent;
    private PlayerController player;
    void Start()
    {
      side =   GetComponent<Collider>();
        colliderParent = gameObject.GetComponentInParent<Collider>(false);

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            permissionAnim = true;
            StartCoroutine(Destroying());
            side.isTrigger = false;
            colliderParent.enabled = false;
            player.scoreHealth1 += 10;
        }
    }


    IEnumerator Destroying()
    {
      
        
        yield return new WaitForSeconds(0.9f);
        
        Destroy(gameObject);
        Destroy(boxless);
            
        
    }


}

