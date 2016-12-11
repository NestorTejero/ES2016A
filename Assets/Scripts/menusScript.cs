using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menusScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas MenuConfiguration;
    public Button startText;
    public Button endText;
    public Button ButtonMute;
    public Button ButtonLevel;
    public Button configurationText;

    public Image ImageYes;
    public Image ImageNo;
    public Image ImageLevelEasy;
    public Image ImageLevelMedium;
    public Image ImageLevelHard;

    public AudioClip MenuPrincipalMusic;
    private UserInterface InterfaceAudioSettings;
    private AudioSource AudioSource;
    private AudioSource AudioSourceGame;

    public LogicConnector GameLogicConnector = LogicConnector.getInstance();
    // Use this for initialization
    void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        MenuConfiguration = MenuConfiguration.GetComponent<Canvas>();
        ButtonMute = ButtonMute.GetComponent<Button>();
        ButtonLevel = ButtonLevel.GetComponent<Button>();

        ImageYes = ImageYes.GetComponent<Image>();
        ImageNo = ImageNo.GetComponent<Image>();
        ImageLevelEasy = ImageLevelEasy.GetComponent<Image>();
        ImageLevelMedium = ImageLevelMedium.GetComponent<Image>();
        ImageLevelHard = ImageLevelHard.GetComponent<Image>();

        startText = startText.GetComponent<Button>();
        endText = endText.GetComponent<Button>();
        configurationText = configurationText.GetComponent<Button>();
        quitMenu.enabled = false;
        MenuConfiguration.enabled = false;
        PlayAudio();
    }

    void Awake()
    {
        InterfaceAudioSettings = GetComponent<UserInterface>();
        AudioSource = GetComponent<AudioSource>();
        AudioSourceGame = new AudioSource();
        PlayAudio();
        
    }

    public void ExitPress() {
        quitMenu.enabled = true;
		configurationText.enabled = false;
        startText.enabled = false;
        endText.enabled = false;
    }

    public void MenuConfig()
    {
        MenuConfiguration.enabled = true;
        ImageYes.enabled = false;
        ImageNo.enabled = false;
        ImageLevelEasy.enabled = false;
        ImageLevelMedium.enabled = false;
        ImageLevelHard.enabled = false;
        startText.enabled = false;
        endText.enabled = false;
    }

    public void Mute()
    {
        ImageYes.enabled = true;
        ImageNo.enabled = true;
        if (ImageLevelEasy.enabled == true)
        {
            ImageLevelEasy.enabled = false;
        }
        if (ImageLevelMedium.enabled == true)
        {
            ImageLevelMedium.enabled = false;
        }
        if (ImageLevelHard.enabled == true)
        {
            ImageLevelHard.enabled = false;
        }
        
    }

    public void Level()
    {
        ImageLevelEasy.enabled = true;
        ImageLevelMedium.enabled = true;
        ImageLevelHard.enabled = true;
        if(ImageYes.enabled == true)
        {
            ImageYes.enabled = false;
        }
        if(ImageNo.enabled == true)
        {
            ImageNo.enabled = false;
        }
        
    }

    public void SetLevelEasy()
    {
        var instance =LogicConnector.getInstance();
        instance.Difficult = LogicConnector.Difficulty.Easy;
        ImageLevelEasy.enabled = false;
        ImageLevelMedium.enabled = false;
        ImageLevelHard.enabled = false;
    }

    public void SetLevelMedium()
    {
        var instance = LogicConnector.getInstance();
        instance.Difficult = LogicConnector.Difficulty.Medium;
        ImageLevelEasy.enabled = false;
        ImageLevelMedium.enabled = false;
        ImageLevelHard.enabled = false;
    }

    public void SetLevelHard()
    {
        var instance = LogicConnector.getInstance();
        instance.Difficult = LogicConnector.Difficulty.Hard;
        ImageLevelEasy.enabled = false;
        ImageLevelMedium.enabled = false;
        ImageLevelHard.enabled = false;
    }

    public void MuteSound()
    {
        if (AudioSource.mute == false)
        {
            AudioSource.mute = true;
        }
        if (AudioSource.volume != 0.0F)
        {
            AudioSource.volume = 0.0F;
        }

        ImageYes.enabled = false;
        ImageNo.enabled = false;
    }

    public void NoPress() {

        if (quitMenu.enabled == true)
        {
            quitMenu.enabled = false;
        }

        if (MenuConfiguration.enabled==true)
        {
            MenuConfiguration.enabled = false;
        }

		configurationText.enabled = true;

        startText.enabled = true;
        endText.enabled = true;
    }

    public void UnMute()
    {
        if (AudioSource.mute == true)
        {
            AudioSource.mute = false;
        }
        if (AudioSource.volume == 0.0F)
		{
			//AudioSource.Play();
			AudioSource.volume = PlayerPrefs.GetFloat("VolumenGeneral") * PlayerPrefs.GetFloat("VolumenJuego");
			//AudioSource.volume = 1.5F;

        }

        ImageYes.enabled = false;
        ImageNo.enabled = false;
    }

    public void startLevel() {
        // Scene sc=SceneManager.GetActiveScene();
        // int numSceneActive = sc.buildIndex;
		SceneManager.LoadScene("Game");
    }

    public void exitGame() {
        Application.Quit();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAudio()
    {
		AudioSource.Play();
		PlayerPrefs.SetFloat ("VolumenGeneral", 1F);
        AudioSource.volume = PlayerPrefs.GetFloat("VolumenGeneral") * PlayerPrefs.GetFloat("VolumenJuego");
		//AudioSource.volume = 1.5F;
    }
}
