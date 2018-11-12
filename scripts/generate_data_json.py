import random

PREFIX = """{
  "experiments": [
    {
      "data": [
        
      ],
      "hand_type": "leap",
      "show_hands": true,
      "use_vive_tracker": false
    },
    {
      "data": [
       
      ],
      "hand_type": "rcup",
      "show_hands": true,
      "use_vive_tracker": false
    },
    {
      "data": [
      
      ],
      "hand_type": "tracker",
      "show_hands": true,
      "use_vive_tracker": true
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
          "enable_tracking": true
  """,
  """"type": "cylinder",
          "scale_x": 0.07,
          "scale_y": 0.1,
          "scale_z": 0.07,
          "xrot_offset": 90.0,
          "y_offset": -0.11,
          %s
          "enable_tracking": true
  """
]

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

        str_to_add = TEMPLATE % (obj, hand_rep) % object_rep
        # Remove final comma.
        if i == num_reps - 1 and obj == OBJECT_TYPES[-1]:
          str_to_add = str_to_add[:-2] + '\n'
          
        f.write(str_to_add)
    f.write(SUFFIX)


