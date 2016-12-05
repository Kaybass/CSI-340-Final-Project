using System;

namespace CourseMan.Domain.ValueObjects
{
	// Value object representing a room where a section meets.
	// Technically, a room is an entity, because two sections cannot 
	// be meeting in the same room at the same time, but in our
	// implementation, the room class is a value object because it 
	// merely represents the ID of a room, as an attribute for a section.
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
