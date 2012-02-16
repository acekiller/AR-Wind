//
//  LocationDetail.h
//  Test
//
//  Created by Qiuhao Zhang on 6/22/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@class Location;


#define kNumberOfEditableRows	3
#define kLatitudeRowIndex		0
#define kLongitudeRowIndex		1
#define kAltitudeRowIndex		2

#define kLabelTag	4096


@interface LocationDetail : UITableViewController <UITableViewDelegate, UITextFieldDelegate>{
	
	Location *location;
	NSArray *fieldLabels;
	NSMutableDictionary *tempValues;
	UITextField *textFieldBeingEdited;

}

@property (nonatomic, retain) Location *location;
@property (nonatomic, retain) NSArray *fieldLabels;
@property (nonatomic, retain) NSMutableDictionary *tempValues;
@property (nonatomic, retain) UITextField *textFieldBeingEdited;


-(IBAction)cancel: (id) sender;
-(IBAction)save: (id) sender;
-(IBAction)textFieldDone: (id) sender;

@end
