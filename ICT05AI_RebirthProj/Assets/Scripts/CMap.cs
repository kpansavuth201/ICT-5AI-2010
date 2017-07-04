using System;
using System.Collections.Generic;
using UnityEngine;
namespace EngineMidterm_Kanon
{
   class CMap
   {
      //Attribute
      public CTile[,] MapTile;
      public int maxX;
      public int maxY;
      public CSprite grass;
      public CSprite wall;


      public CMap(int x, int y)
      {

         MapTile = new CTile[x, y];
         maxX = x;
         maxY = y;
      }//end Constructor

      public void InitMapData()
      {
         //set position
         for (int i = 0; i < maxX; i++)
         {
            for (int j = 0; j < maxY; j++)
            {
               MapTile[i, j] = new CTile(i, j);

            }
         }//end for

         //Create outer wall
         for (int i = 0; i < maxX; i++)
         {
            for (int j = 0; j < maxY; j++)
            {
               if (i == 0 || i == maxX - 1 || j == 0 || j == maxY - 1)
               {
                  MapTile[i, j].SetTerrainType("wall");
                  MapTile[i, j].SetobjectOnTile("wall");
               }//end if
            }
         }//end for
      }//end InitMapData

      public void DrawMap()
      {
         for (int i = 0; i < maxX; i++)
         {
            for (int j = 0; j < maxY; j++)
            {

               if (MapTile[i, j].GetTerrainType() == 0)
               {
                  grass.objectPosition.x = MapTile[i, j].GetPositionX() * grass.objectTexture.width;
                  grass.objectPosition.y = MapTile[i, j].GetPositionY() * grass.objectTexture.height;

                  grass.Draw();
               }
               else if (MapTile[i, j].GetTerrainType() == 1)
               {
                  wall.objectPosition.x = MapTile[i, j].GetPositionX() * wall.objectTexture.width;
                  wall.objectPosition.y = MapTile[i, j].GetPositionY() * wall.objectTexture.height;
                 wall.Draw();
               }//end if else
            }
         }//end for

      }//end Draw

   }//end CMap

   class CTile
   {
      //Attribute
      Vector2 position;
      int terrainType;
      //Map Terrain
      //0 for ground
      //1 for wall
      //2 for water
      int objectOnTile;
      //0 for empty
      //1 for item
      //2 for robot
      //3 for wall
      public int[] objectId; //optional use for team play
      //Robot
      //id, teamId

      public CTile(int x, int y)
      {
         position.x = x;
         position.y = y;
         terrainType = 0;
         objectOnTile = 0;
         objectId = new int[2];
         objectId[0] = 0;
         objectId[1] = 0;
      }//end Constructor

      public void SetPosition(int x, int y)
      {
         position.x = x;
         position.y = y;

      }//end SetPosition

      public void SetTerrainType(String s)
      {
         if (s.Equals("ground"))
         {
            terrainType = 0;
         }
         else if (s.Equals("wall"))
         {
            terrainType = 1;
         }
         else if (s.Equals("water"))
         {
            terrainType = 2;
         }
         else
         {
            terrainType = 0;
         }//end if else           
      }//end SetTerrainType

      public void SetTerrainType(int n)
      {
         if (n > 2 || n < 0)
         {
            terrainType = 0;
         }
         else
         {
            terrainType = n;
         }
      }//end SetTerrainType

      public void SetobjectOnTile(int type, int id0, int id1)
      {
         objectOnTile = type;
         objectId[0] = id0;
         objectId[1] = id1;
      }//end SetobjectOnTile

      public void SetobjectOnTile(int type)
      {
         objectOnTile = type;
      }//end SetobjectOnTile

      public void SetobjectOnTile(String s)
      {
         if (s.Equals("empty"))
         {
            objectOnTile = 0;
         }
         else if (s.Equals("item"))
         {
            objectOnTile = 1;
         }
         else if (s.Equals("robot"))
         {
            objectOnTile = 2;
         }
         else if (s.Equals("wall"))
         {
            objectOnTile = 3;
         }
         else
         {
            objectOnTile = -1;
         }//end if else         
      }//end SetobjectOnTile

      public int GetTerrainType()
      {
         return terrainType;
      }//end GetTerrainType

      public String GetobjectOnTile(String s)
      {
         if (objectOnTile == 0)
         {
            s = "empty";
         }
         else if (objectOnTile == 1)
         {
            s = "item";
         }
         else if (objectOnTile == 2)
         {
            s = "robot";
         }
         else if (objectOnTile == 3)
         {
            s = "wall";
         }
         else
         {
            s = "";
         }
         return s;
      }//end GetobjectOnTile 

      public char GetobjectOnTile(char s)
      {
         if (objectOnTile == 0)
         {
            s = 'e';
         }
         else if (objectOnTile == 1)
         {
            s = 'i';
         }
         else if (objectOnTile == 2)
         {
            s = 'r';
         }
         else if (objectOnTile == 3)
         {
            s = 'w';
         }
         else
         {
            s = 'n';
         }
         return s;
      }//end GetobjectOnTile

      public int GetobjectOnTile()
      {
         return objectOnTile;
      }//end GetobjectOnTile 

      public int GetPositionX()
      {
         return (int)position.x;
      }//GetPositionX

      public int GetPositionY()
      {
         return (int)position.y;
      }//GetPositionY



   }//end CTile

}//end namespace
