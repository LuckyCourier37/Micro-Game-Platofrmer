using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;


public class ConditionForEnd : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private SecondPlayerSc player;
    private bool permission = true;
    private Stopwatch timerForRemove;
    private GameObject[] particles;
    private List<GameObject> particles2 = new List<GameObject>();

    // Update is called once per frame
    private void Start()
    {
       timerForRemove = Stopwatch.StartNew();
    }

    private void FixedUpdate()
    {
        RemoveExplosives();
    }
    private void OnCollisionEnter(Collision collision)
    {
       
            if (collision.gameObject.CompareTag("Player"))
            {
               if (permission)
              {
                permission = false;
                StartCoroutine(SubmitFinish());

              }

            }
        
    }

    IEnumerator SubmitFinish()
    {
        yield return new WaitForSeconds(1f);

        player.FinishGame();
        
    }
    
    private void RemoveExplosives()
    {
        if (timerForRemove.Elapsed.TotalSeconds > 4f)
        {
            particles = GameObject.FindGameObjectsWithTag("Explosive");
            if (particles != null && particles.Length > 0 )
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    particles2.Add(particles[i]);
                    Destroy(particles[i]);
                }
            }
           

            timerForRemove.Reset();
            timerForRemove.Start();
            particles2.Clear();


        }
    }
}
