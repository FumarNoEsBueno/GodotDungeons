using Godot;
using System;

public partial class Estructura : Godot.Node2D
{
	private Sprite2D textura = new Sprite2D();
		public void setPosition(int x, int y){
			this.x = x;
			this.y = y;
			this.textura.Position = new Vector2(x, y);
		}
		public void setTextura(String ruta){
			this.textura.Texture = (Texture2D)GD.Load("res://Images/" + ruta );
			this.AddChild(this.textura);
		}

	private int x;
		public int getX(){return this.x;}
		public void setX(int x){this.x = x;}

	private int y;
		public int getY(){return this.y;}
		public void setY(int y){this.y = y;}
	
}

public partial class paredTesteo : Godot.Node2D{

	private Estructura estructura;

	public paredTesteo(int x, int y){
		this.estructura = new Estructura();
		this.estructura.setTextura("testeo.png");
		this.estructura.setPosition(x, y);
		this.AddChild(this.estructura);
	}
}
