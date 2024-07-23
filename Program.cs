using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text.Json;
using System.Threading.Channels;
using MoviestoreApp.Exceptions;
using MoviestoreApp.models;

namespace MoviestoreApp
{
    internal class Program
    {
        static List<Movie> movies = new List<Movie>(5); //list initilization improves performance
                                                        //It reduces the frequency of reallocations and copying when adding elements.

        static string path = ConfigurationManager.AppSettings["filePath"].ToString();
        static void Main(string[] args)
        {
            DisplayMenu();

        }
        static void DisplayMenu()
        {
            movies = DeserializeMovieList();
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

                int choice;
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.WriteLine();
                    continue; // Skip the rest of the loop and prompt again
                }
                Console.WriteLine();
                try
                {
                    DoTask(choice);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Please enter numbers only!");
                }
                catch (CapacityFullExceptionException cf)
                {
                    Console.WriteLine(cf.Message);
                }
                catch (MovieNotFoundException mnf)
                {
                    Console.WriteLine(mnf.Message);
                }
                catch (MovieStoreIsEmptyException mse)
                {
                    Console.WriteLine(mse.Message);
                }
                catch (InvalidMovieIdException ime)
                {
                    Console.WriteLine(ime.Message);
                }

            }
        }

        static void DoTask(int choice)
        {
            switch (choice)
            {
                case 1:
                    if (movies.Count != 5)
                    {
                        AddNewMovie();
                    }
                    else
                    {
                        throw new CapacityFullExceptionException("Sorry, you cannot add more movies. Movie list is full.\n");
                    }
                    break;


                case 2:
                    if (movies.Count == 0)
                        throw new MovieStoreIsEmptyException("Movie store is Empty!\n");
                    else
                        DisplayMovies();
                    break;

                case 3:
                    Movie findMovie = FindMovieById();
                    if (findMovie != null)
                        Console.WriteLine(findMovie);
                    else
                        throw new MovieNotFoundException("Movie not found!\n");
                    break;

                case 4:
                    RemoveMovieById();
                    break;

                case 5:
                    if (movies.Count == 0)
                        throw new MovieStoreIsEmptyException("Movie Store is already Empty!\n");

                    movies.Clear();
                    Console.WriteLine("Cleared successfully\n");

                    break;

                case 6:
                    SerializeMovieList(movies);
                    Environment.Exit(0);
                    break;


            }
        }

        static void AddNewMovie()
        {
            Console.WriteLine("Enter Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (id <= 0)
                throw new InvalidMovieIdException("Invalid movie Id. Id must be greater than 0");

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

            if (id <= 0)
            {
                throw new InvalidMovieIdException("Invalid movie Id. Id must be greater than 0");
            }
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
                Console.WriteLine("Movie removed successfully!\n");
            }
            else
                throw new MovieNotFoundException("Movie not found\n");

        }

        static void SerializeMovieList(List<Movie> movies)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine(JsonSerializer.Serialize(movies));

            }
        }

        static List<Movie> DeserializeMovieList()
        {
            if (!File.Exists(path))
                return new List<Movie>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<Movie> movies = JsonSerializer.Deserialize<List<Movie>>(sr.ReadToEnd());
                return movies;
            }
        }
    }
}
