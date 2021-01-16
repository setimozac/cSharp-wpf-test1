using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Trip : IEquatable<Trip> 
    {
        private string _destination;
        public string Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                if(value == "" || value.Contains(';'))
                {
                    throw new InvalidDataException("Destination is invalid");
                }
                _destination = value;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == "" || value.Contains(';'))
                {
                    throw new InvalidDataException("Name is invalid");
                }
                _name = value;
            }
        }

        private string _passport;
        public string Passport
        {
            get
            {
                return _passport;
            }
            set
            {
                if (value == "")
                {
                    throw new InvalidDataException("Passport is invalid");
                }
                _passport = value;
            }
        }

        private DateTime _departue;
        public DateTime Departure
        {
            get
            {
                return _departue;
            }
            set
            {
                if (value == null)
                {
                    throw new InvalidDataException("Departure Date is invalid");
                }
                _departue = value;
            }
        }

        private DateTime _return;
        public DateTime Return
        {
            get
            {
                return _return;
            }
            set
            {
                if (value == null)
                {
                    throw new InvalidDataException("Return Date is invalid");
                }
                _return = value;
            }
        }

        public Trip(string line)
        {
            if(line.Split(';').Length != 5) throw new InvalidDataException("corrupted line in the file!");
            Destination = line.Split(';')[0];
            Name = line.Split(';')[1];
            Passport = line.Split(';')[2];
            Departure = DateTime.Parse(line.Split(';')[3]);
            Return = DateTime.Parse(line.Split(';')[3]);
        }

        public Trip(string destination, string name, string passport, DateTime departure, DateTime ret)
        {
            Destination = destination;
            Name = name;
            Passport = passport;
            Departure = departure;
            Return = ret;
        }

        public override string ToString()
        {
            return $"{Destination};{Name};{Passport};{Departure.ToString()};{Return.ToString()}";
        }


        public bool Equals(Trip other)
        {
            if (other == null) return false;
            if (this.Destination.Equals(other.Destination) && this.Name.Equals(other.Name) && this.Passport.Equals(other.Passport) && this.Departure == other.Departure && this.Return == other.Return) return true;
            return false;
        }
    }
}
