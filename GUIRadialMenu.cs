using UnityEngine;
using System.Collections;

public class GUIRadialMenu : GUIElementBase {
	
	public Choice[] choices;
	public float margin = 20;
	public Rect[] buttonRect;
	public DialogHandler _dialogHandler {get; private set;}
	public GUIStyle butStyle = new GUIStyle();

	// Make the buttons on wich the player can click
	void Start () {	
		buttonRect = new Rect[6];
		buttonRect[0] = new Rect(200,10,100,20);
		buttonRect[1] = new Rect(175,30,100,20);	
		buttonRect[2] = new Rect(150,50,100,20);	
		buttonRect[3] = new Rect(450,10,100,20);	
		buttonRect[4] = new Rect(475,30,100,20);
		buttonRect[5] = new Rect(500,50,100,20);
		
		rect = new Rect((float)((Screen.width * 0.5) - (width / 2)), (float)(Screen.height - height - margin), width, height);
	}
	
	// Update is called once per frame
	void Update () {
	}
	//if the choices.length is >= value then draw button[] and put in the text from corresponding choices[]
	public override void Draw ()
	{
		GUI.BeginGroup(rect);
		if (choices.Length >= 1 && GUI.Button(buttonRect[0], choices[0].text, butStyle))
			PressButton(0);
		if (choices.Length >= 2 && GUI.Button(buttonRect[1], choices[1].text, butStyle))
			PressButton(1);
		if (choices.Length >= 3 && GUI.Button(buttonRect[2], choices[2].text, butStyle))
			PressButton(2);
		if (choices.Length >= 4 && GUI.Button(buttonRect[3], choices[3].text, butStyle))
			PressButton(3);
		if (choices.Length >= 5 && GUI.Button(buttonRect[4], choices[4].text, butStyle))
			PressButton(4);
		if (choices.Length >= 6 && GUI.Button(buttonRect[5], choices[5].text, butStyle))
			PressButton(5);
		GUI.EndGroup();
	}
	
	 public void PressButton(int nr)
	{
	       if(nr >= choices.Length || nr < 0)
	               return;
	       
	       _dialogHandler.makeChoice(choices[nr].link);
	}
	
	public void setChoices(Choice[] choices)
	{
		this.choices = choices;
	}
	
	public bool hasChoices()
	{
		if(choices.Length > 0)
			return true;
		
		return false;
	}
	
	public void setDialogHandler(DialogHandler dialogHandler)
	{
	       this._dialogHandler = dialogHandler;
	}
}
