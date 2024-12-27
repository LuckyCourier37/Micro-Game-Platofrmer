using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperSide : MonoBehaviour
{


    private MeshCollider side;
    public DeleteAsset script;
    // Start is called before the first frame update
    void Start()
    {
        side = GetComponent<MeshCollider>();
        side.enabled = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        if( script.permissionAnim) side.enabled = true;
    }
}
