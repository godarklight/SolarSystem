using System;
using System.Collections.Generic;
using System.IO;

namespace Integration
{
    class Program
    {
        static void Main(string[] args)
        {
            //resolution
            double TIMESTEP = 50;
            //180 days
            double INTEGRATETIME = 86400 * 180;

            List<Body> bodies = new List<Body>();
            for (int i = 0; i < 5; i++)
            {
                bodies.Add(new Body(bodies));
            }
            //Epoch is 2021-6-9 0:0:0 http://vo.imcce.fr/webservices/miriade/?forms
            //Sun
            bodies[0].mass = 1.98847e30;

            //Mercury
            bodies[1].mass = 3.301e23;
            bodies[1].state.pos.X = -0.1249124417841 * Constants.au2meters;
            bodies[1].state.pos.Y = -0.4017410392559 * Constants.au2meters;
            bodies[1].state.pos.Z = -0.2016607832270 * Constants.au2meters;
            bodies[1].state.vel.X = 0.0214555677899 * Constants.au2meters / 86400d;
            bodies[1].state.vel.Y = -0.0046468755583 * Constants.au2meters / 86400d;
            bodies[1].state.vel.Z = -0.0047062643884 * Constants.au2meters / 86400d;

            //Venus
            bodies[2].mass = 4.867e24;
            bodies[2].state.pos.X = -0.4191477048837 * Constants.au2meters;
            bodies[2].state.pos.Y = 0.5217402287929 * Constants.au2meters;
            bodies[2].state.pos.Z = 0.2612793341755 * Constants.au2meters;
            bodies[2].state.vel.X = -0.0164884545514 * Constants.au2meters / 86400d;
            bodies[2].state.vel.Y = -0.0112530310070 * Constants.au2meters / 86400d;
            bodies[2].state.vel.Z = -0.0040200953526 * Constants.au2meters / 86400d;

            //Earth
            bodies[3].mass = 5.972e24;
            bodies[3].state.pos.X = -0.2092285002542 * Constants.au2meters;
            bodies[3].state.pos.Y = -0.9114006127413 * Constants.au2meters;
            bodies[3].state.pos.Z = -0.3950880057285 * Constants.au2meters;
            bodies[3].state.vel.X = 0.0165609155055 * Constants.au2meters / 86400d;
            bodies[3].state.vel.Y = -0.0033149089271 * Constants.au2meters / 86400d;
            bodies[3].state.vel.Z = -0.0014377348578 * Constants.au2meters / 86400d;

            //Moon
            bodies[4].mass = 7.348e22;
            bodies[4].state.pos.X = -0.2079659007699 * Constants.au2meters;
            bodies[4].state.pos.Y = -0.9091829576083 * Constants.au2meters;
            bodies[4].state.pos.Z = -0.3941658244831 * Constants.au2meters;
            bodies[4].state.vel.X = 0.0160643537580 * Constants.au2meters / 86400d;
            bodies[4].state.vel.Y = -0.0030997267805 * Constants.au2meters / 86400d;
            bodies[4].state.vel.Z = -0.0012885664780 * Constants.au2meters / 86400d;



            double timeLeft = INTEGRATETIME;


            File.Delete("output.txt");
            using (StreamWriter sw = new StreamWriter("output.txt"))
            {
                while (timeLeft > 0)
                {
                    double deltaTime = TIMESTEP;
                    if (deltaTime > timeLeft)
                    {
                        deltaTime = timeLeft;
                    }
                    timeLeft -= deltaTime;

                    foreach (Body body in bodies)
                    {
                        body.Integrate(deltaTime);
                    }

                    foreach (Body body in bodies)
                    {
                        body.Apply();
                    }

                    string start = "";
                    foreach (Body body in bodies)
                    {
                        sw.Write($"{start}{body.state.pos.X} {body.state.pos.Y} {body.state.pos.Z}");
                        start = " ";
                    }
                    Vector3d earthToMoon = bodies[4].state.pos - bodies[3].state.pos;
                    sw.Write($" {earthToMoon.X} {earthToMoon.Y} {earthToMoon.Z}\n");
                    sw.WriteLine();
                }
            }
        }

        public static void SanityCheck(List<Body> massPoints)
        {
            //Sanity checking
            //Acceleration on earth
            //Console.WriteLine(Calculator.GetAccelerationExcludingBody(new Vector3d(0, 0, 6371000), massPoints, null));
            //Acceleration on moon
            //Console.WriteLine(Calculator.GetAccelerationExcludingBody(massPoints[1].point.pos + new Vector3d(0, 0, 1738000), massPoints, null));
            //Distance from earth to moon 384,400km
            //Console.WriteLine(massPoints[1].point.pos.length);
            //Moon orbital velocity ~900m/s
            //Console.WriteLine(massPoints[1].point.vel.length);
        }

        public static void SanityCheck2()
        {
            //Sanity check 2
            StateVector sv = new StateVector();
            sv.pos = new Vector3d(1, 1, 1);
            sv.vel = new Vector3d(1, 1, 1);
            Func<double, StateVector, StateVector> accelerate = (double time, StateVector initial) => { StateVector retSv = new StateVector(); retSv.pos = initial.vel; retSv.vel = new Vector3d(1, 1, 1); return retSv; };
            sv = Integrator<StateVector>.Integrate(accelerate, 1, sv);
            Console.WriteLine(sv.pos);
            Console.WriteLine(sv.vel);
        }

        public static void SanityCheck3()
        {
            //Sanity check 2
            Vector3d sv = new Vector3d(1, 0, 0);
            Func<double, Vector3d, Vector3d> tanPlus1 = (double time, Vector3d initial) => {
                return new Vector3d(Math.Tan(initial.X) + 1, 0, 0);
            };
            Console.WriteLine(sv.X);
            sv = Integrator<Vector3d>.Integrate(tanPlus1, 0.025, sv);
            Console.WriteLine(sv.X);
            sv = Integrator<Vector3d>.Integrate(tanPlus1, 0.025, sv);
            Console.WriteLine(sv.X);
            sv = Integrator<Vector3d>.Integrate(tanPlus1, 0.025, sv);
            Console.WriteLine(sv.X);
            sv = Integrator<Vector3d>.Integrate(tanPlus1, 0.025, sv);
            Console.WriteLine(sv.X);
        }
    }
}
