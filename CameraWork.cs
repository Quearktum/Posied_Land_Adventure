using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public class CameraWork
    {
        public const int SCREEN_BORDER = 100;
        public static void UpdateCameraPosition(double playerX, double playerY)
        {
            // Test edge of screen boundaries to adjust the camera
            double leftEdge = Camera.X + 3 * SCREEN_BORDER;
            double rightEdge = leftEdge + SplashKit.ScreenWidth() - 10 * SCREEN_BORDER;
            double topEdge = Camera.Y + SCREEN_BORDER;
            double bottomEdge = topEdge + SplashKit.ScreenHeight() - 2 * SCREEN_BORDER;

            // Test if the player is outside the area and move the camera
            // the player will appear to stay still and everything else
            // will appear to move :)

            // Test top/bottom of screen
            if (playerY < topEdge)
            {
                SplashKit.MoveCameraBy(0, playerY - topEdge);
            }
            else if (playerY > bottomEdge)
            {
                if (Camera.Y > -1)
                    SplashKit.MoveCameraBy(0, 0);
                else
                    SplashKit.MoveCameraBy(0, playerY - bottomEdge);
            }

            // Test left/right of screen
            if (playerX < leftEdge)
            {
                if (Camera.X <= 0) 
                    Camera.X = 0;
                else
                    SplashKit.MoveCameraBy(playerX - leftEdge, 0);
            }
            else if (playerX > rightEdge)
            {
                if (Camera.X >= 4213)
                    Camera.X = 4213;
                else
                    SplashKit.MoveCameraBy(playerX - rightEdge, 0);
            }
        }

    }
}
