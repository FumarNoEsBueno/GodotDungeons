using Godot;
using System;
using System.Collections.Generic;

public partial class Node2D : Godot.Node2D
{
	private Coordenada[,] matrizGuia = new Coordenada[50, 50];
	private Casilla[,] matrizReal = new Casilla[1000, 1000];
	private int tamanoTotal;
	private int tamanoMinimo = 50;
	private int tamanoMaximo = 70;

	private int posiblesSalidas = 0;

	private const int dimension = 50;
	private const int provabilidadDeSuperposicion = 50;

	private const char inicio = 'I';
	private const char pasillo = 'p';
	private const char origen = 'i';
	private const char final = 'F';
	private const char salida = 'Q';
	private const char norte = 'N';
	private const char sur = 'S';
	private const char este = 'E';
	private const char oeste = 'O';

	private void generarMatrizGuia(){
		this.matrizGuia[20,20] = new Coordenada(20, 20, origen.ToString());
		this.tamanoTotal = 0;
		List<Coordenada> listaSimbolos = new List<Coordenada>();
		listaSimbolos.Add(this.matrizGuia[20, 20]);
		this.generacionIterativa(listaSimbolos);
		this.anadirSalida();
		//imprimirMazmorra();
	}
    private void generarMatrizReal(){

    }

	private void imprimirMazmorra(){
		String cadena = "";
		for(int i = 0; i < dimension; i++){
			for(int j = 0; j < dimension; j++){
				if(matrizGuia[j, i] != null)cadena = cadena + " " + matrizGuia[j,i].getValue();
				else
					cadena = cadena + " X";
			}
			cadena = cadena + " \n";
		}
		GD.Print(cadena);
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
		this.posiblesSalidas = 0;
		this.generacionIterativa(newListaSimbolos);
	}

	private void anadirSalida(){
		List<Coordenada> posiblesSalidas = new List<Coordenada>();
		for(int i = 0; i < dimension; i++){
			for(int j = 0; j < dimension; j++){
				if(this.matrizGuia[j,i] == null) continue;
				if(this.matrizGuia[j,i].getValue().Trim(final).Trim(pasillo).Length == 1 && 
						!this.matrizGuia[j,i].getValue().Contains(inicio)){
					posiblesSalidas.Add(this.matrizGuia[j,i]);
				}
			}
		}
		Random rnd = new Random();
		int randomNumber = rnd.Next(posiblesSalidas.Count);
		if(posiblesSalidas.Count > 0) posiblesSalidas[randomNumber].setValue(posiblesSalidas[randomNumber] + salida.ToString());
	}

	private List<Coordenada> expandirGeneracion(){
		List<Coordenada> listaSimbolos = new List<Coordenada>();
		for(int i = 0; i < dimension; i++){
			for(int j = 0; j < dimension; j++){
				if(this.matrizGuia[j,i] == null) continue;
				if(this.matrizGuia[j,i].getValue().Contains(final.ToString())){
					this.matrizGuia[j,i].setValue(this.matrizGuia[j,i].getValue().Trim(final) + pasillo.ToString());
					listaSimbolos.Add(this.matrizGuia[j,i]);
				}
			}
		}
		return listaSimbolos;
	}

	private void generarMinimapa(){
		for(int i = 0; i < dimension; i++){
			for(int j = 0; j < dimension; j++){
				if(this.matrizGuia[j,i] == null) continue;
				if(this.matrizGuia[j,i].getValue().Contains(inicio)){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/Inicio.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(salida)){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/SALIDA.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(oeste) && 
						this.matrizGuia[j,i].getValue().Contains(sur) &&
						this.matrizGuia[j,i].getValue().Contains(este) &&
						this.matrizGuia[j,i].getValue().Contains(norte)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/X.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(oeste) && 
						this.matrizGuia[j,i].getValue().Contains(sur) &&
						this.matrizGuia[j,i].getValue().Contains(norte)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/TE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(oeste) && 
						this.matrizGuia[j,i].getValue().Contains(sur) &&
						this.matrizGuia[j,i].getValue().Contains(este)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/TN.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(oeste) && 
						this.matrizGuia[j,i].getValue().Contains(este) &&
						this.matrizGuia[j,i].getValue().Contains(norte)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/TS.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(norte) && 
						this.matrizGuia[j,i].getValue().Contains(este) &&
						this.matrizGuia[j,i].getValue().Contains(sur)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/TO.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(oeste) && 
						this.matrizGuia[j,i].getValue().Contains(sur)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CSCO.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(este) && 
						this.matrizGuia[j,i].getValue().Contains(sur)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CSCE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(oeste) && 
						this.matrizGuia[j,i].getValue().Contains(norte)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CNCO.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(este) && 
						this.matrizGuia[j,i].getValue().Contains(norte)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CNCE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(norte) && 
						this.matrizGuia[j,i].getValue().Contains(sur)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CNCS.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(oeste) && 
						this.matrizGuia[j,i].getValue().Contains(este)
				  ){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/CECO.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(oeste)){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/EE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(sur)){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/NE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(este)){
					Sprite2D sprite = new Sprite2D();
					sprite.Position = new Vector2(j * 16, i * 16);
					sprite.Texture = (Texture2D)GD.Load("res://Images/OE.png");
					this.AddChild(sprite);
					continue;
				} 
				if(this.matrizGuia[j,i].getValue().Contains(norte)){
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
			case String s when (s.Equals(origen.ToString())):
				listaSimbolos.AddRange(processS(simbolo));
				break;
			case String s when (s.Contains(pasillo)):
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
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), pasillo.ToString() + oeste.ToString()));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				break;
			case 1:
				simbolo.setValue("IO");
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), pasillo.ToString() + este.ToString()));
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[0];
				break;
			case 2:
				simbolo.setValue("IN");
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, pasillo.ToString() + sur.ToString()));
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[0];
				break;
			case 3:
				simbolo.setValue("IS");
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, pasillo.ToString() + norte.ToString()));
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[0];
				break;
			case 4:
				simbolo.setValue("IH");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), pasillo.ToString() + oeste.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), pasillo.ToString() + este.ToString()));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[1];
				break;
			case 5:
				simbolo.setValue("IV");
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, pasillo.ToString() + sur.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, pasillo.ToString() + norte.ToString()));
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[1];
				break;
			case 6:
				simbolo.setValue("IX");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), pasillo.ToString() + oeste.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), pasillo.ToString() + este.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, pasillo.ToString() + sur.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, pasillo.ToString() + norte.ToString()));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[2];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[3];
				break;
			case 7:
				simbolo.setValue("IEN");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), pasillo.ToString() + oeste.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, pasillo.ToString() + sur.ToString()));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[1];
				break;
			case 8:
				simbolo.setValue("IES");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), pasillo.ToString() + oeste.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, pasillo.ToString() + norte.ToString()));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[1];
				break;
			case 9:
				simbolo.setValue("ION");
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), pasillo.ToString() + este.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, pasillo.ToString() + sur.ToString()));
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[1];
				break;
			case 10:
				simbolo.setValue("IOS");
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), pasillo.ToString() + este.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, pasillo.ToString() + norte.ToString()));
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[1];
				break;
			case 11:
				simbolo.setValue("ITE");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), pasillo.ToString() + oeste.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, pasillo.ToString() + sur.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, pasillo.ToString() + norte.ToString()));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[2];
				break;
			case 12:
				simbolo.setValue("ITO");
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), pasillo.ToString() + este.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, pasillo.ToString() + sur.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, pasillo.ToString() + norte.ToString()));
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[2];
				break;
			case 13:
				simbolo.setValue("ITN");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), pasillo.ToString() + oeste.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), pasillo.ToString() + este.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() - 1, pasillo.ToString() + sur.ToString()));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = listaSimbolos[2];
				break;
			case 14:
				simbolo.setValue("ITS");
				listaSimbolos.Add(new Coordenada(simbolo.getX() + 1, simbolo.getY(), pasillo.ToString() + oeste.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX() - 1, simbolo.getY(), pasillo.ToString() + este.ToString()));
				listaSimbolos.Add(new Coordenada(simbolo.getX(), simbolo.getY() + 1, pasillo.ToString() + norte.ToString()));
				this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = listaSimbolos[0];
				this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = listaSimbolos[1];
				this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = listaSimbolos[2];
				break;
		}

		return listaSimbolos;
	}

	private Coordenada crecer(Coordenada simbolo, char direccion){
		Random rnd = new Random();
		int randomNumber = rnd.Next(100);
		switch(direccion){
			case este:
				if(simbolo.getX() + 1 >= dimension){
					simbolo.setValue(simbolo.getValue() + final.ToString());
					this.posiblesSalidas++;
				}else if(this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] == null){
					simbolo.setValue(simbolo.getValue().Trim(pasillo) + este.ToString());
					Coordenada simboloNuevo = new Coordenada(simbolo.getX() + 1, simbolo.getY(), pasillo.ToString() + oeste.ToString());
					this.matrizGuia[simbolo.getX() + 1, simbolo.getY()] = simboloNuevo ;
					return simboloNuevo;
				}else if(provabilidadDeSuperposicion > randomNumber && this.posiblesSalidas > 0){
					this.matrizGuia[simbolo.getX() + 1, simbolo.getY()].setValue(this.matrizGuia[simbolo.getX() + 1, simbolo.getY()].getValue() + oeste);
					simbolo.setValue(simbolo.getValue() + este);
				}else{
					simbolo.setValue(simbolo.getValue() + final.ToString());
					this.posiblesSalidas++;
				}
				break;
			case oeste:
				if(simbolo.getX() - 1 < 0){
					simbolo.setValue(simbolo.getValue() + final.ToString());
					this.posiblesSalidas++;
				}else if(this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] == null){
					simbolo.setValue(simbolo.getValue().Trim(pasillo) + oeste.ToString());
					Coordenada simboloNuevo = new Coordenada(simbolo.getX() - 1, simbolo.getY(), pasillo.ToString() + este.ToString());
					this.matrizGuia[simbolo.getX() - 1, simbolo.getY()] = simboloNuevo;
					return simboloNuevo;
				}else if(provabilidadDeSuperposicion > randomNumber && this.posiblesSalidas > 0){
					this.matrizGuia[simbolo.getX() - 1, simbolo.getY()].setValue(this.matrizGuia[simbolo.getX() - 1, simbolo.getY()].getValue() + este);
					simbolo.setValue(simbolo.getValue() + oeste);
				}else{
					simbolo.setValue(simbolo.getValue() + final.ToString());
					this.posiblesSalidas++;
				}
				break;
			case norte:
				if(simbolo.getY() < 0){
					simbolo.setValue(simbolo.getValue() + final.ToString());
					this.posiblesSalidas++;
				}else if(this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] == null){
					simbolo.setValue(simbolo.getValue().Trim(pasillo) + norte.ToString());
					Coordenada simboloNuevo = new Coordenada(simbolo.getX(), simbolo.getY() - 1, pasillo.ToString() + sur.ToString());
					this.matrizGuia[simbolo.getX(), simbolo.getY() - 1] = simboloNuevo;
					return simboloNuevo;
				}else if(provabilidadDeSuperposicion > randomNumber && this.posiblesSalidas > 0){
					this.matrizGuia[simbolo.getX(), simbolo.getY() - 1].setValue(this.matrizGuia[simbolo.getX(), simbolo.getY() - 1].getValue() + sur);
					simbolo.setValue(simbolo.getValue() + norte);
				}else{
					simbolo.setValue(simbolo.getValue() + final.ToString());
					this.posiblesSalidas++;
				}
				break;
			case sur:
				if(simbolo.getY() >= dimension){
					simbolo.setValue(simbolo.getValue() + final.ToString());
					this.posiblesSalidas++;
				}else if(this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] == null){
					simbolo.setValue(simbolo.getValue().Trim(pasillo) + sur.ToString());
					Coordenada simboloNuevo = new Coordenada(simbolo.getX(), simbolo.getY() + 1, pasillo.ToString() + norte.ToString());
					this.matrizGuia[simbolo.getX(), simbolo.getY() + 1] = simboloNuevo;
					return simboloNuevo;
				}else if(provabilidadDeSuperposicion > randomNumber && this.posiblesSalidas > 0){
					this.matrizGuia[simbolo.getX(), simbolo.getY() + 1].setValue(this.matrizGuia[simbolo.getX(), simbolo.getY() + 1].getValue() + norte);
					simbolo.setValue(simbolo.getValue() + sur);
				}else{
					simbolo.setValue(simbolo.getValue() + final.ToString());
					this.posiblesSalidas++;
				}
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
		Coordenada nuevoSimbolo;
		simbolo.setValue(simbolo.getValue().Trim(pasillo));
		switch (randomNumber){
			case 0:
				nuevoSimbolo = this.crecer(simbolo, este);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 1:
				nuevoSimbolo = this.crecer(simbolo, oeste);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 2:
				nuevoSimbolo = this.crecer(simbolo, norte);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 3:
				nuevoSimbolo = this.crecer(simbolo, sur);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 4:
				nuevoSimbolo = this.crecer(simbolo, sur);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, norte);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 5:
				nuevoSimbolo = this.crecer(simbolo, oeste);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, este);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 6:
				nuevoSimbolo = this.crecer(simbolo, oeste);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, norte);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 7:
				nuevoSimbolo = this.crecer(simbolo, oeste);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, sur);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 8:
				nuevoSimbolo = this.crecer(simbolo, este);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, norte);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 9:
				nuevoSimbolo = this.crecer(simbolo, este);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, oeste);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, norte);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 10:
				nuevoSimbolo = this.crecer(simbolo, este);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, oeste);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, sur);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 11:
				nuevoSimbolo = this.crecer(simbolo, norte);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, sur);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, oeste);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
			case 12:
				nuevoSimbolo = this.crecer(simbolo, norte);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, sur);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				nuevoSimbolo = this.crecer(simbolo, este);
				if(nuevoSimbolo != null)listaSimbolos.Add(nuevoSimbolo); 
				break;
		}
		return listaSimbolos;
	}
	
	private void RemoveAndFreeChildren(Node node) {
		foreach (Node child in node.GetChildren()) {
			if (child.GetChildCount() > 0) {
				RemoveAndFreeChildren(child);
			}

			child.GetParent().RemoveChild(child);
			child.QueueFree();
		}
	}

	public override void _Ready()
	{
		this.AddChild(new paredTesteo(16,16));
		this.generarMatrizGuia();
		this.generarMatrizReal();
		this.generarMinimapa();
	}

	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed){
			if (keyEvent.Keycode == Key.T && keyEvent.Pressed){
				for(int i = 0; i < dimension; i++){
					for(int j = 0; j < dimension; j++){
						if(this.matrizGuia[i,j] != null) this.matrizGuia[i,j] = null;
					}
				}
				this.RemoveAndFreeChildren(this);
				this.generarMatrizGuia();
                this.generarMatrizReal();
                this.generarMinimapa();
			}
		}
	}
}
