using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperSideCopy : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshCollider side;
    public DeleteAssetCopy script;
    // Start is called before the first frame update
    void Start()
    {
        side = GetComponent<MeshCollider>();
        side.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (script.permissionAnim) side.enabled = true;
    }
}
