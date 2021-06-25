using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULTanksZombies.Music;

namespace ULTankZombies.Tank
{
    public class MoveState : TankState
    {

        private Rigidbody rb;
        private float horizontalMovement;
        private float verticalMovement;
        private float speed;
        private float rotationSpeed;
        private bool attackStateComming;

        //private Vector3 mousePosition;

        public MoveState(TankController controller, TankStateMachine fsm) : base(controller, fsm)
        {
            rb = controller.GetComponent<Rigidbody>();
            speed = controller.speed;
            rotationSpeed = controller.rotationSpeed;
            attackStateComming = false;
        }

        public override void OnHandleInput()
        {
            horizontalMovement=Input.GetAxis("Horizontal");
            verticalMovement = Input.GetAxis("Vertical");

            if (Input.GetMouseButtonDown(0))
            {
                attackStateComming = true;
                controller.Attack(false);

            }
            if (Input.GetMouseButtonDown(1))
            {
                attackStateComming = true;
                controller.Attack(true);

            }
            if (horizontalMovement == 0f && verticalMovement == 0f)
            {
                controller.Stop();
            }
        }

        public override void OnPhysicsUpdate()
        {
            Vector3 movement = controller.transform.forward*
                verticalMovement*controller.speed*Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);

            controller.transform.Rotate(Vector3.up*horizontalMovement*rotationSpeed);
        }
        public override void OnEnter() 
        {
            if (!attackStateComming)
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("TankRunnig");
                attackStateComming = false;
            }
            
        }



        public override void OnExit() 
        {
            if (!attackStateComming)
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Stop("TankRunnig");
            
        }
    }
}

