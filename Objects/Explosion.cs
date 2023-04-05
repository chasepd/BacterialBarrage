using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Objects
{
    internal class Explosion : GameObject
    {
        private SoundEffect _soundEffect;
        private int trackLife;
        public Explosion(Texture2D texture, SoundEffect soundEffect) : base(texture) 
        {
            trackLife = -1;
            _soundEffect = soundEffect;
            _animationFrames = 6;
            _animationTimeDelay = 0.05;
            _soundEffect.Play();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (trackLife > _animationFrame)
                IsDead = true;

            trackLife = _animationFrame;
        }

        public override void OnCollision(GameObject otherObj) { }
    }
}
