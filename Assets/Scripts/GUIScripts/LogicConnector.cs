﻿using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class LogicConnector {
	public Boolean testMode = false;
	public int testHealth = 3;
	public int testCredit = 1200;
	public int[] testTowerCost = {1000, 2000, 3000};
	public float testTime = 60.0f;

	protected static LogicConnector _instance = null;
	protected LogicConnector () {}

	public static LogicConnector getInstance() {
		if (_instance == null)
			_instance = new LogicConnector ();
		return _instance;
	}

	public int getHealth() {
		Console.Out.WriteLine(_instance.testHealth);
		return (_instance.testMode) ? _instance.testHealth : _instance.testHealth;
	}

	public int getCredit() {
		return (_instance.testMode) ? _instance.testCredit : _instance.testCredit;
	}

	public float getTime() {
		return (_instance.testMode) ? _instance.testTime : _instance.testTime;
	}

	public void placeTower(int type) {
		Debug.Log ("Placing tower of type: " + type);
	}

	public int getTowerCost(int type) {
		return (_instance.testMode) ? _instance.testTowerCost [type] : _instance.testTowerCost [type];
	}
}

