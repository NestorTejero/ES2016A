using UnityEngine;
using System;
using System.Collections;

public class UserInterface : MonoBehaviour {
	[Header("Dimensiones base")]
	public Rect WindowSize = new Rect (0, 0, 950, 600);

	[Header("Background superior")]
	public ScaledRect ContenedorSuperior = new ScaledRect();
	public Texture TexturaContenedorSuperior = null;

	[Header("Elementos superiores")]
	public InterfaceHealth InterfazVida = new InterfaceHealth ();
	public InterfaceMoney InterfazDinero = new InterfaceMoney ();

	[Header("Background inferior")]
	public ScaledRect ContenedorInferior = new ScaledRect();
	public Texture TexturaContenedorInferior = null;

	[Header("Elementos inferiores")]
	public InterfaceAvatar InterfazAvatar = new InterfaceAvatar();
	public InterfaceTowers InterfazTorres = new InterfaceTowers();

	UserInterface() {
		this.ContenedorSuperior.setWindowSize (WindowSize);
		this.ContenedorInferior.setWindowSize (WindowSize);
		this.InterfazVida.setWindowSize (WindowSize);
		this.InterfazDinero.setWindowSize (WindowSize);
		this.InterfazAvatar.setWindowSize (WindowSize);
		this.InterfazTorres.setWindowSize (WindowSize);
	}

	void OnGUI() {
		// Dibujamos los contenerdores superior y inferior.
		GUI.DrawTexture (ContenedorSuperior.getRect(), this.TexturaContenedorSuperior);
		GUI.DrawTexture (ContenedorInferior.getRect(), this.TexturaContenedorInferior);

		InterfazVida.draw ();
		InterfazDinero.draw ();
		InterfazAvatar.draw ();
		InterfazTorres.draw ();
	}
}
