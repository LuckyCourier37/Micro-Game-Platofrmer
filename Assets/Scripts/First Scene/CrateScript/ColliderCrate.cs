using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCrate : MonoBehaviour
{
    // Start is called before the first frame update

    public Animation anim;
    private DeleteAsset side;
    private bool work = false;
    void Start()
    {
        anim = GetComponent<Animation>();
        side = GetComponentInChildren<DeleteAsset>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if( side.permissionAnim && !work)
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
