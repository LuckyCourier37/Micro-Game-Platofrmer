using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPowerUP : MonoBehaviour
{
    // Start is called before the first frame update
    private float amplitude = 0.5f;  // Амплитуда покачивания (насколько высоко/низко двигается)
    private float frequency = 3f;    // Частота покачивания (насколько быстро движется)

    private Vector3 startPosition;  // Начальная позиция объекта
    [SerializeField] private GameObject TitleDoubleJ;

    void Start()
    {
        // Сохраняем начальную позицию Power-up
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Обновляем позицию по вертикали, добавляя синусоидальное движение к начальной позиции
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TitleDoubleJ.SetActive(true);
            StartCoroutine(ExcludeTitle());
           gameObject.GetComponent<Renderer>().enabled = false; 
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    IEnumerator ExcludeTitle()
    {
        yield return new WaitForSeconds(2f);
        TitleDoubleJ.SetActive(false);
        Destroy(gameObject);
    }
}
