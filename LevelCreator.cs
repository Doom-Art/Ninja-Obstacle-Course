using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    internal class LevelCreator
    {
        List<Platform> tempPlatforms, tempSpikes;
        List<RedWalker> tempRWalkers;
        List<Portal> tempPortals;

        public LevelCreator()
        {

        }
        public Level Level0(Texture2D rectangleTex, Texture2D portalTex, Texture2D ghostPlat, Texture2D redWalker, Texture2D rWalkerDoorTex, Texture2D spikeTex, Texture2D exitPortal, SpriteFont font)
        {
            tempPlatforms = new();
            tempSpikes = new();
            tempRWalkers = new();
            tempPortals = new();

            Rectangle[] redWalkerSourceRects = new Rectangle[6] { new Rectangle(22, 8, 56, 83), new Rectangle(122, 8, 56, 83), new Rectangle(222, 8, 56, 83), new Rectangle(22, 108, 56, 83), new Rectangle(122, 108, 56, 83), new Rectangle(222, 108, 56, 83) };

            //Four Borders
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(-200, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(-200, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(-200, -3000, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3400, -2800, 200, 2800), Color.DarkGray));

            //Yellow Tutorial
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(400, -320, 200, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(360, -600, 40, 320), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(680, -320, 80, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(720, -400, 40, 80), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(480, -440, 520, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(570, -520, 40, 80), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(650, -560, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(730, -760, 40, 200), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(920,-520,50,80), new Rectangle(360,-1000,50,80)));

            //Red Ghost Tutorial
            tempPortals.Add(new Portal(portalTex, new Rectangle(1080, -280, 50, 80), new Rectangle(360, -1000, 50, 80)));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(360,-920,720,40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(540, -1080, 80, 160), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(620, -1040, 160, 40), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(620, -960, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(780, -1040, 120, 40), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(820, -1120, 50, 80), new Rectangle(360,-1280,50,80)));

            //Green Fade Tutorial
            tempPortals.Add(new Portal(portalTex, new Rectangle(1200, -280, 50, 80), new Rectangle(360, -1280, 50, 80)));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(360, -1200, 720, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(440, -1400, 40, 200), Color.Green, 0, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(560, -1320, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(600, -1400, 240, 40), Color.Green, 0.6f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(840, -1400, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(850, -1360, 40, 160), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(890,-1280,50,80), new Rectangle(1600,-1280,50,80)));

            //Spike Tutorial
            tempPortals.Add(new Portal(portalTex, new Rectangle(1320, -280, 50, 80), new Rectangle(1600, -1280, 50, 80)));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1600, -1200, 720, 40), Color.Yellow));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1760, -1220, 20, 20), -1200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1880, -1300, 20, 20), -1280));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2080, -1220, 20, 20), -1200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2080, -1380, 20, 20), -1360));

            tempPortals.Add(new Portal(portalTex, new Rectangle(2240, -1280, 50, 80), new Rectangle(1600, -1000, 50, 80)));

            //Red Walker Tutorial
            tempPortals.Add(new Portal(portalTex, new Rectangle(1440, -280, 50, 80), new Rectangle(1600, -1000, 50, 80)));
            
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1600, -920, 720, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(2000, -1000, 60, 80), 1800,2080,rWalkerDoorTex));

            tempPortals.Add(new Portal(portalTex, new Rectangle(2240, -1000, 50, 80), new Rectangle(1600, -280, 50, 80)));

            Level level0 = new Level(tempPlatforms, tempPortals, tempRWalkers, tempSpikes);
            level0.SetFont(font);
            level0.AddSign(new Vector2(20,-490), "Welcome to \nNinja Obstacle Course,\nYou Can set your Preffered \nControls in Settings, if not\nThe default controls are: \n\nSpace for Jump\nA for Left\nD for Right\nLeft Shift for Sprint");
            level0.AddSign(new Vector2(770, -370), "Yellow Blocks are \nthe Core of the Game,\nThey are solid blocks \nthat never change");
            //Marking the skip doors
            level0.AddSign(new Vector2(1040, -380), "     Ghost                                             Spikes\nPlatforms\n                                     Fading                                 Red Walkers\n                               Platforms");
            
            level0.AddSign(new Vector2(870, -570), "Jump to Enter \n     the portal");
            level0.AddSign(new Vector2(90,-1100), "You Can't Jump in Red Ghost Platforms\nYour jumps are also forcibly ended \ninside of them. The Trick is to jump \nto max height before going in");
            level0.AddSign(new Vector2(90,-1400), "Green Platforms fade \ninto and out of existence, \nthey act like Yellow Blocks \nwhen solid and air when not");
            level0.AddSign(new Vector2(1380,-1400), "Spikes are deadly blocks that send you \nback to the start. If you die you can \nwalk straight to the portal right of spawn \nto skip back to this section of the tutorial");
            level0.AddSign(new Vector2(1900,-1500), "Jumping to max height is \nnot always the answer, \nif you let go of jump early \nyou start falling immediately");
            level0.AddSign(new Vector2(1380,-1100), "Red Walkers walk between their two posts\nThey kill you if you touch them\nThe trick is to jump over them \nwhile mainting a good distance");
            level0.SetExit(exitPortal, new Rectangle(1680,-320,120,120));
            level0.SetSpawn(new Vector2(40, -280));
            return level0;
        }
        public Level Level1(Texture2D rectangleTex, Texture2D portalTex, Texture2D ghostPlat, Texture2D exitPortal, SpriteFont font)
        {
            Level level1;
            tempPlatforms = new();
            tempPortals = new();

            //Four Borders
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -3000, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3600, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(900, -200, 1500, 200), Color.DarkGray));

            //First Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(380, -440, 20, 160), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(400, -320, 200, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(650, -320, 110, 40), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(440, -440, 280, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(560, -640, 40, 200), Color.Green, 0f, true, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(720, -440, 40, 120), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(680, -520, 50, 80), new Rectangle(1000, -720, 50, 80)));

            //Second Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(960, -640, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(1080, -840, 120, 240), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1200, -640, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1240, -640, 120, 40), Color.Green, 0.4f, true, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1320, -640, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1360, -640, 320, 40), Color.Green, 0, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1680, -640, 120, 40), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(1720, -720, 50, 80), new Rectangle(3360, -400, 50, 80)));

            //Third Area
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2680, -320, 240, 40), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2920, -320, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3040, -300, 280, 20), Color.Red, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3040, -320, 280, 40), Color.Green, 0.86f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3320, -320, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(3440, -320, 160, 40), Color.White, 0.5f));

            tempPortals.Add(new Portal(portalTex, new Rectangle(2960, -400, 50, 80), new Rectangle(320, -2680, 50, 80)));

            //Final Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(280, -2600, 160, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(440, -2600, 360, 40), Color.Green, 0.1f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(800, -2600, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(840, -2600, 360, 40), Color.Green, 0.7f, true, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1200, -2600, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1240, -2600, 360, 40), Color.Green, 0.3f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1600, -2600, 120, 40), Color.Yellow));

            level1 = new Level(tempPlatforms, tempPortals);
            //Level 1 Signs/Hints
            level1.SetFont(font);
            level1.AddSign(new Vector2(210, -400), "Need some \nhelp? try \nthe Tutorial \nlevel");
            //level1.AddSign(new Vector2(620, -600), "Jump To Enter\n The Portal");
            //level1.AddSign(new Vector2(800, -880), "Red Ghost Platforms \nStop Your Jumps\nAnd Make You Fall");
            level1.AddSign(new Vector2(230, -2760), "Hold Sprint to move Faster \n  when walking on Ground");
            level1.SetExit(exitPortal, new Rectangle(1600, -2720, 120, 120));

            return level1;
        }
        public Level DropLevel(Texture2D rectangleTex, Texture2D portalTex, Texture2D spikeTex,Texture2D exitPortal)
        {
            tempSpikes = new();
            tempRWalkers = new();
            tempPortals = new();
            tempPlatforms = new();

            //Four Borders
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -3000, 3800, 200), Color.DarkGray));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(400, -2240, 1160, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(400, -2200, 40, 160), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(440, -2080, 440, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(880, -2080, 40, 960), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1120, -2200, 40, 1080), Color.Yellow));

            tempSpikes.Add(new Platform(spikeTex, new Rectangle(920, 1, 1, 1), -1840));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1080, 1, 1, 1), -1840));

            tempSpikes.Add(new Platform(spikeTex, new Rectangle(920, 1, 1, 1), -1680));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1050, 1, 1, 1), -1680));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1080, 1, 1, 1), -1680));

            tempSpikes.Add(new Platform(spikeTex, new Rectangle(920, 1, 1, 1), -1520));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(955, 1, 1, 1), -1520));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1080, 1, 1, 1), -1520));

            tempSpikes.Add(new Platform(spikeTex, new Rectangle(920, 1, 1, 1), -1360));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1050, 1, 1, 1), -1360));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1080, 1, 1, 1), -1360));

            tempSpikes.Add(new Platform(spikeTex, new Rectangle(921, 1, 1, 1), -1200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(952, 1, 1, 1), -1200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(983, 1, 1, 1), -1200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1090, 1, 1, 1), -1200));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(800, -1000, 440, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(800, -1120, 40, 120), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1200, -1120, 40, 120), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(800, -1180, 80, 30), Color.Yellow, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1160, -1180, 80, 30), Color.Yellow, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(800, -1160, 80, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1160, -1160, 80, 40), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(1120, -1080, 50, 80), new Rectangle(1280, -2160, 50, 80)));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1160, -2080, 400, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1520, -2200, 40, 160), Color.Yellow));

            Level dropper = new Level(tempPlatforms, tempPortals, tempRWalkers, tempSpikes);
            dropper.SetSpawn(new Vector2(440 , -2160));
            dropper.SetExit(exitPortal, new Rectangle(1400, -2200, 120, 120));
            return dropper;
        }
        public Level Level2(Texture2D rectangleTex, Texture2D portalTex, Texture2D ghostPlat, Texture2D redWalker, Texture2D rWalkerDoorTex, Texture2D spikeTex, Texture2D exitPortal)
        {
            Level level;
            tempPlatforms = new();
            tempRWalkers = new();
            tempPortals = new();
            tempSpikes = new();

            Rectangle[] redWalkerSourceRects = new Rectangle[6] { new Rectangle(22, 8, 56, 83), new Rectangle(122, 8, 56, 83), new Rectangle(222, 8, 56, 83), new Rectangle(22, 108, 56, 83), new Rectangle(122, 108, 56, 83), new Rectangle(222, 108, 56, 83) };

            //Four Borders
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -3000, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3600, -2800, 200, 2800), Color.DarkGray));

            //Part 1
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(1040,-280,60,80), 800,1400,rWalkerDoorTex));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1040,-320, 120,40), Color.Yellow));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1700, -300, 20, 20), -280));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1800, -220, 20, 20), -200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2000, -380, 20, 20), -370));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2000, -220, 20, 20), -200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2200, -300, 20, 20), -280));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2350, -220, 20, 20), -200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2500, -300, 20, 20), -280));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2700, -220, 20, 20), -200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2900, -300, 20, 20), -280));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(3050, -220, 20, 20), -200));

            tempPortals.Add(new Portal(portalTex, new Rectangle(3200, -280,50,80), new Rectangle(400,-680,50,80)));

            //Part 2
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(400,-600,1480,40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(1000, -680, 60, 80), 680, 1400, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(960, -720, 80,40), Color.Yellow));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1680, -620, 20, 20), -600));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1680, -770, 20, 20), -780));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1960,-600,280,40), Color.Green, 0, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2320, -640, 320, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2600, -680, 40, 40), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2720, -680, 200, 40), Color.Yellow));

            //Extra 
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2920, -800, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2520, -880, 320, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1840, -880, 680, 40), Color.Green, 0.3f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1840, -880, 680, 40), Color.Green, 0.3f, true));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2200,-900,20,20), -880));


            level = new Level(tempPlatforms, tempPortals, tempRWalkers, tempSpikes);
            level.SetExit(exitPortal, new Rectangle(2760, -800, 120, 120));

            return level;
        }
        public Level Level3(Texture2D rectangleTex, Texture2D portalTex, Texture2D ghostPlat, Texture2D redWalker, Texture2D rWalkerDoorTex, Texture2D spikeTex,Texture2D exitPortal, SpriteFont font)
        {
            tempPlatforms = new();
            tempRWalkers = new();
            tempPortals = new();
            tempSpikes = new();

            Rectangle[] redWalkerSourceRects = new Rectangle[6] { new Rectangle(22, 8, 56, 83), new Rectangle(122, 8, 56, 83), new Rectangle(222, 8, 56, 83), new Rectangle(22, 108, 56, 83), new Rectangle(122, 108, 56, 83), new Rectangle(222, 108, 56, 83) };

            //Four Borders
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -3000, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3600, -2800, 200, 2800), Color.DarkGray));

            //First Area
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(640, -280, 160, 80), Color.White, 0.5f));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(900, -280, 60, 80), 800, 1040, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(920, -400, 40, 80), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(920, -320, 40, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(1200, -280, 60, 80), 1160, 1400, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(1280, -400, 40, 80), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1280, -320, 40, 40), Color.Yellow));
            tempPortals.Add(new Portal(portalTex, new Rectangle(1520, -280, 50, 80), new Rectangle(280, -800, 50, 80)));
            tempPortals.Add(new Portal(portalTex, new Rectangle(1600, -280, 50, 80), new Rectangle(280, -800, 50, 80)));
            tempPortals.Add(new Portal(portalTex, new Rectangle(1680, -280, 50, 80), new Rectangle(280, -800, 50, 80)));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1880, -220, 20, 20), -200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2040, -220, 20, 20), -200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2280, -220, 20, 20), -200));
            //True Portal is 4th
            tempPortals.Add(new Portal(portalTex, new Rectangle(2400, -280, 50, 80), new Rectangle(1840, -480, 50, 80)));

            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2560, -220, 20, 20), -200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2920, -220, 20, 20), -200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(3400, -220, 20, 20), -200));
            tempPortals.Add(new Portal(portalTex, new Rectangle(3550, -280, 50, 80), new Rectangle(280, -800, 50, 80)));

            //Second Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1760, -400, 240, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2080, -400, 200, 40), Color.Green, 0.49f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2280, -400, 300, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(2280, -480, 60, 80), 2280, 2520, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2400, -600, 40, 80), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2400, -520, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2680, -400, 840, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(3000, -480, 60, 80), 2680, 3400, rWalkerDoorTex, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3030, -520, 120, 40), Color.Yellow));

            //Third Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3520, -520, 80, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3480, -640, 120, 40), Color.Green, 0.1f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2680, -640, 800, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1640, -680, 1040, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(2400, -760, 60, 80), 2240, 2620, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2440, -800, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2440, -880, 40, 80), Color.White, 0.5f));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2080, -700, 20, 20), -680));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1960, -700, 20, 20), -680));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1800, -700, 20, 20), -680));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1640, -700, 20, 20), -680));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1410, -680, 160, 40), Color.Green, 0.6f, true, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1080, -680, 200, 40), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(1120, -760, 50, 80), new Rectangle(400, -920, 50, 80)));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(240, -840, 240, 40), Color.Yellow));
            Level level = new Level(tempPlatforms, tempPortals, tempRWalkers, tempSpikes);
            level.SetFont(font);
            level.AddSign(new Vector2(1500, -330), "Pick a door any door, \n4 Are Fake one is Right");
            level.SetExit(exitPortal, new Rectangle(240, -960, 120, 120));

            return level;
        }
        public Level MazeOfRa(Texture2D rectangleTex, Texture2D portalTex, Texture2D ghostPlat, Texture2D redWalker, Texture2D rWalkerDoorTex, Texture2D spikeTex, Texture2D exitPortal)
        {
            Level maze;
            tempPlatforms = new();
            tempSpikes = new();
            tempRWalkers = new();
            tempPortals = new();

            //Four Borders
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -3000, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3600, -2800, 200, 2800), Color.DarkGray));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(200, -320, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(400, -360, 360, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(400, -320, 40, 120), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(360, -360, 40, 40), Color.Green, 0.4f, true));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(200, -480, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(320, -480, 80, 40), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(400, -680, 40, 240), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(280, -720, 400, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(680, -760, 40, 80), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(680, -680, 320, 40), Color.Yellow));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(320, 1, 1, 1), -880));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(320, 1, 1, 1), -720));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(440, 1, 1, 1), -880));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(440, 1, 1, 1), -720));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(560, 1, 1, 1), -880));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(560, 1, 1, 1), -720));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(520, -480, 80, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(600, -560, 40, 120), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(640, -560, 1360, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2000, -560, 1000,40), Color.Green, 0.9f, true));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2400, 1, 1, 1), -200));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(720,-880,160,40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(880, -920, 120, 80), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1000, -880, 2000, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3000, -880, 360, 40), Color.Green, 0.22f, true));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(3320, -960, 40, 80), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1960, -840, 40, 80), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1480, -760, 520, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1480, -720, 40, 80), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1120, -680, 360, 40), Color.Yellow));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(805, -440, 626, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1165, -400, 40, 80), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(845, -320, 360, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(925, -280, 40, 80), Color.Yellow));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1080, 1, 1, 1), -200));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(720, 1, 1, 1), -200));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1480, -440, 160, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1720, -440, 160, 40), Color.Green, 0.2f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1880, -520, 40, 320), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1480, -400, 40, 160), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1480, -240, 400, 40), Color.Yellow));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1510, 1, 1, 1), -240));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3360, -560, 40, 360), Color.Yellow));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(3400, 1, 1, 1), -550));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(3430, 1, 1, 1), -550));

            maze = new Level(tempPlatforms, tempPortals, tempRWalkers, tempSpikes);
            maze.SetExit(exitPortal, new Rectangle(3440, -320, 120, 120));

            return maze;
        }    
    }
}