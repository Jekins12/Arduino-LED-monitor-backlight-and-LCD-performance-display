# Arduino based monitor backlight and CPU/GPU performance display
![photo](https://user-images.githubusercontent.com/92518823/201471047-e6bfb90b-f147-4e55-b614-4cc33842591b.jpg)
## How it works?
Program connecting to arduino via serial port (USB) and sending temperature values, current time, and data (colors) depending on the chosen mode.
## Available modes
* Temperature - backlight color changes from blue to red depending on GPU temperature
* Single color - pick a color by clicking a button "Select color"
* White - just white, I have nothing to add
* Patriotic - well, try it yourself :)
* Ambilight - in short, dynamically changes color of each LED depending on the average color of the screen in the given area

###
![app](https://user-images.githubusercontent.com/92518823/201470841-2bd74fac-de7e-4d50-8f09-7f31d22ccdcf.png)

## Known bugs
* Ambilight
Ambilight is a pretty heavy mode for arduino, it works but due to simultaneous sending temperature to LCD and color data to LED, data sometimes are mixing together, and random LEDs are glitching and changing color. To "fix" it, I just stop transferring data to LCD until mode change.
