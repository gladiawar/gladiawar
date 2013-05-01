Please visit plyoung.com or plyoung.wordpress.com for more info.
The support forum for this package is at http://plyoung.com/forums/categories/connect-php Please check the above mentioned URLs if the forum seems offline.

This package shows you how to connect a game to a PHP based server. The server 
scripts for this package was written in PHP and uses the Yii Framework.
The client side scripts are all done in C#.

See http://www.yiiframework.com/ for more information on Yii, a high-performance
PHP framework. You need to know how to use the Yii framework in order to
make proper use of this package.

Sample WebPlayer of package, http://www.plyoung.com/misc/unityphp/sample.htm

*** This package features the following systems ...

* Account system: 
Players can Register, Login, Initiate Password Recovery from within the game client.
There is also a sample of how to save a player's settings to the server and load it
when the player login.

* Friends system: 
Friends list. Add and remove friends.

* Chat system:
Public channel for all players and private chat channels between 2 players.

* Simple News and Advert system:
Sample of how to load and show news and adverts in the client.


*** Getting started

As stated in the description of the package, you need to know Yii to be able to use this package so please first read up on it and make sure you can run a simple Yii based demo in your browser before trying this package http://www.yiiframework.com/doc/

Here are the basic steps for getting it to work on your localhost with a clean/default xampp setup. You are on your own if you messed around with the config and somehow made it not work with the htaccess file I provided and the Yii URLManager. This package makes use of the "path" urlFormat. If you don't know what this is you need to read the Yii docs aghain.

I will assume you are using and installed XAMPP to c:/campp and Yii to c:/yii

1) Extract "Assets\Connect PHP\servers\php.zip" to c:/xampp/htdocs/testapp

2) Open browser and enter http://localhost/testapp/url.php If it works, good, go to next step, else fix the problem with your webserver setup.

3) It is important that you have the .htaccess file that I provided also copied to c:/xampp/htdocs/testapp dir, and I will state again that this is for a default xampp install, I am not a web admin and can't know how to make the Yii URLManager work with different server setups; for that you need to read the Yii docs.

4) Setup c:/xampp/htdocs/testapp/index.php and make sure the path you enter for the $yii variable is correct! For example, $yii='c:/yii/framework/yii.php';

5) Open url.php and make changes to the CHANGE_ME part without removing the /game/ and /main/ parts. In this example you want to change it to ...
echo '1';
echo '|http://localhost/testapp/main/';
echo '|http://localhost/testapp/game/';

6) Open browser and enter http://localhost/testapp/url.php You should see some kind of output other than an error. It should be someyhing like this..
1|http://localhost/testapp/main/|http://localhost/testapp/game/

If your output started with junk like this "﻿1" then make sure to save your PHP scripts (like url.php) WITHOUT BOM. Look in your editor's encoding settings or in the save-as dialogue, there should be something along the lines of "UTF-8 without BOM". It is IMPORTANT that the scripts don't send that as the first characters of output. Right click in the page on the browser and choose view source to make sure those characters are not being send.

7) Open browser and senter http://localhost/sdgame/main/ver/ You should get "1000" in the browser output. The server is working if you got this far.

8) Now update MasterServer on client side in \Assets\Connect PHP\scripts\game.cs so you can run the test scene, for example,
public static string MasterUrl = "http://localhost/testapp/url.php";

9) Run test scene. You should get up to a login box. If you do tehn you know the client connected to the server without problems. You wil lalso see a "dump log" button which will open a page in your browser with the data that was send betwene the client and server up to that point.

Contacting Master Server: http://localhost/testapp/url.php
1|http://localhost/testapp/main/|http://localhost/testapp/game/
MainUrl = [http://localhost/testapp/main/]
GameUrl = [http://localhost/testapp/game/]
Version check: http://localhost/testapp/main/ver/
1000

*** Admin

This package do not include any admin to manage the adverts or news posts. You can use 
something like PhpMyAdmin or other SQL client to access your database and add records
to the News and Advert tables.

For Adverts you simply add new records with the URL field poiting to the URL for an advert.
Adverts should be an image like a jpg or png.

For News you need to add records and complete the dt (date) and msg (message) fields.





