﻿/* --------------------------------------------------------------------------------------------	*
 * README: 	Este script es parte del codigo del equipo C (User interface - UX), por favor		*
 * 			no realizes cambios en este archivo si no eres su autor, de su grupo o has			*
 * 			hablado con alguno de ellos para comentar los cambios y se ha hecho un acuerdo.		*
 * 			Anteriormente se han realizado cambios en archivos del equipo C por partes de		*
 * 			otros equipos resultando en una duplicación de las funcionalidades y un uso			*
 * 			incorrecto de variables que estaban destinadas a test internos de la interfaz.		*
 * 			Para evitar tener que hacer el trabajo repetidas veces por favor habla con			*
 * 			los autores, intentemos no pisarnos el trabajo para poder avanzar todos mejor.		*
 * --------------------------------------------------------------------------------------------	*/

/* --------------------------------------------------------------------------------------------	*
 * README2: Este script pretende ser el conector entre la logica de la GUI y la logica del		*
 *			juego. En principio estaba pensado para ser una parte de la logica de la gui		*
 *			que buscara los datos de la logica del juego, pero en sprints anteriores se uso		*
 *			por parte de los otros equipos variables de test de este script como parte de la	*
 *			logica principal del juego.															*
 *			A raiz de esto se han creado variables y metodos para acomodar esta parte de la		*
 * 			logica (Se encuentran bajo el header <Game logic values> y asi prevenir que se		*
 *			vuelvan a utilizar las que son para el testeo de la gui, bajo el header				*
 *			<test values>.																		*
 * -------------------------------------------------------------------------------------------- */
using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class LogicConnector {
	public enum States {InGame, Paused, Settings, GameOver};
	public enum SubStates {InBattling, InBreak};
	[Header("Game linking")]
	public States State = LogicConnector.States.InGame;
	public SubStates SubState = LogicConnector.SubStates.InBattling;
	public GameObject[] TowerObjects;

    //Enum dificultad juego
    public enum Difficulty { Easy, Medium, Hard };
    [Header("Game Difficulty")]
    public Difficulty Difficult = LogicConnector.Difficulty.Easy;
    private String difficulty;

    // Variables de la logica.
    [Header("Game logic values")]
	[SerializeField] private int Health;
	[SerializeField] private int Credit;
	[SerializeField] private float GameTime;
	[SerializeField] private int[] TowerCost = { 100, 500, 1000 };
	[SerializeField] private int enemiesLeft;
	[SerializeField] private int totalEnemies;

	[Header("Test values")]
	[SerializeField] private Boolean testMode = false;
	[SerializeField] private int testHealth = 3;
	[SerializeField] private int testCredit = 500;
	[SerializeField] private int[] testTowerCost = { 100, 500, 1000 };
	[SerializeField] private float testTime = 60.0f;
	[SerializeField] private int testEnemiesLeft = 7;
	[SerializeField] private int testTotalEnemies = 15;

	private UserInterface UserInterface;
	protected static LogicConnector _instance = null;
	protected LogicConnector () {}
	public static LogicConnector getInstance() {
		if (_instance == null)
			_instance = new LogicConnector ();
		return _instance;
	}

	public static void ConnectInterface (UserInterface userInterface) {
		LogicConnector.getInstance().UserInterface = userInterface;
	}
	
	public static int getHealth() {
		LogicConnector instance = LogicConnector.getInstance ();
		return (instance.testMode) ? instance.testHealth : instance.Health;
	}

	public static void setHealth(int Health) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.Health = Health;
	}

	public static void increaseHealth(int Health) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.Health += Health;
	}

	public static void decreaseHealth(int Health) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.Health -= Health;
	}

	public static int getCredit() {
		LogicConnector instance = LogicConnector.getInstance ();
		return (instance.testMode) ? instance.testCredit : instance.Credit;
	}

	public static void setCredit(int Credit) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.Credit = Credit;
	}

	public static void increaseCredit(int Credit) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.Credit += Credit;
	}

	public static void decreaseCredit(int Credit) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.Credit -= Credit;
	}

	public static float getTime() {
		LogicConnector instance = LogicConnector.getInstance ();
		return (instance.testMode) ? instance.testTime : instance.GameTime;
	}

	public static void setTime(float Time) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.GameTime = Time;
	}

	public static void increaseTime(float Time) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.GameTime += Time;
	}

	public static void decreaseTime(float Time) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.GameTime -= Time;
	}

	public static void placeTower(int type) {
		LogicConnector instance = LogicConnector.getInstance ();
		GameObject.FindObjectOfType<TowerPlacement>().SetItem(instance.TowerObjects [type], type, LogicConnector.getTowerCost (type));
    }

	public static int getTowerCost(int type) {
		LogicConnector instance = LogicConnector.getInstance ();
		return (instance.testMode) ? instance.testTowerCost [type] : instance.TowerCost [type];
	}

	/* Getters & Setters to show enemies info per wave */

	public static int getEnemiesLeft(){
		LogicConnector instance = LogicConnector.getInstance ();
		return (instance.testMode) ? instance.testEnemiesLeft : instance.enemiesLeft;
		//return 7;
	}

	public static int getTotalEnemies(){
		LogicConnector instance = LogicConnector.getInstance ();
		return (instance.testMode) ? instance.testTotalEnemies : instance.totalEnemies;
		//return 15;
	}

	public static void setEnemiesLeft(int enemies){
		LogicConnector instance = LogicConnector.getInstance ();
		instance.enemiesLeft = enemies;
	}

	public static void setTotalEnemies(int enemies){
		LogicConnector instance = LogicConnector.getInstance ();
		instance.totalEnemies = enemies;
	}

	public static void decreaseEnemies(){
		LogicConnector instance = LogicConnector.getInstance ();
		instance.testEnemiesLeft -= 1;
	}

	public static void Pause () {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.State = LogicConnector.States.Paused;
		Time.timeScale = 0;
		if (instance.UserInterface != null)
			instance.UserInterface.OnPause ();
	}

	public static void Resume () {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.State = LogicConnector.States.InGame;
		Time.timeScale = 1;
		if (instance.UserInterface != null)
			instance.UserInterface.OnResume ();
	}

	public static void Settings () {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.State = LogicConnector.States.Settings;
		Time.timeScale = 0;
		if (instance.UserInterface != null)
			instance.UserInterface.OnSettings ();
	}

	public static void GameOver () {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.State = LogicConnector.States.GameOver;
		Time.timeScale = 0;
		if (instance.UserInterface != null)
			instance.UserInterface.OnGameOver ();
	}

	public static void Break () {
		LogicConnector.getInstance ().State = LogicConnector.States.InGame;
		LogicConnector.getInstance ().SubState = LogicConnector.SubStates.InBreak;
		Time.timeScale = 1;
	}

	public static void Battling () {
		LogicConnector.getInstance ().State = LogicConnector.States.InGame;
		LogicConnector.getInstance ().SubState = LogicConnector.SubStates.InBattling;
		Time.timeScale = 1;
	}

	public static bool isPaused () {
		LogicConnector instance = LogicConnector.getInstance ();
		return instance.State == LogicConnector.States.Paused;
	}

	public static bool isInGame () {
		LogicConnector instance = LogicConnector.getInstance ();
		return instance.State == LogicConnector.States.InGame;
	}

	public static bool isInSettings () {
		LogicConnector instance = LogicConnector.getInstance ();
		return instance.State == LogicConnector.States.Settings;
	}

	public static bool isGameOver () {
		LogicConnector instance = LogicConnector.getInstance ();
		return instance.State == LogicConnector.States.GameOver;
	}

	public static bool isInBattling () {
		return LogicConnector.getInstance ().SubState == LogicConnector.SubStates.InBattling;
	}

	public static bool isInBreak () {
		return LogicConnector.getInstance ().SubState == LogicConnector.SubStates.InBreak;
	}

	public static void triggerVolumeUpdate () {
		LogicConnector instance = LogicConnector.getInstance ();
		if (instance.UserInterface != null)
			instance.UserInterface.OnVolumeUpdate (instance.State);
	}

    public static void SetDifficulty()
    {
        LogicConnector instance = LogicConnector.getInstance();
        instance.difficulty = instance.Difficult.ToString();
    }

    public static string GetDifficulty()
    {
        LogicConnector instance = LogicConnector.getInstance();
        return instance.difficulty;
    }
}


