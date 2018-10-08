# FlatWorld
Flat Environment For Testing VR Interaction

# Operating in the environment

There are 4 button mappings, and two mapping for joysticks. 

### Button Mappings

XPress
YPress
WPress
ZPress

### Joystick Mappings

PosJR
PoseJL

## Controls

Moving around objects in the scene is a two-step process (thanks to the vive's delightful lack of buttons). On the left controller, hold a selector (XPress, PoseJL up, PoseJL down, PoseJL right, PoseJL left) to move objects around on the table. To move the table, use YPress as a selector. Further, the table can be moved up and down using WPress and ZPress while under the YPress selector.

A configuration can be saved with "WPress" and you can cycle through saved poses with "ZPress"

##

What you can't do yet: 

- Object size/rotation. currently, we can have up to 5 objects in a scene, so perhaps this could be solved by having multiple objects? We could also possibly use the WPress and ZPress to control size for the objects.
- Saving positions to text files for persistence. If the session ends, it's game over man.
- Fancy hand representations. They are currently just the controllers in 3D space.
