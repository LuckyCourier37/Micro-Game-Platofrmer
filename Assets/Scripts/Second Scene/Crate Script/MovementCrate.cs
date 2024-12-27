using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCrate : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody BoxRb;
    [SerializeField] private int speedCube = 20;
    private CubeFacePosition shape;
    private SecondPlayerSc player;

    private List<Vector3> shapeList;
    private bool permissionForMove;
    private float direction = 1;
    private float multiplier = 1;
    private float strengthBounce = 8.5f;

    private List<ContactPoint> contactPoints = new List<ContactPoint>();
    void Start()
    {
        BoxRb = GetComponent<Rigidbody>();
        permissionForMove = false;
        if(gameObject.name == "Crate red") multiplier = 0.6f;
        if (gameObject.name == "Crate White") multiplier = 0.8f;

        player = GameObject.Find("Player").GetComponent<SecondPlayerSc>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BackAndForth(shapeList);
    }

    private void BackAndForth(List<Vector3> parametar)
    {
        if (shapeList != null && permissionForMove)
        {
            if(transform.position.x > parametar[0].x + 0.85f * parametar[1].x * multiplier)
            {
                direction = -1;
            }
            if(transform.position.x < parametar[0].x - 0.75f * parametar[1].x) direction = 1;

            BoxRb.AddForce(Vector3.right * speedCube * direction * Time.deltaTime, ForceMode.Impulse);

        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") && !permissionForMove)
        {
            shape = collision.gameObject.GetComponent<CubeFacePosition>();
            if (shape != null)
            {
                shapeList = shape.GetCollection();
                permissionForMove = true;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                contactPoints.Add(collision.contacts[i]);
            }

            player.scoreHealth1 -= 10;
            player.UpdateScore();

            // Print the normal of the first point in the collision.
            Debug.Log("Normal of the first point: " + contactPoints[0].normal);

            collision.rigidbody.AddForce(Vector3.Reflect(Vector3.Normalize(collision.rigidbody.velocity), -contactPoints[0].normal) * strengthBounce, ForceMode.Impulse);
            // Debug.DrawRay(contactPoints[0].point, -contactPoints[0].normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);

            contactPoints.Clear();
        }
    }
} 
