using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain.ValueObjects
{
	// Entity representing a room where a section meets.
	// Rooms must be entities because two sections cannot be
	// meeting in the same room at the same time.
    public struct Room
    {
        private string buildingName;
        private int roomNumber;

		
		// Create a room given a building name and room number.
        public Room(string buildingName, int roomNumber)
        {
            this.buildingName = buildingName;
            this.roomNumber = roomNumber;
        }

		// Convert a room to a string, containing the building name and room number.
        public override string ToString()
        {
            return (buildingName + " " + roomNumber);
        }

		public override bool Equals(object obj)
		{
			return (obj is Room && (Room) obj == this);
		}

		public override int GetHashCode()
		{
			int hash = 13;
			hash = (hash * 7) + buildingName.GetHashCode();
			hash = (hash * 7) + roomNumber;
			return hash;
		}

		public static bool operator ==(Room a, Room b)
		{
			return (a.buildingName == b.buildingName &&
					a.roomNumber == b.roomNumber);
		}

		public static bool operator !=(Room a, Room b)
		{
			return !(a == b);
		}


        public string BuildingName
        {
            get { return buildingName; }
            set { buildingName = value; }
        }

        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }
    }
}
