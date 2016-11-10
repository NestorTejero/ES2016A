using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceState {

	public enum States {InGame, Paused, Settings, GameOver};
	public States State = InterfaceState.States.InGame;

	protected static InterfaceState _instance = null;
	protected InterfaceState () {}

	public static InterfaceState getInstance() {
		if (_instance == null)
			_instance = new InterfaceState ();
		return _instance;
	}

	public static void Pause () {
		InterfaceState.getInstance ().State = InterfaceState.States.Paused;
		Time.timeScale = 0;
	}

	public static void Resume () {
		InterfaceState.getInstance ().State = InterfaceState.States.InGame;
		Time.timeScale = 1;
	}

	public static void Settings () {
		InterfaceState.getInstance ().State = InterfaceState.States.Settings;
		Time.timeScale = 0;
	}

	public static void GameOver () {
		InterfaceState.getInstance ().State = InterfaceState.States.GameOver;
		Time.timeScale = 0;
	}

	public static bool isPaused () {
		return InterfaceState.getInstance ().State == InterfaceState.States.Paused;
	}

	public static bool isInGame () {
		return InterfaceState.getInstance ().State == InterfaceState.States.InGame;
	}

	public static bool isInSettings () {
		return InterfaceState.getInstance ().State == InterfaceState.States.Settings;
	}

	public static bool isGameOver () {
		return InterfaceState.getInstance ().State == InterfaceState.States.GameOver;
	}
}


