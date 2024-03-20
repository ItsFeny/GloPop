using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f; 
        isPaused = true;
        Debug.Log("Juego pausado");
    }

    void Resume()
    {
        Time.timeScale = 1f; 
        isPaused = false;
        Debug.Log("Juego reanudado");
    }
}
