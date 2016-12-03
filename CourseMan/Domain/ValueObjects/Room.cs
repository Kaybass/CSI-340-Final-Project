using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
    public struct Room
    {
        private string buildingName;
        private int roomNumber;


        public Room(string buildingName, int roomNumber)
        {
            this.buildingName = buildingName;
            this.roomNumber = roomNumber;
        }


        public override string ToString()
        {
            return (buildingName + " " + roomNumber);
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
