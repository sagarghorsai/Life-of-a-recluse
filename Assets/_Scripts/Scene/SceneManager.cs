using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;  // This is to reference the Unity Editor

public class SceneManager : MonoBehaviour
{
    public GameObject settingOption;
    public GameObject MenuSetting;


    private void Start()
    {
        settingOption.SetActive(false);
        MenuSetting.SetActive(true);

    }
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
        settingOption.SetActive(true);
        MenuSetting.SetActive(false);
    }

    public void Menu()
    {
        settingOption.SetActive(false);
        MenuSetting.SetActive(true);
    }


}
   
