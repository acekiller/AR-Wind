using UnityEngine;
using System.Collections.Generic;


public class LevelTwoGUIManager : MonoBehaviour
{
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 5.0f;
		float width = 160.0f;
		float height = 40.0f;
	
	
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Back to 1st Scene" ) )
		{
			Application.LoadLevel( "NativeHelperTestScene" );
		}

	}


}
