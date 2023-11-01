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
        public Level level0(Texture2D rectangleTex, Texture2D portalTex, Texture2D ghostPlat, Texture2D redWalker, Texture2D rWalkerDoorTex, Texture2D spikeTex, Texture2D exitPortal, SpriteFont font)
        {
            tempPlatforms = new();
            tempSpikes = new();
            tempRWalkers = new();
            tempPortals = new();

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
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(560, -520, 40, 80), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(640, -560, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(720, -760, 40, 200), Color.Yellow));

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
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(600, -1400, 240, 40), Color.Green, 0.5f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(840, -1400, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(840, -1360, 40, 160), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(880,-1280,50,80), new Rectangle(1600,-1280,50,80)));

            //Spike Tutorial
            tempPortals.Add(new Portal(portalTex, new Rectangle(1320, -280, 50, 80), new Rectangle(1600, -1280, 50, 80)));

            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1600, -1200, 720, 40), Color.Yellow));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1760, -1220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1880, -1300, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2080, -1220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2080, -1380, 20, 20), Color.White));

            tempPortals.Add(new Portal(portalTex, new Rectangle(2240, -1280, 50, 80), new Rectangle(1600, -1000, 50, 80)));

            //Red Walker Tutorial
            tempPortals.Add(new Portal(portalTex, new Rectangle(1440, -280, 50, 80), new Rectangle(1600, -1000, 50, 80)));

            tempPortals.Add(new Portal(portalTex, new Rectangle(2240, -1000, 50, 80), new Rectangle(1600, -280, 50, 80)));

            Level level0 = new Level(tempPlatforms, tempPortals, tempRWalkers, tempSpikes);
            level0.SetFont(font);
            level0.AddSign(new Vector2(20,-490), "Welcome to \nNinja Obstacle Course,\nYou Can set your Preffered \nControls in Settings, if not\nThe default controls are: \n\nSpace for Jump\nA for Left\nD for Right\nLeft Shift for Sprint");
            level0.AddSign(new Vector2(770, -370), "Yellow Blocks are \nthe Core of the Game,\nThey are solid blocks \nthat never change");
            //Marking the skip doors
            level0.AddSign(new Vector2(1040, -380), "     Ghost                                             Spikes\nPlatforms\n                                     Fading                                 Red Walkers\n                               Platforms");
            level0.SetExit(exitPortal, new Rectangle(1680,-320,120,120));
            level0.SetSpawn(new Vector2(40, -280));
            return level0;
        }
        public Level level1(Texture2D rectangleTex, Texture2D portalTex, Texture2D ghostPlat, Texture2D exitPortal, SpriteFont font)
        {
            Level level1;
            //Level 1 Content
            tempPlatforms = new();
            tempPortals = new();

            //Four Borders
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -3000, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3600, -2800, 200, 2800), Color.DarkGray));
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
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1240, -640, 40, 40), Color.Green, 0.5f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1280, -640, 40, 40), Color.Green, 0.5f, true, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1320, -640, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1360, -640, 320, 40), Color.Green, 0, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1680, -640, 120, 40), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(1720, -720, 50, 80), new Rectangle(3360, -400, 50, 80)));

            //Third Area
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2680, -320, 240, 40), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2920, -320, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3040, -300, 280, 20), Color.Red, 0.1f));
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
            //level1.AddSign(new Vector2(230, -2760), "Hold Sprint to move Faster \n  when walking on Ground");
            level1.SetExit(exitPortal, new Rectangle(1600, -2720, 120, 120));

            return level1;
        }
        public Level level2(Texture2D rectangleTex, Texture2D portalTex, Texture2D ghostPlat, Texture2D redWalker, Texture2D rWalkerDoorTex, Texture2D spikeTex, Texture2D exitPortal, SpriteFont font)
        {
            Level level2;
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

            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(1040,-280,60,80), 800,1400,rWalkerDoorTex));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1040,-320, 120,40), Color.Yellow));


            level2 = new Level(tempPlatforms, tempPortals, tempRWalkers, tempSpikes);

            return level2;
        }
        public Level level3(Texture2D rectangleTex, Texture2D portalTex, Texture2D ghostPlat, Texture2D redWalker, Texture2D rWalkerDoorTex, Texture2D spikeTex,Texture2D exitPortal, SpriteFont font)
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
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1880, -220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2040, -220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2280, -220, 20, 20), Color.White));
            //True Portal is 4th
            tempPortals.Add(new Portal(portalTex, new Rectangle(2400, -280, 50, 80), new Rectangle(1840, -480, 50, 80)));

            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2560, -220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2920, -220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(3400, -220, 20, 20), Color.White));
            tempPortals.Add(new Portal(portalTex, new Rectangle(3550, -280, 50, 80), new Rectangle(280, -800, 50, 80)));

            //Second Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1760, -400, 240, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2080, -400, 200, 40), Color.Green, 0.5f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2280, -400, 300, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(2280, -480, 60, 80), 2280, 2520, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2400, -600, 40, 80), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2400, -520, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2680, -400, 840, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(3000, -480, 60, 80), 2680, 3400, rWalkerDoorTex, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3080, -520, 40, 40), Color.Yellow));

            //Third Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3520, -520, 80, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3480, -640, 120, 40), Color.Green, 0.1f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2680, -640, 800, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1640, -680, 1040, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(2400, -760, 60, 80), 2240, 2620, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2440, -800, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2440, -880, 40, 80), Color.White, 0.5f));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2080, -700, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1960, -700, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1800, -700, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1640, -700, 20, 20), Color.White));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1480, -680, 80, 40), Color.Green, 0.6f, true, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1080, -680, 160, 40), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(1120, -760, 50, 80), new Rectangle(400, -920, 50, 80)));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(240, -840, 240, 40), Color.Yellow));
            Level level3 = new Level(tempPlatforms, tempPortals, tempRWalkers, tempSpikes);
            level3.SetFont(font);
            level3.AddSign(new Vector2(1500, -330), "Pick a door any door, \n4 Are Fake one is Right");
            level3.SetExit(exitPortal, new Rectangle(240, -960, 120, 120));

            return level3;
        }
    }
}