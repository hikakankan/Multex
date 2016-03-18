using System;
	public class MultexMain
	{
		public static void main(string args[])
		{
			try {
				MultexFrame frame = new MultexFrame();
				frame.show();
			    frame.setVisible(false);
				frame.setSize(frame.getInsets().left + frame.getInsets().right  + 230,
						frame.getInsets().top  + frame.getInsets().bottom + 240);
				frame.init();
				frame.start();
				frame.show();
			} catch ( Exception e ) {
				e.printStackTrace();
			}
		}
	}
}
