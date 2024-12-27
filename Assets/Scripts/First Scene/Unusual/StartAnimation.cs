using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem particle;
    void Start()
    {
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
        particle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
