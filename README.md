# CTGP-7 Proximity Chat
The proximity chat functionality in the [CTGP-7 modpack](https://ctgp-7.github.io) allows all players running the proximity chat client to hear each other in race while playing online. Voices will be adjusted depending on how far you are, as well as a few other surprises. ;)

This application runs on a Windows computer or Android device and is available for both the Nintendo 3DS and Citra versions of CTGP-7.

## Download
You can get the latest version from the [releases page](https://github.com/PabloMK7/CTGP7ProximityChatClient/releases/latest).

## Usage
### Setup
1) [Download](https://github.com/PabloMK7/CTGP7ProximityChatClient/releases/latest) the proximity chat client and run it on a device (computer or Android device) that is in the same network as the device (citra or console) running CTGP-7.
2) In CTGP-7, select CTGP-7 Network and make sure to enable proximity chat in the public/private lobby selection screen.
3) When prompted, enter the server IP address shown by CTGP-7 in the proximity chat client and press connect. You can verify the connection was successful if the status indicator turns green. *NOTE: If you wait too long to connect, you may get a communication error. Just try again.*
4) Join any online room, and if other players are also using proximity chat, you will be connected together.

### Controls
- **Volume**: Adjusts the master volume of all the users.
- **Mic**: Changes the input device and adjusts its volume.
- **Doppler**: Changes how much the other player voices change depending on their velocity.

Furthermore, you can adjust the volume of each participant using the slider next to their names.

### Known issues
- Since [Vivox lacks Linux support](https://support.unity.com/hc/en-us/articles/4780622639636-Vivox-Does-Vivox-offer-Linux-support), a native build for Linux is not possible. You may have some luck using [Wine](https://www.winehq.org/) or Steam for Linux.
- When using the Android build, it's not possible to use the proximity chat client and Citra at the same time on the same device. This is due to Unity not being able to run in the background.
- It's not possible to have multiple instances of the proximity chat client on the same device.

## Credits
Voice chat functionality is provided by [Vivox](https://vivox.com/).