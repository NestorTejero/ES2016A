using UnityEngine;
using System;
using System.Collections;

public class UserInterface : MonoBehaviour {
	[Header("General")]
	public Rect WindowSize = new Rect (0, 0, 950, 600);
	public LogicConnector LogicConnector = LogicConnector.getInstance();

	[Header("HUD Superior")]
	public InterfaceContainer ContenedorSuperior = new InterfaceContainer();
	public InterfaceHealth InterfazVida = new InterfaceHealth ();
	public InterfaceMoney InterfazDinero = new InterfaceMoney ();
	public InterfaceTime InterfazTiempo = new InterfaceTime ();

	[Header("HUD Inferior")]
	public InterfaceContainer ContenedorInferior = new InterfaceContainer();
	public InterfaceAvatar InterfazAvatar = new InterfaceAvatar();
	public InterfaceTowers InterfazTorres = new InterfaceTowers();

	UserInterface() {
		ScaledRect.WindowSize = this.WindowSize;
	}

	void OnGUI() {
		// Dibujamos los contenerdores superior y inferior.
		ContenedorSuperior.Draw ();
		ContenedorInferior.Draw ();
		InterfazVida.Draw ();
		InterfazDinero.Draw ();
		InterfazAvatar.Draw ();
		InterfazTorres.draw ();
		InterfazTiempo.Draw ();
	}
}
