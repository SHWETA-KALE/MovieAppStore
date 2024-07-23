using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviestoreApp.models
{
    internal class Movie
    {
        //properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Genre { get; set; }

        public Movie() { }


        public Movie(int id, string name, int yearofrelease)
        {
            Id = id;
            Name = name;
            YearOfRelease = yearofrelease;
        }

        public Movie(int id, string name, int yearofrelease, string genre) : this(id, name, yearofrelease)
        {
            Genre = genre;
        }

        public static Movie AddMovie(int id, string name, int yearofrelease, string genre)
        {
            return new Movie(id, name, yearofrelease, genre);
        }

        public override string ToString()
        {
            return $"Name: {Name}\n" +
                $"Id: {Id}\n" +
                $"Year of Release: {YearOfRelease} \n" +
                $"Genre: {Genre}\n";
        }
    }
}
