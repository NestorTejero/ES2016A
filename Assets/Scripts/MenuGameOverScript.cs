using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuGameOverScript : MonoBehaviour {

    public Button Return;
    public Button Continue;
    private AudioSource AudioSource;
    public AudioClip MenuPrincipalMusic;
    public Text ResultScore;
    public Text ResultRounds;
    public Text ResultEnemies;
    public Text ResultTowersBuilt;
    public Text ResultTowersSold;
    public Text ResultGold;
    public Text ResultTime;
    public Text GameOver;

    private double time;
    private int min, sec;

    // Use this for initialization
    void Start()
    {
        Return = Return.GetComponent<Button>();
        Continue = Continue.GetComponent<Button>();
        GameOver = GameOver.GetComponent<Text>();
        ResultScore = ResultScore.GetComponent<Text>();
        ResultRounds = ResultRounds.GetComponent<Text>();
        ResultEnemies = ResultEnemies.GetComponent<Text>();
        ResultTowersBuilt = ResultTowersBuilt.GetComponent<Text>();
        ResultTowersSold = ResultTowersSold.GetComponent<Text>();
        ResultGold = ResultGold.GetComponent<Text>();
        ResultTime = ResultTime.GetComponent<Text>();

        if (LogicConnector.getWin())
            GameOver.text = "Victory !";
        else
            GameOver.text = "Game Over";

        ResultScore.text = LogicConnector.getScore().ToString();
        ResultRounds.text = LogicConnector.getRound().ToString();
        ResultEnemies.text = LogicConnector.getEnemies().ToString();
        ResultTowersBuilt.text = LogicConnector.getTowersBuilt().ToString();
        ResultTowersSold.text = LogicConnector.getTowersSold().ToString();
        ResultGold.text = LogicConnector.getGoldEarned().ToString();

        time = LogicConnector.getTotalTime();
        min = 0;
        while (time >= 60)
        {
            time -= 60;
            min += 1;
        }
        sec = (int)time;

        ResultTime.text = min + ":";
        if (sec < 10)
            ResultTime.text += "0";
        ResultTime.text += sec;

        PlayAudio();
    }

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        if (!LogicConnector.getWin())
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
