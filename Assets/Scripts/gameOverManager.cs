using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverManager : MonoBehaviour
{
    public Text textoPuntaje;
    public Text textoGameOver;
    
    //SetupDefeat se activa cuando se detecta que el gameObject Tank no existe, dando a entender que fue destruido.
    public void SetupDefeat(int score)
    {
        GameObject.Find("Canvas").SetActive(false);
        gameObject.SetActive(true);
        textoGameOver.text = "¡Has muerto!";
        textoPuntaje.text = "Obtuvieste un score de " + score;
    }
    //SetupVictory se activa cuando se detecta que los 4 gameObjects de mausoleos no existen, dando a entender que fueron destrudios y gano el jugador.
    public void SetupVictory(int score)
    {
        GameObject.Find("Canvas").SetActive(false);
        gameObject.SetActive(true);
        textoGameOver.text = "¡Has ganado!";
        textoPuntaje.text = "Obtuvieste un score de " + score;
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Juego");
    }
    public void MenuButton() 
    {
        SceneManager.LoadScene("Menu");
    }
}
