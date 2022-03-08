using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;  // to freeze the real time
    }
    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
