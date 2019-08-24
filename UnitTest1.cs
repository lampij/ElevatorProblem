using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevatorProblem
{
    [TestClass]
    public class ElevatorTests
    {
        [TestMethod]
        public void EveryoneOnTheRightFloor()
        {
            Building b = new Building(100);
            b.RandomlyDistributePeople(100);
            b.RunElevator();
            Assert.IsTrue(b.People.All(x => x.CurrentFloor == x.DestinationFloor)); 
        }
    }
}
