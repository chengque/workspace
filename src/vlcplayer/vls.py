#!/usr/bin/env python
# -*- coding: utf-8 -*- 
import md5 
import os 
import sys
from time import clock as now 

path=sys.argv[2];
ss=sys.argv[1];
sz=sys.argv[3];
os.system("rm *.mkv")
print path
os.system("ffmpeg -i \""+path+"\" "+ss)

f=open(path+'.txt','w')
f.write(sys.argv[0]+' \"'+sys.argv[1]+'\" \"'+sys.argv[2]+'\" \"'+sys.argv[3]+'\"');
f.close();

pathdir=os.getcwd();

files=os.listdir(pathdir);
filenames='';
i=0;
for file in files:
	real_path=os.path.join(pathdir,file) 
	if os.path.isfile(real_path) == True and real_path.find("mkv")>0 and not file.startswith("_"):
		if i<1:
			filenames='\''+real_path+'\''
		else:
			filenames=filenames+' +\''+real_path+'\''
		i=i+1;
#			os.system('ffmpeg -i \''+real_path+'\' -f mp4 -strict -2 \''+path+'/out/'+file+'.mp4\'')
#			if i>1:
#				break;
print filenames
f=open('tmp.txt','w')
f.write(filenames);
f.close();
os.system('mkvmerge -o \''+path+sz+'.apact.mkv\' '+filenames)
#os.system('rm *.mkv')
#os.system('ffmpeg -f concat -i tmp.txt -vcodec copy -acodec copy \''+path+'/nect.avi\'')
