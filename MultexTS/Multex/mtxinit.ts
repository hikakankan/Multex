function makeButton(id: string, settings: ViewSettings, text: string): WYRoundButton
{
	var canvas = <HTMLCanvasElement>document.getElementById(id);
	var gc = canvas.getContext("2d");
	var button = new WYRoundButton(gc, settings, 12);
	button.setText(text);
	button.setRect(0, 0, canvas.width, canvas.height);
	button.draw();
	def_mouse_event(canvas, button);
	return button;
}

function mtxinit()
{
	var settings = new ViewSettings();
	document.body.style.background = settings.BodyBackColor.getColor();
	document.body.style.color = settings.BodyTextColor.getColor();
	var mult_canvas = <HTMLCanvasElement>document.getElementById("mult");
	var panel = new CSPanel(mult_canvas, settings.CalcAreaBackColor);
	def_mouse_event(mult_canvas, panel);
	var create_problem_button = makeButton("create-problem", settings, "Next");
	var show_answer_button = makeButton("show-answer", settings, "Answer");
	var mult = new MultexMultiplication(panel, new AnswerText("answer", settings), new KeyboardSettings(), settings);

    var mult1_canvas = document.getElementById("mult1");
	var mult1_numeric = new WYNumericSlider(mult1_canvas, settings, 2, 1, 10);
	mult1_numeric.draw();
	def_mouse_event(mult1_canvas, mult1_numeric);

    var mult2_canvas = document.getElementById("mult2");
	var mult2_numeric = new WYNumericSlider(mult2_canvas, settings, 2, 1, 10);
	mult2_numeric.draw();
	def_mouse_event(mult2_canvas, mult2_numeric);

	create_problem_button.onclick = function()
	{
		mult.create_mult(mult1_numeric.getNumber(), mult2_numeric.getNumber());
	}

	show_answer_button.onclick = function()
	{
		mult.show_answer();
	}

	var single_mode_canvas = document.getElementById("single-mode");
	var single_mode_check = new WYCheckBox(single_mode_canvas, settings, "Single");
	single_mode_check.draw();
	def_mouse_event(single_mode_canvas, single_mode_check);

	single_mode_check.oncheck = function()
	{
		mult.set_single_mode(single_mode_check.getValue());
	}

	var auto_answer_mode_canvas = document.getElementById("auto-answer-mode");
	var auto_answer_mode_check = new WYCheckBox(auto_answer_mode_canvas, settings, "Auto");
	auto_answer_mode_check.draw();
	def_mouse_event(auto_answer_mode_canvas, auto_answer_mode_check);

	auto_answer_mode_check.oncheck = function()
	{
		mult.set_auto_answer_mode(auto_answer_mode_check.getValue());
	}

	mult.create_panels(2, 2);
}
