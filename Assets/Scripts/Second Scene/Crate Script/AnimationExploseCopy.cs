using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExploseCopy : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem explosion;
    public DeleteAssetCopy script;
    private bool permissionAnim = false;

    // Update is called once per frame
    void Update()
    {
        if (script.permissionAnim && !permissionAnim)
        {

            StartCoroutine(Instantaite());
            permissionAnim = true;
        }


    }

    IEnumerator Instantaite()
    {
        yield return new WaitForSeconds(0.6f);
        Instantiate(explosion, gameObject.transform.position, explosion.transform.rotation);
    }
}
