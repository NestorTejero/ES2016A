using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceMoney {
	public InterfaceContainer Contenedor = new InterfaceContainer ();
	public InterfaceLabel Label = new InterfaceLabel();

	public InterfaceMoney () {}

	public void Draw() {
		this.Contenedor.Draw ();

		this.Label.SetText (LogicConnector.getCredit ().ToString ());
		this.Label.Draw ();
	}
}


