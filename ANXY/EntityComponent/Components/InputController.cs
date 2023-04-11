using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace ANXY.EntityComponent.Components
{
    internal class InputController : Component
    {
        public class InputSettings
        {
            public Movement Movement { get; set; }
            public Jump Jump { get; set; }
            public MainMenu MainMenu { get; set; }
            public ShowFPS ShowFPS { get; set; }
            public CapFPS CapFPS { get; set; }
        }
        public class Movement
        {
            public string Up { get; set; }
            public string Down { get; set; }
            public string Left { get; set; }
            public string Right { get; set; }
        }
        public class Jump
        {
            public string Key { get; set; }
        }
        public class MainMenu
        {
            public string Key { get; set; }
        }
        public class ShowFPS
        {
            public string Key { get; set; }
        }
        public class CapFPS
        {
            public string Key { get; set; }
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            //keyboard input
            var state = Keyboard.GetState();

        }

        public void Load(string filePath)
        {
            var serializer = new XmlSerializer(typeof(InputController));
            using (var stream = File.OpenRead(filePath))
            {
                return serializer.Deserialize(stream) as InputController;
            }
        }

        public void Save(string filePath)
        {
            var serializer = new XmlSerializer(typeof(InputConfig));
            using (var stream = File.Create(filePath))
            {
                serializer.Serialize(stream, this);
            }
        }

        public void ResetToDefaults()
        {

        }
    }
}
