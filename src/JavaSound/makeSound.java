/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 
package makeSound;
*/
/**
 *
 * @author Ivan
 */

import java.io.File;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;
import javax.sound.sampled.Clip;
import javax.sound.sampled.DataLine;
import javax.sound.sampled.LineEvent;
import javax.sound.sampled.LineListener;
import javax.sound.sampled.LineUnavailableException;
import javax.sound.sampled.UnsupportedAudioFileException;
import javax.sound.sampled.FloatControl;

class makeSound {

    /**
     * @param args the command line arguments
     *
     */
    
    public static void main(String args[])
    {
        makeSound ms=new makeSound();
        ms.Sound(1, 0, 44200,"Ringout.wav");
    }
    
    public void Sound(double gain, float panVal, int sampleRate, String tonePath) 
	{
        // specify the sound to play
		// (assuming the sound can be played by the audio system)
		File soundFile = new File(tonePath);
		AudioInputStream sound = null;
			try 
			{
				sound = AudioSystem.getAudioInputStream(soundFile);
			} 
			catch (UnsupportedAudioFileException ex) 
			{
				Logger.getLogger(makeSound.class.getName()).log(Level.SEVERE, null, ex);
			} 
			catch (IOException ex) 
			{
				Logger.getLogger(makeSound.class.getName()).log(Level.SEVERE, null, ex);
			}
		// load the sound into memory (a Clip)
		DataLine.Info info = new DataLine.Info(Clip.class, sound.getFormat());
		Clip clip = null;
			try 
			{
				clip = (Clip) AudioSystem.getLine(info);
			} 
			catch (LineUnavailableException ex) 
			{
				Logger.getLogger(makeSound.class.getName()).log(Level.SEVERE, null, ex);
			}
        try 
		{
            clip.open(sound);
        } 
		catch (LineUnavailableException ex) 
		{
            //Logger.getLogger(makeSound.class.getName()).log(Level.SEVERE, null, ex);
        } 
		catch (IOException ex) 
		{
            Logger.getLogger(makeSound.class.getName()).log(Level.SEVERE, null, ex);
        }
		// double gain = .5D; // number between 0 and 1 (loudest)
        FloatControl gainControl = (FloatControl) clip
                .getControl(FloatControl.Type.MASTER_GAIN);
		FloatControl panControl = (FloatControl) clip
                .getControl(FloatControl.Type.PAN);
		FloatControl sampleControl = (FloatControl) clip	
				.getControl(FloatControl.Type.SAMPLE_RATE);
	float dB = (float) (Math.log(gain) / Math.log(10.0) * 20.0);
	
	//float dB = (float) gain;
//        float panVal=(float) -1;
	sampleControl.setValue(sampleRate);
	panControl.setValue(panVal);
	gainControl.setValue(dB);
	clip.setFramePosition(0);
	clip.loop(0);
//	System.out.format("%f\n", dB);
    }
}
