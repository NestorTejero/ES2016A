using UnityEngine;
using System.Collections;

public class HomeBehavior : MonoBehaviour {

    public Score score;

    public float maxHealth = 100f;
    public float lifes = 3f;
    public float health = 100f;
    public int initCredit = 300;        // Initial ammount of credit

    // Damage thresholds
    private int threshold = 0;          // current threshold
    public Mesh[] thresholdMeshes;            // threshold meshes          

    // Collision management.
    void OnTriggerEnter(Collider other)
    {
        // This function could prove itself useful if we want to implement shooter enemies.
    }

    // Use this for initialization
    void Start () {
        LogicConnector.setCredit(initCredit);
        score = GameObject.Find("GameScripts").GetComponent<Score>();
    }

    // Take damage.
    public void takeDamage (float damage)
    {
        health = Mathf.Max(0, health - damage);
		LogicConnector.setHealth( (int)(health*lifes/ maxHealth)+1 );

        if (health == 0)
        {
            SelfDestroy();
        }
        else
        {
            // Change mesh if threshold has been reached:
            if (ThesholdIsReached() && thresholdMeshes.Length != 0)
            {      
                gameObject.GetComponent<MeshFilter>().mesh = thresholdMeshes[threshold]; // swap mesh
                threshold = (int)Mathf.Min(threshold + 1, thresholdMeshes.Length - 1);   // update threshold
            }
        }
    }

    // Can be modified to add cool effects when the entity is destroyed.
    protected virtual void SelfDestroy()
    {
        Destroy(gameObject);    
        GameOver();
    }

    private bool ThesholdIsReached()
    {
        int n = thresholdMeshes.Length;     // number of reachable thresholds
        return health <= (maxHealth * (n - threshold) / (n + 1));
    }

    private void GameOver()
    {
        LogicConnector.setHealth(0);

        LogicConnector.setRound(score.getRound());
        LogicConnector.setEnemies(score.getEnemies());
        LogicConnector.setTowersBuilt(score.getTowersBuilt());
        LogicConnector.setTowersSold(score.getTowersSold());
        LogicConnector.setGoldEarned(score.getGoldEarned());
        LogicConnector.setTotalTime(score.getTime());
        LogicConnector.setScore(score.getScore());
        LogicConnector.setWin(false);

        LogicConnector.GameOver();
    }
}
