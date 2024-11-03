using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuTitle;
    public GameObject menuContent;
    public GameObject OptionContent;
    public GameObject SoundContent;
    public GameObject ControlContent;
    public GameObject creditContent;
    private void Start()
    {
        menuTitle.SetActive(true);
        menuContent.SetActive(true);
        OptionContent.SetActive(false);
        SoundContent.SetActive(false);
        ControlContent.SetActive(false);
        creditContent.SetActive(false);

    }

    public void MainMenu()
    {
        menuTitle.SetActive(true);
        menuContent.SetActive(true);
        OptionContent.SetActive(false);
        SoundContent.SetActive(false);
        ControlContent.SetActive(false);
        creditContent.SetActive(false);


    }
    public void Setting()
    {
        menuTitle.SetActive(false);
        menuContent.SetActive(false);
        OptionContent.SetActive(true);
        SoundContent.SetActive(false);
        ControlContent.SetActive(false);
        creditContent.SetActive(false);


    }

    public void SoundSetting()
    {
        menuTitle.SetActive(false);
        menuContent.SetActive(false);
        OptionContent.SetActive(false);
        SoundContent.SetActive(true);
        ControlContent.SetActive(false);
        creditContent.SetActive(false);

    }

    public void ControlSetting()
    {
        menuTitle.SetActive(false);
        menuContent.SetActive(false);
        OptionContent.SetActive(false);
        SoundContent.SetActive(false);
        ControlContent.SetActive(true); 
        creditContent.SetActive(false);

    }

    public void CreditSetting()
    {
        menuTitle.SetActive(false);
        menuContent.SetActive(false);
        OptionContent.SetActive(false);
        SoundContent.SetActive(false);
        ControlContent.SetActive(false);
        creditContent.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }



    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighCount");
        DayCounter.Instance.HighCount = 1; // Reset HighCount to default in the script
        PlayerPrefs.Save();
        Debug.Log("High score reset.");
    }

}

