try {
	System.IO.File.WriteAllText("Log.txt", "Starting up ... ");
	using var game = new minijam.Game1();
	game.Run();
}
catch (System.Exception e) {
	System.IO.File.WriteAllText("Log.txt", e.ToString());
}

