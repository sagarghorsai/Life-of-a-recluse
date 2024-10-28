using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingOption;
    public GameObject MenuSetting;
    public GameObject TitleImage;
    public GameObject HelpSetting;
    public GameObject ExtraSetting;
    public GameObject Credits;
    private AudioManager audiomanager;
    private void Start()
    {
        settingOption.SetActive(false);
        HelpSetting.SetActive(false);
        Credits.SetActive(false);
        MenuSetting.SetActive(true);
        TitleImage.SetActive(true);

        audiomanager = GetComponent<AudioManager>();

    }
  

    public void Options()
    {
        settingOption.SetActive(true);
        MenuSetting.SetActive(false);
        TitleImage.SetActive(false);
        HelpSetting.SetActive(false);
        ExtraSetting.SetActive(false);
        Credits.SetActive(false);

    }

    public void Menu()
    {
        settingOption.SetActive(false);
        MenuSetting.SetActive(true);
        TitleImage.SetActive(true);
        HelpSetting.SetActive(false);
        ExtraSetting.SetActive(true);
        Credits.SetActive(false);



    }

    public void Help()
    {
        settingOption.SetActive(false);
        MenuSetting.SetActive(false);
        TitleImage.SetActive(false);
        HelpSetting.SetActive(true);
        ExtraSetting.SetActive(false);
        Credits.SetActive(false);


    }
    public void Credit()
    {
        settingOption.SetActive(false);
        MenuSetting.SetActive(false);
        TitleImage.SetActive(false);
        HelpSetting.SetActive(false);
        ExtraSetting.SetActive(false);
        Credits.SetActive(true);


    }
    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighCount");
        DayCounter.Instance.HighCount = 1; // Reset HighCount to default in the script
        PlayerPrefs.Save();
        Debug.Log("High score reset.");
    }

}

