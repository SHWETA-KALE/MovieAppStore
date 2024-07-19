using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using MoviestoreApp.models;

namespace MoviestoreApp
{
    internal class Program
    {
        static List<Movie> movies = new List<Movie>(5); //list initilization improves performance
                                                        //It reduces the frequency of reallocations and copying when adding elements.
        static void Main(string[] args)
        {
            DisplayMenu();
        }
        static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("==============WELCOME TO MOVIE STORE DEVELOPED BY: SHWETA================\n");
                Console.WriteLine("What would you like to do?\n");
                Console.WriteLine($"1.Add new Movie.\n" +
                     $"2.Display All Movies.\n" +
                     $"3.Find Movie by ID.\n" +
                     $"4.Remove Movie by ID.\n" +
                     $"5.Clear All Movies\n" +
                     $"6.Exit\n");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                DoTask(choice);
            }
        }

        static void DoTask(int choice)
        {
            switch (choice)
            {
                case 1:
                    if (movies.Count !=5)
                    {
                        AddNewMovie();
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you cannot add more movies. Movie list is full.\n");
                    }
                    break;


                case 2:
                    if (movies.Count == 0)
                        Console.WriteLine("Movie strore is Empty");
                    else
                        DisplayMovies();
                    break;

                case 3:
                    Movie findMovie = FindMovieById();
                    if (findMovie != null)
                        Console.WriteLine(findMovie);
                    else
                        Console.WriteLine("Movie not found!");
                    break;

                case 4:
                    RemoveMovieById();
                    break;

                case 5:
                    if (movies.Count == 0)
                        Console.WriteLine("Movie Store is already Empty!\n");
                    else
                    {
                        movies.Clear();
                        Console.WriteLine("Cleared successfully");
                    }
                    break;

                case 6:
                    Environment.Exit(0);
                    break;


            }
        }

        static void AddNewMovie()
        {
            Console.WriteLine("Enter Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Year Of Release: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Genre: ");
            string genre = Console.ReadLine();

            Movie newMovie = Movie.AddMovie(id, name, year, genre);
            movies.Add(newMovie);

            Console.WriteLine("New Movie added successfully\n");
        }

        static void DisplayMovies()
        {
            //foreach(Movie movie in movies)
            //{
            //    Console.WriteLine(movie);
            //}
            //linq
            movies.ForEach(movie => Console.WriteLine(movie));
        }

        static Movie FindMovieById()
        {
            Movie findMovie = null;
            Console.WriteLine("Enter Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            //foreach(Movie movie in movies)
            //{
            //    if(movie.Id == id)
            //        findMovie = movie;
            //}
            //linq
            findMovie = movies.Where(item => item.Id == id).FirstOrDefault();
            return findMovie;
        }

        static void RemoveMovieById()
        {
            Movie findMovie = FindMovieById();
            if (findMovie != null)
            {
                movies.Remove(findMovie);
                Console.WriteLine("Movie removed successfully!");
            }
            else
                Console.WriteLine("Movie not found");

        }
    }
}
