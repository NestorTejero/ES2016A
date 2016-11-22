using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menusScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas MenuConfiguration;
    public Button startText;
    public Button endText;
    public Button configurationText;

    public AudioClip MenuPrincipalMusic;
    private UserInterface InterfaceAudioSettings;
    private AudioSource AudioSource;
    private AudioSource AudioSourceGame;
    // Use this for initialization
    void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        MenuConfiguration = MenuConfiguration.GetComponent<Canvas>();
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
		MenuConfiguration.enabled = false;
        startText.enabled = false;
        endText.enabled = false;
    }

    public void MenuConfig()
    {
        MenuConfiguration.enabled = true;
        startText.enabled = false;
        endText.enabled = false;
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
			print (PlayerPrefs.GetFloat("VolumenJuego"));
			print (PlayerPrefs.GetFloat("VolumenGeneral"));
			//AudioSource.volume = 1.5F;

        }
    }

    public void startLevel() {
        Scene sc=SceneManager.GetActiveScene();
        int numSceneActive = sc.buildIndex;
        SceneManager.LoadScene(numSceneActive+1);
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
		print (PlayerPrefs.GetFloat("VolumenJuego"));
		print (PlayerPrefs.GetFloat("VolumenGeneral"));
        AudioSource.volume = PlayerPrefs.GetFloat("VolumenGeneral") * PlayerPrefs.GetFloat("VolumenJuego");
		//AudioSource.volume = 1.5F;
    }
}
