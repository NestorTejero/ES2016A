/* --------------------------------------------------------------------------------------------	*
 * README: 	Este script es parte del codigo del equipo C (User interface - UX), por favor		*
 * 			no realizes cambios en este archivo si no eres su autor, de su grupo o has			*
 * 			hablado con alguno de ellos para comentar los cambios y se ha hecho un acuerdo.		*
 * 			Anteriormente se han realizado cambios en archivos del equipo C por partes de		*
 * 			otros equipos resultando en una duplicación de las funcionalidades y un uso			*
 * 			incorrecto de variables que estaban destinadas a test internos de la interfaz.		*
 * 			Para evitar tener que hacer el trabajo repetidas veces por favor habla con			*
 * 			los autores, intentemos no pisarnos el trabajo para poder avanzar todos mejor.		*
 * --------------------------------------------------------------------------------------------	*/
using UnityEngine;
using System;
using System.Collections;

[ExecuteInEditMode]
public class UserInterface : MonoBehaviour {
	[Header("General")]
	public Rect WindowSize = new Rect (0, 0, 950, 600);
	public AudioClip GameMusic;
	public AudioClip MenuMusic;
	public LogicConnector GameLogicConnector = LogicConnector.getInstance();

	[Header("HUD Superior")]
	public InterfaceContainer ContenedorSuperior = new InterfaceContainer();
	public InterfaceHealth InterfazVida = new InterfaceHealth ();
	public InterfaceMoney InterfazDinero = new InterfaceMoney ();
	public InterfaceTime InterfazTiempo = new InterfaceTime ();
	public interfaceEnemies interfaceEnemies = new interfaceEnemies ();
	public MenuButton BotonPausa = new MenuButton ();
	public MenuButton BotonStart = new MenuButton ();

	[Header("HUD Inferior")]
	public InterfaceContainer ContenedorInferior = new InterfaceContainer();
	public InterfaceAvatar InterfazAvatar = new InterfaceAvatar();
	public InterfaceTowers InterfazTorres = new InterfaceTowers();

	[Header("HUD Pausa")]
	public InterfacePause InterfazPausa = new InterfacePause();

	[Header("HUD Settings")]
	public InterfaceSettings InterfazSettings = new InterfaceSettings();

	// Private attributes.
	private AudioSource AudioSource;

	UserInterface() {
		ScaledRect.WindowSize = this.WindowSize;
		LogicConnector.ConnectInterface (this);
	}

	void Awake () {
		this.AudioSource = GetComponent<AudioSource> ();
		this.AudioSource.clip = null;
		LogicConnector.Resume ();
	}

	void OnGUI() {
		// Interfaces del juego.
		ContenedorSuperior.Draw ();
		ContenedorInferior.Draw ();
		InterfazVida.Draw ();
		InterfazDinero.Draw ();
		InterfazAvatar.Draw ();
		InterfazTorres.draw ();
		InterfazTiempo.Draw ();
		interfaceEnemies.Draw ();

		if (BotonPausa.Draw ()) {
			LogicConnector.Pause ();
		}

		if (BotonStart.Draw ()) {
			// Early start.
		}

		// Elementos con mas "z index".
		InterfazPausa.Draw ();
		InterfazSettings.Draw ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			switch (LogicConnector.getInstance().State) {
			case LogicConnector.States.InGame:
				LogicConnector.Pause ();
				break;
			case LogicConnector.States.Paused:
				LogicConnector.Resume ();
				break;
			case LogicConnector.States.Settings:
				LogicConnector.Pause ();
				break;
			}
		}
	}

	public void OnPause () {
		if (this.AudioSource.clip != this.MenuMusic) {
			this.AudioSource.clip = this.MenuMusic;
			this.AudioSource.Play ();
			this.AudioSource.volume = PlayerPrefs.GetFloat ("VolumenGeneral") * PlayerPrefs.GetFloat ("VolumenMenu");
		}
	}

	public void OnResume () {
		if (this.AudioSource.clip != this.GameMusic) {
			this.AudioSource.clip = this.GameMusic;
			this.AudioSource.Play ();
			this.AudioSource.volume = PlayerPrefs.GetFloat ("VolumenGeneral") * PlayerPrefs.GetFloat ("VolumenJuego");
		}

	}

	public void OnSettings () {
		if (this.AudioSource.clip != this.MenuMusic) {
			this.AudioSource.clip = this.MenuMusic;
			this.AudioSource.Play ();
			this.AudioSource.volume = PlayerPrefs.GetFloat ("VolumenGeneral") * PlayerPrefs.GetFloat ("VolumenMenu");
		}
	}

	public void OnGameOver () {

	}

	public void OnVolumeUpdate (LogicConnector.States State) {
		switch (State) {
		case LogicConnector.States.InGame:
			this.AudioSource.volume = PlayerPrefs.GetFloat ("VolumenGeneral") * PlayerPrefs.GetFloat ("VolumenJuego");
			break;
		case LogicConnector.States.Paused:
			this.AudioSource.volume = PlayerPrefs.GetFloat ("VolumenGeneral") * PlayerPrefs.GetFloat ("VolumenMenu");
			break;
		case LogicConnector.States.Settings:
			this.AudioSource.volume = PlayerPrefs.GetFloat ("VolumenGeneral") * PlayerPrefs.GetFloat ("VolumenMenu");
			break;
		}
	}
}
