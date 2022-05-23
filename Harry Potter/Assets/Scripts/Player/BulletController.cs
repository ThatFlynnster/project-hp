using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HP
{
    public class BulletController : MonoBehaviour
    {
        public BaseStats stats;

        [SerializeField]
        private GameObject attackDecal;

        private float speed;
        private AnimationCurve speedCurve;
        private float maxLifeTime = 5f;
        private float lifeTime = 0f;

        public Vector3 target { get; set; }
        public bool hit { get; set; }

        private void OnEnable()
        {
            Destroy(gameObject, maxLifeTime);
        }

        void Start()
        {
            speedCurve = stats.atkVelocity;
        }

        void Update()
        {
            lifeTime += Time.deltaTime;
            speed = speedCurve.Evaluate(lifeTime);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (!hit && Vector3.Distance(transform.position, target) < 0.005f)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            //ContactPoint contact = other.GetContact(0);
            //GameObject.Instantiate(attackDecal, contact.point, Quaternion.LookRotation(contact.normal));

            Destroy(gameObject);
        }
    }
}