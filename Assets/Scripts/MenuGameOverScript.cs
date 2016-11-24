using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuGameOverScript : MonoBehaviour {

    public Button Return;
    public Button Continue;
    private AudioSource AudioSource;
    public AudioClip MenuPrincipalMusic;

    // Use this for initialization
    void Start()
    {
        Return = Return.GetComponent<Button>();
        Continue = Continue.GetComponent<Button>();
        PlayAudio();
    }

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        PlayAudio();
    }
    public void ReturnPress()
    {
        //Scene sc = SceneManager.GetActiveScene();
        //int numSceneActive = sc.buildIndex;
        SceneManager.LoadScene("menuScene");
        //SceneManager.UnloadScene(numSceneActive);

    }

    public void ContinuePress()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio()
    {
        AudioSource.Play();
        PlayerPrefs.SetFloat("VolumenGeneral", 1F);
        AudioSource.volume = PlayerPrefs.GetFloat("VolumenGeneral") * PlayerPrefs.GetFloat("VolumenJuego");
        //AudioSource.volume = 1.5F;
    }
}
