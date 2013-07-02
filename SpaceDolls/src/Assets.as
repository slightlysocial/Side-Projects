package
{
	import flash.display.Bitmap;
	import flash.utils.Dictionary;
	
	import starling.textures.Texture;
	import starling.textures.TextureAtlas;

	public class Assets
	{
		[Embed(source = "../assets/welcome_playButton.png")]  
		private static const WelcomePlayBtn:Class;

		[Embed(source = "../assets/space-background.jpg")]  
		private static const BgSpace:Class;

		[Embed(source = "../assets/novi-stars.png")]  
		private static const girlWelcome:Class;
		
		[Embed(source = "../assets/red_planet.png")]  
		private static const redPlanet:Class;
		
		[Embed(source = "../assets/BgLayer1.jpg")]  
		private static const BgLayer1:Class;
		
		[Embed(source = "../assets/BgLayer2.png")]  
		private static const BgLayer2:Class;
		
		[Embed(source = "../assets/BgLayer3.png")]  
		private static const BgLayer3:Class;
		
		[Embed(source = "../assets/BgLayer4.png")]  
		private static const BgLayer4:Class;
		
		[Embed(source = "../assets/asteroids/Asteroids_64x64_001.png")]  
		private static const Asteroid1:Class;
		
		[Embed(source = "../assets/asteroids/Asteroids_64x64_002.png")]  
		private static const Asteroid2:Class;
		
		[Embed(source = "../assets/asteroids/Asteroids_64x64_003.png")]  
		private static const Asteroid3:Class;
		
		[Embed(source = "../assets/asteroids/Asteroids_64x64_004.png")]  
		private static const Asteroid4:Class;
		
		[Embed(source = "../assets/asteroids/Asteroids_64x64_005.png")]  
		private static const Asteroid5:Class;
		
		[Embed(source = "../assets/asteroids/Asteroids_64x64_006.png")]  
		private static const Asteroid6:Class;
		
		[Embed(source = "../assets/asteroids/Asteroids_64x64_007.png")]  
		private static const Asteroid7:Class;
		
		[Embed(source = "../assets/asteroids/Asteroids_64x64_008.png")]  
		private static const Asteroid8:Class;
		
		[Embed(source = "../assets/particles/texture.png")]  
		private static const ParticleTexture:Class;
		
		public static var gameTextures:Dictionary = new Dictionary();
		private static var gameTextureAtlas:TextureAtlas;
		
		[Embed(source = "../assets/powerups/effects.png")]  
		public static const AtlasTextureGame:Class;
		
		[Embed(source = "../assets/powerups/effects.xml", mimeType="application/octet-stream")]  
		public static const AtlasXmlGame:Class;
		
		public static function getAtlas():TextureAtlas
		{
			if(gameTextureAtlas == null)
			{
				var texture:Texture = getTexture("AtlasTextureGame");
				var xml:XML = XML(new AtlasXmlGame());
				gameTextureAtlas = new TextureAtlas(texture, xml);
			}
			return gameTextureAtlas;
		}
			
		public static function getTexture(name:String):Texture
		{
			if(gameTextures[name] == undefined)
			{
				var bitmap:Bitmap = new Assets[name]();
				gameTextures[name] = Texture.fromBitmap(bitmap);
			}
			return gameTextures[name];
		}
	}
}