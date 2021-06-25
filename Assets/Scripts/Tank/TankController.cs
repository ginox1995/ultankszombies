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


        private IdleState idleState;
        private MoveState moveState;
        private AttackState attackState;

        private void Start()
        {
            fsm = new TankStateMachine();
            moveState = new MoveState(this, fsm);
            idleState = new IdleState(this, fsm);
            attackState = new AttackState(this, fsm);

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
    }
}

