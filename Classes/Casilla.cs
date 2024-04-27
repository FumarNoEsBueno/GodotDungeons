using Godot;
using System;
using System.Collections.Generic;

public partial class Casilla : Godot.Node2D
{
    private List<Node2D> listaEntidades = new List<Node2D>();
    private Node2D estructura = new Node2D();
    private List<Node2D> listaObjetos = new List<Node2D>();

    private int x;
        public int getX(){return this.x;}
        public void setX(int x){this.x = x;}

    private int y;
        public int getY(){return this.y;}
        public void setY(int y){this.y = y;}

    public Casilla(int x,
            int y,
            Node2D estructura,
            List<Node2D> listaEntidades,
            List<Node2D> listaObjetos){

        this.estructura = estructura;
        this.listaEntidades = listaEntidades;
        this.listaObjetos = listaObjetos;
        this.x = x;
        this.y = y;
    }
}
