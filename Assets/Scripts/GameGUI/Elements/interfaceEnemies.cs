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

[Serializable]
public class interfaceEnemies {
	public InterfaceContainer Contenedor = new InterfaceContainer ();
	public enemiesLeftLabel Label = new enemiesLeftLabel ();
	public totalEnemiesLabel Label2 = new totalEnemiesLabel ();

	public interfaceEnemies () {}

	public void Draw () {
		if ((LogicConnector.isInGame()) && (LogicConnector.isInBattling ())) {
			this.Contenedor.Draw ();

			this.Label.SetText (LogicConnector.getEnemiesLeft().ToString () + "/");
			this.Label.Draw ();
			this.Label2.SetText (LogicConnector.getTotalEnemies().ToString ());
			this.Label2.Draw ();
		}

	}
}

