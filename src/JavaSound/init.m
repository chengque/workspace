p = mfilename('fullpath')
p = p(1:end-4)
javaaddpath(p)
disp([p,' added to java path']);
