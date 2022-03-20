using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject attackDecal;

    private float speed = 100f;
    private float lifeTime = 5f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < 0.005f)
        {
            Destroy(gameObject);
            print("max range reached");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //ContactPoint contact = other.GetContact(0);
        //GameObject.Instantiate(attackDecal, contact.point, Quaternion.LookRotation(contact.normal));

        if (other.CompareTag("Environment"))
        {
            Destroy(gameObject);
            print("collided with something");
        }

    }
}
