CSC=csc
TARGET=Humserver
BINPATH=..\bin\\
OUTPUTPATH=$(BINPATH)$(TARGET).exe
REFER=$(BINPATH)Meters.dll,$(BINPATH)HumTransmit.dll,System.Windows.Forms.DataVisualization.dll

all:$(TARGET).exe

$(TARGET).exe:*.cs
	CSC /t:winexe /r:$(REFER) /debug+ /out:$(OUTPUTPATH) *.cs

clean:
	del $(OUTPUTPATH)
	

