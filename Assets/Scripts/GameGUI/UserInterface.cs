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

public class UserInterface : MonoBehaviour {
	[Header("General")]
	public Rect WindowSize = new Rect (0, 0, 950, 600);
	public LogicConnector LogicConnector = LogicConnector.getInstance();
	public InterfaceState InterfaceState = InterfaceState.getInstance();

	[Header("HUD Superior")]
	public InterfaceContainer ContenedorSuperior = new InterfaceContainer();
	public InterfaceHealth InterfazVida = new InterfaceHealth ();
	public InterfaceMoney InterfazDinero = new InterfaceMoney ();
	public InterfaceTime InterfazTiempo = new InterfaceTime ();
	public interfaceEnemies interfaceEnemies = new interfaceEnemies ();

	[Header("HUD Inferior")]
	public InterfaceContainer ContenedorInferior = new InterfaceContainer();
	public InterfaceAvatar InterfazAvatar = new InterfaceAvatar();
	public InterfaceTowers InterfazTorres = new InterfaceTowers();

	[Header("HUD Pausa")]
	public InterfacePause InterfazPausa = new InterfacePause();

	[Header("HUD Settings")]
	public InterfaceSettings InterfazSettings = new InterfaceSettings();

	[Header("TEMPORAL - Hasta que se busque sitio")]
	public MenuButton BotonPausa = new MenuButton ();


	UserInterface() {
		ScaledRect.WindowSize = this.WindowSize;
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
			InterfaceState.Pause ();
		}

		// Elementos con mas "z index".
		InterfazPausa.Draw ();
		InterfazSettings.Draw ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			switch (this.InterfaceState.State) {
			case InterfaceState.States.InGame:
				InterfaceState.Pause ();
				break;
			case InterfaceState.States.Paused:
				InterfaceState.Resume ();
				break;
			case InterfaceState.States.Settings:
				InterfaceState.Pause ();
				break;
			}
		}
	}
}
