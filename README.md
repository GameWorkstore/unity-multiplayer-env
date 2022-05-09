# Unity Multiplayer Environment Tool
A tool to let the user quickly duplicate their unity project *without copying all the assets*, for multiplayer testing.
Enable your Unity projects to work with 1 single source code and many simlink projects side by side.
Use it your own risk!

# Why?
One method of quickly debugging multiplayer code is to run multiple unity editors of the same project, and inspect each instance as it works its way through the server/client functions. This is disabled by design for a unity project because of file IO concerns, so this tool lets you get around that by cloning your unity project and creating a series of hard links/junctions in your new cloned folder. These links point back to the original, which can let you edit code and see the results fairly quickly in each cloned unity.

# How to install

At package.json, add these lines of code:
```json
"com.gameworkstore.unitymultiplayerenv": "https://github.com/GameWorkstore/unity-multiplayer-env.git#1.0.0"
```

And wait for unity to download and compile the package.

you can upgrade your version by including the release version at end of the link:
```json
"com.gameworkstore.unitymultiplayerenv": "https://github.com/GameWorkstore/unity-multiplayer-env.git#1.0.0"
```

# Where?
The new cloned project will be placed library folder.

To open the window which allows to create a clone and manage it, in Unity Editor go to "Window/Unity Multiplayer Environment".
After the clone is created, you can launch it from the same window ("Open clone project" button will appear there).
No need to add the clone to Unity Hub or anywhere else.
