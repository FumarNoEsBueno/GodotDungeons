using Godot;
using System;
using System.Collections.Generic;

public partial class Node2D : Godot.Node2D
{
	private Coordenada[,] matrizGuia = new Coordenada[50, 50];
	private int[,] matrizReal = new int[1000, 1000];
	private int tamanoTotal;
	private int tamanoMinimo = 50;
	private int tamanoMaximo = 70;
	private int dimension = 50;

	private void generarMatrizGuia(){
		this.matrizGuia[20,20] = new Coordenada(20, 20, "i");
		this.tamanoTotal = 0;
		List<Coordenada> listaSimbolos = new List<Coordenada>();
		listaSimbolos.Add(this.matrizGuia[20, 20]);
		this.generacionIterativa(listaSimbolos);
		this.anadirSalida();
		imprimirMazmorra();
	}

	private void imprimirMazmorra(){
		String wea = "";
		for(int i = 0; i < 50; i++){
			for(int j = 0; j < 50; j++){
				if(matrizGuia[j, i] != null)wea = wea + " " + matrizGuia[j,i].getValue();
				else
					wea = wea + " X";
			}
			wea = wea + " \n";
		}
		GD.Print(wea);
		GD.Print("Tamaño total: " + this.tamanoTotal);
	}

	private void generacionIterativa(List<Coordenada> listaSimbolos){
		if(listaSimbolos.Count == 0){
			if(this.tamanoTotal < this.tamanoMinimo){
				listaSimbolos = this.expandirGeneracion();
			}else return;
		} 
		if(this.tamanoTotal > this.tamanoMaximo) return;

		List<Coordenada> newListaSimbolos = new List<Coordenada>();
		listaSimbolos.ForEach(delegate (Coordenada simbolo){
				newListaSimbolos.AddRange(this.procesarSimbolos(simbolo));
				});

		this.generacionIterativa(newListaSimbolos);
	}

	private void anadirSalida(){
		List<Coordenada> posiblesSalidas = new List<Coordenada>();
		for(int i = 0; i < 50; i++){
			for(int j = 0; j < 50; j++){
				if(this.matrizGuia[j,i] == null) continue;
				if(this.matrizGuia[j,i].getValue().Trim('F').Length == 2 && 
						!this.matrizGuia[j,i].getValue().Contains("I")){
					posiblesSalidas.Add(this.matrizGuia[j,i]);
				}
			}
		}
		Random rnd = new Random();
		int randomNumber = rnd.Next(posiblesSalidas.Count);
		posiblesSalidas[randomNumber].setValue(posiblesSalidas[randomNumber] + "Q");
	}

	private List<Coordenada> expandirGeneracion(){
		List<Coordenada> listaSimbolos = new List<Coordenada>();
		for(int i = 0; i < 50; i++){
			for(int j = 0; j < 50; j++){
				if(this.matrizGuia[j,i] == null) continue;
				if(this.matrizGuia[j,i].getValue().Contains("F")){
					this.matrizGuia[j,i].setValue(this.matrizGuia[j,i].getValue().Trim('F') + "p");
					listaSimbolos.Add(this.matrizGuia[j,i]);
				}
			}
		}
		return listaSimbolos;
	}

	private void generarMatrizReal(){
		for(int i = 0; i < 50; i++){
			for(int j = 0; j < 50; j++){
				if(this.matrizGuia[j,i] == null) continue;
				if(this.matrizGuia[j,i].getValue().Contains("I")){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/Inicio.png");
					this.AddChild(sprite);
				} 
				if(this.matrizGuia[j,i].getValue().Contains("Q")){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/SALIDA.png");
					this.AddChild(sprite);
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CO") && 
						this.matrizGuia[j,i].getValue().Contains("CS") &&
						this.matrizGuia[j,i].getValue().Contains("CE") &&
						this.matrizGuia[j,i].getValue().Contains("CN")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/X.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CO") && 
						this.matrizGuia[j,i].getValue().Contains("CS") &&
						this.matrizGuia[j,i].getValue().Contains("CN")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/TE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CO") && 
						this.matrizGuia[j,i].getValue().Contains("CS") &&
						this.matrizGuia[j,i].getValue().Contains("CE")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/TN.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CO") && 
						this.matrizGuia[j,i].getValue().Contains("CE") &&
						this.matrizGuia[j,i].getValue().Contains("CN")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/TS.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CN") && 
						this.matrizGuia[j,i].getValue().Contains("CE") &&
						this.matrizGuia[j,i].getValue().Contains("CS")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/TO.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CO") && 
						this.matrizGuia[j,i].getValue().Contains("CS")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CSCO.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CE") && 
						this.matrizGuia[j,i].getValue().Contains("CS")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CSCE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CO") && 
						this.matrizGuia[j,i].getValue().Contains("CN")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CNCO.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CE") && 
						this.matrizGuia[j,i].getValue().Contains("CN")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CNCE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CN") && 
						this.matrizGuia[j,i].getValue().Contains("CS")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CNCS.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CO") && 
						this.matrizGuia[j,i].getValue().Contains("CE")
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CECO.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CO")){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/EE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CS")){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/NE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CE")){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/OE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains("CN")){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/SE.png");
					this.AddChild(sprite);
					continue;
				} 
			}
		}
	}

	private List<Coordenada> procesarSimbolos(Coordenada simbolo){
		List<Coordenada> listaSimbolos = new List<Coordenada>();
		this.tamanoTotal++;
		switch(simbolo.getValue()){
			case String s when (s.Equals("i")):
				listaSimbolos.AddRange(processS(simbolo));
				break;
			case String s when (s.Contains("p")):
				listaSimbolos.AddRange(processP(simbolo));
				break;
		}
		return listaSimbolos;
	}

	private List<Coordenada> processS(Coordenada simbolo){
		List<Coordenada> listaSimbolos = new List<Coordenada>();
		Random rnd = new Random();
		int randomNumber = rnd.Next(15);
		switch(randomNumber){
			case 0:
				simbolo.setValue("IE");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), "pCO"));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				break;
			case 1:
				simbolo.setValue("IO");
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), "pCE"));
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[0];
				break;
			case 2:
				simbolo.setValue("IN");
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, "pCS"));
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[0];
				break;
			case 3:
				simbolo.setValue("IS");
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, "pCN"));
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[0];
				break;
			case 4:
				simbolo.setValue("IH");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), "pCO"));
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), "pCE"));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[1];
				break;
			case 5:
				simbolo.setValue("IV");
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, "pCS"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, "pCN"));
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[1];
				break;
			case 6:
				simbolo.setValue("IX");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), "pCO"));
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), "pCE"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, "pCS"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, "pCN"));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[2];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[3];
				break;
			case 7:
				simbolo.setValue("IEN");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), "pCO"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, "pCS"));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[1];
				break;
			case 8:
				simbolo.setValue("IES");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), "pCO"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, "pCN"));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[1];
				break;
			case 9:
				simbolo.setValue("ION");
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), "pCE"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, "pCS"));
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[1];
				break;
			case 10:
				simbolo.setValue("IOS");
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), "pCE"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, "pCN"));
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[1];
				break;
			case 11:
				simbolo.setValue("ITE");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), "pCO"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, "pCS"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, "pCN"));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[2];
				break;
			case 12:
				simbolo.setValue("ITO");
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), "pCE"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, "pCS"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, "pCN"));
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[2];
				break;
			case 13:
				simbolo.setValue("ITN");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), "pCO"));
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), "pCE"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, "pCS"));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[2];
				break;
			case 14:
				simbolo.setValue("ITS");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), "pCO"));
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), "pCE"));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, "pCN"));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[2];
				break;
		}

		return listaSimbolos;
	}

    private Coordenada crecer(Coordenada simbolo, string direccion){
        switch(direccion){
            case "E":
                if(simbolo.getX() + 1 >= this.dimension){
                    simbolo.setValue(simbolo.getValue() + "F");
                    break;
                }
                if(this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] == null){
                    simbolo.setValue(simbolo.getValue().Trim('p') + "CE");
                    Coordenada simboloNuevo = new Coordenada(simbolo.getX() + 1, simbolo.getY(), "pCO");
                    this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = simboloNuevo ;
                    return simboloNuevo;
                }else{
                    this.matrizGuia[simbolo.getX() + 1, simbolo.getY()].setValue(this.matrizGuia[simbolo.getX() + 1, simbolo.getY()].getValue() + "CO");
                    simbolo.setValue(simbolo.getValue() + "CEF");
                }
                break;
            case "O":
                if(simbolo.getX() - 1 < 0){
                    simbolo.setValue(simbolo.getValue() + "F");
                }
                if(this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] == null){
                    simbolo.setValue(simbolo.getValue().Trim('p') + "CO");
                    Coordenada simboloNuevo = new Coordenada(simbolo.getX() - 1, simbolo.getY(), "pCE");
                    this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = simboloNuevo;
                    return simboloNuevo;
                }else simbolo.setValue(simbolo.getValue() + "F");
                break;
            case "N":
                if(simbolo.getY() < 0){
                    simbolo.setValue(simbolo.getValue() + "F");
                    break;
                }
                if(this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] == null){
                    simbolo.setValue(simbolo.getValue().Trim('p') + "CN");
                    Coordenada simboloNuevo = new Coordenada(simbolo.getX(), simbolo.getY() - 1, "pCS");
                    this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = simboloNuevo;
                    return simboloNuevo;
                }else simbolo.setValue(simbolo.getValue() + "F");
                break;
            case "S":
                if(simbolo.getY() >= this.dimension){
                    simbolo.setValue(simbolo.getValue() + "F");
                    break;
                }
                if(this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] == null){
                    simbolo.setValue(simbolo.getValue().Trim('p') + "CS");
                    Coordenada simboloNuevo = new Coordenada(simbolo.getX(), simbolo.getY() + 1, "pCN");
                    this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = simboloNuevo;
                    return simboloNuevo;
                }else simbolo.setValue(simbolo.getValue() + "F");
                break;
            default:
                return null;
        }
        return null;
    }

    private List<Coordenada> processP(Coordenada simbolo){
        List<Coordenada> listaSimbolos = new List<Coordenada>();
        Random rnd = new Random();
        int randomNumber = rnd.Next(12);
        //int randomNumber = 2;
        Coordenada nuevoSimbolo;
        simbolo.setValue(simbolo.getValue().Trim('p'));
        switch (randomNumber){
            case 0:
                nuevoSimbolo = this.crecer(simbolo, "E");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 1:
                nuevoSimbolo = this.crecer(simbolo, "O");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 2:
                nuevoSimbolo = this.crecer(simbolo, "N");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 3:
                nuevoSimbolo = this.crecer(simbolo, "S");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 4:
                nuevoSimbolo = this.crecer(simbolo, "S");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "N");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 5:
                nuevoSimbolo = this.crecer(simbolo, "O");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "E");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 6:
                nuevoSimbolo = this.crecer(simbolo, "O");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "N");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 7:
                nuevoSimbolo = this.crecer(simbolo, "O");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "S");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 8:
                nuevoSimbolo = this.crecer(simbolo, "E");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "N");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 9:
                nuevoSimbolo = this.crecer(simbolo, "E");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "O");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "N");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 10:
                nuevoSimbolo = this.crecer(simbolo, "E");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "O");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "S");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 11:
                nuevoSimbolo = this.crecer(simbolo, "N");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "S");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "O");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
            case 12:
                nuevoSimbolo = this.crecer(simbolo, "N");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "S");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                nuevoSimbolo = this.crecer(simbolo, "E");
                if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
                break;
        }
        return listaSimbolos;
    }

    public override void _Ready()
    {
        this.generarMatrizGuia();
        this.generarMatrizReal();
    }

    public override void _Process(double delta)
    {
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed){
            if (keyEvent.Keycode == Key.T && keyEvent.Pressed){
                for(int i = 0; i < 50; i++){
                    for(int j = 0; j < 50; j++){
                        if(this.matrizGuia[i,j] != null) this.matrizGuia[i,j] = null;
                    }
                }
                foreach(Sprite2D node in this.GetChildren()){
                    this.RemoveChild(node);
                    node.QueueFree();
                }
                this.generarMatrizGuia();
                this.generarMatrizReal();
            }
        }
    }
}
