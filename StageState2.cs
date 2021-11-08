using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public class StageState2 : GameState
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
            terrain = new Bitmap("TerrainStage2", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Terrain\\terrain2.png");

            inputmanager = new InputManager();

            player = new Player(terrain);

            EnemyFrog frog_1 = new EnemyFrog(1420, 300, terrain, true, player);
            EnemyFrog frog_2 = new EnemyFrog(4280, 300, terrain, true, player);
            EnemyPlant plant_1 = new EnemyPlant(1890, 300, terrain, true, player);
            EnemyPlant plant_2 = new EnemyPlant(4750, 300, terrain, true, player);
            EnemySnail snail_1 = new EnemySnail(2350, 300, terrain, true, player);
            EnemySnail snail_2 = new EnemySnail(3200, 300, terrain, true, player);

            KeyPickup key = new KeyPickup(4901, 300, terrain, player);
            ArrowPickup arrowpickup_1 = new ArrowPickup(530, 300, terrain, player);
            ArrowPickup arrowpickup_2 = new ArrowPickup(620, 300, terrain, player);
            ArrowPickup arrowpickup_3 = new ArrowPickup(2505, 300, terrain, player);
            ArrowPickup arrowpickup_4 = new ArrowPickup(2661, 300, terrain, player);

            gate = new Gate(2565, 300, terrain, player);

            List<Character> characters = new List<Character>() { player, frog_1, frog_2, plant_1, plant_2, snail_1, snail_2 };
            List<IPickupAble> pickups = new List<IPickupAble>() { key, arrowpickup_1, arrowpickup_2, arrowpickup_3, arrowpickup_4 };
            gameManager = new GameManager(characters, pickups);

            inputmanager.Add(player);

            player.HitManager.AddDamageableObj(frog_1.CharacterWeapon);
            player.HitManager.AddDamageableObj(frog_2.CharacterWeapon);
            player.HitManager.AddDamageableObj(plant_1.CharacterWeapon);
            player.HitManager.AddDamageableObj(plant_2.CharacterWeapon);
            player.HitManager.AddDamageableObj(snail_1.CharacterWeapon);
            player.HitManager.AddDamageableObj(snail_2.CharacterWeapon);

            frog_1.HitManager.AddDamageableObj(player.CharacterWeapon);
            frog_2.HitManager.AddDamageableObj(player.CharacterWeapon);
            plant_1.HitManager.AddDamageableObj(player.CharacterWeapon);
            plant_2.HitManager.AddDamageableObj(player.CharacterWeapon);
            snail_1.HitManager.AddDamageableObj(player.CharacterWeapon);
            snail_2.HitManager.AddDamageableObj(player.CharacterWeapon);
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
            if(manager.CurrentStage != 2)
                manager.CurrentStage = 2;

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
