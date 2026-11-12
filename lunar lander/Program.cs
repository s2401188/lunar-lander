namespace lunar_lander;

using Raylib_cs;

    internal class Program
    {
        static void Main(string[] args)
        {
            Program game = new Program();
            game.Run();
        }


        Texture2D shipImage;
        float ship_y = 8;
        float ship_velocity = 0;
        float gravity_acceleration = 15;
        float engine_acceleration = -25;


        float fuel = 100;
        float fuel_comsumption = 20;

        bool gameOver = false;
        string message = "";

        void Run()
        {
            Raylib.InitWindow(640, 480, "LunarLander");

            shipImage = Raylib.LoadTexture
                ("playerShip2_blue.png");

            while (Raylib.WindowShouldClose()
                == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground
                    (Color.DarkPurple);


                float total_acceleration = 0.0f;
                total_acceleration += gravity_acceleration;
                if (Raylib.IsKeyDown(KeyboardKey.Space) && fuel > 0)
                {
                    total_acceleration += engine_acceleration;

                    fuel -= fuel_comsumption * Raylib.GetFrameTime();

                    if (fuel < 0)
                    {
                        fuel = 0;
                    }
                }

                float deltatime = Raylib.GetFrameTime();
                ship_velocity += total_acceleration * deltatime;

                ship_y += ship_velocity * deltatime; Raylib.DrawText($"Y: {ship_y} Vel: {ship_velocity}", 0, 0, 16, Color.Yellow);
            Raylib.DrawText($"Fuel: {fuel:0}", 10, 50, 16, Color.Red);

            Raylib.DrawTexture(shipImage, Raylib.GetScreenWidth() / 2 - shipImage.
                Width / 2, (int)ship_y, Color.White);

            float landingWidth = shipImage.Width * 2;
            float landingHeight = 40;

            Raylib.DrawRectangle(
                (int)(Raylib.GetScreenWidth() / 2 - landingWidth / 2),
                (int)(Raylib.GetScreenHeight() - landingHeight),
                (int)landingWidth,
                (int)landingHeight,
                Color.DarkGray);
            float ship_bottom = ship_y + shipImage.Height;
            float landing_top = Raylib.GetScreenHeight() - landingHeight;
            int screenMiddleX = Raylib.GetScreenWidth() / 2;
            Raylib.DrawLine(
                screenMiddleX,
                (int)ship_bottom,
                (int)screenMiddleX,
                (int)landing_top, Color.Red);

            if (ship_bottom >= landing_top && !gameOver)
            {
                gameOver = true;
                if (MathF.Abs(ship_velocity) > 50)
                {
                    message = "Hävisit! Vauhti oli liian kova.";
                }
                else
                {
                    message = "Voitit Pelin laskeuduit turvallisesti";
                    ship_velocity = 0;



                }
            }

            if (gameOver)
            {
                Raylib.DrawText(
                    message,
                    150,
                    200,
                    25,
                    Color.Magenta);
            }
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
} 
    