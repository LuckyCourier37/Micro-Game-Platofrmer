using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCrateCopy : MonoBehaviour
{
    private Animation anim;
    private DeleteAssetCopy side;
    private bool work = false;
    void Start()
    {
        anim = GetComponent<Animation>();
        side = GetComponentInChildren<DeleteAssetCopy>();


    }

    // Update is called once per frame
    void Update()
    {
        if (side.permissionAnim && !work)
        {

            work = true;
            AnimChange();
        }


    }


    IEnumerator ChangeAnim()
    {
        yield return new WaitForSeconds(1.2f);


    }

    private void AnimChange()
    {
        //  anim.Play("Crate_Close", PlayMode.StopSameLayer);
        anim.Play("Crate_Open", PlayMode.StopSameLayer);
    }
}
