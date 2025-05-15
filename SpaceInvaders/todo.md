check from what point it start to draw the character on 0Y
eliminate the invisible space that wrappes the sprite in photoshop
change all the magic numbers to const values saved in a env
despawn the entities that exit the area
collision box
change speeds for characters to double instead of int, make the bots move slower
spawn the player on the middle of the ox axis of the screen


<!-- mgcb-editor Content/Content.mgcb
mgcb -@:"Content/Content.mgcb" /outputDir:Content/bin -->
dotnet mgcb -@:"Content/Content.mgcb" /outputDir:Content/bin