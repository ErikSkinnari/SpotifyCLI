# Spotify CLI

This is a small hobby-project that makes a CLI for Spotify made with .NET C#.

## Setup
0. Clone or fork this repository
1. Add github-packages to your nuget-sources, because a dependency of this project is [my CLI library](https://github.com/Drawserqzez/CLI/)
2. Start coding :)

## Running the app after publishing
You'll wanna put the exe in the PATH someplace, and maybe change the name of the .exe to something shorter.

## Todo

- [ ] Make an error handler
- [ ] Make commands to do things
- [x] Make a template for the config-file
- [x] If no config file is found, guide the user to create one
- [ ] Start TDD instead of doing bad things

## SpotifyAPI-NET

This program uses a library [SpotifyAPI-NET](https://github.com/JohnnyCrazy/SpotifyAPI-NET) by Jonas Dellinger provided under the MIT license. 

```
Copyright 2020 Jonas Dellinger

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```