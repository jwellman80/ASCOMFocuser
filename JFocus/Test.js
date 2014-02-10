var H = new ActiveXObject("ASCOM.Utilities.Chooser");
H.DeviceType = "Focuser";							// Make chooser for Focusers
var F = new ActiveXObject(H.Choose("")); 		// Create instance of selected Driver
F = null;
H = null;
