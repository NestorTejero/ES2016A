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
using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceSettings {
	public InterfaceContainer Contenedor = new InterfaceContainer();
	public InterfaceContainer ContenedorBlur = new InterfaceContainer();
	public interfaceEnemies interfaceEnemies = new interfaceEnemies ();
	public InterfaceTime InterfazTiempo = new InterfaceTime ();

	public InterfaceLabel SettingsTitle = new InterfaceLabel ();

	[Header("Controles")]
	public MenuButton BotonVolver = new MenuButton();
	/*public MenuButton BotonSilenciarGeneral = new MenuButton();
	public MenuButton BotonSilenciarMenu = new MenuButton();
	public MenuButton BotonSilenciarJuego = new MenuButton ();*/

	[Header("Slider preferencias")]
	public InterfaceSlider VolumenGeneral = new InterfaceSlider ();
	public InterfaceSlider VolumenMenu = new InterfaceSlider ();
	public InterfaceSlider VolumenJuego = new InterfaceSlider ();


	public InterfaceSettings () {}

	public void Draw () {
		if (LogicConnector.isInSettings ()) {
			this.ContenedorBlur.Draw ();
			this.Contenedor.Draw ();
			this.SettingsTitle.Draw ();

			if (this.BotonVolver.Draw ()) {
				LogicConnector.Pause ();
			}

		/*	if (this.BotonSilenciarGeneral.Draw ()) {
				PlayerPrefs.SetFloat ("VolumenGeneral", 0);
				LogicConnector.triggerVolumeUpdate ();
			}

			if (this.BotonSilenciarMenu.Draw ()) {
				PlayerPrefs.SetFloat ("VolumenMenu", 0);
				LogicConnector.triggerVolumeUpdate ();
			}

			if (this.BotonSilenciarJuego.Draw ()) {
				PlayerPrefs.SetFloat ("VolumenJuego", 0);
				LogicConnector.triggerVolumeUpdate ();
			}*/

			float PreviousVolumenGeneral = PlayerPrefs.GetFloat ("VolumenGeneral");
			this.VolumenGeneral.Value = PreviousVolumenGeneral;
			float NewVolumenGeneral = this.VolumenGeneral.Draw ();
			PlayerPrefs.SetFloat ("VolumenGeneral", NewVolumenGeneral);


			float PreviousVolumenMenu = PlayerPrefs.GetFloat ("VolumenMenu");
			this.VolumenMenu.Value = PreviousVolumenMenu;
			float NewVolumenMenu = this.VolumenMenu.Draw ();
			PlayerPrefs.SetFloat ("VolumenMenu", NewVolumenMenu);


			float PreviousVolumenJuego = PlayerPrefs.GetFloat ("VolumenJuego");
			this.VolumenJuego.Value = PreviousVolumenJuego;
			float NewVolumenJuego = this.VolumenJuego.Draw ();
			PlayerPrefs.SetFloat ("VolumenJuego", NewVolumenJuego);


			if (PreviousVolumenGeneral != NewVolumenGeneral ||
				PreviousVolumenMenu != NewVolumenMenu ||
				PreviousVolumenJuego != NewVolumenJuego) {
				LogicConnector.triggerVolumeUpdate ();
			}

		} 
	}
}


