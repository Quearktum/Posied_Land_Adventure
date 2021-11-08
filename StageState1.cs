using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public class StageState1 : GameState
    {
        Bitmap background;
        Bitmap terrain;

        Player player;

        InputManager inputmanager;
        GameManager gameManager;

        Gate gate;

        public override void OnStateEnter()
        {
            background = new Bitmap("Background", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Backgrounds\\Background.png");
            terrain = new Bitmap("TerrainStage1", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Terrain\\terrain1.png");

            inputmanager = new InputManager();

            player = new Player(terrain);

            EnemyFrog frog = new EnemyFrog(530, 300, terrain, true, player);

            KeyPickup key = new KeyPickup(491, 300, terrain, player);
            ArrowPickup arrowpickup = new ArrowPickup(100, 300, terrain, player);

            gate = new Gate(960, 300, terrain, player);

            List<Character> characters = new List<Character>() { player, frog};
            List<IPickupAble> pickups = new List<IPickupAble>() { key, arrowpickup};
            gameManager = new GameManager(characters, pickups);

            inputmanager.Add(player);

            player.HitManager.AddDamageableObj(frog.CharacterWeapon);

            frog.HitManager.AddDamageableObj(player.CharacterWeapon);

        }

        public override void OnStateExit()
        {
            Camera.X = 0;
            Camera.Y = 0;
        }


        public override void CheckInput()
        {
            inputmanager.CheckInput();
        }

        public override void Update()
        {
            if(manager.CurrentStage != 1)
                manager.CurrentStage = 1;

            //Update camera
            CameraWork.UpdateCameraPosition(player.CharacterX, player.CharacterY);
            //  Update characters through game manager
            gameManager.Update();
            gate.Update();

            if (gate.StageCompleted)
                manager.SetCurrentState<CompletedState>();
            else if (player.Health <= 0)
                manager.SetCurrentState<FailedState>();
        }

        public override void Draw()
        {
            //  Draw background, terrain and miscellaneous
            SplashKit.DrawBitmap(background, 0, 0);
            SplashKit.DrawBitmap(terrain, 0, 0);
            //  Draw characters through game manager
            gameManager.Draw();
            gate.Draw();
            //  Draw text
            SplashKit.DrawText(SplashKit.PointToString(Camera.Position), Color.White, 0, 712, SplashKit.OptionToScreen());
        }
    }
}
