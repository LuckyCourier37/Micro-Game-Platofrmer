using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionStay(Collision collision)
    {
        // ���������, ���� ����� ��������� �� �����
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��������� ����, ����� �������� ������ ��������� �����
            float uphillForce = 6f;
            Vector3 uphillDirection = Vector3.ProjectOnPlane(Vector3.right, collision.contacts[0].normal).normalized;
            // Debug.DrawLine(transform.position , uphillDirection * 8f, Color.black);
            // Debug.Log(uphillDirection);
            collision.rigidbody.AddForce(uphillDirection * uphillForce, ForceMode.Acceleration);
        }
    }
}
