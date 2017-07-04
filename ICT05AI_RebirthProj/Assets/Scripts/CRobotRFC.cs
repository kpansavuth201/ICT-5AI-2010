using System;
using System.Collections.Generic;
using UnityEngine;

namespace EngineMidterm_Kanon
{
   class CRobotRFC : CRobot
   {
       //Attribute Received when begin turn
         //hp 
         //energy 
         //Defence 
         //WalkRange 
         //AttackDamage 
         //RadarRange 
         //EnergyCapacity 
         //StatusPointRemain 
         //score

       //Upgrade usage point
           //  experienceGuageMax[0] = 20; //AttackDamage
           //  experienceGuageMax[1] = 15; //Defence
           //  experienceGuageMax[2] = 7;  //WalkRange
           //  experienceGuageMax[3] = 5;  //RadarRange
           //  experienceGuageMax[4] = 40; //EnergyCapacity

             //StatusMaxLevel[0] = 5;
             //StatusMaxLevel[1] = 2;
             //StatusMaxLevel[2] = 6;
             //StatusMaxLevel[3] = 5;
             //StatusMaxLevel[4] = 12;      


     
          


      public CRobotRFC(CRobot s)
         : base()
      {         
      }//end Consturctor

      public override void Update()
      {
          subTurnCount++;
      }//end Update


      public override char ThinkWalk()
      {
         //return MoveUpOnly();

         return '0';
      }//end ThinkWalk

      public override char ThinkAttack()
      {
         return '0';
      }//end ThinkAttack
      
      public override char ThinkRadar()
      {
          return TurnRadar2DirectionBeginTurn();
         //return 'u';
      }//end ThinkRadar

      public override int ThinkUpgrade()
      {
          return 0;
          
      }//end ThinkUpgrade

      public override void CleanUp()
      {
         subTurnCount = -1;
      }//end CleanUp




     
          //Custom Attribute
          public char radarDirection = 'u';
          bool isBeginTurn = false;
          bool isWalked = false;
          public int subTurnCount = -1;


          //public int EnermyFoundInRadar()
          //{
          //       //        0 1 2 3 4 5 6 7 8 9 10   i line
          //       //    4  [-|-|-|-|-|-|-|-|-|-|-]
          //       //    3  [                     ]   array formation
          //       //    2  [                     ]   
          //       //    1  [                     ]
          //       //    0  [-|-|-|-|-|-|-|-|-|-|-]
          //       //    j line        X  <- robot

          //   for (int i = 0; i < 11; i++)
          //   {
          //      for (int j = 0; j < 5; j++)
          //      {
          //         myRadar.Left.infoReceived[
          //         }
          //      }//end for
             

          //}//end EnermyFoundInRadar

          public char TurnRadar2DirectionBeginTurn()
          {
              if (subTurnCount == 0)
              {
                  radarDirection = 'l';
                  return radarDirection;
              }
              else if (subTurnCount == 1)
              {
                  radarDirection = 'r';
                  return radarDirection;
              }
              else
              {
                  return '0';
              }
          }//end TurnRadar2DirectionBeginTurn


          private char MoveUpOnly()
          {
              return 'u';
          }//end MoveUpOnly

          public char TurnRadar4Direction()
          {
              
                  if (radarDirection == 'u')
                  {
                      radarDirection = 'd';
                  }
                  else if (radarDirection == 'd')
                  {
                      radarDirection = 'l';
                  }
                  else if (radarDirection == 'l')
                  {
                      radarDirection = 'r';
                  }
                  else if (radarDirection == 'r')
                  {
                      radarDirection = 'u';
                  }              

              return radarDirection;

          }//TurnRadar4Direction

     

   }//end CRobotRFC
}//end namespace
