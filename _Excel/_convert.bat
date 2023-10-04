echo off
del/q *.bak
del data\*.*/q
cls
path="C:\devtool\jdk1.8\bin";
set classpath = .;"C:\devtool\jdk1.8\lib\tools.jar";"C:\devtool\jdk1.8\lib\dt.jar";
javac ItemTool.java
java ItemTool


@echo ======================================
@echo 		Client File Move
@echo ======================================
copy iteminfo.xml		..\Assets\Resources\txt\iteminfo.xml

copy gameinfo.xml		..\Assets\Resources\txt\gameinfo.xml

copy tooltip.xml		..\Assets\Resources\txt\tooltip.xml


rem @echo ======================================
rem @echo 		Server File
rem @echo ======================================
rem 
rem del ..\_SQL\00_02_01Table_iteminfo.sql
rem copy iteminfoQuery.sql				..\_SQL\00_02_01Table_iteminfo.sql
rem 
pause
