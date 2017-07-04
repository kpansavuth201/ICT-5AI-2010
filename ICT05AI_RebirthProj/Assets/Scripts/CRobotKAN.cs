using System;
using System.Collections.Generic;



namespace EngineMidterm_Kanon
{
   class CRobotKAN : CRobot
   {
      //My Attribute
      public char radarDirection = 'l';
      public char walkDirection = 'l';
      public char attackDirection = 'l';
      public int upp = 30;
      public int dop = 30;
      public int lep = 30;
      public int rip = 30;
      public int formation = 0;//0=no e in range 1=have e in range 
      public int random = 0;
      public int radarCount = 4;
      public int en = 8;

      public System.Random ran = new Random();


      public char genMove()
      {

         for (int i = 0; i < 11; i++)
         {
            for (int j = 0; j < 5; j++)
            {
               if (myRadar.Up.infoReceived[i, j] == 'r')
               {
                  upp += 10;
               }
               if (myRadar.Up.infoReceived[i, j] == 'w')
               {
                  upp--;
               }
               if (myRadar.Down.infoReceived[i, j] == 'r')
               {
                  dop += 10;
               }
               if (myRadar.Down.infoReceived[i, j] == 'w')
               {
                  dop--;
               }
               if (myRadar.Left.infoReceived[i, j] == 'r')
               {
                  lep += 10;
               }
               if (myRadar.Left.infoReceived[i, j] == 'w')
               {
                  lep--;
               }
               if (myRadar.Right.infoReceived[i, j] == 'r')
               {
                  rip += 10;
               }
               if (myRadar.Right.infoReceived[i, j] == 'w')
               {
                  rip--;
               }

            }
         }
         random = ran.Next((upp + dop + lep + rip) + 1);
         if (random < upp)
         {
            walkDirection = 'u';
         }
         else if (random < upp + dop)
         {
            walkDirection = 'd';
         }
         else if (random < upp + dop + lep)
         {
            walkDirection = 'l';
         }
         else if (random < upp + dop + lep + rip)
         {
            walkDirection = 'r';
         }

         attackDirection = walkDirection;

         return walkDirection;
      }

      public CRobotKAN(CRobot s)
         : base()
      {
      }//end Consturctor

      public override void Update()
      {
      }//end Update

      public override char ThinkRadar()
      {
         if (en > 3)
         {
            if (radarCount > 0)
            {
               radarCount--;
               en--;
               return TurnRadar4Direction();
            }
         }
         else
         {
            return 'p';
         }
         return '0';

      }//end ThinkRadar


      public override char ThinkWalk()
      {
         //return MoveUpOnly();
         if (en > 3)
         {
            if (radarCount == 0)
            {
               en--;
               return genMove();
            }
         }
         else
         {
            return 'p';
         }
         return '0';

      }//end ThinkWalk

      public override char ThinkAttack()
      {
         return attackDirection;
      }//end ThinkAttack


      public override void CleanUp()
      {
         upp = 30;
         dop = 30;
         lep = 30;
         rip = 30;
         en = 8;
         radarCount = 4;

      }//end CleanUp

      private char MoveUpOnly()
      {

         return 'u';
      }//end MoveUpOnly

      private char radarAround()
      {
         if (radarDirection == 'u')
         { radarDirection = 'l'; }
         if (radarDirection == 'l')
         { radarDirection = 'd'; }
         if (radarDirection == 'd')
         { radarDirection = 'r'; }
         if (radarDirection == 'r')
         { radarDirection = 'u'; }
         return radarDirection;
      }

      private char TurnRadar4Direction()
      {
         if (energy >= 1)
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
         }

         return radarDirection;

      }//TurnRadar4Direction

   }//end CRobotRFC
}//end namespace
