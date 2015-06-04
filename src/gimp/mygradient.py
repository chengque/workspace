#!/usr/bin/env python


# This tells Python to load the Gimp module 
from gimpfu import *

# This is the function that will perform actual actions
def auto_gradient_function(image, drawable, fore,back):
    layer_hard_light = pdb.gimp_layer_new_from_visible(image, image, "Hard light")
    layer_hard_light.mode = OVERLAY_MODE
    layer_hard_light.name = "Hard light"
    image.add_layer(layer_hard_light, -3)
    pdb.gimp_context_set_foreground(fore);
    pdb.gimp_context_set_background(back);
    pdb.plug_in_gradmap(image,drawable);
    return

# This is the plugin registration function
register(
    "auto_gradient",    
    "Auto Gradient Python-Fu",   
    "This script does nothing and is extremely good at it",
    "Michel Ardan", 
    "Michel Ardan Company", 
    "April 2010",
    "<Image>/MyScripts/Auto Gradient", 
    "*", 
    [
      (PF_COLOR, 'some_text', 'Some text input for our plugin', 'Write something'),
      (PF_COLOR, 'some_integer', 'Some number input for our plugin', 2010)
    ], 
    [],
    auto_gradient_function,
    )

main()
