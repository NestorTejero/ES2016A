using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceStory { 
	private int LastRound = -1;
	private float Delta = 0;
	private float ShowTime = 10;
	public ScaledRect position;
	public InterfaceLabel box = new InterfaceLabel();
	public String[] StoryBits;

	public InterfaceStory () {}

	public void Draw () {
		if (LastRound < LogicConnector.getRound ()) {
			Delta = Time.time; 
			LastRound = LogicConnector.getRound ();
		}

		if (Delta + ShowTime > Time.time) {
			GUI.Window (1111, position.getRect(), WindowFunc, "Historia");
		}
	}

	public void WindowFunc(int id) {
		if (StoryBits.Length > LogicConnector.getRound ()) {
			box.SetText (StoryBits [LogicConnector.getRound ()]);
			box.Draw ();
		}

	}
}

