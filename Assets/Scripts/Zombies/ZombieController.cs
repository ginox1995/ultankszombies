using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULTanksZombies.Music;

namespace ULTanksZombies.Zombies
{
    public class ZombieController : MonoBehaviour
    {
        
        public Transform tank;
        public SettingsComponent settings;
        GameManager gm;
        private ZombieStateMachine fsm;
        private ChasingState chasingState;
        private void Start()
        {
            if (tank==null)
            {
                //tank=GameObject.Find("Tank").transform;
                tank = GameManager.Instance.tank;
            }

            fsm = new ZombieStateMachine();
            chasingState = new ChasingState(this,fsm);
            fsm.Start(chasingState);
            gm = FindObjectOfType<GameManager>();
            
        }
        private void Update()
        {
            fsm.CurrentState.onHandleInput();
            fsm.CurrentState.onLogicUpdate();
        }
        private void FixedUpdate()
        {
            fsm.CurrentState.onPhysicsUpdate();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(gameObject);
                gm.GiveScore();

            }
        }
    }
}

