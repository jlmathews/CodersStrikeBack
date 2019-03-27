using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class Point<T>
{
    public T x;
    public T y;

    public Point(T x, T y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }

    public static double Distance(Point<int> pt1, Point<int> pt2)
    {
        double x1 = Math.Pow(pt2.x - pt1.x, 2);
        double y1 = Math.Pow(pt2.y - pt1.y, 2);
        double dist = Math.Sqrt(x1 + y1);

        return dist;
    }

    public static double Distance(Point<double> pt1, Point<double> pt2)
    {
        double x1 = Math.Pow(pt2.x - pt1.x, 2);
        double y1 = Math.Pow(pt2.y - pt1.y, 2);
        double dist = Math.Sqrt(x1 + y1);

        return dist;
    }

}

public class Vector
{
    public Point<double> pt;

    public Vector(double x, double y)
    {
        this.pt = new Point<double>(x, y);
    }

    public Vector(Point<int> pt0, Point<int> pt1)
    {
        this.pt = new Point<double>(pt1.x - pt0.x, pt1.y - pt0.y);
        Console.Error.WriteLine($"Vector: {pt.x}, {pt.y}");
    }

    public Vector(Point<double> pt0, Point<double> pt1)
    {
        this.pt = new Point<double>(pt1.x - pt0.x, pt1.y - pt0.y);
        Console.Error.WriteLine($"Vector: {pt.x}, {pt.y}");
    }

    public double Length()
    {
        double length = Math.Sqrt(Math.Pow(pt.x, 2) + Math.Pow(pt.y, 2));
        Console.Error.WriteLine($"length: {length}");
        return length;
    }

    public Vector Normalize()
    {
        Vector u;
        double x, y, dist;

        dist = Length();
        if (dist != 0)
        {
            x = (double)pt.x / dist;
            y = (double)pt.y / dist;
        }
        else
        {
            x = 0;
            y = 0;
        }

        u = new Vector(x, y);
        Console.Error.WriteLine($"Normalize: {u.pt.x}, {u.pt.y}");

        return u;
    }

    public Point<int> FindPointOnLine(Point<int> pt, int distance)
    {
        Vector u = Normalize();
        return new Point<int>((int)(pt.x + u.pt.x * distance),
            (int)(pt.y + u.pt.y * distance));
    }
}

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    public static int SelectThrust(int nextCheckpointAngle, int distradNextCheckPt)
    {
        int thrust;

        if ((nextCheckpointAngle < 10) && (nextCheckpointAngle > -10))
        {
            thrust = 100;
        }
        else if ((nextCheckpointAngle < 25) && (nextCheckpointAngle > -25))
        {
            thrust = 100;
        }
        else if ((nextCheckpointAngle < 45) && (nextCheckpointAngle > -45))
        {
            thrust = 90;
        }
        else if ((nextCheckpointAngle < 60) && (nextCheckpointAngle > -60))
        {
            thrust = 90;
        }
        else if ((nextCheckpointAngle < 75) && (nextCheckpointAngle > -75))
        {
            thrust = 90;
        }
        else if ((nextCheckpointAngle < 90) && (nextCheckpointAngle > -90))
        {
            thrust = 90;
        }
        else
        {
            thrust = 0;
        }

        int maxThrust;

        if (distradNextCheckPt < 1500)
        {
            maxThrust = 90;
        }
        else if (distradNextCheckPt < 1000)
        {
            maxThrust = 75;
        }
        if (distradNextCheckPt < 500)
        {
            maxThrust = 30;
        }
        else
        {
            maxThrust = 100;
        }

        if (thrust > maxThrust)
        {
            thrust = maxThrust;
        }

        return thrust;
    }

    static void Main(string[] args)
    {
        string[] inputs;

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);
            int nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
            int nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
            int nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
            int nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
            inputs = Console.ReadLine().Split(' ');
            int opponentX = int.Parse(inputs[0]);
            int opponentY = int.Parse(inputs[1]);

            int thrust = 0;
            int nextCheckpointRadius = 600;

            Point<int> racerPt = new Point<int>(x, y);
            Point<int> nextCheckpoint = new Point<int>(nextCheckpointX, nextCheckpointY);
            double distanceNextCheckPoint = Point<int>.Distance(racerPt, nextCheckpoint);

            Vector vCheckPtToRacer = new Vector(nextCheckpoint, racerPt);
            Point<int> radNextCheckPt = vCheckPtToRacer.FindPointOnLine(nextCheckpoint, nextCheckpointRadius);
            double distradNextCheckPt = Point<int>.Distance(racerPt, radNextCheckPt);
            Console.Error.WriteLine($"nextCheckpoint: {nextCheckpoint.x}, {nextCheckpoint.y}");
            Console.Error.WriteLine($"radNextCheckPt: {radNextCheckPt.x}, {radNextCheckPt.y}");
            Console.Error.WriteLine($"Radius next pt: {distradNextCheckPt}");

            thrust = SelectThrust(nextCheckpointAngle, (int)distradNextCheckPt);

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            Console.Error.WriteLine($"Angle: {nextCheckpointAngle}");
            Console.Error.WriteLine($"Thrust: {thrust}");

            int destX = radNextCheckPt.x;
            int destY = radNextCheckPt.y;
            // You have to output the target position
            // followed by the power (0 <= thrust <= 100)
            // i.e.: "x y thrust"
            if ((distanceNextCheckPoint > 8000) && (nextCheckpointAngle < 10) && (nextCheckpointAngle > -10))
            {
                Console.WriteLine($"{destX} {destY} BOOST");
            }
            else
            {
                Console.WriteLine($"{destX} {destY} {thrust}");
            }
        }
    }
}