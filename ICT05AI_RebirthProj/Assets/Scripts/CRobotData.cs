using System;
using System.Collections.Generic;
using UnityEngine;

namespace EngineMidterm_Kanon
{
   class CRobotData
   {
      //Attribute
      //Identify

      int id;
      int teamId;

      bool isDead;

      //Resource
      int hp;
      int energy;
      int walkStepCount;
      Vector2 position;
      
      //u for up
      //d for down
      //l for left
      //r for right

      //Status
      
      
      int AttackDamage;
      int Defence;
      int WalkRange;
      int RadarRange;
      int EnergyCapacity;

      //Status experience
      //Robot can increse level of status by
      //get status experience full
      int StatusPointRemain;
      public int[] experienceGuage;
      public int[] experienceGuageMax;
      public int[] StatusMaxLevel;


      int score;
      int dummy;

      //Constructor
      public CRobotData(int _id, int _teamId)
      {
         id = _id;
         teamId = _teamId;
      }//end Constructor

      public void Init()
      {
         isDead = false;

         hp = 20;
         energy = 8;

         AttackDamage = 3;
         Defence = 0;
         WalkRange = 3;         
         RadarRange = 3;
         EnergyCapacity = 8;

         StatusPointRemain = 0;
         walkStepCount = 0;

         experienceGuage = new int[6];
         experienceGuageMax = new int[6];
         StatusMaxLevel = new int[6];
         for (int i = 0; i < 6; i++)
         {
            experienceGuage[i] = 0;
         }
         //Set experience Guage
         experienceGuageMax[0] = 30; //AttackDamage
         experienceGuageMax[1] = 25; //Defence
         experienceGuageMax[2] = 10;  //WalkRange
         experienceGuageMax[3] = 5;  //RadarRange
         experienceGuageMax[4] = 40; //EnergyCapacity
         experienceGuageMax[5] = 100;

         StatusMaxLevel[0] = 5;
         StatusMaxLevel[1] = 2;
         StatusMaxLevel[2] = 6;
         StatusMaxLevel[3] = 5;
         StatusMaxLevel[4] = 12;
         StatusMaxLevel[5] = 1;


         score = 0;
         dummy = 0;
      }//end

      public int GetId()
      {
         return id;
      }//end GetId

      public int GetInfo(int whatInfo)
      {
         switch (whatInfo)
         {
            case 1: return hp;

            case 2: return energy;

            case 3: return Defence;

            case 4: return WalkRange;

            case 5: return AttackDamage;

            case 6: return RadarRange;

            case 7: return EnergyCapacity;

            case 8: return StatusPointRemain;

            case 9: return walkStepCount;

            case 10: return score;

            case 11: return dummy;

            default: return -1;

         }//end switch

      }//end GetInfo

      public int GetInfo(String whatInfo)
      {
          switch (whatInfo)
          {
              case "hp": return hp;

              case "energy": return energy;

              case "Defence": return Defence;

              case "WalkRange": return WalkRange;

              case "AttackDamage": return AttackDamage;

              case "RadarRange": return RadarRange;

              case "EnergyCapacity": return EnergyCapacity;

              case "StatusPointRemain": return StatusPointRemain;

              case "walkStepCount": return walkStepCount;

              case "score": return score;

              case "dummy": return dummy;

              default: return -1;

          }//end switch

      }//end GetInfo

      public String GetInfoDescription(int whatInfo)
      {
         switch (whatInfo)
         {
            case 1: return "hp";

            case 2: return "energy";

            case 3: return "Defence";

            case 4: return "WalkRange";

            case 5: return "AttackDamage";

            case 6: return "RadarRange";

            case 7: return "EnergyCapacity";

            case 8: return "StatusPointRemain";

            case 9: return "walkStepCount";

            case 10: return "score";

            case 11: return "dummy";



            default: return "";

         }//end switch

      }//end GetInfoDescription

      public bool UseEnergy(int energyConsumed)
      {
         if (energy >= energyConsumed)
         {
            energy -= energyConsumed;
            return true;
         }
         else
         {
            return false;
         }//end if else

      }//end UseEnergy

      public void plusScore(int _score)
      {
         score += _score;
      }//end plusScore

      public bool Attacked(int damage)
      {
         damage -= Defence; // reduce damage by defence

         if (damage > hp)
         {
            hp = 0;
         }
         else
         {
            hp -= damage;
         }

         if (hp <= 0)
            isDead = true;

         return isDead;
      }//end Attacked

      public void upgradeStatus(int whatStatus)
      {
          if (whatStatus >= 0 && whatStatus <= 4)//check if not out of array index
          {
              switch (whatStatus)
              {
                  case 0: if (AttackDamage < StatusMaxLevel[whatStatus])//check if level is not max
                      {
                          experienceGuage[whatStatus] = 0;
                          AttackDamage++;
                      }
                      break;

                  case 1: if (Defence < StatusMaxLevel[whatStatus])//check if level is not max
                      {
                          experienceGuage[whatStatus] = 0;
                          Defence++;
                      }
                      break;

                  case 2: if (WalkRange < StatusMaxLevel[whatStatus])//check if level is not max
                      {
                          experienceGuage[whatStatus] = 0;
                          WalkRange++;
                      }
                      break;

                  case 3: if (RadarRange < StatusMaxLevel[whatStatus])//check if level is not max
                      {
                          experienceGuage[whatStatus] = 0;
                          RadarRange++;
                      }
                      break;

                  case 4: if (EnergyCapacity < StatusMaxLevel[whatStatus])//check if level is not max
                      {
                          experienceGuage[whatStatus] = 0;
                          EnergyCapacity++;
                      }
                      break;

                  default:
                      break;
              }//end switch
          }//end if

      }//end upgradeStatus

      public bool upgradeExperience(int whatStatus)
      {
          if (whatStatus >= 0 && whatStatus <= 4)//check if not out of array index
          {
              experienceGuage[whatStatus]++;
              if (experienceGuage[whatStatus] >= experienceGuageMax[whatStatus])
              {
                  upgradeStatus(whatStatus);
                  return true;
              }
              else
              {
                  return false;
              }
          }//end if
          return false;
      }//end upgradeStatus

      public void RefillEnergyAndWalkStep()
      {
         energy = EnergyCapacity;
         walkStepCount = 0;
      }//end RefillEnergyAndWalkStep

      public void CountStep()
      {
         walkStepCount++;
      }

      public void SetPosition(int x, int y)
      {
         position.x = x;
         position.y = y;
      }//end 

      public int GetPositionX()
      {
         return (int)position.x;
      }//GetPositionX

      public int GetPositionY()
      {
         return (int)position.y;
      }//GetPositionY

      public bool GetIsDead()
      {
         return isDead;
      }//end GetIsDead

   }//end CRobotData

}//end namespace
