using System;
using java.applet;
using java.awt;
using java.awt.event;

namespace FractalDiagram
{
	public class MultexFrame : Frame implements WindowListener, ActionListener
	{
		DigitButton digit11;
		DigitButton digit12;
		DigitButton digit13;
		DigitButton digit14;
		DigitButton digit21;
		DigitButton digit22;
		DigitButton digit23;
		DigitButton digit24;
		DigitButton digit31;
		DigitButton digit32;
		DigitButton digit33;
		DigitButton digit34;
		DigitButton digit41;
		DigitButton digit42;
		DigitButton digit43;
		DigitButton digit44;
		DigitButton digit51;
		DigitButton digit52;
		DigitButton digit53;
		DigitButton digit54;
		TextField text_exp1;
		TextField text_exp2;
		Button button_set;

		public MultexFrame()
		{
			setTitle("Multex");
		}

		public void init()
		{
			digit11 = new DigitButton();
			digit12 = new DigitButton();
			digit13 = new DigitButton();
			digit14 = new DigitButton();
			digit21 = new DigitButton();
			digit22 = new DigitButton();
			digit23 = new DigitButton();
			digit24 = new DigitButton();
			digit31 = new DigitButton();
			digit32 = new DigitButton();
			digit33 = new DigitButton();
			digit34 = new DigitButton();
			digit41 = new DigitButton();
			digit42 = new DigitButton();
			digit43 = new DigitButton();
			digit44 = new DigitButton();
			digit51 = new DigitButton();
			digit52 = new DigitButton();
			digit53 = new DigitButton();
			digit54 = new DigitButton();
			Panel panel2 = new Panel();
			panel2.setLayout(new GridLayout(5, 4));
			panel2.add(digit14);
			panel2.add(digit13);
			panel2.add(digit12);
			panel2.add(digit11);
			panel2.add(digit24);
			panel2.add(digit23);
			panel2.add(digit22);
			panel2.add(digit21);
			panel2.add(digit34);
			panel2.add(digit33);
			panel2.add(digit32);
			panel2.add(digit31);
			panel2.add(digit44);
			panel2.add(digit43);
			panel2.add(digit42);
			panel2.add(digit41);
			panel2.add(digit54);
			panel2.add(digit53);
			panel2.add(digit52);
			panel2.add(digit51);

			digit11.setFrame(true);
			digit12.setFrame(true);
			digit13.setLock(true);
			digit14.setLock(true);
			digit21.setFrame(true);
			digit22.setFrame(true);
			digit23.setLock(true);
			digit24.setLock(true);
			digit21.setUnderLine(true);
			digit22.setUnderLine(true);
			digit23.setUnderLine(true);
			digit24.setUnderLine(true);
			digit31.setFrame(true);
			digit32.setFrame(true);
			digit33.setFrame(true);
			digit34.setLock(true);
			digit41.setLock(true);
			digit42.setFrame(true);
			digit43.setFrame(true);
			digit44.setFrame(true);
			digit41.setUnderLine(true);
			digit42.setUnderLine(true);
			digit43.setUnderLine(true);
			digit44.setUnderLine(true);
			digit51.setFrame(true);
			digit52.setFrame(true);
			digit53.setFrame(true);
			digit54.setFrame(true);
			digit24.setSymbol("mult");
			setnumbers("12", "34");

			text_exp1 = new TextField(5);
			text_exp2 = new TextField(5);
			button_set = new Button("set");
			Panel panel1 = new Panel();
			panel1.add(button_set);
			panel1.add(text_exp1);
			panel1.add(text_exp2);
			setLayout(new BorderLayout());
			add("Center", panel2);
			//add("South", panel1);
			addWindowListener(this);
			button_set.addActionListener(this);
		}

		private void setnumbers(string s1, String s2)
		{
			if ( s1.length() == 2 ) {
				if ( Character.isDigit(s1.charAt(0)) && Character.isDigit(s1.charAt(1)) ) {
					digit12.setNumber(Integer.valueOf(s1.Substring(0, 1)).intValue());
					digit11.setNumber(Integer.valueOf(s1.Substring(1, 2)).intValue());
				}
			} else if ( s1.length() == 1 ) {
				if ( Character.isDigit(s1.charAt(0)) ) {
					digit11.setNumber(-1);
					digit12.setNumber(Integer.valueOf(s1.Substring(0, 1)).intValue());
				}
			}
			if ( s2.length() == 2 ) {
				if ( Character.isDigit(s2.charAt(0)) && Character.isDigit(s2.charAt(1)) ) {
					digit22.setNumber(Integer.valueOf(s2.Substring(0, 1)).intValue());
					digit21.setNumber(Integer.valueOf(s2.Substring(1, 2)).intValue());
				}
			} else if ( s2.length() == 1 ) {
				if ( Character.isDigit(s2.charAt(0)) ) {
					digit21.setNumber(-1);
					digit22.setNumber(Integer.valueOf(s2.Substring(0, 1)).intValue());
				}
			}
		}

		public void destroy()
		{
		}

		public void paint(Graphics g)
		{
		}

		public void start()
		{
		}

		public void stop()
		{
		}

		public void windowOpened(WindowEvent e)
		{
		}

		public void windowClosing(WindowEvent e)
		{
			dispose();
			System.exit(0);
		}

		public void windowClosed(WindowEvent e)
		{
		}

		public void windowIconified(WindowEvent e)
		{
		}

		public void windowDeiconified(WindowEvent e)
		{
		}

		public void windowActivated(WindowEvent e)
		{
		}

		public void windowDeactivated(WindowEvent e)
		{
		}

		public void actionPerformed(ActionEvent e)
		{
			if ( e.getSource() == button_set ) {
				setnumbers(text_exp1.getText(), text_exp2.getText());
			}
		}
	}
}
