using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTrap : MonoBehaviour
{
    // Start is called before the first frame update
    private float rotationSpeed;
    private float impulseForce = 30f;

    void Start()
    {
        rotationSpeed = 400f;
        if (gameObject.name == "Props_1a")
        {
            rotationSpeed = 500f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime , Space.Self);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Проверяем, что столкнулись с шариком
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // Определяем среднюю точку контакта
                ContactPoint contact = collision.contacts[0];
                
                // Вычисляем направление импульса — это нормаль к поверхности в точке столкновения
                Vector3 impulseDirection = contact.normal;
               // Debug.Log(impulseDirection);
               // Debug.DrawLine(collision.gameObject.transform.position, impulseDirection * 6f, Color.white, 2.5f);
                // Добавляем импульсную силу к шарику
                ballRigidbody.AddForce(impulseDirection * impulseForce, ForceMode.Impulse);
                ballRigidbody.AddForce(Vector3.up * 6f, ForceMode.Impulse);
            }
        }
    }
}
