class AnswerText
{
    private textBox: WYCanvasTextBox;

    public constructor(id: string, settings: ViewSettings) {
        this.textBox = createTextBox(id, settings, "");
    }

    public setText = function(text: string): void
	{
		this.textBox.setText(text);
	}

    public addText = function (text: string): void
	{
		this.textBox.setText(this.textBox.getText() + text);
	}

    public setCorrectAndTime = function (answer, time): void
	{
		this.textBox.setText("○");
	}

    public setIncorrectAndTime = function (answer, time): void
	{
		this.textBox.setText("×");
	}

    public setCorrect = function (answer): void
	{
		this.textBox.setText("○");
	}

    public setIncorrect = function (answer): void
	{
		this.textBox.setText("×");
	}

    public clear = function (): void
	{
		this.textBox.setText("");
	}
}
