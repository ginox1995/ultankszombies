using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULTanksZombies.Tank
{
    [RequireComponent(typeof(Rigidbody))]
    public class TankController : MonoBehaviour
    {
        public float speed;
        public float rotationSpeed;
        public GameObject bulletPrefab;
        public GameObject specialBulletPrefab;
        public float fireRate;
        public float specialFireRate;

        private TankStateMachine fsm;
        private Transform firePoint;

        //private IdleState idleState;
        private MoveState moveState;

        private void Start()
        {
            fsm = new TankStateMachine();
            moveState = new MoveState(this, fsm);

            fsm.Start(moveState);

            firePoint = transform.GetChild(0).GetChild(0).transform;
        }

        private void Update()
        {
            fsm.CurrentState.OnHandleInput();
            fsm.CurrentState.OnLogicUpdate();
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
