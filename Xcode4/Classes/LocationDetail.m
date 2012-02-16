//
//  LocationDetail.m
//  Test
//
//  Created by Qiuhao Zhang on 6/22/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "LocationDetail.h"
#import "Location.h"


@implementation LocationDetail
@synthesize location, fieldLabels, tempValues, textFieldBeingEdited;

-(IBAction)cancel: (id) sender {
	
	NSLog(@"cancel");
	[self.navigationController popViewControllerAnimated:YES];
	
	
}

-(IBAction)save: (id) sender {
	
	if (textFieldBeingEdited != nil) {
		NSNumber *tagAsNum = [[NSNumber alloc] initWithInt:textFieldBeingEdited.tag];
		
		[tempValues setObject:textFieldBeingEdited.text forKey:tagAsNum];
		[tagAsNum release];
	}
	
	for (NSNumber *key in [tempValues allKeys]) {
		switch ([key intValue]) {
			case kLatitudeRowIndex:
				location.latitude = [tempValues objectForKey:key];
				break;
			case kLongitudeRowIndex:
				location.longitude = [tempValues objectForKey:key];
				break;
			case kAltitudeRowIndex:
				location.altitude = [tempValues objectForKey:key];
				break;

			default:
				break;
		}
	}
	
	[self.navigationController popViewControllerAnimated:YES];
	
	NSArray *allControllers = self.navigationController.viewControllers;
	UITableViewController *parent = [allControllers lastObject];
	
	[parent.tableView reloadData];
	
	
}

-(IBAction)textFieldDone: (id) sender {
	[sender	 resignFirstResponder];
		
}


#pragma mark -
#pragma mark View lifecycle


- (void)viewDidLoad {
	
	[[UIApplication sharedApplication] setStatusBarHidden:NO];
	[[self navigationController] setNavigationBarHidden:NO];
	
	self.title = @"Location Settings";
	
	NSArray *list = [[NSArray alloc] initWithObjects:@"Latitude", @"Longitude", @"Altitude", nil];
	
	self.fieldLabels = list;
	[list release];
	
	UIBarButtonItem *cancelButton = [[UIBarButtonItem alloc] initWithTitle:@"Cancel" 
																	style:UIBarButtonItemStyleDone 
																	target:self
																	action:@selector(cancel:)];
	self.navigationItem.leftBarButtonItem = cancelButton;
	[cancelButton release];
	
	UIBarButtonItem *saveButton = [[UIBarButtonItem alloc] initWithTitle:@"Save" 
																   style:UIBarButtonItemStyleDone 
																  target:self 
																  action:@selector(save:)];
	self.navigationItem.rightBarButtonItem = saveButton;
	[saveButton release];
	
	NSMutableDictionary *dict = [[NSMutableDictionary alloc] init];
	self.tempValues = dict;
	[dict release];
	
	
	
    [super viewDidLoad];

    // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
    // self.navigationItem.rightBarButtonItem = self.editButtonItem;
}


/*
- (void)viewWillAppear:(BOOL)animated {
    [super viewWillAppear:animated];
}
*/
/*
- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
}
*/
/*
- (void)viewWillDisappear:(BOOL)animated {
    [super viewWillDisappear:animated];
}
*/
/*
- (void)viewDidDisappear:(BOOL)animated {
    [super viewDidDisappear:animated];
}
*/
/*
// Override to allow orientations other than the default portrait orientation.
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation {
    // Return YES for supported orientations.
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
*/


#pragma mark -
#pragma mark Table view data source

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    // Return the number of sections.
    return 1;
}


- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    // Return the number of rows in the section.
    return kNumberOfEditableRows;
}


// Customize the appearance of table view cells.
- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    static NSString *CellIdentifier = @"Cell";
    
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
    if (cell == nil) {
        cell = [[[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifier] autorelease];
		
		UILabel *label = [[UILabel alloc] initWithFrame:
						  CGRectMake(10, 10, 95, 25)];
		label.textAlignment = UITextAlignmentLeft;
		label.tag = kLabelTag;
		label.font = [UIFont boldSystemFontOfSize:17];
		
		[cell.contentView addSubview:label];
		[label release];
		
		UITextField *textField = [[UITextField alloc] initWithFrame:
								  CGRectMake(95, 12, 200, 25)];
		textField.clearsOnBeginEditing = NO;
		[textField setDelegate:self];
		textField.returnKeyType = UIReturnKeyDone;
		[textField addTarget:self action:@selector(textFieldDone:) 
			forControlEvents:UIControlEventEditingDidEndOnExit];
		
		[cell.contentView addSubview:textField];
		
    }
    
	// Configure the cell...
	NSInteger row = [indexPath row];
	UILabel *label = (UILabel *)[cell viewWithTag:kLabelTag];
	UITextField *textField = nil;
	
	for (UIView *oneView in cell.contentView.subviews) {
		if ([oneView isMemberOfClass:[UITextField class]]) {
			textField = (UITextField *)oneView;
		}
	}
	
	
	label.text = [fieldLabels objectAtIndex:row];
	NSNumber *rowAsNum = [[NSNumber alloc] initWithInt:row];
	switch (row) {
		case kLatitudeRowIndex:
			if ([[tempValues allKeys] containsObject:rowAsNum]) {
				textField.text = [tempValues objectForKey:rowAsNum];
			}
			
			else {
				textField.text = location.latitude;
			} 
			break;
			
		case kLongitudeRowIndex:
			if ([[tempValues allKeys] containsObject:rowAsNum]) {
				textField.text = [tempValues objectForKey:rowAsNum];
			}
			
			else {
				textField.text = location.longitude;
			}
			break;
			
		case kAltitudeRowIndex:
			if ([[tempValues allKeys] containsObject:rowAsNum]) {
				textField.text = [tempValues objectForKey:rowAsNum];
			}
			
			else {
				textField.text = location.altitude;
			}
			break;
		default:
			break;
	}
	
	if (textFieldBeingEdited == textField) {
		textFieldBeingEdited = nil;
	}
	
	textField.tag = row;
	[rowAsNum release];
    
    return cell;
}


/*
// Override to support conditional editing of the table view.
- (BOOL)tableView:(UITableView *)tableView canEditRowAtIndexPath:(NSIndexPath *)indexPath {
    // Return NO if you do not want the specified item to be editable.
    return YES;
}
*/


/*
// Override to support editing the table view.
- (void)tableView:(UITableView *)tableView commitEditingStyle:(UITableViewCellEditingStyle)editingStyle forRowAtIndexPath:(NSIndexPath *)indexPath {
    
    if (editingStyle == UITableViewCellEditingStyleDelete) {
        // Delete the row from the data source.
        [tableView deleteRowsAtIndexPaths:[NSArray arrayWithObject:indexPath] withRowAnimation:UITableViewRowAnimationFade];
    }   
    else if (editingStyle == UITableViewCellEditingStyleInsert) {
        // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
    }   
}
*/


/*
// Override to support rearranging the table view.
- (void)tableView:(UITableView *)tableView moveRowAtIndexPath:(NSIndexPath *)fromIndexPath toIndexPath:(NSIndexPath *)toIndexPath {
}
*/


/*
// Override to support conditional rearranging of the table view.
- (BOOL)tableView:(UITableView *)tableView canMoveRowAtIndexPath:(NSIndexPath *)indexPath {
    // Return NO if you do not want the item to be re-orderable.
    return YES;
}
*/


#pragma mark -
#pragma mark Table view delegate

- (NSIndexPath *)tableView:(UITableView *)tableView willSelectRowAtIndexPath:(NSIndexPath *)indexPath {
	
	return nil;
	
	
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
    // Navigation logic may go here. Create and push another view controller.
    /*
    <#DetailViewController#> *detailViewController = [[<#DetailViewController#> alloc] initWithNibName:@"<#Nib name#>" bundle:nil];
    // ...
    // Pass the selected object to the new view controller.
    [self.navigationController pushViewController:detailViewController animated:YES];
    [detailViewController release];
    */
}


#pragma mark -
#pragma mark Text field delegate

-(void)textFieldDidBeginEditing:(UITextField *)textField {
	
	self.textFieldBeingEdited = textField;
	
}

- (void)textFieldDidEndEditing:(UITextField *)textField {
	
	NSNumber *tagAsNum = [[NSNumber alloc] initWithInt:textField.tag];
	[tempValues setObject:textField.text forKey:tagAsNum];
	[tagAsNum release];
	
}

#pragma mark -
#pragma mark Memory management

- (void)didReceiveMemoryWarning {
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Relinquish ownership any cached data, images, etc. that aren't in use.
}

- (void)viewDidUnload {
    // Relinquish ownership of anything that can be recreated in viewDidLoad or on demand.
    // For example: self.myOutlet = nil;
}


- (void)dealloc {

	[location release];
	[fieldLabels release];
	[tempValues release];
	[textFieldBeingEdited release];
	
	location = nil;
	fieldLabels = nil;
	tempValues = nil;
	textFieldBeingEdited = nil;
	
    [super dealloc];
}


@end

