[![discord](https://img.shields.io/badge/discord-OpenNos-blue.svg?style=flat)](https://discord.gg/N8eqPUh)

#Instructions to contribute#

##Disclaimer##
This project is a community project not for commercial use. The emulator itself is proof of concept of our idea to try out anything what's not possible on original servers. The result is to learn and program together for prove the study. 

##Legal##

This Website and Project is in no way affiliated with, authorized, maintained, sponsored or endorsed by Gameforge or any of its affiliates or subsidiaries. This is an independent and unofficial server for educational use ONLY. Using the Project might be against the TOS.

#Attention!#
This emulation software is not open source to host any private servers. It is open source to work together community-based.
We do not provide any modified client files. The alorithms are based on our logic.

##Special Information for Hamachii and VPN Users##
If you want to use the Servers you need to Modify the Program.cs of both OpenNos.Login and OpenNos.World and rebuild the code.
- Change "127.0.0.1" to ip found in hamachi (Eg. "12.34.56.789")
- Dont forget to Modify the app.config of the Login-Server to the correct redirection (<server Name="S1-OpenNos" WorldPort="1337" WorldIp="12.34.56.789" channelAmount="1" />)

###Contribution is only possible with Visual Studio 2015 (Community or other editions), MySQL and [StyleCop extension](https://stylecop.codeplex.com/)###
#BEFORE CREATING ISSUE#
- Troubleshooting can be found [here](Troubleshooting.md)

##1 Install SSDT For Visual Studio##
http://go.microsoft.com/fwlink/?LinkID=393520&clcid=0x409

##2 Install MySQL##
http://dev.mysql.com/downloads/windows/installer/

Installer Packages:
- Custom Installation
  - MySQL Server x64 (Database Server)
  - MySQL Workbench x64 (Data Edtiting)
  - MySQL Notifier x86 (Taskbar Icon Status)
  - MySQL for Visual Studio x86
  - Connector/NET x86 (according to our test theres a issue with 6.9.9 version please use [6.9.8 instead](https://downloads.mysql.com/archives/get/file/mysql-connector-net-6.9.8.msi))
  
- Port: 3306
- User: test
- Password: test

##3 Use the NuGet Package Manager to Update the Database##

- Go to Tools -> NuGet Package Manager -> Package Manager Console
- Choose Project OpenNos.DAL.EF.MySQL
- Type 'update-database' and update the Database
