RocketNetUnity
==============

Unity implementation of GNU Rocket Client.

This is an Unity extension for GNU Rocket .NET implementation: https://github.com/kebby/RocketNet/

Usage
=====

Add Rocket.cs to Gameobject and set your music sample to generated Audio source. 

When rocket class public IsPlayer is true, it tries to find tracks from project root. When it is false, it is trying to connect to editor.

Add your desired track names to Tracks enum in Rocket.cs.

float GetValue(Tracks name) returns current value in selected track.

float GetTime() returns current time.

int GetRow() return current row.

For more documentation and the editor, check the original: https://github.com/kusma/rocket/ - RocketNet maps the API to two classes (Device and Track) that stay true to the original way of using it.
