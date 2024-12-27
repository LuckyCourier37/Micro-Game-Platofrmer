using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Rotary : MonoBehaviour
{
    // Start is called before the first frame update
    private float rotationSpeed = 50f;
    private Stopwatch timer;
    private bool StopRotate;
    private int way = 0;
    void Start()
    {
        StopRotate = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (StopRotate) gameObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);

        if (transform.rotation.eulerAngles.y > 270f && way < 1 )
        {
            StopRotate = false;
            StartCoroutine(Timer());
            way++;
        }
        if (transform.rotation.eulerAngles.y < 90f && transform.rotation.eulerAngles.y > 88f && way == 1)
        {
            StopRotate = false;
            StartCoroutine(Timer());
            way = 0;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
        StopRotate = true;
    }
}
