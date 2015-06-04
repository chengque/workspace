from visual import *
from OpenGL.GL import *
from OpenGL.GLU import *
from OpenGL.GLUT import *
import common
import sys
import string

filename='pmvs/models/option-0000.ply'

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


laa=len(aa)
xaa=0;
yaa=0;
zaa=0;
for v in aa:
	xaa=xaa+v[0];
	yaa=yaa+v[1];
	zaa=zaa+v[2];
xaa=xaa/laa;
yaa=yaa/laa;
zaa=zaa/laa;

print xaa,yaa,zaa

window = 0

zdep=0;
zx=0;
lmouse=0;
ytr=0;
zy=0;
lmousey=0;

#sph = common.sphere(16,16,1)
camera = common.camera()
#plane = common.plane(12,12,1.,1.)
def InitGL(width,height):
    glClearColor(0.1,0.1,0.5,0.1)
    glClearDepth(1.0)
    glPolygonMode(GL_FRONT_AND_BACK, GL_LINE)
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()
    gluPerspective(45.0,float(width)/float(height),0.1,100.0)    
#    camera.move(0.0,0,-50)    
    
def DrawGLScene():
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)
    glMatrixMode(GL_MODELVIEW) 
    camera.setLookat()     
    glPushMatrix()
    glTranslatef(camera.xtran,0,camera.zdep) 
#    glLoadIdentity() 
    glRotatef(camera.yr,0,1,0)  
#    glPushMatrix()
    glRotatef(camera.zr,0,0,1)    
       
#    plane.draw() 
#    glTranslatef(-1.5,0.0,0.0)  
    glPointSize(2.0)
    glBegin(GL_POINTS)    
    for v in aa:
         glColor3f(v[6]/255, v[7]/255, v[8]/255)
         glVertex3f(v[0]-xaa, v[2]-zaa,-(v[1]-yaa))
    glEnd()
#    glutWireTeapot(2);  

    glPopMatrix()
#    glPopMatrix()
#   glTranslatef(3.0, 0.0, 0.0) 
#    camera.setLookat() 
#    glRotatef(1,0,1,0)     
          
    glutSwapBuffers()

def mouseButton( button, mode, x, y ):	
	if button == GLUT_RIGHT_BUTTON and mode==GLUT_DOWN:
		camera.mouselocation=[x,y]
		camera.mousemode=button
	if button == GLUT_LEFT_BUTTON and mode==GLUT_DOWN:
		camera.lmouselocation=[x,y]
		camera.mousemode=button


def ReSizeGLScene(Width, Height): 
    glViewport(0, 0, Width, Height)		
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()
    gluPerspective(45.0, float(Width)/float(Height), 0.1, 100.0)
    glMatrixMode(GL_MODELVIEW)
    
def main():
    global window
    glutInit(sys.argv)
    glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH)
    glutInitWindowSize(640,400)
    glutInitWindowPosition(800,400)
    window = glutCreateWindow("opengl")
    glutDisplayFunc(DrawGLScene)
    glutIdleFunc(DrawGLScene)
    glutReshapeFunc(ReSizeGLScene)
    glutMouseFunc( mouseButton )
    glutMotionFunc(camera.mouse)
    glutKeyboardFunc(camera.keypress)
    glutSpecialFunc(camera.keypress)
    InitGL(640, 480)
    glutMainLoop()

main()
