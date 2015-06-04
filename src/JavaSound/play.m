function play(soundname)
p = mfilename('fullpath');
p = p(1:end-4);

if nargin < 1
    xx = dir([p, '*.wav']);
    disp('List of available Sounds');
    disp('************************');
    for iSound = 1:length(xx);
    disp(xx(iSound).name(1:end-4));
    end
    disp('************************');
    return;
end

tt = makeSound(1,0,44200, [p, soundname, '.wav']);
clear tt;
end