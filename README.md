# Spotify CLI

This is a small hobby-project that makes a CLI for Spotify, written in C#.
The goal of the project is to make as good of a Spotify experience as possible from the terminal, becaue I thought it'd be fun/useful to control spotify from there :D

## Setup
0. Clone or fork this repository
1. Add github-packages to your nuget-sources, because a dependency of this project is [my CLI library](https://github.com/Drawserqzez/CLI/)
2. Start coding :)

## Feature suggestions
If there's a feature you'd like to see, please create an issue for it and I'll look it over :) 
Alternatively, you could try to make it yourself and create a pr :D

## Publishing and running the app
0. Open your terminal of choice and run the following command:

    ` dotnet publish -o <output-directory> -r <your-system> `

1. Put the published contents in your PATH if you haven't already
2. Rename the .exe to something shorter for easier use

## Todo

- [ ] Make more commands
- [x] Make a template for the config-file
- [x] If no config file is found, guide the user to create one

## SpotifyAPI-NET

This program uses a library [SpotifyAPI-NET](https://github.com/JohnnyCrazy/SpotifyAPI-NET) by Jonas Dellinger provided under the MIT license. 

```
Copyright 2020 Jonas Dellinger

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```