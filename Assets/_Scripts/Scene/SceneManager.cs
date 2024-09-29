using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;  // This is to reference the Unity Editor

public class SceneManager : MonoBehaviour
{ 
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
        Application.Quit();

    }

    public void Options()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");

    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }


}
   
