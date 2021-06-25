using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ULTanksZombies.Music;

namespace ULTankZombies
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance { get { return instance; } }
        public AudioSource HordeStartSound;
        public AudioSource CountdownAudio;
        public GameObject[] zombies;

        private int hordeNumber = 5;
        private List<GameObject> zombiesHorde = new List<GameObject>();
        private float spawnZombieTime = 3;
        private float spawnHordeTime = 60;
        public GameObject zombieFast;
        public GameObject zombieFat;
        private float timerToSpawnZombie=0f;

        public Transform tank;
        [SerializeField] Text countdownHorde;
        [SerializeField] Text ScoreText;
        private bool HordaEnProgreso = false;
        private bool PlayCountdown = false;
        private int Score = 0;
        

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            PlayCountdown = false;
            spawnHordeTime = 60;
        }
        private void Start()
        {
            
            for (int i = 0; i < hordeNumber; i++)
            {
                if (i % 2 == 0)
                {
                    zombiesHorde.Add(zombieFast);
                }
                else
                {
                    zombiesHorde.Add(zombieFat);
                }
            }
        }
        private void Update()
        {
            ScoreText.text = "Zombies eliminados: " + Score;
            //Debug.Log(Score);
            if (GameObject.FindGameObjectsWithTag("ZombieHorde").Length >0)
            {
                HordaEnProgreso = true;

            }
            else if (GameObject.FindGameObjectsWithTag("ZombieHorde").Length == 0)
            {
                HordaEnProgreso = false;
                spawnHordeTime -= 1 * Time.deltaTime;
                countdownHorde.color = Color.black;
            }
            if (!HordaEnProgreso)
            {
                countdownHorde.text = "Horda de zombies spawnea en: " + spawnHordeTime.ToString("0");
            }
            else
            {
                countdownHorde.text = "HORDA ZOMBIE EN PROGRESO!";
            }
            if (spawnHordeTime < 11f || HordaEnProgreso)
            {
                countdownHorde.color = Color.red;
                
            }
            //ARREGLAR COUNTDOWN
            if (spawnHordeTime < 10.5f && spawnHordeTime>10f)
            {

                PlayCountdown = true;
            }
            if (PlayCountdown)
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("CountDown");
                PlayCountdown = false;
            }
            
            if (timerToSpawnZombie>spawnZombieTime)
            {
                SpawnZombies();
                timerToSpawnZombie = 0;
            }
            timerToSpawnZombie += Time.deltaTime;
            if (spawnHordeTime<0 && !HordaEnProgreso)
            {
                spawnHordeTime = 60;
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("ZombieHordeSound");
                SpawnHorde();
                                
            }
            
        }

        private void SpawnZombies()
        {
            int posZombies = Random.Range(0, zombies.Length);
            Vector3 spawnPosition = new Vector3(
                Random.Range(tank.position.x-50, tank.position.x+50),
                0.5f,
                Random.Range(tank.position.z - 50, tank.position.z + 50)
            );
            if (spawnPosition.x>=250)
            {
                spawnPosition.x = tank.position.x - 30;
            }
            else if (spawnPosition.x <= -250)
            {
                spawnPosition.x = tank.position.x + 30;
            }
            if (spawnPosition.z>=250)
            {
                spawnPosition.z = tank.position.z - 30;
            }
            else if (spawnPosition.z<=250)
            {
                spawnPosition.z = tank.position.z + 30;
            }
            Instantiate(zombies[posZombies], spawnPosition, Quaternion.identity);
        }

        private void SpawnHorde()
        {
            float espacio = 1.5f;
            Vector3 spawnHordePosition = new Vector3(
                Random.Range(tank.position.x - 30, tank.position.x + 30),
                0.5f,
                Random.Range(tank.position.z - 30, tank.position.z + 30));
            if (spawnHordePosition.x>=245)
            {
                spawnHordePosition.x = tank.position.x - 50;
            }
            else if (spawnHordePosition.x <=-245)
            {
                spawnHordePosition.x = tank.position.x + 50;
            }
            if (spawnHordePosition.z >= 245)
            {
                spawnHordePosition.z = tank.position.z - 50;
            }
            else if (spawnHordePosition.z <= -245)
            {
                spawnHordePosition.z = tank.position.z + 50;
            }
            for (int i = 0; i < zombiesHorde.Count; i++)
            {
                Instantiate(zombiesHorde[i], spawnHordePosition, Quaternion.identity);
                spawnHordePosition = new Vector3(spawnHordePosition.x+espacio, 0.5f, spawnHordePosition.z+espacio);
                espacio++;
            }
            zombiesHorde.Add(zombieFast);
            zombiesHorde.Add(zombieFat);
        }
        public void GiveScore()
        {
            Score++;
        }
    }
}

