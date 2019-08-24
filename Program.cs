using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Building b = new Building(100);
            b.RandomlyDistributePeople(50);
            b.RunElevator();
            
        }
    }

    public class Building
    {
        public int floors;

        public Elevator ele;

        public List<Rider> People = new List<Rider>();
        public Building(int floors)
        {
            this.floors = floors;
            ele = new Elevator();   
        }

        public void RandomlyDistributePeople(int NumberOfPeople)
        {
            Random r = new Random();
            this.People = new List<Rider>();
            for (int i = 0; i < NumberOfPeople; i++)
            {
                var floorStart = r.Next(0, floors);
                var floorEnd = r.Next(0, floors);
                while (floorStart == floorEnd)
                {
                    floorEnd = r.Next(0, floors);
                }
                People.Add(new Rider(floorStart, floorEnd));
            }
        }

        public void RunElevator()
        {

            //Describe movement
            for (int i = 0; i <= this.floors; i++)
            {
                var peopleGettingOn = People.Where(x => x.StartingFloor == ele.CurrentFloor && (x.DestinationFloor != x.CurrentFloor)).ToList();
                foreach (var r in peopleGettingOn)
                {
                    this.People.Remove(r);
                }
                ele.CurrentFloor = i;
                ele.Moved(i);
                for (int j = 0; j < this.People.Count(); j++)
                {
                    var personObj = this.People.ToArray()[j];
                    if (personObj.CurrentFloor != personObj.DestinationFloor)
                    {
                        personObj.TotalSteps += 1;
                    }
                }
                ele.Pickup(peopleGettingOn);
                var peopleGettingOff = ele.DropOff();
                this.People.AddRange(peopleGettingOff);
            }
            for (int i = this.floors; i >= 0; i--)
            {
                var peopleGettingOff = ele.DropOff();
                this.People.AddRange(peopleGettingOff);
                var peopleGettingOn = People.Where(x => x.StartingFloor == ele.CurrentFloor && (x.DestinationFloor != x.CurrentFloor)).ToList();
                foreach (var r in peopleGettingOn)
                {
                    this.People.Remove(r);
                }
                ele.CurrentFloor = i;
                ele.Moved(i);
                ele.Pickup(peopleGettingOn);
            }

        }
    }

    public class Rider
    {
        public int StartingFloor;
        public int DestinationFloor;
        public int CurrentFloor;
        public int TotalSteps;
        public Rider(int StartingFloor, int DestinationFloor)
        {
            this.StartingFloor = StartingFloor;
            this.DestinationFloor = DestinationFloor;
        }
    }

    public class Elevator
    {
        List<Rider> Riders = new List<Rider>();
        public int CurrentFloor;

        public int TotalFloorsTraversed;
        public Elevator()
        {
            this.CurrentFloor = 0;
        }

        public void Pickup(IEnumerable<Rider> Rider)
        {
            foreach (var r in Rider)
            {
                this.Riders.Add(r);
            }
        }

        public void Moved(int floor)
        {
            for (int i = 0; i < this.Riders.Count(); i++)
            {
                this.Riders.ToArray()[i].CurrentFloor = floor;
                this.Riders.ToArray()[i].TotalSteps += 1;
            }
            this.TotalFloorsTraversed += 1;
        }

        public IEnumerable<Rider> DropOff()
        {
            var RidersGettingOff = Riders.Where(x => x.DestinationFloor == this.CurrentFloor).ToList();
            foreach (var r in RidersGettingOff)
            {
                r.CurrentFloor = this.CurrentFloor;
                this.Riders.Remove(r);
            }

            return RidersGettingOff;
        }
    }

}
