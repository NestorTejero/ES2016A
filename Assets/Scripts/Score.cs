using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    private int round;
    private int enemies;
    private int towersBuilt;
    private int towersSold;
    private int goldEarned;
    private double time;
    private int score;


	// Use this for initialization
	void Start () {
        round = 0;
        enemies = 0;
        towersBuilt = 0;
        towersSold = 0;
        goldEarned = 0;
        time = 0.0;
        score = 0;

	}
	
    void Update()
    {
        time += Time.deltaTime;
    }

    // Getters, Setters, Inc

    public int getRound()
    {
        return round;
    }

    public int getEnemies()
    {
        return enemies;
    }

    public int getTowersBuilt()
    {
        return towersBuilt;
    }

    public int getTowersSold()
    {
        return towersSold;
    }

    public int getGoldEarned()
    {
        return goldEarned;
    }

    public double getTime()
    {
        return time;
    }

    public int getScore()
    {
        return score;
    }

    public void setRound(int round)
    {
        this.round = round;
        score += 100;
    }

    public void incEnemies()
    {
        this.enemies++;
        score += 1;
    }

    public void incTowersBuilt()
    {
        this.towersBuilt++;
    }

    public void incTowersSold()
    {
        this.towersSold++;
    }

    public void incGoldEarned(int goldEarned)
    {
        this.goldEarned += goldEarned;
    }

}
