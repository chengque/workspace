#!/usr/bin/env python
# -*- coding: utf-8 -*- 
import md5 
import os 
import sys
from time import clock as now 
import os.path

hpath=sys.argv[1];

hfiles=os.listdir(hpath);

hostname=os.popen('hostname').read();
print hostname
if hostname.find('5520')>0:
	cdir='/media/chnengque/'
else:
	cdir='/media/chengque/'

for hfile in hfiles:
	os.system('rm *.mkv')

	hreal_path=os.path.join(hpath,hfile) 
	if os.path.isfile(hreal_path) == True and hreal_path.find("txt")>0 and not hfile.startswith("_"):
		print "Open script:"+hreal_path
		f=open(hreal_path,'r')
		cmd=f.read();
		f.close();
		cmd.replace('\"','\'')
		if hostname.find('5520')>0:
			cdir='/media/chnengque/Seagate Backup Plus Drive'
		else:
			cmd=cmd.replace('chnengque','chengque')
			cdir='/media/chengque/Seagate Backup Plus Drive'
		
		try:
			sz=cmd.split(' \'')[-1].replace('\'','');
			filepath=cmd.split(' \'')[-2].replace('\'','');
			print 'filepath:'+filepath;
			ss=cmd.split(' \'')[1].replace('\'','');
			filedir,filename=os.path.split(filepath)
			print ss
		except:
			print "IO exception"
			continue;
		os.system("ffmpeg -i \'"+filepath+"\' "+ss)
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

		print filenames
		f=open('tmp.txt','w')
		f.write(filenames);
		f.close();
		filepath=filepath+sz+'.apact.mkv';
		filepath=filepath.replace('\'','');
		os.system('mkvmerge -o \''+filepath+'\' '+filenames)
		newpath=os.path.join(hpath,'_'+hfile) 
	        print hreal_path
		os.system('mv \''+hreal_path+'\' \''+newpath+'\'');
		newfilename='_'+filename+sz+'.apact.mkv';
		newpath=filedir+newfilename;
		os.system('ffmpeg -i \''+filepath+'\' -vcodec copy -acodec libmp3lame -ab 128k -map 0 \''+newpath+'\'');
		targetpath=os.path.join(cdir+'/HD filesystem/Windows NT/acol/',newfilename);
		print targetpath
		os.system('mv \''+newpath+'\' \''+targetpath+'\'')
		os.system('rm \''+filepath+'\'')
		print 'Finish:'+filepath
		print sz

