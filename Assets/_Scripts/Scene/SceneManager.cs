using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;  // This is to reference the Unity Editor

public class SceneManager : MonoBehaviour
{
    public GameObject settingOption;
    public GameObject MenuSetting;
    public GameObject TitleImage;
    public GameObject HelpSetting;
    public GameObject HelpButton;

    private AudioManager audiomanager;
    private void Start()
    {
        settingOption.SetActive(false);
        HelpSetting.SetActive(false);
        MenuSetting.SetActive(true);
        TitleImage.SetActive(true);

        audiomanager = GetComponent<AudioManager>();

    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        AudioManager.Instance.PlayMusic("LevelMusic");
    }

    public void Quit()
    {
        Application.Quit();

    }

    public void Options()
    {
        settingOption.SetActive(true);
        MenuSetting.SetActive(false);
        TitleImage.SetActive(false);
        HelpSetting.SetActive(false);
        HelpButton.SetActive(false);
    }

    public void Menu()
    {
        settingOption.SetActive(false);
        MenuSetting.SetActive(true);
        TitleImage.SetActive(true);
        HelpSetting.SetActive(false);
        HelpButton.SetActive(true);


    }

    public void Help()
    {
        settingOption.SetActive(false);
        MenuSetting.SetActive(false);
        TitleImage.SetActive(false);
        HelpSetting.SetActive(true);
        HelpButton.SetActive(false);

    }


    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighCount");
    }


}
   
