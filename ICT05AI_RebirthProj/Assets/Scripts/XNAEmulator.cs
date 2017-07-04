using UnityEngine;
using System.Collections;
using EngineMidterm_Kanon;

public class XNAEmulator : MonoBehaviour {

	private CArena myArena;
	private const int mapSizeX = 25;
	private const int mapSizeY = 15;
	private const int tileSize = 32;
	
	public Texture2D grassTex;
	public Texture2D wallTex;
	public Texture2D robotTex;
	
	private void SimulateUpate()
	{
		myArena.Update();
	}
	
	private void SimulateDraw()
	{
		myArena.Draw();
	}

	// Use this for initialization
	void Start () {
		myArena = new CArena(mapSizeX,mapSizeY,tileSize);
		myArena.InitArena();
		
		for(int i=0;i<myArena.robotList.Count;++i)
		{
			myArena.robotList[i].robot = new CSprite(robotTex);
		}
		
		myArena.myMap.grass = new CSprite(grassTex);
		myArena.myMap.wall = new CSprite(wallTex);
		
	}
	
	// Update is called once per frame
	void Update () {
		SimulateUpate();
	}
	
	void OnGUI()
	{
		myArena.Draw();
	}
	
}
