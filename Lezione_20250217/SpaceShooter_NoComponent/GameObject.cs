﻿using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public class GameObject : IUpdatable, IDrawable {

        protected Sprite sprite;
        protected Texture texture;

        protected float speed;

        protected Rigidbody rigidbody;
        public Rigidbody Rigidbody {
            get { return rigidbody; }
        }

        public virtual Vector2 Position {
            get { return sprite.position; }
            set { sprite.position = value; }
        }
        public int Width {
            get { return (int)sprite.Width; }
        }
        public int Height {
            get { return (int)sprite.Height; }
        }
        public virtual bool IsActive {
            get {
                return true;
            }
            set {

            }
        }


        public GameObject(string textureName) {
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(texture.Width, texture.Height);
            rigidbody = new Rigidbody(this);
            rigidbody.IsCollisionAffected = true;
            UpdateMgr.AddItem(this);
            DrawMgr.AddItem(this);
        }

        public GameObject (string textureName, int spriteWidth, int spriteHeight) {
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(spriteWidth, spriteHeight);
            rigidbody = new Rigidbody(this);
            UpdateMgr.AddItem(this);
            DrawMgr.AddItem(this);
        }

        public virtual void Update () {

        }

        public virtual void LateUpdate() {

        }


        public virtual void Draw () {
            sprite.DrawTexture(texture);
        }

        public virtual void OnCollide (GameObject other) {

        }

    }
}
