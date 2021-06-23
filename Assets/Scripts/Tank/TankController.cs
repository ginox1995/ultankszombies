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
        private Transform firepoint;
        public GameObject bulletPrefab;
        public float fireRate;


        //private IdleState idleState;
        private MoveState moveState;

        private void Start()
        {
            fsm = new TankStateMachine();
            moveState = new MoveState(this,fsm);

            fsm.Start(moveState);
            firepoint = transform.GetChild(0).GetChild(0).transform;
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
        public void Fire()
        {
            Instantiate(bulletPrefab,firepoint.position,firepoint.rotation);
        }
    }
}

