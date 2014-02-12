Simple ASCOM Focuser example.  

This is an ok example of how to construct an ASCOM focuser such that the serial connection is protected.  

DoUpdate() is used to throttle the Get Properties such that it if they're queried within 1 second the buffered values will be used.  