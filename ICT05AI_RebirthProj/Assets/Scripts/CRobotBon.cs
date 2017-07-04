using System;
using System.Collections.Generic;
using UnityEngine;

namespace EngineMidterm_Kanon
{
   class CRobotBon : CRobot
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
     



      public override void Update()
      {
      	Debug.Log("Rpbpt Update " + subTurnCount);
      
         subTurnCount++;

         if (subTurnCount == 2)
         {
            numbersOfEnermy = EnermyFoundInRadar('r'); //not use
            numbersOfEnermy += EnermyFoundInRadar('l');

            if (MarkNearestEnermy('l') > MarkNearestEnermy('r'))
            {
               if (MarkNearestEnermy('l') <= 4)
               {
                  enermyInRangeLeft = true;
               }
            }
            else
            {
               if (MarkNearestEnermy('r') <= 4)
               {
                  enermyInRangeRight = true;
               }
            }

            if (numbersOfEnermy == 0)
               activeBerserkMode = true;
         }//end if subTurnCount

      }//end Update


      public override char ThinkWalk()
      {
		return 'u';


         if (activeBerserkMode)
            return BerserkWalk();
         else
            return walkToNearestEnermy();
      }//end ThinkWalk

      public override char ThinkAttack()
      {
         if (activeBerserkMode)
            return BerserkAttack();
         else
            return AttackNearestEnermy();
      }//end ThinkAttack

      public override char ThinkRadar()
      {
         if (activeBerserkMode)
            return '0';
         else
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
         enermyInRangeLeft = false;
         enermyInRangeRight = false;
         enermyInRangeUp = false;
         enermyInRangeDown = false;
         numbersOfEnermy = 0;
         NearestEnermyDistance = -1;
         resetMarkNearestEnermy();

         activeBerserkMode = false;
         berserkAtkUp = true;

      }//end CleanUp





      //Custom Attribute
      public char radarDirection = 'u';
      bool isBeginTurn = false;
      bool isWalked = false;
      public int subTurnCount = -1;
      public int numbersOfEnermy = 0;
      public int distance1 = 0;
      public bool enermyInRangeLeft = false;
      public bool enermyInRangeRight = false;
      public bool enermyInRangeUp = false;
      public bool enermyInRangeDown = false;

      public Vector2 markNearestEnermyUp;
      public Vector2 markNearestEnermyDown;
      public Vector2 markNearestEnermyLeft;
      public Vector2 markNearestEnermyRight;
      public int NearestEnermyDistance = -1;


      public bool activeBerserkMode = false;
      public bool berserkAtkUp = true;



      //        0 1 2 3 4 5 6 7 8 9 10   i line
      //    4  [-|-|-|-|-|-|-|-|-|-|-]
      //    3  [                     ]   array formation
      //    2  [                     ]   
      //    1  [                     ]
      //    0  [-|-|-|-|-|-|-|-|-|-|-]
      //    j line        X  <- robot


      //    j line        X <- robot
      //    0  [-|-|-|-|-|-|-|-|-|-|-]
      //    1  [                     ]   array formation
      //    2  [                     ]   
      //    3  [                     ]
      //    4  [-|-|-|-|-|-|-|-|-|-|-]
      //        0 1 2 3 4 5 6 7 8 9 10   i line   

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


      public char BerserkAttack()
      {

         //if (berserkAtkUp)
         //{
         //   berserkAtkUp = !berserkAtkUp;
         //   return 'u';
         //}
         //else
         //   return 'd';

         if (UnityEngine.Random.Range(0,2) == 0)
         {
           // berserkAtkUp = !berserkAtkUp;
            return 'u';
         }
         else
            return 'd';


      }

      public char BerserkWalk()
      {

         //if (subTurnCount >= 4)
         //{
         //   if (subTurnCount >= 6)
         //      return 'l';
         //   else
         //      return 'r';
         //}
         //return '0';
		if (UnityEngine.Random.Range(0,2) == 0)
         {
				if (UnityEngine.Random.Range(0,2) == 0)
                        return 'l';       
            else
               return 'r';
          
       
         }
         else
            return '0';
      }


      public CRobotBon(CRobot s)
         : base()
      {

      }//end Consturctor

      private void resetMarkNearestEnermy()
      {
         markNearestEnermyUp = new Vector2(0, 0);
         markNearestEnermyDown = new Vector2(0, 0);
         markNearestEnermyLeft = new Vector2(0, 0);
         markNearestEnermyRight = new Vector2(0, 0);

      }//end resetMarkNearestEnermy

      private int MarkNearestEnermy(char direction)
      {
         resetMarkNearestEnermy();
         int distance = -1;
         int minDistance = -9999;
         int i2 = 0;
         if (EnermyFoundInRadar(direction) > 0)
         {
            switch (direction)
            {
               case 'u':
                  for (int i = 0; i < 11; i++)
                  {
                     for (int j = 0; j < 5; j++)
                     {
                        if (myRadar.Up.infoReceived[i, j] == 'r')
                        {
                           if (i > 5)
                              i2 = i - 5;
                           else
                              i2 = (-1) * (i - 5);
                           distance = j + i2;
                           if (minDistance <= distance)
                           {
                              minDistance = distance;
                              NearestEnermyDistance = minDistance;
                              markNearestEnermyUp = new Vector2(i, j);
                           }//end if minDistance

                        }//end if
                     }
                  }//end for
                  return NearestEnermyDistance;


               case 'd':
                  for (int i = 0; i < 11; i++)
                  {
                     for (int j = 0; j < 5; j++)
                     {
                        if (myRadar.Down.infoReceived[i, j] == 'r')
                        {
                           if (i > 5)
                              i2 = i - 5;
                           else
                              i2 = (-1) * (i - 5);
                           distance = j + i2;
                           if (minDistance <= distance)
                           {
                              minDistance = distance;
                              NearestEnermyDistance = minDistance;
                              markNearestEnermyDown = new Vector2(i, j);
                           }//end if minDistance

                        }//end if
                     }
                  }//end for
                  return NearestEnermyDistance;

               case 'l':
                  for (int i = 0; i < 11; i++)
                  {
                     for (int j = 0; j < 5; j++)
                     {
                        if (myRadar.Left.infoReceived[i, j] == 'r')
                        {
                           if (i > 5)
                              i2 = i - 5;
                           else
                              i2 = (-1) * (i - 5);
                           distance = j + i2;
                           if (minDistance <= distance)
                           {
                              minDistance = distance;
                              NearestEnermyDistance = minDistance;
                              markNearestEnermyLeft = new Vector2(i, j);
                           }//end if minDistance

                        }//end if
                     }
                  }//end for
                  return NearestEnermyDistance;

               case 'r':
                  for (int i = 0; i < 11; i++)
                  {
                     for (int j = 0; j < 5; j++)
                     {
                        if (myRadar.Right.infoReceived[i, j] == 'r')
                        {
                           if (i > 5)
                              i2 = i - 5;
                           else
                              i2 = (-1) * (i - 5);

                           distance = j + i2;

                           if (minDistance <= distance)
                           {
                              minDistance = distance;
                              NearestEnermyDistance = minDistance;
                              markNearestEnermyRight = new Vector2(i, j);
                           }//end if minDistance

                        }//end if
                     }
                  }//end for
                  return NearestEnermyDistance;

               default:
                  return NearestEnermyDistance;

            }//end switch                  
         }//end if
         return distance;
      }//end MarkNearestEnermy

      private char walkToNearestEnermy()
      {

         if (subTurnCount >= 2)
         {
            if (enermyInRangeRight)
               return walkToNearestEnermy('r');
            else if (enermyInRangeLeft)
               return walkToNearestEnermy('l');
         }
         return '0';
      }

      private char walkToNearestEnermy(char direction)
      {
         if (EnermyFoundInRadar(direction) > 0 && NearestEnermyDistance != 0)
         {
            switch (direction)
            {
               case 'u':


                  break;

               case 'l': if (markNearestEnermyLeft.y >= 0)
                  {
                     markNearestEnermyLeft.y--;
                     NearestEnermyDistance--;
                     return 'l';
                  }
                  else if (markNearestEnermyLeft.x != 5)
                  {
                     if (markNearestEnermyLeft.x < 5)
                     {
                        markNearestEnermyLeft.x++;
                        NearestEnermyDistance--;
                        enermyInRangeUp = true;
                        return 'u';
                     }
                     else
                     {
                        markNearestEnermyLeft.x--;
                        NearestEnermyDistance--;
                        enermyInRangeDown = true;
                        return 'd';
                     }

                  }

                  break;

               case 'r': if (markNearestEnermyRight.y >= 0)
                  {
                     markNearestEnermyRight.y--;
                     NearestEnermyDistance--;
                     return 'r';
                  }
                  else if (markNearestEnermyRight.x != 5)
                  {
                     if (markNearestEnermyRight.x < 5)
                     {
                        markNearestEnermyRight.x++;
                        NearestEnermyDistance--;
                        enermyInRangeUp = true;
                        return 'u';
                     }
                     else
                     {
                        markNearestEnermyRight.x--;
                        NearestEnermyDistance--;
                        enermyInRangeDown = true;
                        return 'd';
                     }

                  }

                  break;


               default:
                  break;
            }//end switch                  
         }//end if
         return '0';
      }//end walkToNearestEnermy

      public int EnermyFoundInRadar(char direction)
      {
         int count = 0;

         if (direction == 'u')
         {
            for (int i = 0; i < 11; i++)
            {
               for (int j = 0; j < 5; j++)
               {
                  if (myRadar.Up.infoReceived[i, j] == 'r')
                     count++;
               }
            }//end for
         }
         else if (direction == 'd')
         {
            for (int i = 0; i < 11; i++)
            {
               for (int j = 0; j < 5; j++)
               {
                  if (myRadar.Down.infoReceived[i, j] == 'r')
                     count++;
               }
            }//end for
         }
         else if (direction == 'l')
         {
            for (int i = 0; i < 11; i++)
            {
               for (int j = 0; j < 5; j++)
               {
                  if (myRadar.Left.infoReceived[i, j] == 'r')
                     count++;
               }
            }//end for
         }
         else if (direction == 'r')
         {
            for (int i = 0; i < 11; i++)
            {
               for (int j = 0; j < 5; j++)
               {
                  if (myRadar.Right.infoReceived[i, j] == 'r')
                     count++;
               }
            }//end for
         }//end if else

         return count;
      }//end EnermyFoundInRadar

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

      private char AttackNearestEnermy()
      {
         if (NearestEnermyDistance == 0)
         {
            if (enermyInRangeUp)
               return 'u';
            if (enermyInRangeDown)
               return 'd';
            if (enermyInRangeLeft)
               return 'l';
            if (enermyInRangeRight)
               return 'r';

         }
         return '0';
      }//end AttackNearestEnermy

      public char TurnRadar4Direction()
      {

         if (radarDirection == 'u' && radarDirection != 'd')
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



   }//end CRobotBon
}//end namespace
