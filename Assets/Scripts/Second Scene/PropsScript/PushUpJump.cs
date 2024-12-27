using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushUpJump : MonoBehaviour
{

    private float expansionSpeed = 40.0f;  // Скорость расширения
    private float maxScaleY = 1.0f; // Максимальный масштаб по оси Y
    private float minScaleY = 0.10f;
   
    [SerializeField] private bool isCompressing = false;

    private bool isExpanding = false;     // Флаг для отслеживания состояния
    private Vector3 startScale;
    
    private void Start()
    {
      startScale = transform.localScale;
    }
    void FixedUpdate()
    {
       Expanding();


        CompressingRun();

        
    }

    // Метод для запуска расширения
    private void StartExpansion()
    {
        isExpanding = true;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
           var Rb = collision.rigidbody;
            
            Rb.velocity = new Vector3 (0f , Rb.velocity.y, 0);
            Rb.angularVelocity = new Vector3(0f, Rb.angularVelocity.y, 0);
            Rb.AddForce(new Vector3(-0.05f, 1f, 0f) * 950f * Time.deltaTime, ForceMode.Impulse);
           
            StartCoroutine(DelayForJump());

        }

    }

    IEnumerator DelayForJump( )
    {

        yield return new WaitForSeconds(0.1f);
        StartExpansion();
        
    }
    IEnumerator DelayForCompress()
    {

        yield return new WaitForSeconds(0.1f);
        isCompressing = true;

    }


    private void Expanding()
    {
        if (isExpanding && !isCompressing)
        {
           
           
            // transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, maxScaleY, expansionSpeed * Time.deltaTime), transform.localScale.z);

            // Остановка при достижении максимального размера
            if (transform.localScale.y >= maxScaleY)
            {
                isExpanding = false;
                isCompressing = true;
                Debug.Log("IsCompressing" + isCompressing);
            }
        }
    }

    private void CompressingRun()
    {
        if (isCompressing)
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, minScaleY, expansionSpeed * Time.deltaTime), transform.localScale.z);

            if (transform.localScale.y <= minScaleY)
            {

                isCompressing = false;
            }
        }
       
    }


}
