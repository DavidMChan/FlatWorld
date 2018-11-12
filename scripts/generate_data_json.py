import math
import random

PREFIX = """{
  "experiments": [
    {
      "data": [
        {
          "type": "neutral",
          "scale_x": 0.001,
          "scale_y": 0.001,
          "scale_z": 0.001,
          "xrot_offset": 90.0,
          "y_offset": -0.02,
          "show_error": false,
          "error_type": "heatmap",
          "enable_tracking": true
        }
      ],
      "hand_type": "leap",
      "show_hands": true,
      "use_vive_tracker": false
    },
    {
      "data": [
        {
          "type": "neutral",
          "scale_x": 0.001,
          "scale_y": 0.001,
          "scale_z": 0.001,
          "xrot_offset": 90.0,
          "y_offset": -0.02,
          "show_error": false,
          "error_type": "heatmap",
          "enable_tracking": true
        }
      ],
      "hand_type": "rcup",
      "show_hands": true,
      "use_vive_tracker": false
    },
    {
      "data": [
        {
          "type": "neutral",
          "scale_x": 0.001,
          "scale_y": 0.001,
          "scale_z": 0.001,
          "xrot_offset": 90.0,
          "y_offset": -0.02,
          "show_error": false,
          "error_type": "heatmap",
          "enable_tracking": true
        }
      ],
      "hand_type": "tracker",
      "show_hands": true,
      "use_vive_tracker": true
    },
    {
      "data": [
        {
          "type": "neutral",
          "scale_x": 0.001,
          "scale_y": 0.001,
          "scale_z": 0.001,
          "xrot_offset": 90.0,
          "y_offset": -0.02,
          "show_error": true,
          "error_type": "heatmap",
          "enable_tracking": true
        }
      ],
      "show_hands": false,
      "use_vive_tracker": false
    },
    {
      "data": [
        {
          "type": "neutral",
          "scale_x": 0.001,
          "scale_y": 0.001,
          "scale_z": 0.001,
          "xrot_offset": 90.0,
          "y_offset": -0.02,
          "show_error": true,
          "error_type": "shell",
          "enable_tracking": true
        }
      ],
      "show_hands": false,
      "use_vive_tracker": false
    },
"""

SUFFIX = """  ]
}
"""

TEMPLATE = """    {
      "data": [
        {
          %s
        }
      ],
      "show_hands": true,
      %s
    },
"""

HAND_REPS = [
  """"hand_type": "leap",
      "use_vive_tracker": false""",
  """"hand_type": "rcup",
      "use_vive_tracker": false""",
  """"hand_type": "tracker",
      "use_vive_tracker": true"""
]
OBJECT_REPS = [
  "\"show_error\": false,",
  "\"show_error\": true,\n          \"error_type\": \"shell\",",
  "\"show_error\": true,\n          \"error_type\": \"heatmap\",",
]
OBJECT_TYPES = [
  """"type": "controller",
          "scale_x": 1.0,
          "scale_y": 1.0,
          "scale_z": 1.0,
          "xrot_offset": 105.0,
          "y_offset": 0.005,
          %s
          %s
          "enable_tracking": true
  """,
  """"type": "cylinder",
          "scale_x": 0.07,
          "scale_y": 0.1,
          "scale_z": 0.07,
          "xrot_offset": 90.0,
          "y_offset": -0.11,
          %s
          %s
          "enable_tracking": true
  """
]

# Specify number of participants.
NUM_FILES = 10

for i in range(NUM_FILES):
  with open('data_{}.json'.format(i), 'w') as f:
    f.write(PREFIX)
    num_reps = len(HAND_REPS)*len(OBJECT_REPS)
    # Create a randomized list unique to each object for treatment order.
    # object_randomization = {obj:random.sample(list(range(num_reps)), num_reps) for obj in OBJECT_TYPES}
    hand_rep_order = random.sample(list(range(len(HAND_REPS))), len(HAND_REPS))
    object_type_order = random.sample(list(range(len(OBJECT_TYPES))), len(OBJECT_TYPES))
    object_rep_order = random.sample(list(range(len(OBJECT_REPS))), len(OBJECT_REPS))

    # for i in range(num_reps): # for x in y
    for hand_rep_i in hand_rep_order:
      hand_rep = HAND_REPS[hand_rep_i]
      # Cycle through objects.
      for obj_type_i in object_type_order:
        obj = OBJECT_TYPES[obj_type_i]
        for object_rep_i in object_rep_order:
          object_rep = OBJECT_REPS[object_rep_i]
        # Last for loop
          # current_index = object_randomization[obj][i]
          # Grab appropriate hand and object representation.
          # hand_rep = HAND_REPS[current_index%len(HAND_REPS)]
          # object_rep = OBJECT_REPS[current_index//len(HAND_REPS)]
          # .02 is our `r`
          r = .02
          x_offset = random.random()*r
          z_offset = math.sqrt(r**2 - x_offset**2)
          if random.random() < .5:
            x_offset *= -1
          if random.random() < .5:
            z_offset *= -1
          offset = '"x_offset": %.5f,\n          "z_offset": %.5f,' % (x_offset, z_offset)

          str_to_add = TEMPLATE % (obj, hand_rep) % (object_rep, offset)
          # Remove final comma.
          if i == num_reps - 1 and obj == OBJECT_TYPES[-1]:
            str_to_add = str_to_add[:-2] + '\n'
            
          f.write(str_to_add)
    f.write(SUFFIX)


