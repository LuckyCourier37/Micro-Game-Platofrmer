using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFacePosition : MonoBehaviour
{
    [SerializeField] private Vector3 center;

    [SerializeField] private List<Vector3> boundsFigure = new List<Vector3>();
    void Start()
    {
        // Получаем Renderer объекта, чтобы использовать его Bounds
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            // Получаем Bounds объекта
            Bounds bounds = renderer.bounds;


            boundsFigure.Add(bounds.center);
            boundsFigure.Add(bounds.extents);
            center = bounds.center;
            // Выводим информацию о Bounds в лог

            /* Debug.Log("Bounds Center: " + bounds.center);
             Debug.Log("Bounds Size: " + bounds.size);
             Debug.Log("Bounds Min: " + bounds.min);
             Debug.Log("Bounds Max: " + bounds.max); */
        }
    }

    public List<Vector3> GetCollection()
    {

        return boundsFigure;
    }

}
