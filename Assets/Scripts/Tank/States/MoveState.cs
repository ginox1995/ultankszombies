using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULTanksZombies.Tank
{
    public class MoveState : TankState
    {
        private AudioSource audioSource;
        private Rigidbody rb;
        private float horizontalMovement;
        private float verticalMovement;
        private float speed;
        private float rotationSpeed;
        private float fireRate;
        private float specialFireRate;

        //private Vector3 mousePosition;
        private bool shoot = false;
        private float countdown = 0f;
        private bool specialShoot = false;
        private float specialCountdown = 0f;

        public MoveState(TankController controller, TankStateMachine fsm) : base(controller, fsm)
        {
            rb = controller.GetComponent<Rigidbody>();
            speed = controller.speed;
            rotationSpeed = controller.rotationSpeed;
            fireRate = controller.fireRate;
            specialFireRate = controller.specialFireRate;
            audioSource = controller.GetComponent<AudioSource>();
        }

        public override void OnHandleInput()
        {
            base.OnHandleInput();
            horizontalMovement =  Input.GetAxis("Horizontal");
            verticalMovement = Input.GetAxis("Vertical");

            if (Input.GetMouseButtonDown(0) && countdown <= 0f)
            {
                //Fire
                shoot = true;
                countdown = 1f / fireRate;
            }
            countdown -= Time.deltaTime;

            if (Input.GetMouseButtonDown(1) && specialCountdown <= 0f)
            {
                //Fire
                specialShoot = true;
                specialCountdown = 1f / specialFireRate;
            }
            specialCountdown -= Time.deltaTime;

        }

        public override void OnLogicUpdate()
        {
            base.OnLogicUpdate();
            if (shoot)
            {
                controller.Fire(false);
                shoot = false;
            }
            if (specialShoot)
            {
                controller.Fire(true);
                specialShoot = false;
            }
            if (horizontalMovement == 0f || verticalMovement == 0f)
            {
                Debug.Log("stop");
                controller.Stop();
            }
        }

        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();

            Vector3 movement = controller.transform.forward
                * verticalMovement * controller.speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);


            controller.transform.Rotate(Vector3.up * horizontalMovement * rotationSpeed);
        }
        public override void OnEnter() 
        {
            audioSource.Play();
        }



        public override void OnExit() 
        {
            audioSource.Stop();
        }
    }
}
