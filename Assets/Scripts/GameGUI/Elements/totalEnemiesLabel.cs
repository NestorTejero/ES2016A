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
public class totalEnemiesLabel {
	public int FontSize = 24;
	public Color TextColor = new Color(0, 0, 0);
	public ScaledRect Position = new ScaledRect ();
	public String Text;
	public TextAnchor Alignment = TextAnchor.MiddleCenter;

	public totalEnemiesLabel () {}

	public void Draw () {
		GUIStyle LabelStyle = new GUIStyle ();
		LabelStyle.fontSize = (int)(this.FontSize * Position.getXScale());
		LabelStyle.normal.textColor = this.TextColor;
		LabelStyle.alignment = this.Alignment;

		GUI.Label(this.Position.getRect(), this.Text, LabelStyle);
	}

	public void SetText(String Text) {
		this.Text = Text;
	}
}