makeSound.class is a Java object which enables wav sound reproduction.
It automatically detects your hardware settings and opens/closes all resources.

It can easily be implemented in Matlab:

-First add java path to the folder where makeSound class is saved:
javaaddpath('C:\myJavaFolder')

-Then just use it:
makeSound(1,0,44200,'C:/mySounds/Bleep.wav');

*First variable represents gain value - 1 is straight reproduction according to your
current audio settings. Amplification is linear, but it is recommended that in your
final solution you implement logarithmic gain increase.

*Second variable represents panning - this value states which of two speakers is going to
be dominant and by how much. Values can be in range [-1,1], where -1 is complete
dominance of left speaker and 1 of the right one. 0 evenly distributes sound on both 
speakers.

*Third variable represents sampling rate which will be used for sound reproduction.

*Fourth variable represents exact path to the audio file.

-- Ivan
