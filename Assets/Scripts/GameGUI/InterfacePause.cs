using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfacePause {
	public InterfaceContainer Contenedor = new InterfaceContainer();
	public InterfaceContainer ContenedorBlur = new InterfaceContainer();

	public InterfaceLabel MenuTitle = new InterfaceLabel ();

	[Header("Opciones menu")]
	public MenuButton BotonContinuar = new MenuButton ();
	public MenuButton BotonOpciones = new MenuButton ();
	public MenuButton BotonSalir = new MenuButton ();


	public InterfacePause () {}

	public void Draw () {
		if (InterfaceState.isPaused ()) {
			this.ContenedorBlur.Draw ();
			this.Contenedor.Draw ();
			this.MenuTitle.Draw ();

			if (this.BotonContinuar.Draw ()) {
				InterfaceState.Resume ();
			}
			if (this.BotonOpciones.Draw ()) {
				InterfaceState.Settings ();
			}
			if (this.BotonSalir.Draw ()) {
				Application.Quit ();
			}
		}
	}
}


