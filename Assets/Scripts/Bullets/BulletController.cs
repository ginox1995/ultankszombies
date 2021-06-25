using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULTanksZombies.Music;

namespace ULTanksZombies.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletController : MonoBehaviour
    {
        public float force;
        private float currentDurability;
        public float maxDurability;
        
        void Start()
        {
            currentDurability = 0;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Zombie"))
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("ZombieShooted");
                currentDurability += 1;
            }
            if ((!collision.gameObject.CompareTag("Player") && currentDurability == maxDurability) || collision.gameObject.CompareTag("Ground"))
                Destroy(gameObject);
            

        }

    }
}
