# LightGun

The app allows you to use your android phone as a pointing device and as an arcade lightgun in particular. The app consists of two parts: client which must be installed on a phone and server which must be installed on PC. The client tracks its position and rotation using augmented reality engine, then finds a point where an imaginary line parallel to the edge of the screen intersects the surface of a monitor, then sends this point to the server via http-request. Also the client sends button presse to the server. The server puts the Windows cursor at the coordinates of intersection point and also simulates mouse and keyboard button presses. The app was tested for emulating Nintendo Wii rail shooters, specifically House of the Dead: Overkill. Currently only Huawei AR Engine being supported but can be easily converted to Google AR Core or other AR engines.

In order to run:

0. The phone and PC must be in the same Wi-Fi network.
1. Install client to your phone. 
2. Run server. Enter the width and height of the screen in pixels.
3. Run app. Enter the IP adress and Port of a PC with running server.
4. Rotate a phone so that its screen looks to the left. Then place a phone so that it is perpendicular to the surface of the screen. Press Calibrate button. Then press Connect button.
5. If everything is OK - the mouse cursor will start moving in correspondence with your phone. In server change the multiplier variable - this variable corresponds to
6. the physical dimensions of the screen. Tune it until you will be satisfied with the intersection point.  
 

![ezgif com-gif-maker (6)](https://user-images.githubusercontent.com/66104180/159117664-e378130d-e772-49f9-8ec2-4753c8e1c123.gif)

![ezgif com-gif-maker (4)](https://user-images.githubusercontent.com/66104180/159117364-2d8ab055-494d-4da1-8cc4-fc13959d2e75.gif)

![ezgif com-gif-maker (5)](https://user-images.githubusercontent.com/66104180/159117365-640c5898-972d-427d-b3f3-d647c3867928.gif)
