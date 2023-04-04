# chatgpt-ifplatform
I used ChatGPT-4 to design and code an Interactive Fiction platform on top of .NET/C#.

I've gone through several iterations to learn how to impose my will on ChatGPT-4. It's not as simple as asking it to do things and the current timeout limitations prevent extended work periods. I've had to start new chats, give it an overview of our progress, then paste in all of the current code before continuing the design and implementation process.

That said, it's going very well and I have learned quite a few new things.

Look at the World class in the WorldModel project. It's based on an in-memory bidirectional graph data structure.

Not much to it, right? But the power of this kind of data structure is sizable. When we want to connect any two "things" within our world, we just create a bidirectional edge between two nodes. We hide the graphiness from the author by building out the IF things in a Standard Library.

Now look at the Core class in the StandardLibrary project.

You can see the fundamental IF code interacting with the World Model.

This is not a working system yet, but it's getting close.
