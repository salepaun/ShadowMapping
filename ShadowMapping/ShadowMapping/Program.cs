﻿namespace ShadowMapping
{
    public static class Program
    {
        private static void Main()
        {
            using (var window = new Window(800, 600, "LearnOpenTK - Light caster - directional"))
            {
                window.Run(60.0);
            }
        }
    }
}
