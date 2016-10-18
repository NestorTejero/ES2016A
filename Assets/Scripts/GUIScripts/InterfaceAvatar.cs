using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceAvatar {
	public InterfaceContainer Contenedor = new InterfaceContainer ();

	[Header("Propiedades avatar")]
	public Texture Avatar = null;
	public ScaledRect DimensionesAvatar = new ScaledRect();

	public InterfaceAvatar () {}

	public void Draw() {
		this.Contenedor.Draw ();

		ScaledRect AvatarPosition = new ScaledRect (this.DimensionesAvatar);
		AvatarPosition.IncrementPosition (this.Contenedor.Dimensions);
		GUI.DrawTexture (AvatarPosition.getRect (), this.Avatar);
	}
}

