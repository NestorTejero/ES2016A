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
public class InterfaceSlider {
	public ScaledRect Position = new ScaledRect();
	public String Text;
	public int TextWidth;
	public GUIStyle SliderStyle = new GUIStyle();
	public GUIStyle ThumbStyle = new GUIStyle ();

	public InterfaceLabel Label = new InterfaceLabel();
	public float Value = 0.0f;

	public InterfaceSlider () {
	}

	public float Draw () {

		ScaledRect TextPosition = new ScaledRect (this.Position);
		TextPosition.rect.width = TextWidth;

		this.Label.Position = TextPosition;
		this.Label.Alignment = TextAnchor.MiddleLeft;
		this.Label.Text = this.Text;

		this.Label.Draw ();

		ScaledRect FinalPosition = new ScaledRect (this.Position);
		FinalPosition.rect.x += this.TextWidth;
		FinalPosition.rect.width -= this.TextWidth;

		this.Value = GUI.HorizontalSlider (FinalPosition.getRect (), this.Value, 0.0f, 100.0f);
		return this.Value;
	}
}


