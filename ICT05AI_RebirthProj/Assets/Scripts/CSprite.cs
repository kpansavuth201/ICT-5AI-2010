using System;
using System.Collections.Generic;
using UnityEngine;
namespace EngineMidterm_Kanon
{
   class CSprite
   {
      
	  private static float _ratio;
	  private static bool isRatioSet = false;
      private static float ratio
      {
      	get{
				if(!isRatioSet)
				{
					_ratio = Screen.height * 0.002f;
					isRatioSet = true;
				}
				return _ratio;
      	}
      }
      public Rect drawRect;
  
      //Attribute
      //Scale Rotate Position
      public Vector2 objectCenter;
      public int spriteWidth;
      public int spriteHeight;

      public Vector2 objectPosition;
      public float objectRotation;
      public float objectScale;

      //Texture
      public Texture2D objectTexture;
      public float objectAlpha;
      public int objectVisible;


      //Physics
      public Vector2 objectVelocity;
      public Vector2 toObject;
      public float toAngle;

      //Control
      public int down;
      public int up;
      public int left;
      public int right;
      public int selected;
      public int objectAvailable;

      //Util
      public float[] delay;
      public int maxDelay;


      //Constructor
      public CSprite(Texture2D MainTexture)
      {
      
         //Texture
         objectTexture = MainTexture;
         objectAlpha = 255;
         objectVisible = 1;

         //Scale Rotate Position
         spriteWidth = objectTexture.width / 2;
         spriteHeight = objectTexture.height / 2;
         objectCenter = Vector2.zero;
         //objectCenter = new Vector2(spriteWidth, spriteHeight); //optional, comment to toggle off
         objectPosition = Vector2.zero;
         objectRotation = 0.0f;
         objectScale = 1.0f;

         //Physics
         objectVelocity = Vector2.zero;
         toObject = objectPosition;

         //Control      
         down = 0;
         up = 0;
         left = 0;
         right = 0;
         selected = 0;
         objectAvailable = 0;

         //Util
         maxDelay = 60;
         delay = new float[maxDelay];
         for (int i = 0; i < maxDelay; i++)
         {
            delay[i] = -1;
         }

      }//end Constructor


      //Constructor for inheritance 
      public CSprite(CSprite s)
      {
         //Texture
         objectTexture = s.objectTexture;
         objectAlpha = 255;
         objectVisible = 1;

         //Scale Rotate Position
         spriteWidth = s.objectTexture.width / 2;
         spriteHeight = s.objectTexture.height / 2;
         objectCenter = Vector2.zero;
         //objectCenter = new Vector2(spriteWidth, spriteHeight); //optional, comment to toggle off
         objectPosition = Vector2.zero;
         objectRotation = 0.0f;
         objectScale = 1.0f;

         //Physics
         objectVelocity = Vector2.zero;
         toObject = objectPosition;

         //Control      
         down = 0;
         up = 0;
         left = 0;
         right = 0;
         selected = 0;
         objectAvailable = 0;

         //Util
         maxDelay = 60;
         delay = new float[maxDelay];
         for (int i = 0; i < maxDelay; i++)
         {
            delay[i] = -1;
         }

      }//end Constructor for inheritance 


      //Draw
      public void Draw()
      {
//         s.Draw(objectTexture, objectPosition, null, Color.White, objectRotation,
//                objectCenter, objectScale, SpriteEffects.None, 0);
			drawRect = new Rect(objectPosition.x * ratio,
			                   objectPosition.y *ratio,
			                   objectScale * objectTexture.width * ratio ,
			                   objectScale* objectTexture.height * ratio);
			GUI.DrawTexture(drawRect,objectTexture);
      }//end Draw

/*
      //Physics Method

      public float objectDirection()
      {
         return ((float)Math.Atan2(this.objectVelocity.y, this.objectVelocity.x));
      }//end objectDirection

      public float objectSpeed()
      {
         return
             (
                 (float)Math.Sqrt(Math.Pow(Math.Cos(this.objectDirection()) * this.objectVelocity.x, 2)
                                   + Math.Pow(Math.Sin(this.objectDirection()) * this.objectVelocity.y, 2))
             );
      }//end objectSpeed

      public float distanceToObject(Vector2 Position)
      {
         return ((float)Math.Sqrt(Math.Pow((this.objectPosition.x - Position.x), 2)
                 + Math.Pow((this.objectPosition.y - Position.y), 2)));
      }//end distanceToObject

      public float distanceFrom(Vector2 Position, Vector2 Position2)
      {
         return ((float)Math.Sqrt(Math.Pow((Position2.x - Position.x), 2) + Math.Pow((Position2 - Position.y), 2)));
      }//end distancefrom

      public float distanceToObject(double x, double y)
      {
         return ((float)Math.Sqrt(Math.Pow((this.objectPosition.x - x), 2) + Math.Pow((this.objectPosition.y - y), 2)));
      }//end distanceToObject

      public float angleToObject(Vector2 Position)
      {
         //center at center of texture
         //return ((float)Math.Atan2((Position.Y - this.objectPosition.Y+this.objectTexture.Height/2), 
         //         (Position.X - this.objectPosition.X+this.objectTexture.Width/2)));

         //center at up left corner
         return ((float)Math.Atan2((Position.y - this.objectPosition.y), (Position.x - this.objectPosition.x)));

      }//end angleToObject

      public void goTo(float x, float y, float speed)
      {
         float xw, yw;
         if (distanceToObject(x, y) > this.objectSpeed())
         {
            xw = (x - this.objectPosition.X) / distanceToObject(x, y);
            yw = (y - this.objectPosition.Y) / distanceToObject(x, y);
            this.objectVelocity.X = (float)Math.Cos(this.objectDirection()) * xw * speed;
            this.objectVelocity.Y = (float)Math.Sin(this.objectDirection()) * yw * speed;
         }
      }//end goTo

      public void goToAngle(float angle, float speed)
      {
         float x, y;
         x = (float)Math.Cos(angle);
         y = (float)Math.Sin(angle);
         this.objectVelocity.X = (float)(x * speed);
         this.objectVelocity.Y = (float)(y * speed);

      }//end goToAngle

      public Vector2 goToAnglexy(double angle)
      {
         Vector2 w;
         w.X = (float)Math.Cos(angle) * 50;
         w.Y = (float)Math.Sin(angle) * 50;
         return (w);
      }//end goToAnglexy

      public void gravity(float plus, float angle, float max)
      {
         if (this.objectSpeed() < max)
         {
            this.objectVelocity.Y += (float)Math.Sin(angle * MathHelper.Pi / 180) * plus;
            if (angle == 90 || angle == -90)
            {
               this.objectVelocity.X += 0;
            }
            else
            {
               this.objectVelocity.X += (float)Math.Cos(angle * MathHelper.Pi / 180) * plus;
            }
         }
      }//end gravity


      //Collision

      public bool colliSionSqr(CSprite a) //origin at coner
      {
         if (this.objectPosition.X < a.objectPosition.X + a.objectTexture.Width)
         {
            if (this.objectPosition.X + this.objectTexture.Width > a.objectPosition.X)
            {
               if (this.objectPosition.Y < a.objectPosition.Y + a.objectTexture.Height)
               {
                  if (this.objectPosition.Y + this.objectTexture.Height > a.objectPosition.Y)
                  {
                     return (true);
                  }
               }
            }
         }
         return (false);
      }//end colliSionSqr

      public bool colliSionSqr_percent(CSprite a, float percent, float percent2)
      {
         if (this.objectPosition.X - this.objectTexture.Width * percent / 100 < a.objectPosition.X + a.objectTexture.Width * percent2 / 100)
         {
            if (this.objectPosition.X + this.objectTexture.Width * percent / 100 > a.objectPosition.X - a.objectTexture.Width * percent2 / 100)
            {
               if (this.objectPosition.Y - this.objectTexture.Height * percent / 100 < a.objectPosition.Y + a.objectTexture.Height * percent2 / 100)
               {
                  if (this.objectPosition.Y + this.objectTexture.Height * percent / 100 > a.objectPosition.Y - a.objectTexture.Height * percent2 / 100)
                  {
                     return (true);
                  }
               }
            }
         }
         return (false);
      }//end colliSionSqr_percent

      public bool colliSionD(CSprite a)
      {
         if (this.objectPosition.X < a.objectPosition.X + a.objectTexture.Width - 5)
         {
            if (this.objectPosition.X + this.objectTexture.Width > a.objectPosition.X + 5)
            {
               if (this.objectPosition.Y + this.objectTexture.Height + this.objectVelocity.Y >= a.objectPosition.Y)
               {
                  if (this.objectPosition.Y + this.objectTexture.Height < a.objectPosition.Y + 5)
                  {
                     return (true);
                  }
               }
            }
         }
         return (false);
      }//end colliSionD

      public bool colliSionR(CSprite a)
      {
         if (this.objectPosition.Y < a.objectPosition.Y + a.objectTexture.Height - 5)
         {
            if (this.objectPosition.Y + this.objectTexture.Height > a.objectPosition.Y + 5)
            {
               if (this.objectPosition.X + this.objectTexture.Width > a.objectPosition.X - this.objectVelocity.X)
               {
                  if (this.objectPosition.X + this.objectTexture.Width < a.objectPosition.X + a.objectTexture.Width)
                  {
                     return (true);
                  }
               }
            }
         }
         return (false);
      }//end colliSionR

      public bool colliSionL(CSprite a)
      {
         if (this.objectPosition.Y < a.objectPosition.Y + a.objectTexture.Height - 5)
         {
            if (this.objectPosition.Y + this.objectTexture.Height > a.objectPosition.Y + 5)
            {
               if (this.objectPosition.X > a.objectPosition.X - this.objectVelocity.X)
               {
                  if (this.objectPosition.X < a.objectPosition.X + a.objectTexture.Width)
                  {
                     return (true);
                  }
               }
            }
         }
         return (false);
      }//end colliSionL

      public void fixColliSionD(float a)
      {
         if (this.objectPosition.Y + this.objectTexture.Height + this.objectVelocity.Y >= a)
         {
            if (this.objectPosition.Y < a)
            {
               this.objectVelocity.Y = 0;
               this.objectPosition.Y = a - this.objectTexture.Height;
            }
         }
      }//end fixColliSionD

      public void fixColliSionU(float a)
      {
         if (this.objectPosition.Y + this.objectVelocity.Y < a)
         {
            this.objectVelocity.Y = 0;
            this.objectPosition.Y += a - this.objectPosition.Y;
         }
      }//end fixColliSionU 

      public void fixColliSionL(float a)
      {
         if (this.objectPosition.X + this.objectTexture.Width + this.objectVelocity.X > a)
         {
            this.objectVelocity.X = 0;
            this.objectPosition.X -= this.objectPosition.X + this.objectTexture.Width - a;
         }
      }//end fixColliSionL

      public void fixColliSionR(float a)
      {
         if (this.objectPosition.X + this.objectVelocity.X < a)
         {
            this.objectVelocity.X = 0;
            this.objectPosition.X -= this.objectPosition.X + this.objectTexture.Width - a;
         }
      }//end fixColliSionR


      // Mouse event 

      //---------------Mouse rollover----------------
      public bool Mouse_Rollover(MouseState m)
      {
         // Check if two sprites collide
         if (this.objectPosition.X + this.objectTexture.Width > m.X &&
         this.objectPosition.X < m.X &&
         this.objectPosition.Y + this.objectTexture.Height > m.Y &&
         this.objectPosition.Y < m.Y)
            return true;
         else
            return false;
      }

      //---------------Mouse left Press----------------
      public bool Mouse_Left_Press(MouseState m)
      {
         // Check if two sprites collide
         if (this.Mouse_Rollover(m) && (m.LeftButton == ButtonState.Pressed))
            return true;
         else
            return false;
      }

      //---------------Mouse left Released----------------
      public bool Mouse_Left_Released(MouseState m)
      {
         // Check if two sprites collide
         if (this.Mouse_Rollover(m) && (m.LeftButton == ButtonState.Released))
            return true;
         else
            return false;
      }

      //---------------Mouse left click----------------
      public bool Mouse_Left_Click(MouseState m, int _is_click)
      {
         // Check if two sprites collide
         if (this.Mouse_Rollover(m) && (m.LeftButton == ButtonState.Released) && _is_click == 1)
            return true;
         else
            return false;
      }

      //---------------Mouse right Press----------------
      public bool Mouse_Right_Press(MouseState m)
      {
         // Check if two sprites collide
         if (this.Mouse_Rollover(m) && (m.RightButton == ButtonState.Pressed))
            return true;
         else
            return false;
      }

      //---------------Mouse right Released----------------
      public bool Mouse_Right_Released(MouseState m)
      {
         // Check if two sprites collide
         if (this.Mouse_Rollover(m) && (m.RightButton == ButtonState.Released))
            return true;
         else
            return false;
      }
*/

   }//end class CSprite
}//end namespace
