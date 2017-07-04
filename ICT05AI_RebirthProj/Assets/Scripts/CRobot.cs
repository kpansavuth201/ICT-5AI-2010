using System;
using System.Collections.Generic;
using UnityEngine;

namespace EngineMidterm_Kanon
{
   class CRobot //: CSprite
   {
      //Attribute
      public CSprite robot;
      public CRadar myRadar;

      //info Attribute
      protected int hp;
      protected int energy;
      //status Attribute
      protected int Defence;
      protected int WalkRange;
      protected int AttackDamage;
      protected int RadarRange;
      protected int EnergyCapacity;
      protected int StatusPointRemain;
      protected int score;


      //Constructor
      public CRobot()
      {
         myRadar = new CRadar();

      }//end Constructor

      public void InitRobot()
      {

      }//end InitRobot


      public void ReceiveRobotInfo(int _hp, int _energy, int _Defence, int _WalkRange, 
                                   int _AttackDamage,int _RadarRange, int _EnergyCapacity, 
                                   int _StatusPointRemain,int _score, int _dummy)  // -1 = unknow information
      {
         hp = _hp;
         energy = _energy;
         Defence = _Defence;
         WalkRange = _WalkRange;
         AttackDamage = _AttackDamage;
         RadarRange = _RadarRange;
         EnergyCapacity = _EnergyCapacity;
         StatusPointRemain = _StatusPointRemain;
         score = _score;
      }//end ReceiveRobotInfo
      
      
      //Think method
      //
      public virtual char ThinkRadar()
      {
         return '0';
      }//end ThinkRadar

      public virtual char ThinkWalk()
      {
         return '0';
      }//end ThinkWalk

      public virtual char ThinkAttack()
      {
         return '0';
      }//end ThinkAttack

      public virtual int ThinkUpgrade()
      {
         return 0;
      }//end ThinkUpgrade


      //Game Method

      public virtual void Update()
      {
      }//end Update

      public virtual void CleanUp()
      {
      }//end CleanUp


      public void Draw()
      {
         robot.Draw();
      }//end Draw


   }//end class CRobot


   class CRadar
   {
      //Attribute      
      public CRadarUp Up;
      public CRadarDown Down;
      public CRadarLeft Left;
      public CRadarRight Right;

      public CRadar()
      {
          Up = new CRadarUp();
         Down = new CRadarDown();
         Left = new CRadarLeft();
         Right = new CRadarRight();
      }//end Constructor


      public void ReceiveInfoUp(int i, int j, char info)
      {
         if (i >= 0 && i < 11 && j >= 0 && j < 5)
            Up.infoReceived[i, j] = info;
      }//end ReceiveInfoUp

      public void ReceiveInfoDown(int i, int j, char info)
      {
         if (i >= 0 && i < 11 && j >= 0 && j < 5)
            Down.infoReceived[i, j] = info;
      }//end ReceiveInfoDown

      public void ReceiveInfoLeft(int i, int j, char info)
      {
         if (i >= 0 && i < 11 && j >= 0 && j < 5)
            Left.infoReceived[i, j] = info;
      }//end ReceiveInfoLeft

      public void ReceiveInfoRight(int i, int j, char info)
      {
         if (i >= 0 && i < 11 && j >= 0 && j < 5)
            Right.infoReceived[i, j] = info;
      }//end ReceiveInfoRight

      public String DecodeInfo(char info)
      {
         String decode = "none";

         switch (info)
         {
            case 'n': decode = "none"; //unknown information
               break;
            case 'e': decode = "empty";
               break;
            case 'i': decode = "item";
               break;
            case 'r': decode = "robot";
               break;
            case 'w': decode = "wall";
               break;

            default: decode = "error";
               break;
         }//end switch

         return decode;
      }//end DecodeInfo


   }//end class CRadar

   class CRadarUp
   {
      public char[,] infoReceived;

      public CRadarUp()
      {
         infoReceived = new char[11, 5];
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
               infoReceived[i, j] = 'n';
            }
         }//end for

         //        0 1 2 3 4 5 6 7 8 9 10   i line
         //    4  [-|-|-|-|-|-|-|-|-|-|-]
         //    3  [                     ]   array formation
         //    2  [                     ]   
         //    1  [                     ]
         //    0  [-|-|-|-|-|-|-|-|-|-|-]
         //    j line        X  <- robot

      }//end Constrctor

      public void Clear()
      {
         infoReceived = new char[11, 5];
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
               infoReceived[i, j] = 'n';
            }
         }//end for
      }//end Clear

   }//end CRadarUp


   class CRadarDown
   {
      public char[,] infoReceived;

      public CRadarDown()
      {
         infoReceived = new char[11, 5];
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
               infoReceived[i, j] = 'n';
            }
         }//end for

         //    j line        X <- robot
         //    0  [-|-|-|-|-|-|-|-|-|-|-]
         //    1  [                     ]   array formation
         //    2  [                     ]   
         //    3  [                     ]
         //    4  [-|-|-|-|-|-|-|-|-|-|-]
         //        0 1 2 3 4 5 6 7 8 9 10   i line   


      }//end Constrctor

      public void Clear()
      {
         infoReceived = new char[11, 5];
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
               infoReceived[i, j] = 'n';
            }
         }//end for
      }//end Clear

   }//end CRadarDown


   class CRadarLeft
   {
      public char[,] infoReceived;

      public CRadarLeft()
      {
         infoReceived = new char[11, 5];
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
               infoReceived[i, j] = 'n';
            }
         }//end for

         // i line         
         // 0  [-|-|-|-|-]
         // 1  [         ]   
         // 2  [         ]   
         // 3  [         ]
         // 4  [         ]    
         // 5  [         ]X  array formation
         // 6  [         ]
         // 7  [         ]
         // 8  [         ]
         // 9  [         ]
         //10  [-|-|-|-|-]
         //     4 3 2 1 0   j line   


      }//end Constrctor

      public void Clear()
      {
         infoReceived = new char[11, 5];
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
               infoReceived[i, j] = 'n';
            }
         }//end for
      }//end Clear

   }//end CRadarLeft

   class CRadarRight
   {
      public char[,] infoReceived;

      public CRadarRight()
      {
         infoReceived = new char[11, 5];
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
               infoReceived[i, j] = 'n';
            }
         }//end for

         // i line         
         // 0  [-|-|-|-|-]
         // 1  [         ]   
         // 2  [         ]   
         // 3  [         ]
         // 4  [         ]    array formation
         // 5 X[         ]
         // 6  [         ]
         // 7  [         ]
         // 8  [         ]
         // 9  [         ]
         //10  [-|-|-|-|-]
         //     0 1 2 3 4   j line   


      }//end Constrctor

      public void Clear()
      {
         infoReceived = new char[11, 5];
         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
               infoReceived[i, j] = 'n';
            }
         }//end for
      }//end Clear

   }//end CRadarRight

}//end namespace
