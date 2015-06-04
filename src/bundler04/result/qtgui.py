# -*- coding:utf-8 -*-
# file: PyQtButtonEvent.py
#
import sys
from PyQt4 import QtCore, QtGui 
import os
ipath='';
import shlex, subprocess



class MyWindow(QtGui.QWidget):
 path='';
 state=0;
 ipath='./examples/ET';
 def __init__(self):
  QtGui.QWidget.__init__(self)
  self.timer=QtCore.QTimer()
  QtCore.QObject.connect(self.timer,QtCore.SIGNAL("timeout()"), self.OnTimer)
  self.setWindowTitle('PyQt')       # 设置窗口标题
  self.resize(600,80)        # 设置窗口大小
  gridlayout = QtGui.QGridLayout()     # 创建布局组件
  self.labelInfo = QtGui.QLabel( "labelInfo" )
  self.labelInfo.setText( "Select a folder" )
  gridlayout.addWidget(self.labelInfo, 0,0,1,2)
  self.button1 = QtGui.QPushButton('Folder')  # 生成Button1
  gridlayout.addWidget(self.button1, 1, 0)
  self.button2 = QtGui.QPushButton('Construct')  # 生成Button2
  gridlayout.addWidget(self.button2, 1, 1)
  self.setLayout(gridlayout)       # 向窗口中添加布局组件
  self.connect(self.button1,        # Button1事件
    QtCore.SIGNAL('clicked()'),     # clicked()信号
    self.OnButton1)        # 插槽函数
  self.connect(self.button2,        # Button2事件
    QtCore.SIGNAL('clicked()'),     # clicked()信号
    self.OnButton2)        # 插槽函数
 def OnButton1(self):         # Button1插槽函数
  self.ipath=str(self.getfolder())
  self.labelInfo.setText(self.ipath)
  
 def OnButton2(self):         # Button2插槽函数
  ipath=self.path
  self.state=0;
  os.system("rm *.txt");
  os.system("rm -R bundle");
  os.system("rm -R pmvs");
  os.system("rm -R prepare");
  os.system("rm -R tmp");
  os.system("cp -R "+self.ipath+" tmp")
#  os.system("cp -R "+ipath+" tmp")
  self.startprocess("../RunBundler.sh tmp");
  return
  if self.path!='':
	command_line2="sudo python showply.py "+ipath#注意引号是字符串的意思
  	args2=command_line2 #shlex.split(str(command_line2))
  	print args2
	self.labelInfo.setText("Processing ...")
  	self.process=subprocess.Popen(str(args2),shell=True,stderr=subprocess.PIPE)
        self.timer=QtCore.QTimer()
        QtCore.QObject.connect(self.timer,QtCore.SIGNAL("timeout()"), self.OnTimer)
        self.timer.start(100)
	
 def startprocess(self,args2):
	self.labelInfo.setText("Processing ...")
  	self.process=subprocess.Popen(str(args2),shell=True,stderr=subprocess.PIPE)

        self.timer.start(100)
	
 def getfolder(self):   
  return QtGui.QFileDialog.getExistingDirectory(None,'Hello','./',QtGui.QFileDialog.ShowDirsOnly)

 def readline(self):   
  return 'Stage '+str(self.state)+': '+self.process.stderr.readline();


 def OnTimer(self):
  line='Processing ...'
  if self.process.poll() == 0: 
	self.state=self.state+1;
	self.timer.stop();
	line='Select a folder'
	line=self.readline()
	if line!='':
		self.labelInfo.setText(line)
	if self.state==1:
		self.startprocess("../bin/Bundle2PMVS prepare/list.txt bundle/bundle.out");
	if self.state==2:
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
			elif 'echo Please' in l:
				continue
			else:    
				temp1 = l  
				ffto.write(temp1)  
		ffrom.close()
		ffto.close()
		os.system("chmod a+x ./pmvs/prep_pmvs.sh");

		self.startprocess("./pmvs/prep_pmvs.sh");
	if self.state==3:
		self.startprocess("../bin/cmvs pmvs/");
	if self.state==4:
		self.startprocess("../bin/genOption pmvs/");
	if self.state==5:
		self.startprocess("../bin/pmvs2 pmvs/ option-0000");
	if self.state==6:
		print 'Complete'
		subprocess.Popen(str("python ./viewmodel.py"),shell=True,stderr=subprocess.PIPE)
	return
  line=self.readline()
  if line!='':
	self.labelInfo.setText(line)
	


app = QtGui.QApplication(sys.argv)
mywindow = MyWindow() 
mywindow.show()
app.exec_()

