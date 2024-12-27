using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFacePosition : MonoBehaviour
{
    [SerializeField] private Vector3 center;

    [SerializeField] private List<Vector3> boundsFigure = new List<Vector3>();
    void Start()
    {
        // �������� Renderer �������, ����� ������������ ��� Bounds
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            // �������� Bounds �������
            Bounds bounds = renderer.bounds;


            boundsFigure.Add(bounds.center);
            boundsFigure.Add(bounds.extents);
            center = bounds.center;
            // ������� ���������� � Bounds � ���

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
