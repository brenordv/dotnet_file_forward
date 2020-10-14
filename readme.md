# What is this?
I've created this project as a proof of concept for a friend that's stuck with a...rather limited programming language (that doesn't support https requests).
This application forwards a file to an api's endpoint. Simple as that.

Another thing i wanted to demonstrate was how to use appsettings.json from a .net core 3.1 Console Application.

There's not much use for this application and i've made it quite hastly, so expect bugs and things that could be coded better.


# Projects

## FileForward
The main project. Here's all the logic and all you need to use it.

## FileForwardCLI
Simple "real world" implementation of the engine. To use:
1. You can pass a *filename* to the commandline and the application wil read the file and upload it's contents (as bytes) to the endpoint.

2. Pass the *file content as a byte array converted to string* and the application will convert it back to byte[] and then upload it.

3. Pass a *folder* in the commandline and the app will send all the files there (non-recursive) to the API.

4. Mix and match any of those options .


## FileForwardConsole
A simple test program, to demonstrate how to use the engine.

## FileForwardTests
A couple tests for my validation helpers. I was going to create more tests, but i realized this is just a simple PoC that i'll probably never use again... so...


# Important note
As a dummy backend, i'm using JSONPlaceholder -> https://jsonplaceholder.typicode.com/
It's great! you should give it a try! :D