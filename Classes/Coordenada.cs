using Godot;
using System;
using System.Collections.Generic;

public partial class Coordenada
{
    private int x;
        public int getX(){return this.x;}
        public void setX(int x){this.x = x;}

    private int y;
        public int getY(){return this.y;}
        public void setY(int y){this.y = y;}

    private String value;
        public String getValue(){return this.value;}
        public void setValue(String Value){this.value = Value;}

    public Coordenada(int x, int y, String value){
        this.x = x;
        this.y = y;
        this.value = value;
    }
}
