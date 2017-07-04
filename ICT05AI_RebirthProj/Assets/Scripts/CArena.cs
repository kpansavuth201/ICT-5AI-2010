using System;
using System.Collections.Generic;
using UnityEngine;

namespace EngineMidterm_Kanon
{
   class CArena 
   {

      //Attribute
     
      public CMap myMap;
      int mapSizeX;
      int mapSizeY;
      int tileSize;

      //Timer Control
      float timePos = 0f;
      float delayTime = 0.01f;
      int frameCount = 0;

      //Turn Control
      int currentPlayerTurn = 0;
      bool isTakeAction = false;
      bool isRefillStep = true;

		public List<CRobot> robotList = new List<CRobot>();

      //test2
      //public CRobotCustom test2;
      //test2

      private int maxRobot{
			get{ return robotList.Count; } 
		
      }
      CRobotData[] testData;
      bool[] markAsDead;

      //Input Control
//      MouseState mouseState;
//      MouseState oldMouseState;
//      KeyboardState keyboardState;
//      KeyboardState oldKeyboardState;
      bool[] showRobotInfo;


      //Constructor
      public CArena( int maxX, int maxY, int _tileSize)
      {
         mapSizeX = maxX;
         mapSizeY = maxY;
         tileSize = _tileSize;
      }//end Constructor



      //Game Method

      public void InitArena()
      {
         //Initiallize
         myMap = new CMap(mapSizeX, mapSizeY);
         myMap.InitMapData();

		robotList.Add( new CRobotBon(new CRobot()) );	
		robotList.Add( new CRobotKAN(new CRobot()) );
		robotList.Add( new CRobotKAN(new CRobot()) );
		robotList.Add( new CRobotKAN(new CRobot()) );
		robotList.Add( new CRobotKAN(new CRobot()) );
		robotList.Add( new CRobotKAN(new CRobot()) );
		robotList.Add( new CRobotKAN(new CRobot()) );
		robotList.Add( new CRobotKAN(new CRobot()) );

         testData = new CRobotData[maxRobot];
         for (int i = 0; i < maxRobot; i++)
         {
            testData[i] = new CRobotData(i, 1);
            testData[i].Init();
         }

         markAsDead = new bool[maxRobot];
         for (int i = 0; i < maxRobot; i++)
         {
            markAsDead[i] = false;
         }

		Vector2 []initPos = new Vector2[maxRobot];
			for(int i=0;i<maxRobot;++i)
			{
				initPos[i] = new Vector2(UnityEngine.Random.Range(1,22), UnityEngine.Random.Range(1,15));
			}
		 
		
         
         for(int i=0;i<robotList.Count;++i)
         {
			int index = i;
			testData[index].SetPosition((int)(initPos[index].x), (int)(initPos[index].y));
			myMap.MapTile[(int)(initPos[index].x), (int)(initPos[index].y)].SetobjectOnTile("robot");				
         }        		

         //Input Control
         showRobotInfo = new bool[maxRobot];
         for (int i = 0; i < maxRobot; i++)
         {
            showRobotInfo[i] = true;
         }//end for
         
      }//end InitArena

      public void LoadContent()
      {

      }//end LoadContent

      public void Update()
      {
         //Input Control
         UpdateMouse();
         UpdateKeyboard();

		timePos += Time.deltaTime;
		if( timePos >= delayTime)
		{
				timePos -= delayTime;
				AssignPlayerTurn( testData[currentPlayerTurn], robotList[ currentPlayerTurn ] );
					
				//currentPlayerTurn = 0; //reset currentPlayerTurn
				Debug.Log("frameCount "+frameCount);
				frameCount++;
		}
                    
      }//end Arena Update

      public void AssignPlayerTurn(CRobotData whatRobotData, CRobot whatRobot )
      {
         if (!markAsDead[whatRobotData.GetId()])
         {
            // <-- refill energy, walk Step & Update robot -->
            //
            //
            isTakeAction = false;
            if (isRefillStep)
            {
               whatRobotData.RefillEnergyAndWalkStep();
               
               isRefillStep = false;
            }//end if RefillStep

            // <-- Receive Info -->
            whatRobot.ReceiveRobotInfo(whatRobotData.GetInfo(1), whatRobotData.GetInfo(2),
                                       whatRobotData.GetInfo(3), whatRobotData.GetInfo(4),
                                       whatRobotData.GetInfo(5), whatRobotData.GetInfo(6),
                                       whatRobotData.GetInfo(7), whatRobotData.GetInfo(8),
                                       whatRobotData.GetInfo(9), whatRobotData.GetInfo(10));
            //
            //
            // <-- end refill energy, walk Step & Update robot -->


            // <-- Update Step -->
            //
            //
            //
            whatRobot.Update();


            // <-- Action Step -->
            //If not take action, energy will be consumed by 1
            //
            //
            if (RequestRadar(whatRobotData, whatRobot, whatRobot.ThinkRadar())) //<-- change robot here, change Think method here
            {
                isTakeAction = true;
            }


            if (ReQuestWalk(whatRobotData, whatRobot.ThinkWalk()))
            {
               isTakeAction = true;
            }//end if check if walk

            if (ReQuestAttack(whatRobotData, whatRobot.ThinkAttack()))
            {
               isTakeAction = true;
            }//end if check if attack

            
            //
            //
            //
            //
            //end  <-- Action Step -->




            //check Do nothing energy consume
            if (!isTakeAction)
            {
               whatRobotData.UseEnergy(1);
            }//end if energy consume



            if (whatRobotData.GetInfo(2) <= 0)//check Out of energy to pass turn
            {
               if (currentPlayerTurn == maxRobot - 1)
                  currentPlayerTurn = 0; // <--assign pass turn 
               else
                  currentPlayerTurn++; // <--assign pass turn 
               isRefillStep = true;

               // <-- Upgrade Status Step -->
               //
               RequestUpgrade(whatRobotData, whatRobot.ThinkUpgrade());


               
               // <-- CleanUp Robot Step -->
               whatRobot.CleanUp();
            }//end if 

         }
         else //statement for dead robot
         {
            if (currentPlayerTurn == maxRobot - 1)
               currentPlayerTurn = 0; // <--assign pass turn 
            else
               currentPlayerTurn++; // <--assign pass turn 
            isRefillStep = true;
         }

      }//end AssignPlayerTurn
           
      public bool ReQuestWalk(CRobotData whatRobotData, char whatDirection)
      {
         switch (whatDirection)
         {
            case 'u':   //verify energy and walkStepCount
               if (whatRobotData.GetInfo(2) > 0 && whatRobotData.GetInfo(9) < whatRobotData.GetInfo(4))
               {
                  //verify not out of map
                  if (whatRobotData.GetPositionY() > 0)
                  {
                     //verify empty space
                     if (myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY() - 1].GetobjectOnTile() == 0)
                     {
                        ApplyWalk(whatRobotData, whatDirection);
                        return true;
                     }                     
                  }
               }
               return false;

            case 'd':   //verify energy and walkStepCount
               if (whatRobotData.GetInfo(2) > 0 && whatRobotData.GetInfo(9) < whatRobotData.GetInfo(4))
               {
                  //verify not out of map
                  if (whatRobotData.GetPositionY() < mapSizeY-1)
                  {
                     //verify empty space
                     if (myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY() + 1].GetobjectOnTile() == 0)
                     {
                        ApplyWalk(whatRobotData, whatDirection);
                        return true;
                     }                    
                  }
               }
               return false;

            case 'l':   //verify energy and walkStepCount
               if (whatRobotData.GetInfo(2) > 0 && whatRobotData.GetInfo(9) < whatRobotData.GetInfo(4))
               {
                  //verify not out of map
                  if (whatRobotData.GetPositionX() > 0)
                  {
                     //verify empty space
                     if (myMap.MapTile[whatRobotData.GetPositionX() - 1, whatRobotData.GetPositionY()].GetobjectOnTile() == 0)
                     {
                        ApplyWalk(whatRobotData, whatDirection);
                        return true;
                     }                      
                  }
               }
               return false;

            case 'r':   //verify energy and walkStepCount
               if (whatRobotData.GetInfo(2) > 0 && whatRobotData.GetInfo(9) < whatRobotData.GetInfo(4))
               {
                  //verify not out of map
                  if (whatRobotData.GetPositionX() < mapSizeX-1)
                  {
                     //verify empty space
                     if (myMap.MapTile[whatRobotData.GetPositionX() + 1, whatRobotData.GetPositionY()].GetobjectOnTile() == 0)
                     {
                        ApplyWalk(whatRobotData, whatDirection);
                        return true;
                     }                     
                  }
               }
               return false;


            default: return false;
         }//end switch


      }//end ReQuestWalk

      private void ApplyWalk(CRobotData whatRobotData, char whatDirection)
      {
         switch (whatDirection)
         {
            case 'u': myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY()].SetobjectOnTile("empty"); //empty space
               whatRobotData.SetPosition(whatRobotData.GetPositionX(), whatRobotData.GetPositionY() - 1); //move robot
               myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY()].SetobjectOnTile("robot"); //apply robot to new position
               whatRobotData.UseEnergy(1);
               whatRobotData.CountStep();
               break;

            case 'd': myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY()].SetobjectOnTile("empty"); //empty space
               whatRobotData.SetPosition(whatRobotData.GetPositionX(), whatRobotData.GetPositionY() + 1); //move robot
               myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY()].SetobjectOnTile("robot"); //apply robot to new position
               whatRobotData.UseEnergy(1);
               whatRobotData.CountStep();
               break;

            case 'l': myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY()].SetobjectOnTile("empty"); //empty space
               whatRobotData.SetPosition(whatRobotData.GetPositionX() - 1, whatRobotData.GetPositionY()); //move robot
               myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY()].SetobjectOnTile("robot"); //apply robot to new position
               whatRobotData.UseEnergy(1);
               whatRobotData.CountStep();
               break;

            case 'r': myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY()].SetobjectOnTile("empty"); //empty space
               whatRobotData.SetPosition(whatRobotData.GetPositionX() + 1, whatRobotData.GetPositionY()); //move robot
               myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY()].SetobjectOnTile("robot"); //apply robot to new position
               whatRobotData.UseEnergy(1);
               whatRobotData.CountStep();
               break;

            default:
               break;
         }//end switch
      }//end ApplyWalk


      public bool ReQuestAttack(CRobotData whatRobotData, char whatDirection)
      {
         switch (whatDirection)
         {
            case 'u':   //verify energy
               if (whatRobotData.GetInfo(2) >= whatRobotData.GetInfo(5))
               {
                  //verify not out of map
                  if (whatRobotData.GetPositionY() > 0)
                  {
                     //verify empty space
                     if (myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY() - 1].GetobjectOnTile() == 2)
                     {
                        ApplyAttack(whatRobotData, whatDirection);
                        return true;
                     }
                  }
               }
               whatRobotData.UseEnergy(1);
               return false;

            case 'd':   //verify energy
               if (whatRobotData.GetInfo(2) >= whatRobotData.GetInfo(5))
               {
                  //verify not out of map
                  if (whatRobotData.GetPositionY() < mapSizeY-1)
                  {
                     //verify empty space
                     if (myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY() + 1].GetobjectOnTile() == 2)
                     {
                        ApplyAttack(whatRobotData, whatDirection);
                        return true;
                     }
                  }
               }
               whatRobotData.UseEnergy(1);
               return false;

            case 'l':   //verify energy
               if (whatRobotData.GetInfo(2) >= whatRobotData.GetInfo(5))
               {
                  //verify not out of map
                  if (whatRobotData.GetPositionX() > 0)
                  {
                     //verify empty space
                     if (myMap.MapTile[whatRobotData.GetPositionX() - 1, whatRobotData.GetPositionY()].GetobjectOnTile() == 2)
                     {
                        ApplyAttack(whatRobotData, whatDirection);
                        return true;
                     }
                  }
               }
               whatRobotData.UseEnergy(1);
               return false;

            case 'r':   //verify energy
               if (whatRobotData.GetInfo(2) >= whatRobotData.GetInfo(5))
               {
                  //verify not out of map
                  if (whatRobotData.GetPositionX() < mapSizeX -1)
                  {
                     //verify empty space
                     if (myMap.MapTile[whatRobotData.GetPositionX() + 1, whatRobotData.GetPositionY()].GetobjectOnTile() == 2)
                     {
                        ApplyAttack(whatRobotData, whatDirection);
                        return true;
                     }
                  }
               }
               whatRobotData.UseEnergy(1);
               return false;

            default: return false;
         }//end switch


      }//end ReQuestAttack


      private void ApplyAttack(CRobotData whatRobotData, char whatDirection)
      {
         CRobotData temp = null; // Robot Attacked
         switch (whatDirection)
         {
            case 'u': if (SearchRobotOnTile(whatRobotData.GetPositionX(), whatRobotData.GetPositionY() - 1) != -1)//found robot
               {
                  temp = testData[SearchRobotOnTile(whatRobotData.GetPositionX(), whatRobotData.GetPositionY() - 1)];
               }
               break;
            case 'd': if (SearchRobotOnTile(whatRobotData.GetPositionX(), whatRobotData.GetPositionY() + 1) != -1)//found robot
               {
                  temp = testData[SearchRobotOnTile(whatRobotData.GetPositionX(), whatRobotData.GetPositionY() + 1)];
               }
               break;
            case 'l': if (SearchRobotOnTile(whatRobotData.GetPositionX() - 1, whatRobotData.GetPositionY()) != -1)//found robot
               {
                  temp = testData[SearchRobotOnTile(whatRobotData.GetPositionX() - 1, whatRobotData.GetPositionY())];
               }
               break;
            case 'r': if (SearchRobotOnTile(whatRobotData.GetPositionX() + 1, whatRobotData.GetPositionY()) != -1)//found robot
               {
                  temp = testData[SearchRobotOnTile(whatRobotData.GetPositionX() + 1, whatRobotData.GetPositionY())];
               }
               break;
            default:
               break;
         }//end switch

         if (temp != null)
         {
             int remainHp = temp.GetInfo("hp");
             if (temp.Attacked(whatRobotData.GetInfo("AttackDamage")) == true)//assign damage
            {
               whatRobotData.plusScore(remainHp + 3);
               ApplyDead(temp);
               
            }
            else
            {
                whatRobotData.plusScore(whatRobotData.GetInfo("AttackDamage"));
            }
             whatRobotData.UseEnergy(3);
         }//end if

      }//end ApplyAttack

      public bool RequestRadar(CRobotData whatRobotData, CRobot whatRobot, char whatDirection)
      {
         //verify input
         if (whatDirection == 'u' || whatDirection == 'd' || whatDirection == 'l' || whatDirection == 'r')
         {
            //verify energy
            if (whatRobotData.GetInfo(2) >= whatRobotData.GetInfo(6))
            {
               ApplyRadar(whatRobotData, whatRobot, whatDirection);
               return true;
            }//end if verify energy
         }//end verify input
               return false;         
      }//end ReQuestRadar

      private void ApplyRadar(CRobotData whatRobotData, CRobot whatRobot, char whatDirection)
      {
          switch (whatDirection)
          {
            case 'u':whatRobot.myRadar.Up.Clear(); //clear Radar data
                     for (int u = -whatRobotData.GetInfo(6); u <= whatRobotData.GetInfo(6); u++)
                     {
                        for (int v = 0; v < whatRobotData.GetInfo(6); v++)
                        {
                           //check if not out of map
                           if (whatRobotData.GetPositionX() + u >= 0 &&
                              whatRobotData.GetPositionX() + u < mapSizeX &&
                              whatRobotData.GetPositionY() - v-1 >= 0 &&
                              whatRobotData.GetPositionY() - v-1 < mapSizeY)
                           {
                              whatRobot.myRadar.ReceiveInfoUp(u +whatRobotData.StatusMaxLevel[3], v, 
                                 myMap.MapTile[whatRobotData.GetPositionX() + u,
                                 whatRobotData.GetPositionY() - v-1].GetobjectOnTile('s'));

                           }//end if
                        }
                     }//end for
                     whatRobotData.UseEnergy(1);
                     break;
            case 'd': whatRobot.myRadar.Down.Clear(); //clear Radar data
                     for (int u = -whatRobotData.GetInfo(6); u <= whatRobotData.GetInfo(6); u++)
                     {
                        for (int v = 0; v < whatRobotData.GetInfo(6); v++)
                        {
                           //check if not out of map
                           if (whatRobotData.GetPositionX() + u >= 0 &&
                              whatRobotData.GetPositionX() + u < mapSizeX &&
                              whatRobotData.GetPositionY() + v+1 >= 0 &&
                              whatRobotData.GetPositionY() + v+1 < mapSizeY)
                           {
                               whatRobot.myRadar.ReceiveInfoDown(u + whatRobotData.StatusMaxLevel[3], v, 
                                 myMap.MapTile[whatRobotData.GetPositionX() + u,
                                 whatRobotData.GetPositionY() + v+1].GetobjectOnTile('s'));
                           }//end if
                        }
                     }//end for
                     whatRobotData.UseEnergy(1);
                     break;
            case 'l': whatRobot.myRadar.Left.Clear(); //clear Radar data
                     for (int u = 0; u < whatRobotData.GetInfo(6); u++)
                     {
                         for (int v = -whatRobotData.GetInfo(6); v <= whatRobotData.GetInfo(6); v++)
                         {
                             //check if not out of map
                             if (whatRobotData.GetPositionX() - u-1 >= 0 &&
                                whatRobotData.GetPositionX() - u-1 < mapSizeX &&
                                whatRobotData.GetPositionY() + v >= 0 &&
                                whatRobotData.GetPositionY() + v < mapSizeY)
                             {
                                 whatRobot.myRadar.ReceiveInfoLeft(v + whatRobotData.StatusMaxLevel[3], u,
                                    myMap.MapTile[whatRobotData.GetPositionX() - u-1,
                                    whatRobotData.GetPositionY() + v].GetobjectOnTile('s'));
                             }//end if
                         }
                     }//end for
                     whatRobotData.UseEnergy(1);
                     break;
            case 'r': whatRobot.myRadar.Right.Clear(); //clear Radar data
                     for (int u = 0; u < whatRobotData.GetInfo(6); u++)
                     {
                         for (int v = -whatRobotData.GetInfo(6); v <= whatRobotData.GetInfo(6); v++)
                         {
                             //check if not out of map
                             if (whatRobotData.GetPositionX() + u + 1 >= 0 &&
                                whatRobotData.GetPositionX() + u + 1 < mapSizeX &&
                                whatRobotData.GetPositionY() + v >= 0 &&
                                whatRobotData.GetPositionY() + v < mapSizeY)
                             {
                                 whatRobot.myRadar.ReceiveInfoRight(v + whatRobotData.StatusMaxLevel[3], u,
                                    myMap.MapTile[whatRobotData.GetPositionX() + u + 1,
                                    whatRobotData.GetPositionY() + v].GetobjectOnTile('s'));
                             }//end if
                         }
                     }//end for
                     whatRobotData.UseEnergy(1);
                     break;
            default:
                     break;
          }//end switch
      }//end ApplyRadar


      public bool RequestUpgrade(CRobotData whatRobotData, int whatStatus)
      {
          return whatRobotData.upgradeExperience(whatStatus); ;
      }//end RequestUpgrade


      public void ApplyDead(CRobotData whatRobotData)
      {
         markAsDead[whatRobotData.GetId()] = true;
         myMap.MapTile[whatRobotData.GetPositionX(), whatRobotData.GetPositionY()].SetobjectOnTile("empty");        
      }//end ApplyDead

      public int SearchRobotOnTile(int x, int y)
      {
         for (int i = 0; i < maxRobot; i++)
         {
            if ((testData[i].GetPositionX() == x) && (testData[i].GetPositionY() == y))
            {
               return i;
            }
         }//end for
         return -1;
      }//end SearchRobotOnTile

      public void UpdateMouse()
      {
//         mouseState = Mouse.GetState();
//
//         if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
//         {
//            if (SearchRobotOnTile(mousePosX(), mousePosY()) != -1)
//            {
//               showRobotInfo[SearchRobotOnTile(mousePosX(), mousePosY())] =
//                       !showRobotInfo[SearchRobotOnTile(mousePosX(), mousePosY())];  //toggle on Robot info Display   
//            }
//         }//end if    
//         oldMouseState = mouseState;
      }//end UpdateMouse

      public int mousePosX()
      {
//         if (mouseState.X / tileSize < 0)
//         {
//            return 0;
//         }
//         else if (mouseState.X / tileSize >= mapSizeX)
//         {
//            return mapSizeX - 1;
//         }
//         else
//         {
//            return mouseState.X / tileSize;
//         }
			return 0;
      }//end mousePosBlockX

      public int mousePosY()
      {
//         if (mouseState.Y / tileSize < 0)
//         {
//            return 0;
//         }
//         else if (mouseState.Y / tileSize >= mapSizeY)
//         {
//            return mapSizeY - 1;
//         }
//         else
//         {
//            return mouseState.Y / tileSize;
//         }
			return 0;
      }//end mousePosBlockX

      public void UpdateKeyboard()
      {
//         // check keyboard r for reset world pos
//         keyboardState = Keyboard.GetState();
//         if (isKey(Keys.Z))
//         {
//            delayTime *= 2;
//         }
//
//         if (isKey(Keys.X))
//         {
//            delayTime = 1000;
//         }
//
//         if (isKey(Keys.C))
//         {
//            //delayTime /= 2;
//             delayTime = 7;
//         }
//
//         oldKeyboardState = keyboardState;
      }//end UpdateKeyboard

      public void Draw()
      {
			
			myMap.DrawMap();

//         //draw map
//
//         //test
		for(int i=0; i< robotList.Count; ++i)
		{
			CRobot tmp = robotList[i];
				tmp.robot.objectPosition = new Vector2(testData[i].GetPositionX() * tmp.robot.objectTexture.width,
				                                       testData[i].GetPositionY() * tmp.robot.objectTexture.height);
				tmp.Draw(); 
		}
         
         
         GUILayout.Label("PlayerTurn : "+currentPlayerTurn);
			DrawProgramInfo();
			//
			//         //test2
//         if (!testData[1].GetIsDead())
//         {
//            test2.robot.objectPosition = new Vector2(testData[1].GetPositionX() * test2.robot.objectTexture.Width,
//                                                     testData[1].GetPositionY() * test2.robot.objectTexture.Height);
//            test2.Draw(spriteBatch);
//            spriteBatch.DrawString(myUtil.font1, "id" + " = " + testData[1].GetId(),
//                                         new Vector2(testData[1].GetPositionX() * tileSize, testData[1].GetPositionY() * tileSize), Color.White);
//         }


         
      }//end Draw

      public void DrawProgramInfo()
      {

         DrawRobotInfo();
         DrawRadarInfo();

//         //Draw ObjectOnTile info            
//         spriteBatch.DrawString(myUtil.font1, Convert.ToString(myMap.MapTile[mousePosX(), mousePosY()].GetobjectOnTile("")),
//                     new Vector2(myMap.MapTile[mousePosX(), mousePosY()].GetPositionX() * tileSize,
//                         myMap.MapTile[mousePosX(), mousePosY()].GetPositionY() * tileSize), Color.White);
//
//         //Draw mouse position
//         spriteBatch.DrawString(myUtil.font1, "x : " + mouseState.X / tileSize, new Vector2(mouseState.X, mouseState.Y - 30), Color.White);
//         spriteBatch.DrawString(myUtil.font1, "y : " + mouseState.Y / tileSize, new Vector2(mouseState.X, mouseState.Y - 20), Color.White);
//
//         //Draw SearchRobotOnTile
//         if (SearchRobotOnTile(mouseState.X / tileSize, mouseState.Y / tileSize) == -1)
//         {
//             spriteBatch.DrawString(myUtil.font1, "no robot found",
//                                            new Vector2(mouseState.X, mouseState.Y - 40), Color.White);
//         }
//         else
//         {
//             spriteBatch.DrawString(myUtil.font1, "robot id : " + 
//                Convert.ToString(SearchRobotOnTile(mouseState.X / tileSize, mouseState.Y / tileSize)),
//                new Vector2(mouseState.X, mouseState.Y - 40), Color.White);
//         }//end if

         DrawAlgorithm();

//         //Draw frameCount
//         spriteBatch.DrawString(myUtil.font1, "delay time = " + delayTime, new Vector2(700, 575), Color.White);
//         spriteBatch.DrawString(myUtil.font1, "currentPlayerTurn = " + currentPlayerTurn, new Vector2(700, 590), Color.White);
//         spriteBatch.DrawString(myUtil.font1, "isRefillStep = " + isRefillStep, new Vector2(700, 600), Color.White);
//         spriteBatch.DrawString(myUtil.font1, "isTakeAction = " + isTakeAction, new Vector2(700, 610), Color.White);
//         spriteBatch.DrawString(myUtil.font1, "frameCount = " + frameCount, new Vector2(700, 620), Color.White);
      }//end DrawProgramInfo    

      public void DrawRobotInfo()
      {         
         for (int k = 0; k < maxRobot; k++)
         {
            if (showRobotInfo[k])
            {
              int count = -1;
              for (int i = 1; i <= 10; i++)
               {
           			if(i == 1 || i == 10){
					Rect _rect = new Rect(
						robotList[k].robot.drawRect.x ,
							robotList[k].robot.drawRect.y+ ((++count)*10),
						200,
						20);
					
						GUI.Label(	_rect,
						          testData[k].GetIsDead()? ("Dead") :
						  ("" + k + " " + testData[k].GetInfoDescription(i) + " = " + testData[k].GetInfo(i)));
					}	
						
			
               }
            }//end if
         }//end for

      }//end DrawRobotInfo

      public void DrawRadarInfo()
      {
         //Draw RadarUp info 
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
//               spriteBatch.DrawString(myUtil.font1, Convert.ToString(test1.myRadar.Up.infoReceived[i, j]),
//                     new Vector2(mouseState.X + i * 10, mouseState.Y + (-j) * 10 - 100), Color.White);

            }
         }//end for
//         spriteBatch.DrawString(myUtil.font1, "x", new Vector2(mouseState.X + 5 * 10, mouseState.Y + (1) * 10 - 100), Color.White);

         //Draw RadarDown info 
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
//               spriteBatch.DrawString(myUtil.font1, Convert.ToString(test1.myRadar.Down.infoReceived[i, j]),
//                     new Vector2(mouseState.X + i * 10, mouseState.Y + (j) * 10 + 100), Color.White);

            }
         }//end for
//         spriteBatch.DrawString(myUtil.font1, "x", new Vector2(mouseState.X + 5 * 10, mouseState.Y + (1) * 10 + 80), Color.White);

         //Draw RadarLeft info 
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
//               spriteBatch.DrawString(myUtil.font1, Convert.ToString(test1.myRadar.Left.infoReceived[i, j]),
//                     new Vector2(mouseState.X - j * 10 - 100, mouseState.Y + (i) * 10 - 50), Color.White);

            }
         }//end for
//         spriteBatch.DrawString(myUtil.font1, "x", new Vector2(mouseState.X - 1 * 10 - 80, mouseState.Y + (5) * 10 - 50), Color.White);

         //Draw RadarRight info 
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
//               spriteBatch.DrawString(myUtil.font1, Convert.ToString(test1.myRadar.Right.infoReceived[i, j]),
//                     new Vector2(mouseState.X + j * 10 + 180, mouseState.Y + (i) * 10 - 50), Color.White);

            }
         }//end for
//         spriteBatch.DrawString(myUtil.font1, "x", new Vector2(mouseState.X + 1 * 10 + 180 - 20, mouseState.Y + (5) * 10 - 50), Color.White);
//
//         spriteBatch.DrawString(myUtil.font1, "radar direction = " + test1.radarDirection, new Vector2(700, 565), Color.White);



      }//end DrawRadarInfo

      public void DrawAlgorithm()
      {
//          spriteBatch.DrawString(myUtil.font1, "enermyFound = " + test2.numbersOfEnermy, new Vector2(600, 565), Color.White);
//
//          spriteBatch.DrawString(myUtil.font1, "nearest x = " + test2.markNearestEnermyRight.X, new Vector2(600, 575), Color.White);
//          spriteBatch.DrawString(myUtil.font1, "nearest y = " + test2.markNearestEnermyRight.Y, new Vector2(600, 585), Color.White);
//          spriteBatch.DrawString(myUtil.font1, "distance1 = " + test2.distance1, new Vector2(600, 595), Color.White);
//          spriteBatch.DrawString(myUtil.font1, "Nearest dis = " + test2.NearestEnermyDistance, new Vector2(600, 605), Color.White);
      }//end DrawAlgorithm

   }//end CArena

}//end namespace
