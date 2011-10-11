using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


public enum AnimationType
{
	Fade,
	Reveal,
	MoveIn,
	Push
}


public enum AnimationSubtype
{
	FromLeft,
	FromRight,
	FromTop,
	FromBottom
}



public class NativeToolkitBinding
{
    [DllImport("__Internal")]
    private static extern void _nativeToolkitInit();

	// Initializes the NativeHelper.  This will automatically happen if you call any other method
	// in the NativeToolkitBinding class
    public static void init()
    {
        if( Application.platform != RuntimePlatform.OSXEditor )
			_nativeToolkitInit();
    }
    

    [DllImport("__Internal")]
    private static extern void _nativeToolkitActivateUIWithController( string controllerName );

	// Loads a view controller with the name controllerName along with it's nib file.  If there is a
	// nib file named controllerName + '-Pad' and the current device is an iPad, it will automatically
	// load the iPad nib
    public static void activateUIWithController( string controllerName )
    {
        if( Application.platform != RuntimePlatform.OSXEditor )
			_nativeToolkitActivateUIWithController( controllerName );
    }
	
	
    [DllImport("__Internal")]
    private static extern void _nativeToolkitDeactivateUI();

	// Close any open view controllers from Unity
    public static void deactivateUI()
    {
        if( Application.platform != RuntimePlatform.OSXEditor )
			_nativeToolkitDeactivateUI();
    }
    
    
    [DllImport("__Internal")]
    private static extern void _nativeToolkitSetAnimationTypeAndSubtype( string type, string subtype );

	// Sets the animation type and subtype that will be used whenever showing or hiding a view controller
    public static void setAnimationTypeAndSubtype( AnimationType type, AnimationSubtype subtype )
    {
        if( Application.platform != RuntimePlatform.OSXEditor )
		{
			// convert the types to strings and lowercase the first letter
			string aType = type.ToString();
			aType = aType.Substring( 0, 1 ).ToLower() + aType.Substring( 1 );
			string aSubtype = subtype.ToString();
			aSubtype = aSubtype.Substring( 0, 1 ).ToLower() + aSubtype.Substring( 1 );
			
			_nativeToolkitSetAnimationTypeAndSubtype( aType, aSubtype );
		}
    }

    [DllImport("__Internal")]
    private static extern void _nativeToolkitSetAnimationDuration( float duration );

	// Sets the duration of the transition animations when showing or hiding a view controller
    public static void setAnimationDuration( float duration )
    {
        if( Application.platform != RuntimePlatform.OSXEditor )
			_nativeToolkitSetAnimationDuration( duration );
    }

}
