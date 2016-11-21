using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceState {

	public enum States {InGame, Paused, Settings, GameOver};
	public enum SubStates {InBattling, InBreak}
	public States State = InterfaceState.States.InGame;
	public SubStates SubState = InterfaceState.SubStates.InBattling;

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

	public static void Break () {
		InterfaceState.getInstance ().State = InterfaceState.States.InGame;
		InterfaceState.getInstance ().SubState = InterfaceState.SubStates.InBreak;
		Time.timeScale = 1;
	}

	public static void Battling () {
		InterfaceState.getInstance ().State = InterfaceState.States.InGame;
		InterfaceState.getInstance ().SubState = InterfaceState.SubStates.InBattling;
		Time.timeScale = 1;
	}
	/*public static void RestWave () {
		InterfaceState.getInstance ().State = InterfaceState.States.InRest;
		Time.timeScale = 1;
	}*/

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

	/* NEW METHODS */

	public static bool isInBattling () {
		return InterfaceState.getInstance ().SubState == InterfaceState.SubStates.InBattling;
	}

	public static bool isInBreak () {
		return InterfaceState.getInstance ().SubState == InterfaceState.SubStates.InBreak;
	}
}


