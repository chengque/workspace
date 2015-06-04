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

import threading;
from matplotlib import rc

rc('text',usetex=True)

filename='option-0000.ply'

print "File Name:"+filename




a=0;
file=open(filename)
lines=file.readlines()[13:-1]
aa=[]
for line in lines:
	temp=line.replace('\n','').split(' ');
	a=[];
	for str in temp:
		a.append(string.atof(str));
	aa.append(a)
v=aa[2];
print v[2]

plt.xticks(fontsize=40) 

plt.yticks(fontsize=40)


ax=plt.figure(num=1,figsize=(6,4))

fig=ax.gca(projection="3d")


print len(aa)
scene=display(title='Quadrotor Simulator',x=1,center=(0,0,0))
		
scene.ambient=0.2
		
scene.width=1000;
		
scene.height=750;
		
scene.range=(5,1,1)
#scene.autoscale=True
		
scene.forward=(0,-0.25,-1)
#distant_light(direction=(0,-1,0),color=color.red)
		
local_light(pos=(0,100,0),color=color.red)

for v in aa:
	sphere(pos=(v[0],v[2],-v[1]),color=(v[6]/255,v[7]/255,v[8]/255),radius=0.005,material=materials.plastic)
print 'Done'