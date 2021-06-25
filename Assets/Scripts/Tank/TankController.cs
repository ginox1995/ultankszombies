using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULTankZombies.Tank
{
    [RequireComponent(typeof(Rigidbody))]
    public class TankController : MonoBehaviour
    {
        public float speed;
        public float rotationSpeed;
        private TankStateMachine fsm;
        private Transform firePoint;
        public GameObject bulletPrefab;
        public GameObject specialBulletPrefab;
        public float fireRate;
        public float specialFireRate;
        private float damaged=3;
        private SpriteRenderer heart1;
        private SpriteRenderer heart2;
        private SpriteRenderer heart3;

        private IdleState idleState;
        private MoveState moveState;
        private AttackState attackState;

        private void Start()
        {
            fsm = new TankStateMachine();
            moveState = new MoveState(this, fsm);
            idleState = new IdleState(this, fsm);
            attackState = new AttackState(this, fsm);
            heart1 = GameObject.Find("Heart1").GetComponent<SpriteRenderer>();
            heart2 = GameObject.Find("Heart2").GetComponent<SpriteRenderer>();
            heart3 = GameObject.Find("Heart3").GetComponent<SpriteRenderer>();

            fsm.Start(idleState);

            firePoint = transform.GetChild(0).GetChild(0).transform;
        }

        private void Update()
        {
            fsm.CurrentState.OnHandleInput();
            fsm.CurrentState.OnLogicUpdate();
        }

        public void Move()
        {
            fsm.ChangeState(moveState);
        }

        public void Stop()
        {
            fsm.ChangeState(idleState);
        }

        public void Attack(bool attackType)
        {
            fsm.ChangeState(attackState, attackType);
        }

        private void FixedUpdate()
        {
            fsm.CurrentState.OnPhysicsUpdate();
        }

        public void Fire(bool specialBullet)
        {

            GameObject prefab = (specialBullet) ? specialBulletPrefab : bulletPrefab;
            
            Instantiate(prefab, firePoint.position, firePoint.rotation);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Zombie") || collision.gameObject.CompareTag("ZombieHorde"))
            {
                if (damaged == 1)
                    Destroy(gameObject);

                Damaged(damaged);
                damaged -= 1;
                
                
            }
        }
        private void Damaged(float heart)
        {
            switch (heart)
            {
                case 1:
                    heart1.enabled = false;
                    break;
                case 2:
                    heart2.enabled = false;
                    break;
                case 3:
                    heart3.enabled = false;
                    break;
            }
        }

    }
}

