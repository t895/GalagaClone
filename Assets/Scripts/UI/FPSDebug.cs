using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDebug : MonoBehaviour
{
    //public int fpsTarget = 144;
    //private float deltaTime = 0.0f;
 
	void Start()
	{
		QualitySettings.vSyncCount = 0;
	}

	void Update()
	{
		//deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        //Application.targetFrameRate = fpsTarget;

        /*if(Input.GetKey(KeyCode.KeypadPlus))
            fpsTarget++;
        if(Input.GetKey(KeyCode.KeypadMinus))
            fpsTarget--;*/
	}
 
	/*void OnGUI()
	{
		int w = Screen.width, h = Screen.height;
 
		GUIStyle style = new GUIStyle();
 
		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (0.0f, 1.0f, 0.0f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)" + $" {fpsTarget} Target" 
		+ " Press '+' and '-' on the numpad to control FPS Target", msec, fps);
		GUI.Label(rect, text, style);
	}*/

}
