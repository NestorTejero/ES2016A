using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class LogicConnector {
	// Variables de la logica.
	[Header("Game logic values")]
	[SerializeField] private int Health;
	[SerializeField] private int Credit;
	[SerializeField] private float Time;
	[SerializeField] private int[] TowerCost = { 100, 500, 1000 };

	[Header("Game linking")]
	public GameObject[] TowerObjects;

	/* NOTA PARA EL FUTURO LECTOR:
	 * A continuación se hayan variables de TEST no deben ser usadas fuera de este código
	 * y sirven principalmente para testear la aplicación. No son para uso en la lógica
	 * del juego. Thanks!.
	 * (Se han añadido variables por si se desea utilizar esta clase como contenedor de
	 * los costes de las torres, creditos del jugador y vida de la torre)
	 */
	[Header("Test values")]
	[SerializeField] private Boolean testMode = false;
	[SerializeField] private int testHealth = 3;
	[SerializeField] private int testCredit = 500;
	[SerializeField] private int[] testTowerCost = { 100, 500, 1000 };
	[SerializeField] private float testTime = 60.0f;

	protected static LogicConnector _instance = null;
	protected LogicConnector () {}
	public static LogicConnector getInstance() {
		if (_instance == null)
			_instance = new LogicConnector ();
		return _instance;
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
		return (instance.testMode) ? instance.testTime : instance.Time;
	}

	public static void setTime(float Time) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.Time = Time;
	}

	public static void increaseTime(float Time) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.Time += Time;
	}

	public static void decreaseTime(float Time) {
		LogicConnector instance = LogicConnector.getInstance ();
		instance.Time -= Time;
	}

	public static void placeTower(int type) {
		LogicConnector instance = LogicConnector.getInstance ();
		GameObject.FindObjectOfType<TowerPlacement>().SetItem(instance.TowerObjects [type], type, LogicConnector.getTowerCost (type));
    }

	public static int getTowerCost(int type) {
		LogicConnector instance = LogicConnector.getInstance ();
		return (instance.testMode) ? instance.testTowerCost [type] : instance.TowerCost [type];
	}
}


