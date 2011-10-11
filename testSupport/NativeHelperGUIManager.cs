using UnityEngine;
using System.Collections.Generic;


public class NativeHelperGUIManager : MonoBehaviour
{
	void Start()
	{
		// jump to landscape left in case we arent there already (Unity 3.2 syntax)
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		
		// initialize the NativeToolkitBinding.  This is only necessary if you don't call any other NativeToolkitBinding methods.
		// We do it here in case a call comes in (or other interruption occurs) before we call any other methods.
		NativeToolkitBinding.init();
	}


	void OnEnable()
	{
		// convenient place to listen to events
		NativeToolkitManager.appWillResignActive += appWillResignActive;
	}
	
	
	void OnDisable()
	{
		// stop listenting to events
		NativeToolkitManager.appWillResignActive -= appWillResignActive;
	}
	
	
	// Application will resign active event handler
	void appWillResignActive()
	{
		// this will get fired from native code whenever a call comes in, text message, push
		// notification or other interruption.  This is a great hook for pausing your game
		// so we will show the NativePauseViewController
		NativeToolkitBinding.activateUIWithController( "NativePauseViewController" );
	}


	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 5.0f;
		float width = ( Screen.width == 960.0f ) ? 310.0f : 155.0f;
		float height = ( Screen.width == 960.0f ) ? 80.0f : 40.0f;
		float heightPlus = height + ( ( Screen.width == 960f ) ? 20.0f : 10.0f );
	
	
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Load TestViewController" ) )
		{
			NativeToolkitBinding.activateUIWithController( "TestViewController" );
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Use Fade" ) )
		{
			NativeToolkitBinding.setAnimationTypeAndSubtype( AnimationType.Fade, AnimationSubtype.FromTop );
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Use Reveal, Top" ) )
		{
			NativeToolkitBinding.setAnimationTypeAndSubtype( AnimationType.Reveal, AnimationSubtype.FromTop );
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Use Reveal, Bottom" ) )
		{
			NativeToolkitBinding.setAnimationTypeAndSubtype( AnimationType.Reveal, AnimationSubtype.FromTop );
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Use Push, Left" ) )
		{
			NativeToolkitBinding.setAnimationTypeAndSubtype( AnimationType.Push, AnimationSubtype.FromLeft );
		}
		
		
		yPos = 5.0f;
		xPos = Screen.width - 5.0f - width;
		
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Set Duration to 0.1" ) )
		{
			NativeToolkitBinding.setAnimationDuration( 0.1f );
		}

		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Use MoveIn, Left" ) )
		{
			NativeToolkitBinding.setAnimationTypeAndSubtype( AnimationType.MoveIn, AnimationSubtype.FromLeft );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Use MoveIn, Top" ) )
		{
			NativeToolkitBinding.setAnimationTypeAndSubtype( AnimationType.MoveIn, AnimationSubtype.FromTop );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Use MoveIn, Bottom" ) )
		{
			NativeToolkitBinding.setAnimationTypeAndSubtype( AnimationType.MoveIn, AnimationSubtype.FromBottom );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Native Pause" ) )
		{
			NativeToolkitBinding.activateUIWithController( "NativePauseViewController" );
		}
	}


}
