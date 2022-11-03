using Microsoft.Xna.Framework.Content;
using System.Threading.Tasks;
using ANXY.Player;

namespace ANXY
{
    public class ContentLoader
    {
        public static async Task Init(ContentManager content)
        {
            await PlayerSpriteFactory.Instance.LoadAllTextures(content);
        }
    }
}
