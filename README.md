# DayNightPapers 

<p align="center">
  <img src="https://github.com/nikolajlauridsen/DayNightPapers/blob/master/ReadmeImages/Screenshot.PNG?raw=true">
  <br>
  <b>One wallpaper for the day, one for the night</b><br>
  <br>
</p>

# Table of contents
- [DayNightPapers](#daynightpapers)
- [Table of contents](#table-of-contents)
- [Why](#why)
- [Installation](#installation)
  - [First time setup](#first-time-setup)
- [Credits and third party licenses](#credits-and-third-party-licenses)
- [Isues, bugs or suggestions](#isues-bugs-or-suggestions)



# Why

Sitting up late one night working I minimized my windows and was imidiately blinded by a way to bright wallpaper, I really liked this wallpaper however and decided fix this and DayNightPapers was born. 

The premise is simple, when the sun rises the DayNightPapers automatically switches you wallpaper to your day time wallpaper, and when the sun sets it switches your wallpaper again to you night time wallpaper, this way you'll never be blinded by a bright wallpaper again. 


# Installation

**When you launch the program for the first time you will most likely get a virus waring from windows, this is because the executable isn't signed, this isn't a virus. If you want to avoid this you can compile the executable yourself and it won't happen, feel free to take a look at the source code as well.**

If you want the software to start automatically when windows starts, which is obviosuly optimal, you need to go through a few installation steps.

1. Download the newest version of the software from the [releases page](https://github.com/nikolajlauridsen/DayNightPapers/releases)
2. Unzip the file
3. Open the unzipped folder and create a shortcut to DayNightPapers.exe
4. Open the startup folder, the easiest way to do this is to open the run menu by pressing windows-r and then typing ```shell:startup``` into the run prompt, see image below
5. Copy or cut and paste the shortcut of DayNightPapers.exe into the startup folder
6. Enjoy

![Run prompt](https://github.com/nikolajlauridsen/DayNightPapers/blob/master/ReadmeImages/run_prompt.PNG?raw=true)

## First time setup

It's recommended that you open the software manually the first time. 

The first time you open the software you'll be met with this page
![Setup page](https://github.com/nikolajlauridsen/DayNightPapers/blob/master/ReadmeImages/setup_page.PNG?raw=true)

In order for the software to know when the sun rises and sets it needs to know your coordinates. To find your coordinates you can use [this site](https://www.gps-coordinates.net/), I'm not affiliated with this site but it seems to work well. 

**Be aware that you might have to change the dot to a comma if you live in a region where comma is used to seperate decimals**

It's not nececary to use your exact coordinates, your town or city will suffice. I don't use your coordinates for anything else than forwarding it to [sunrise-sunset.org](https://sunrise-sunset.org) however I'm not affiliated with that site, so I can't vouch for how they handle your data. 

Once you've set up your coordinates simply just press the submit button and you'll be navigated to the main page, here simply choose your day wallpaper and your night wallpaper with the select wallpaper button, and you're all set to go.

# Credits and third party licenses

The api for fetching sunrise and sunset time is provided by [Sunrise-Sunset.org](https://sunrise-sunset.org) thanks for supplying an awesome free api :heart:  

TrayIcon is called Hardcodet.NotifyIcon.Wpf and is made by Philipp Sumi

Sun icon made by Freepik from www.flaticon.com

Moon icon made by iconixar from www.flaticon.com

Wrench icon designed by Freepik from www.flaticon.com

# Isues, bugs or suggestions 
Feel free to create an issue using the github issue feature, I'd love to hear from you
