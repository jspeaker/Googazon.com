# Googazon.com

Need-Based Micro-Services Demonstration

This sample project pairs with the following presentation to demonstrate a simple, working, need-based micro-services platform.

The user-interface is quick and dirty by my standards, and serves to simply display results in a reasonable looking fashion. It is interesting and important, however, that the user-interface employs SignalR (which could just as well be WebSockets) in order to keep a socket connection open between client and server and provide for the ability to push results to the client as the become available.

The server-side code is the primary focus of this demonstration. It provides a Need Expression API, a function that hydrates the appropriate River (Service Bus Topics for unfulfilled or Queue for fulfilled), and functions that Fulfill Needs and push Fulfilled Needs from the queue to the client asynchronously.

https://prezi.com/p/st2nowahp4y8/?present=1
