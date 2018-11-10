import random

PREFIX = """{
  "experiments": [
    {
      "data": [
        
      ],
      "hand_type": "leap",
      "show_hands": true,
      "kinect_x_offset": 0.0,
      "kinect_y_offset": 0.0,
      "kinect_z_offset": 0.0
    },
    {
      "data": [
       
      ],
      "hand_type": "tracker",
      "show_hands": true,
      "kinect_x_offset": 0.0,
      "kinect_y_offset": 0.0,
      "kinect_z_offset": 0.0
    },
    {
      "data": [
      
      ],
      "hand_type": "cont",
      "show_hands": true,
      "kinect_x_offset": 0.0,
      "kinect_y_offset": 0.0,
      "kinect_z_offset": 0.0
    },
    {
      "data": [
        
      ],
      "hand_type": "ghostcont",
      "show_hands": true,
      "kinect_x_offset": 0.0,
      "kinect_y_offset": 0.0,
      "kinect_z_offset": 0.0
    },
"""

SUFFIX = """  ]
}
"""

TEMPLATE = """    {
      "data": [
        {
          "type": "%s",
          "repr": "%s",
          "wireframe": false
        }
      ],
      "hand_type": "%s",
      "show_hands": true,
      "kinect_x_offset": 0.0,
      "kinect_y_offset": 0.0,
      "kinect_z_offset": 0.0
    },
"""

HAND_REPS = ['leap', 'tracker', 'cont']
OBJECT_REPS = ['foo', 'bar', 'baz']
OBJECT_TYPES = ['controller', 'cup']
# TODO: Add object-specific offsets:
#   - Update TEMPLATE above with more string interpolation in correct places
#   - Update `str_to_add` line below to interpolate relevant values.

# Specify number of participants.
NUM_FILES = 5

for i in range(NUM_FILES):
  with open('data_{}.json'.format(i), 'w') as f:
    f.write(PREFIX)
    num_reps = len(HAND_REPS)*len(OBJECT_REPS)
    # Create a randomized list unique to each object for treatment order.
    object_randomization = {obj:random.sample(list(range(num_reps)), num_reps) for obj in OBJECT_TYPES}

    for i in range(num_reps):
      # Cycle through objects.
      for obj in OBJECT_TYPES:
        current_index = object_randomization[obj][i]
        # Grab appropriate hand and object representation.
        hand_rep = HAND_REPS[current_index%len(HAND_REPS)]
        object_rep = OBJECT_REPS[current_index//len(HAND_REPS)]

        str_to_add = TEMPLATE % (object_rep, obj, hand_rep)
        # Remove final comma.
        if i == num_reps - 1 and obj == OBJECT_TYPES[-1]:
          str_to_add = str_to_add[:-2] + '\n'
          
        f.write(str_to_add)
    f.write(SUFFIX)


