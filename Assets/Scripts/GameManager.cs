using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ULTankZombies
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance { get { return instance; } }
        public gameOverManager managerGameOver;
        public AudioSource HordeStartSound;
        public AudioSource CountdownAudio;
        public GameObject[] zombies;
        public Transform[] hordeSpawnPoints;
        private int hordeNumber = 5;
        private List<GameObject> zombiesHorde = new List<GameObject>();
        private float spawnZombieTime = 3;
        private float spawnHordeTime = 60;
        public GameObject zombieFast;
        public GameObject zombieFat;
        private float timerToSpawnZombie=0f;
        private bool mausoleumSupDerUp;
        private bool mausoleumSupIzqUp;
        private bool mausoleumInfIzqUp;
        private bool mausoleumInfDerUp;
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
        }
        private void Start()
        {
            mausoleumSupDerUp=true;
            mausoleumSupIzqUp = true;
            mausoleumInfIzqUp = true;
            mausoleumInfDerUp = true;
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
            //Esta parte del código es para activar el gameover, tanto para cuando se gana o pierde detectando si el tanque fue destruido o si los mausoleos fueron destruidos
            if (GameObject.Find("Tank")==null)
            {
                managerGameOver.SetupDefeat(Score);
            }
            else if(mausoleumSupDerUp==false && mausoleumSupIzqUp==false && mausoleumInfIzqUp==false && mausoleumInfDerUp==false)
            {
                managerGameOver.SetupVictory(Score);
            }
            //Añade los booleanos para que el juego sepa que los mausoleos fueron destruidos.
            if (GameObject.Find("MausoleumSupDer")==null)
            {
                mausoleumSupDerUp = false;
            }
            if (GameObject.Find("MausoleumSupIzq") == null)
            {
                mausoleumSupIzqUp = false;
            }
            if (GameObject.Find("MausoleumInfIzq") == null)
            {
                mausoleumInfIzqUp = false;
            }
            if (GameObject.Find("MausoleumInfDer") == null)
            {
                mausoleumInfDerUp = false;
            }
            //Muestra del score del juego.
            ScoreText.text = "Zombies eliminados: " + Score;
            //Detecta si hay una horda en progreso buscando el tag de horda, si aun hay un zombie de horda vivo, la nueva horda no se iniciará hasta que todos los zombies de la horda se destruyan
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
            //Mensajes de la horda
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
            //Activación del countdown de la Horda.
            if (spawnHordeTime < 10.5f && spawnHordeTime>10f)
            {

                PlayCountdown = true;
            }
            if (PlayCountdown)
            {
                CountdownAudio.Play();
                PlayCountdown = false;
            }
            //Spawneo de la horda y de los zombies regulares.
            if (timerToSpawnZombie>spawnZombieTime)
            {
                SpawnZombies();
                timerToSpawnZombie = 0;
            }
            timerToSpawnZombie += Time.deltaTime;
            if (spawnHordeTime<0 && !HordaEnProgreso)
            {
                spawnHordeTime = 60;
                HordeStartSound.Play();
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
            float espacio = 0.5f;
            Vector3 spawnHordePosition = new Vector3(0, 0.5f, 0);
            
            if (tank.position.x > 0 && tank.position.z > 0)
            {
                if (mausoleumSupDerUp)
                {
                    spawnHordePosition = new Vector3(hordeSpawnPoints[0].position.x, 0.5f, hordeSpawnPoints[0].position.z - 3.2f);

                }
                else
                {
                    spawnHordePosition = new Vector3(0, 0.5f, 0);
                }
                for (int i = 0; i < zombiesHorde.Count; i++)
                {
                    Instantiate(zombiesHorde[i], spawnHordePosition, Quaternion.identity);
                    spawnHordePosition = new Vector3(spawnHordePosition.x - espacio, 0.5f, spawnHordePosition.z);
                    espacio++;
                }

            }
            else if (tank.position.x < 0 && tank.position.z > 0)
            {
                if (mausoleumSupIzqUp)
                {
                    spawnHordePosition = new Vector3(hordeSpawnPoints[1].position.x, 0.5f, hordeSpawnPoints[1].position.z - 3.2f);
                }
                else
                {
                    spawnHordePosition = new Vector3(0, 0.5f, 0);
                }
                for (int i = 0; i < zombiesHorde.Count; i++)
                {
                    Instantiate(zombiesHorde[i], spawnHordePosition, Quaternion.identity);
                    spawnHordePosition = new Vector3(spawnHordePosition.x + espacio, 0.5f, spawnHordePosition.z);
                    espacio++;
                }
            }
            else if (tank.position.x < 0 && tank.position.z < 0)
            {
                if (mausoleumInfIzqUp)
                {
                    spawnHordePosition = new Vector3(hordeSpawnPoints[2].position.x, 0.5f, hordeSpawnPoints[2].position.z - 3.2f);
                }
                else
                {
                    spawnHordePosition = new Vector3(0, 0.5f, 0);
                }
                for (int i = 0; i < zombiesHorde.Count; i++)
                {
                    Instantiate(zombiesHorde[i], spawnHordePosition, Quaternion.identity);
                    spawnHordePosition = new Vector3(spawnHordePosition.x + espacio, 0.5f, spawnHordePosition.z);
                    espacio++;
                }

            }
            else if (tank.position.x > 0 && tank.position.z < 0)
            {
                if (mausoleumInfDerUp)
                {
                    spawnHordePosition = new Vector3(hordeSpawnPoints[3].position.x, 0.5f, hordeSpawnPoints[3].position.z - 3.2f);
                }
                else
                {
                    spawnHordePosition = new Vector3(0, 0.5f, 0);
                }
                for (int i = 0; i < zombiesHorde.Count; i++)
                {
                    Instantiate(zombiesHorde[i], spawnHordePosition, Quaternion.identity);
                    spawnHordePosition = new Vector3(spawnHordePosition.x - espacio, 0.5f, spawnHordePosition.z);
                    espacio++;
                }
            }
            else if (tank.position.x == 0 && tank.position.z == 0)
            {
                for (int i = 0; i < zombiesHorde.Count; i++)
                {
                    Instantiate(zombiesHorde[i], spawnHordePosition, Quaternion.identity);
                    spawnHordePosition = new Vector3(spawnHordePosition.x + espacio - espacio, 0.5f, spawnHordePosition.z - espacio);
                    espacio++;
                }
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

