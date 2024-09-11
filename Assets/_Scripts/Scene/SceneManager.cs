using UnityEngine;
using UnityEngine.SceneManagement;

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


}
   
