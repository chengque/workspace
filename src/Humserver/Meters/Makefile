
CSC=csc
TARGET=Meters
BINPATH=..\bin\\
OUTPUTPATH=$(BINPATH)$(TARGET).dll

all:$(TARGET).dll

$(TARGET).dll:*.cs
	CSC /t:library /out:$(OUTPUTPATH) *.cs

clean:
	del $(OUTPUTPATH)
