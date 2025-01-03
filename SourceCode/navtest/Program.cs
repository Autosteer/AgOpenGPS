
using System;
Console.WriteLine(1);
namespace navtest
{
    public class CSim// клас для симуляції руху трактора // a class for simulating the movement of a tractor
    {
        //private readonly FormGPS mf; // зміна для обєкту форма, яка ініціалізується в класі CSim // change for the shape object that is initialized in the CSim class

        #region properties sim

        public double altitude = 300;// висота для симуляції не змінюється // height for simulation does not change
        public double startlatitude=0, startlongitude=0; // початкові координати // initial coordinates
        public double latitude, longitude; //  координати //  coordinates

                                                                                        // tractor course?, simulation step, angle of rotation, change to delay the angle of rotation (actuator model)
        public double headingTrue, stepDistance = 0.0, steerAngle, steerangleAve = 0.0; // курс трактора?, крок симуляції, кут повроту, зміна для затримки кута поворота (модель виконавчого механізму)
        public double steerAngleScrollBar = 0;// зміна для ручного вводу кута повороту // change for manual input of rotation angle

        public bool isAccelForward, isAccelBack;// напрям руху // direction

        #endregion properties sim 
               public CSim()
                {
                    
                    latitude = startlatitude;
                    longitude = startlongitude;
                }

        //public CSim(FormGPS _f)
        //{
        //   mf = _f;
        //    latitude = Properties.Settings.Default.setGPS_SimLatitude;
        //    longitude = Properties.Settings.Default.setGPS_SimLongitude;
        //}

       
                       public void DoSimTick(double _st)
                       {
                           steerAngle = _st;

                           double diff = Math.Abs(steerAngle - steerangleAve);

                           if (diff > 11)
                           {
                               if (steerangleAve >= steerAngle)
                               {
                                   steerangleAve -= 6;
                               }
                               else steerangleAve += 6;
                           }
                           else if (diff > 5)
                           {
                               if (steerangleAve >= steerAngle)
                               {
                                   steerangleAve -= 2;
                               }
                               else steerangleAve += 2;
                           }
                           else if (diff > 1)
                           {
                               if (steerangleAve >= steerAngle)
                               {
                                   steerangleAve -= 0.5;
                               }
                               else steerangleAve += 0.5;
                           }
                           else
                           {
                               steerangleAve = steerAngle;
                           }

                           //mf.mc.actualSteerAngleDegrees = steerangleAve;// вивід в зовнішню програму

                           double temp = stepDistance * Math.Tan(steerangleAve * 0.0165329252) / 2;
                           headingTrue += temp;
                           if (headingTrue > glm.twoPI) headingTrue -= glm.twoPI;
                           if (headingTrue < 0) headingTrue += glm.twoPI;

            // mf.pn.vtgSpeed = Math.Abs(Math.Round(4 * stepDistance * 10, 2)); // розрахунок швидкості в зовнішню програму 
            //mf.pn.AverageTheSpeed();// зовнішній метод класу nmea усереднення швидкості !!!залежить від класу CNMEA!!!

            //Calculate the next Lat Long based on heading and distance // Метод для обчислення нових координат за курсом та дистанцією !!!залежить від класу NMEA!!!
            //CalculateNewPostionFromBearingDistance(glm.toRadians(latitude), glm.toRadians(longitude), headingTrue, stepDistance / 1000.0);

          //  mf.pn.ConvertWGS84ToLocal(latitude, longitude, out mf.pn.fix.northing, out mf.pn.fix.easting); // !!! CNMEA!!!

                          // mf.pn.headingTrue = mf.pn.headingTrueDual = glm.toDegrees(headingTrue); // виводимо курс
                         //  mf.ahrs.imuHeading = mf.pn.headingTrue;
                         //  if (mf.ahrs.imuHeading >= 360) mf.ahrs.imuHeading -= 360; // відслідковуємо перехід через нуль

                         //  mf.pn.latitude = latitude; // виводимо нові координати
                        //   mf.pn.longitude = longitude;

                        //   mf.pn.hdop = 0.7; // якість сигналу

                           //temp = Math.Abs(mf.pn.latitude * 100);// тут якимось чином моделюємо висоту і виводимо її 
                           //temp -= ((int)(temp));
                           //temp *= 100;
                           //mf.pn.altitude = temp + 200;

                           //temp = Math.Abs(mf.pn.longitude * 100); // аналогічно
                           //temp -= ((int)(temp));
                           //temp *= 100;
                           //mf.pn.altitude += temp;

                           // mf.pn.satellitesTracked = 12;// кількість супутників

                           //mf.sentenceCounter = 0;// ??
                          // mf.UpdateFixPosition(); // запускаємо автопілот

                           if (isAccelForward)// напрям руху задається ззовні та розрахунок кроку 
                           {
                               isAccelBack = false;
                               stepDistance += 0.02;
                               if (stepDistance > 0.12) isAccelForward = false;
                           }

                           if (isAccelBack)
                           {
                               isAccelForward = false;
                               stepDistance -= 0.01;
                               if (stepDistance < -0.06) isAccelBack = false;
                           }
                       }
        /*
                      public void CalculateNewPostionFromBearingDistance(double lat, double lng, double bearing, double distance)
                      {
                          double R = distance / 6371.0; // Earth Radius in Km

                          double lat2 = Math.Asin((Math.Sin(lat) * Math.Cos(R)) + (Math.Cos(lat) * Math.Sin(R) * Math.Cos(bearing)));
                          double lon2 = lng + Math.Atan2(Math.Sin(bearing) * Math.Sin(R) * Math.Cos(lat), Math.Cos(R) - (Math.Sin(lat) * Math.Sin(lat2)));

                          latitude = glm.toDegrees(lat2);
                          longitude = glm.toDegrees(lon2);
                      }*/
    }
}