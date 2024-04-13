namespace CarNS
{
    public enum Direction
    {
        FORWARD,
        BACKWARD,
        IMMOBILE
    }
    public class Car
    {

        private int m_gear = 0;
        private int m_speed = 0;
        private bool m_isEngineOn = false;
        private string m_lastErrorMessage = "";

        private readonly Dictionary<int, Tuple<int, int>> gearSpeedRange = new Dictionary<int, Tuple<int, int>>
    {
        { -1, Tuple.Create(-20, 0) },
        { 0, Tuple.Create(0, 150) },
        { 1, Tuple.Create(0, 30) },
        { 2, Tuple.Create(20, 50) },
        { 3, Tuple.Create(30, 60) },
        { 4, Tuple.Create(40, 90) },
        { 5, Tuple.Create(50, 150) }
    };

        public bool TurnOnEngine()
        {
            m_isEngineOn = true;
            return true;
        }

        public bool TurnOffEngine()
        {
            if ((m_gear != 0) || (m_speed != 0))
            {
                m_lastErrorMessage = "Stop and shift to neutral to turn off the engine.\n";
                return false;
            }

            m_isEngineOn = false;
            return true;
        }

        private bool CheckSpeedRange(int speed, int gear)
        {
            if (!gearSpeedRange.ContainsKey(gear))
                return false;

            var range = gearSpeedRange[gear];
            return speed >= range.Item1 && speed <= range.Item2;
        }

        public bool SetGear(int gear)
        {
            if (!IsTurnedOn())
            {
                m_lastErrorMessage = "Unable to set gear: Engine is turned off.";
                return false;
            }

            if (!gearSpeedRange.ContainsKey(gear))
            {
                m_lastErrorMessage = "Unable to set gear: invalid gear argument";
                return false;
            }

            switch (gear)
            {
                case -1:
                    if (m_speed != 0 && m_gear != gear)
                    {
                        m_lastErrorMessage = "Unable to set reverse gear while moving.";
                        return false;
                    }
                    break;

                case 0:
                    break;

                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    if (!CheckSpeedRange(m_speed, gear))
                    {
                        m_lastErrorMessage = "Unable to set gear: unsuitable current speed";
                        return false;
                    }
                    break;

                default:
                    m_lastErrorMessage = "Internal error: invalid gear argument";
                    return false;
            }

            m_gear = gear;
            return true;
        }

        public bool SetSpeed(int speed)
        {
            if (speed < 0)
            {
                m_lastErrorMessage = "Unable to set speed: speed cannot be negative value.";
                return false;
            }

            if (!IsTurnedOn())
            {
                m_lastErrorMessage = "Unable to set speed: engine is turned off.";
                return false;
            }

            switch (m_gear)
            {
                case -1:
                    if (-speed < gearSpeedRange[-1].Item1)
                    {
                        m_lastErrorMessage = "Unable to set speed: exceed reverse speed limit";
                        return false;
                    }
                    m_speed = -speed;
                    return true;

                case 0:
                    if (speed > Math.Abs(m_speed))
                    {
                        m_lastErrorMessage = "Unable to set speed: impossible to accelerate at neutral gear";
                        return false;
                    }
                    m_speed = (m_speed < 0) ? -speed : speed;
                    return true;

                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    if (!CheckSpeedRange(speed, m_gear))
                    {
                        m_lastErrorMessage = "Unable to set speed: speed does not suit currently selected gear.";
                        return false;
                    }
                    m_speed = speed;
                    return true;

                default:
                    m_lastErrorMessage = "Internal error: invalid gear value.";
                    return false;
            }
        }

        public bool IsTurnedOn()
        {
            return m_isEngineOn;
        }

        public Direction GetDirection()
        {
            if (m_speed > 0)
                return Direction.FORWARD;
            else if (m_speed == 0)
                return Direction.IMMOBILE;
            else
                return Direction.BACKWARD;
        }

        public int GetSpeed()
        {
            return Math.Abs(m_speed);
        }

        public int GetGear()
        {
            return m_gear;
        }

        public string GetLastErrorMessage()
        {
            return m_lastErrorMessage;
        }

        public static void Main(string[] args)
        {
        }
    }
}