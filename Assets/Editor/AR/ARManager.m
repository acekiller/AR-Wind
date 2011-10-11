//
//  ARManager.m
//  Unity-iPhone
//
//  Created by Mike on 12/17/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "ARManager.h"


@implementation ARManager

@synthesize textureId = _textureId, width = _width, height = _height;

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject

+ (ARManager*)sharedManager
{
	static ARManager *sharedManager = nil;
	
	if( !sharedManager )
		sharedManager = [[ARManager alloc] init];
	
	return sharedManager;
}


- (id)init
{
	// early out for no support
	if( ![ARManager isCaptureAvailable] )
		return nil;
	
	if( ( self = [super init] ) )
	{
		_textureId = -1;
		_orientation = [UIApplication sharedApplication].statusBarOrientation;
	}
	return self;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Public

+ (BOOL)isCaptureAvailable
{
	Class class = NSClassFromString( @"AVCaptureDevice" );
	
	// we need a legit class and at least one device
	return ( class && [ARManager availableDevices].count );
}


+ (NSArray*)availableDevices
{
	return [AVCaptureDevice devicesWithMediaType:AVMediaTypeVideo];
}


- (void)startCameraCaptureWithDevice:(NSString*)deviceId lowQuality:(BOOL)useLowQuality
{
	if( session )
	{
		NSLog( @"camera capture already running" );
		return;
	}
	
	// Unity created a new texture for us so change a couple settings on it
	glBindTexture( GL_TEXTURE_2D, _textureId );
	
	// this is necessary for non-power-of-two textures
	glTexParameteri( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR );
	glTexParameteri( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
	glTexParameteri( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE );
	glTexParameteri( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE );
	
	
	// Create the AVCapture Session
	session = [[AVCaptureSession alloc] init];
	[session beginConfiguration];
	
	// Get the default camera device
	AVCaptureDevice *camera = [AVCaptureDevice deviceWithUniqueID:deviceId];
	
	// Create a AVCaptureInput with the camera device
	NSError *error = nil;
	AVCaptureDeviceInput *cameraInput = [[AVCaptureDeviceInput alloc] initWithDevice:camera error:&error];
	if( cameraInput == nil )
	{
		NSLog( @"Error creating camera capture: %@", [error localizedDescription] );
		return;
	}
	
	if( [session canAddInput:cameraInput] )
		[session addInput:cameraInput];
	
	// Set the output
	AVCaptureVideoDataOutput *videoOutput = [[AVCaptureVideoDataOutput alloc] init];
	videoOutput.alwaysDiscardsLateVideoFrames = YES;
	
	if( [session canAddOutput:videoOutput] )
		[session addOutput:videoOutput];
	
	// create a queue to run the capture on
	//dispatch_queue_t captureQueue = dispatch_queue_create( "captureQueue", NULL );
	
	// setup our delegate and use the main queue because we will set the texture in the callback
	// and we need to be on the main thread
	[videoOutput setSampleBufferDelegate:self queue:dispatch_get_main_queue()];
	
	// configure the pixel format
	videoOutput.videoSettings = [NSDictionary dictionaryWithObject:[NSNumber numberWithUnsignedInt:kCVPixelFormatType_32BGRA] forKey:(id)kCVPixelBufferPixelFormatTypeKey];
	videoOutput.minFrameDuration = CMTimeMake( 1, 30 );
	
	// and the size of the frames we want
	if( useLowQuality )
		[session setSessionPreset:AVCaptureSessionPresetLow];
	else
		[session setSessionPreset:AVCaptureSessionPresetMedium];
	
	[session commitConfiguration];
	
	// Start the session
	[session startRunning];		
}


- (void)stopCameraCapture
{
	_textureId = -1;
	
	[session stopRunning];
	[session release];
	session = nil;
}


- (void)setExposureMode:(AVCaptureExposureMode)exposureMode
{
	if( !session || session.inputs.count == 0 )
		return;
	
	AVCaptureDeviceInput *input = [session.inputs objectAtIndex:0];
	if( [input.device lockForConfiguration:nil] )
	{
		if( [input.device isExposureModeSupported:exposureMode] )
			input.device.exposureMode = exposureMode;
		[input.device unlockForConfiguration];
	}
}


- (void)setFocusMode:(AVCaptureFocusMode)focusMode
{
	if( !session || session.inputs.count == 0 )
		return;
	
	AVCaptureDeviceInput *input = [session.inputs objectAtIndex:0];
	if( [input.device lockForConfiguration:nil] )
	{
		if( [input.device isFocusModeSupported:focusMode] )
			input.device.focusMode = focusMode;
		[input.device unlockForConfiguration];
	}
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark AVCaptureVideoDataOutputSampleBufferDelegate

- (void)saveScreenshot:(CVImageBufferRef)cvimgRef
{
	// only save the nth frame
	static int currentFrame = 0;
	currentFrame++;
	
	if( currentFrame != 200 )
		return;
	
	NSLog( @"------------------ SAVING THE CURRENT FRAME -------------------" );
	
	// access the data
	int width = CVPixelBufferGetWidth( cvimgRef );
	int height = CVPixelBufferGetHeight( cvimgRef );
	
	// get the raw image bytes
	uint8_t *buf = (uint8_t*)CVPixelBufferGetBaseAddress( cvimgRef );
	size_t bprow = CVPixelBufferGetBytesPerRow( cvimgRef );
	
	// turn it into something useful
	CGColorSpaceRef colorSpace = CGColorSpaceCreateDeviceRGB();
	CGContextRef context = CGBitmapContextCreate( buf, width, height, 8, bprow, colorSpace, kCGBitmapByteOrder32Little | kCGImageAlphaNoneSkipFirst );
	
	CGImageRef image = CGBitmapContextCreateImage( context );
	CGContextRelease( context );
	CGColorSpaceRelease( colorSpace );
	
	UIImageWriteToSavedPhotosAlbum( [UIImage imageWithCGImage:image], nil, nil, nil );
	CGImageRelease( image );
}


- (void)captureOutput:(AVCaptureOutput*)captureOutput didOutputSampleBuffer:(CMSampleBufferRef)sampleBuffer fromConnection:(AVCaptureConnection*)connection
{
	CMFormatDescriptionRef formatDesc = CMSampleBufferGetFormatDescription( sampleBuffer );
	CMVideoDimensions videoDimensions = CMVideoFormatDescriptionGetDimensions( formatDesc );

	CVImageBufferRef pixelBuffer = CMSampleBufferGetImageBuffer( sampleBuffer );
	CVPixelBufferLockBaseAddress( pixelBuffer, 0 );

	
	// actual pixel buffer size debug
	if( NO )
	{
		int width = CVPixelBufferGetWidth( pixelBuffer );
		int height = CVPixelBufferGetHeight( pixelBuffer );
		
		NSLog( @"pixel buffer width: %d, height: %d", width, height );
	}
	
	
	glBindTexture( GL_TEXTURE_2D, _textureId );
	
	unsigned char * linebase = (unsigned char *)CVPixelBufferGetBaseAddress( pixelBuffer );
	
	glTexSubImage2D( GL_TEXTURE_2D, 0, 0, 0, videoDimensions.width, videoDimensions.height, GL_BGRA_EXT, GL_UNSIGNED_BYTE, linebase );
	
	CVPixelBufferUnlockBaseAddress( pixelBuffer, 0 );
}


@end
