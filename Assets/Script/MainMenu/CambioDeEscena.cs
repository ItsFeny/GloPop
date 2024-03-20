using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    // Este m�todo ser� llamado cuando se haga clic en el bot�n
    public void CambiarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Carga la escena especificada
    }

    public void Salir()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
