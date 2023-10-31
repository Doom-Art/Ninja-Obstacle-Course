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
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -3000, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3600, -2800, 200, 2800), Color.DarkGray));

            //Yellow Tutorial
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(400, -320, 400, 40), Color.Yellow));

            Level level0 = new Level(tempPlatforms, tempPortals, tempRWalkers, tempSpikes);
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
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(360, -440, 40, 120), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(360, -320, 240, 40), Color.Yellow));
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
            level1.AddSign(new Vector2(620, -600), "Jump To Enter\n The Portal");
            level1.AddSign(new Vector2(800, -880), "Red Ghost Platforms \nStop Your Jumps\nAnd Make You Fall");
            level1.AddSign(new Vector2(230, -2760), "Hold Sprint to move Faster \n  when walking on Ground");
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
            level3.AddSign(new Vector2(350, -380), "Beware of the Red Walkers, \nThey kill on Sight, While patrolling \nDuring The Night");
            level3.AddSign(new Vector2(1500, -330), "Pick a door any door, \n4 Are Fake one is Right");
            level3.AddSign(new Vector2(1800, -330), "Watch out for \n  Spikes Ahead");
            level3.SetExit(exitPortal, new Rectangle(240, -960, 120, 120));

            return level3;
        }
    }
}
