#-*- utf-8 -*-

import matplotlib
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import *
import scipy.io as sio
import numpy as ny
import os
import sys
import string
from visual import *
from OpenGL.GL import *
from OpenGL.GLU import *
from OpenGL.GLUT import *
import common
import sys

import threading;
from matplotlib import rc

def runsfm(ipath):
	filename='pmvs/models/option-0000.ply'
	print "File Name:"+filename

	os.system("rm *.txt");
	os.system("rm -R bundle");
	os.system("rm -R pmvs");
	os.system("rm -R prepare");
	os.system("rm -R tmp");

	os.system("cp -R "+ipath+" tmp")

#	print "Stage 1 ...."
	os.system("../RunBundler.sh tmp");
#	print "Stage 2 ...."
	os.system("../bin/Bundle2PMVS prepare/list.txt bundle/bundle.out");
#	print "Stage 3 ...."
	os.system("mv ./pmvs/prep_pmvs.sh ./pmvs/prep_pmvs.sh_bk");
	ffrom=open("./pmvs/prep_pmvs.sh_bk","r")  
	ffto=open("./pmvs/prep_pmvs.sh","w")   
	while True:  
		l = ffrom.readline()  
		if not l:  
		        break  
		if 'BUNDLER_BIN_PATH=' in l:  
		        temp1 = l.replace('BUNDLER_BIN_PATH=','BUNDLER_BIN_PATH='+os.path.split(os.getcwd())[0]+'/bin')
		        ffto.write(temp1)  
		elif 'echo' in l:
			continue
		else:    
		        temp1 = l  
		        ffto.write(temp1)  
	ffrom.close()
	ffto.close()
	os.system("chmod a+x ./pmvs/prep_pmvs.sh");
	os.system("./pmvs/prep_pmvs.sh");

	print "Stage 4 ...."
	os.system("../bin/cmvs pmvs/");
	os.system("../bin/genOption pmvs/");
	os.system("../bin/pmvs2 pmvs/ option-0000");

	a=0;

	import viewmodel
	viewmodel

ipath=sys.argv[1];
runsfm(ipath)


