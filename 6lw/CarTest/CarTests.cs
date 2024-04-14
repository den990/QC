using CarNS;

namespace CarTests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void GetStartValues()
        {
            // Arrange
            Car car = new Car();

            // Assert
            Assert.AreEqual(0, car.GetGear(), "Invalid start gear");
            Assert.AreEqual(0, car.GetSpeed(), "Invalid start speed");
            Assert.AreEqual(false, car.IsTurnedOn(), "Invalid start status engine");
            Assert.AreEqual(Direction.IMMOBILE, car.GetDirection(), "Invalid start direction");
        }

        [TestMethod]
        public void SwitchingEngineOnWhenEngineIsOff()
        {
            // Arrange
            Car car = new Car();

            // Action
            car.TurnOnEngine();

            // Assert
            Assert.AreEqual(true, car.IsTurnedOn(), "Invalid status engine");
        }

        [TestMethod]
        public void SwitchingEngineOnWhenEngineIsOn()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();

            // Action
            car.TurnOnEngine();

            // Assert
            Assert.AreEqual(true, car.IsTurnedOn(), "Invalid status engine");
        }

        [TestMethod]
        public void SwitchingEngineOffWhenEngineIsOn()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();

            // Action
            car.TurnOffEngine();

            // Assert
            Assert.AreEqual(false, car.IsTurnedOn(), "Invalid status engine");
        }

        [TestMethod]
        public void SwitchingEngineOffWhenEngineIsOnAndGearIsnot_Neutral()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);

            // Action
            car.TurnOffEngine();

            // Assert
            Assert.AreEqual(true, car.IsTurnedOn(), "Invalid status engine");
        }

        [TestMethod]
        public void SwitchingEngineOffWhenEngineIsOnAndSpeedIsnotZeroAndGearIsnotNeutral()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(20);

            // Action
            car.TurnOffEngine();

            // Assert
            Assert.AreEqual(true, car.IsTurnedOn(), "Invalid status engine");
        }

        [TestMethod]
        public void SetGearRWhenEngineOff()
        {
            // Arrange
            Car car = new Car();

            // Action
            car.SetGear(-1);

            // Assert
            Assert.AreEqual(0, car.GetGear(), "Invalid gear");
        }

        [TestMethod]
        public void SetGear1WhenEngineOn()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();

            // Action
            car.SetGear(1);

            // Assert
            Assert.AreEqual(1, car.GetGear(), "Invalid gear");
        }

        [TestMethod]
        public void SetGear2WhenEngineOnAndSpeedIsnotInIntervalOf2Gear()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();

            // Action
            car.SetGear(2);

            // Assert
            Assert.AreEqual(0, car.GetGear(), "Invalid gear");
        }

        [TestMethod]
        public void SetGearRWhenEngineOnAndSpeedIsZero()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();

            // Action
            car.SetGear(-1);

            // Assert
            Assert.AreEqual(-1, car.GetGear(), "Invalid gear");
        }

        [TestMethod]
        public void SetGearRWhenEngineOffAndSpeedIsnotZero()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(-1);
            car.SetSpeed(20);
            car.SetGear(0);
            // Action
            car.SetGear(-1);

            // Assert
            Assert.AreEqual(0, car.GetGear(), "Invalid gear");
        }

        [TestMethod]
        public void SetGear1FromRGearWhenEngineOnAndSpeedIsnotZero()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(-1);
            car.SetSpeed(20);

            // Action
            car.SetGear(1);

            // Assert
            Assert.AreEqual(-1, car.GetGear(), "Invalid gear");
        }

        [TestMethod]
        public void SetGearRWhenEngineOnSpeedZeroAndGearAlreadyR()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(-1);
            // Action
            car.SetGear(-1);

            // Assert
            Assert.AreEqual(-1, car.GetGear(), "Invalid gear");
        }


        [TestMethod]
        public void SetSpeedWhenEngineIsOff()
        {
            // Arrange
            Car car = new Car();

            // Action
            car.SetSpeed(20);

            // Assert
            Assert.AreEqual(0, car.GetSpeed(), "invalid speed");
        }

        [TestMethod]
        public void SetSpeedWhenEnginIsOnWithZeroGear()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();

            // Action
            car.SetSpeed(20);

            // Assert
            Assert.AreEqual(0, car.GetSpeed(), "Invalid speed");
        }

        [TestMethod]
        public void SetSpeedWhichInIntervalOf1GearWhenEnginIsOn()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);

            // Action
            car.SetSpeed(20);

            // Assert
            Assert.AreEqual(20, car.GetSpeed(), "Invalid speed");
            Assert.AreEqual(Direction.FORWARD, car.GetDirection(), "Invalid direction");
        }

        [TestMethod]
        public void SetSpeedWhichIsMoreThanIntervalOf1GearWhenEngineIsOn()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);

            // Action
            car.SetSpeed(60);

            // Assert
            Assert.AreEqual(0, car.GetSpeed(), "Invalid speed");
        }

        [TestMethod]
        public void SetSpeedWhichIsLessThanIntervalOf1GearWhenEnginIsOn()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);

            // Action
            car.SetSpeed(-10);

            // Assert
            Assert.AreEqual(0, car.GetSpeed(), "Invalid speed");
            Assert.AreEqual(Direction.IMMOBILE, car.GetDirection(), "Invalid direction");
        }

        [TestMethod]
        public void SetSpeedWhichInIntervalOfRGearWhenEnginIsOn()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(-1);

            // Action
            car.SetSpeed(10);

            // Assert
            Assert.AreEqual(-10, car.GetSpeed(), "Invalid speed");
            Assert.AreEqual(Direction.BACKWARD, car.GetDirection(), "Invalid direction");

        }

        [TestMethod]
        public void SetSpeedWhichInIntervalOfNGearWhenEngineIsOn()
        {
            // Arrange
            Car car = new Car();
            car.TurnOnEngine();

            // Action
            car.SetSpeed(0);

            // Assert
            Assert.AreEqual(0, car.GetSpeed(), "Invalid speed");

        }
    }
}