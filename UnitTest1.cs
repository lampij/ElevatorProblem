using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevatorProblem
{
    [TestClass]
    public class ElevatorTests
    {
        Building b = new Building(100);

        public ElevatorTests(){
            var r = new Random();
            int firstRun  = r.Next(100, 10000);
            Trace.WriteLine($"Running {firstRun} people.");
            b.RandomlyDistributePeople(r.Next(100, 10000));
            b.RunElevator();
        }

        [TestMethod]
        public void EveryoneOnTheRightFloor()
        {
            Assert.IsTrue(b.People.All(x => x.CurrentFloor == x.DestinationFloor)); 
        }

        [TestMethod]
        public void OptomizationGoal1(){
            var r = new Random();
            for (int i = 0; i < 5; i++)
            {
                int nextRun = r.Next(100, 10000);
                Trace.WriteLine($"Running {nextRun} people.");
                b.RandomlyDistributePeople(nextRun);
                b.RunElevator();
                Assert.IsTrue(b.People.Average(x => x.TotalSteps) < b.floors*2);   
            }
        }

        [TestMethod]
        public void OptomizationGoal2(){
            var r = new Random();
            for (int i = 0; i < 5; i++)
            {
                int nextRun = r.Next(100, 10000);
                Trace.WriteLine($"Running {nextRun} people.");
                b.RandomlyDistributePeople(nextRun);
                b.RunElevator();
                Assert.IsTrue(b.People.Average(x => x.TotalSteps) < b.floors + (b.floors * .5));   
            }
        }
    }
}
