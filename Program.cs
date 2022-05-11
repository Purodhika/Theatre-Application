using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Purodhika_Assignment
{

    public enum MovieDay { Sun, Mon, Tue, Wed, Thu, Fri, Sat } //represents the days of the week

    [Flags]
    public enum MovieGenre  //represents categories of movies and assigns appropriate values
    {
        Unrated = 0,
        Action = 1,
        Adventure = 2,
        Animation = 4,
        Comedy = 8,
        Documentary = 16,
        Fantasy = 32,
        Horror = 64,
        Musical = 128,
        Mystery = 256,
        Romance = 512,
    }

    public struct Time
    {
        public int hours { get; }
        public int minutes { get; }
        public int seconds { get; }

        public Time(int hours, int minutes =0, int seconds=0)  //constructor to allocate time
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }
        
        public static bool operator ==(Time lhs, Time rhs) //to check the time difference between two movies
        {
            return Math.Abs(lhs.hours * 60 + lhs.minutes - rhs.hours * 60 - rhs.minutes) <= 15; ; //returns true if difference is less than 15 minutes
        }

        public static bool operator !=(Time lhs, Time rhs)  //overrides the method to do reverse of equality operator
        {
            return (lhs!=rhs);
        }
        public override string ToString()
        {
            return $"{hours}:{minutes}";
        }

        class Movie
        {
            public int length { get; }  //length of movie in mins
            public int year { get; }    //year of release
            public String title { get; }  //title of the movie
            public MovieGenre genre { get; private set; } //genre is enum representing genre of movie

            public List<string> Cast { get; } //represents names of actors in the movie

            public Movie(string name, int year, int length)    
            {
                Cast = new List<string>();  //initializes cast property to an mpty list of string

                this.title = name;
                this.year = year;
                this.length = length;
                this.Cast = new List<string>();
            }
            public void AddActor(string actor)     //to add actors to the list
            {
                Cast.Add(actor);
            }

            public void SetGenre(MovieGenre genre1) //takes enum arg and assigns it to property 
            {
                genre = genre1;
            }


            public override string ToString()  //overrides same method of object class
            {
                return $"{title} ({year}) {length}min ({genre}) {string.Join(",", Cast)}";
            }

        }
        struct Show
        {
            public double Price { get; }  //price of ticket
            public MovieDay Day { get; }   //enum representig day
            public Movie Movie { get; }  //object reference of movie class
            public Time Time { get; }   //object of time class

            public Show(Movie movie, MovieDay day, double price, Time time)  //sets the values 
            {
                //assigning values to properties
                this.Movie = movie;
                this.Day = day;
                this.Price = price;
                this.Time = time;

            }

            public override string ToString()    //overrides method of object class
            {
                return $"{Day} {Time} {Movie} ${Price}";
            }

        }

        class Theatre
        {
            private List<Show> shows;                //list of shows
            public string Name { get; }
            public Theatre(string name)
            {
                Name = name;
                shows = new List<Show>();           //initializes shows property new shows list
            }

            public void AddShow(Show s1)           //adds new movie details and shows
            {
                shows.Add(s1);
            }
            public void PrintShows()              //prints all shows
            {
                Console.WriteLine();
                Console.WriteLine(Name);
                Console.WriteLine("All Shows");
                Console.WriteLine("===============");
                int i =1;
                foreach (Show show in shows)
                {
                    Console.WriteLine(i+ ": "+show);
                    i++;
                }
            }
            public void PrintShows(MovieGenre genre)    //compares movie genre and displays genre specific movies
            {
                Console.WriteLine();
                Console.WriteLine(Name);
                Console.WriteLine( genre +" Movies");
                Console.WriteLine("===============");
                int i = 1;
                foreach (Show show in shows)
                {
                    if (show.Movie.genre.HasFlag(genre))
                    {
                        Console.WriteLine(i + ": " +show);
                        i++;

                    }
                }
            }
            public void PrintShows(MovieDay day)       //compares movie's day and displays day specific movies
            {
                Console.WriteLine();
                Console.WriteLine(Name);
                Console.WriteLine("Movies streaming on " +day);
                Console.WriteLine("===============");
                int i = 1;
                foreach (Show show in shows)
                {
                    if (show.Day.Equals(day))
                    {
                        Console.WriteLine(i + ": " +show);
                        i++;
                    }
                }
            }
            public void PrintShows(Time time)          //compares time of movies and displays the output
            {
                Console.WriteLine();
                Console.WriteLine(Name);
                Console.WriteLine("Movies running @" +time);
                Console.WriteLine("===============");
                int i = 1;
                foreach (Show show in shows)
                {
                    if (show.Time == time)
                        Console.WriteLine(i + ": " + show);
                    i++;
                }

            }
            public void PrintShows(string actor)      //displays the movies of a particular actor
            {
                Console.WriteLine();
                Console.WriteLine(Name);
                Console.WriteLine(actor + " Movies");
                Console.WriteLine("===============");
                int i = 1;
                foreach (Show show in shows)
                {
                    if (show.Movie.Cast.Contains(actor))
                    {
                        Console.WriteLine(i+ ": " +show);
                        i++;
                    }
                }
            }
            public void PrintShows(MovieDay day, Time time)    //displays the movies of a specific day & time
            {
                Console.WriteLine();
                Console.WriteLine(Name);
                Console.WriteLine(day + " Movies @" +time);
                Console.WriteLine("===============");
                int i = 1;
                foreach (Show show in shows)
                {
                    if (show.Day.Equals(day) && show.Time == time)
                    {
                        Console.WriteLine(i+": " + show);
                        i++;
                    }
                }
            }

        }
        class Program
        {
            public static void Main(string[] args)
            {
                Movie terminator = new Movie("Terminator 2: Judgement Day", 1991, 105);
                terminator.AddActor("Arnold Schwarzenegger");
                terminator.SetGenre(MovieGenre.Horror | MovieGenre.Action);
                terminator.AddActor("Linda Hamilton");
                Show s1 = new Show(terminator, MovieDay.Mon, 5.95, new Time(11, 35, 0));

                Console.WriteLine(s1);              //displays one object

                Theatre eglinton = new Theatre("Cineplex");
                eglinton.AddShow(s1);
                eglinton.PrintShows();              //displays one object

                Movie godzilla = new Movie("Godzilla 2014", 2014, 123);
                godzilla.AddActor("Aaron Johnson");
                godzilla.AddActor("Ken Watanabe");
                godzilla.AddActor("Elizabeth Olsen");
                godzilla.SetGenre(MovieGenre.Action | MovieGenre.Documentary | MovieGenre.Comedy);

                Movie trancendence = new Movie("Transendence", 2014, 120);
                trancendence.AddActor("Johnny Depp");
                trancendence.AddActor("Morgan Freeman");
                trancendence.SetGenre(MovieGenre.Comedy);
                eglinton.AddShow(new Show(trancendence, MovieDay.Sun, 7.87, new Time(18, 05, 0)));

                Movie m1 = new Movie("The Shawshank Redemption", 1994, 120);
                m1.AddActor("Tim Robbins");
                m1.AddActor("Morgan Freeman");
                m1.SetGenre(MovieGenre.Action);
                eglinton.AddShow(new Show(m1, MovieDay.Sun, 8.87, new Time(14, 05, 0)));

                Movie avengers = new Movie("Avengers: Endgame", 2019, 120);
                avengers.AddActor("Robert Downey Jr.");
                avengers.AddActor("Chris Evans");
                avengers.AddActor("Chris Hemsworth");
                avengers.AddActor("Scarlett Johansson");
                avengers.AddActor("Mark Ruffalo");
                avengers.SetGenre(MovieGenre.Action | MovieGenre.Fantasy | MovieGenre.Adventure);
                eglinton.AddShow(new Show(avengers, MovieDay.Sat, 12.25, new Time(21, 05, 0)));

                m1 = new Movie("Olympus Has Fallen", 2013, 120);
                m1.AddActor("Gerard Butler");
                m1.AddActor("Morgan Freeman");
                m1.SetGenre(MovieGenre.Action);
                eglinton.AddShow(new Show(m1, MovieDay.Sun, 8.87, new Time(16, 05, 0)));

                m1 = new Movie("The Mask of Zorro", 1998, 136);
                m1.AddActor("Antonio Banderas");
                m1.AddActor("Anthony Hopkins");
                m1.AddActor("Catherine Zeta-Jones");
                m1.SetGenre(MovieGenre.Action | MovieGenre.Romance);
                eglinton.AddShow(new Show(m1, MovieDay.Sun, 8.87, new Time(16, 05, 0)));

                m1 = new Movie("Four Weddings and a Funeral", 1994, 117);
                m1.AddActor("Hugh Grant");
                m1.AddActor("Andie MacDowell");

                m1.AddActor("Kristin Scott Thomas");
                m1.SetGenre(MovieGenre.Comedy | MovieGenre.Romance);
                eglinton.AddShow(new Show(m1, MovieDay.Sat, 8.87, new Time(15, 05, 0)));
                eglinton.AddShow(new Show(m1, MovieDay.Tue, 6.50, new Time(16, 05, 0)));

                m1 = new Movie("You've Got Mail", 1998, 119);
                m1.AddActor("Tom Hanks");
                m1.AddActor("Meg Ryan");
                m1.SetGenre(MovieGenre.Comedy | MovieGenre.Romance);
                eglinton.AddShow(new Show(m1, MovieDay.Sat, 8.87, new Time(15, 05, 0)));

                m1 = new Movie("The Poison Rose", 2019, 98);
                m1.AddActor("John Travolta");
                m1.AddActor("Morgan Freeman");
                m1.AddActor("Brendan Fraser");
                m1.SetGenre(MovieGenre.Action | MovieGenre.Romance);
                eglinton.AddShow(new Show(m1, MovieDay.Sun, 10.25, new Time(22, 05, 0)));

                Movie car3 = new Movie("Cars 3", 2017, 109);
                car3.AddActor("Owen Williams");
                car3.AddActor("Cristela Alonzo");
                car3.AddActor("Arnie Hammer");
                car3.AddActor("Chris Cooper");
                car3.SetGenre(MovieGenre.Comedy | MovieGenre.Animation | MovieGenre.Romance);
                eglinton.AddShow(new Show(car3, MovieDay.Sat, 6.40, new Time(09, 55, 0)));
                eglinton.AddShow(new Show(car3, MovieDay.Sat, 6.50, new Time(11, 05, 0)));

                Movie toys4 = new Movie("Toys Story 4", 2019, 89);
                toys4.AddActor("Keanu Reeves");
                toys4.AddActor("Christina Hendricks");
                toys4.AddActor("Tom Hanks");
                toys4.AddActor("Tim Allen");
                toys4.SetGenre(MovieGenre.Comedy | MovieGenre.Fantasy | MovieGenre.Animation);
                eglinton.AddShow(new Show(toys4, MovieDay.Sat, 6.40, new Time(14, 10)));

                eglinton.AddShow(new Show(godzilla, MovieDay.Mon, 6.89, new Time(13, 55, 0)));
                eglinton.AddShow(new Show(avengers, MovieDay.Sat, 12.25, new Time(21, 05, 0)));
                eglinton.AddShow(new Show(godzilla, MovieDay.Sun, 6.89, new Time(14, 00)));
                eglinton.AddShow(new Show(toys4, MovieDay.Sat, 6.40, new Time(14, 10)));
                eglinton.AddShow(new Show(avengers, MovieDay.Sat, 12.25, new Time(21, 05, 0)));
                eglinton.AddShow(new Show(godzilla, MovieDay.Sun, 6.89, new Time(16, 55, 0)));
                eglinton.AddShow(new Show(avengers, MovieDay.Sat, 12.25, new Time(21, 05, 0)));
                eglinton.AddShow(new Show(m1, MovieDay.Sat, 10.25, new Time(20, 35, 0)));
                eglinton.AddShow(new Show(godzilla, MovieDay.Wed, 8.50, new Time(22, 05)));
                eglinton.AddShow(new Show(avengers, MovieDay.Tue, 10.75, new Time(20, 30)));
                eglinton.AddShow(new Show(godzilla, MovieDay.Thu, 8.50, new Time(20, 15)));
                eglinton.AddShow(new Show(avengers, MovieDay.Wed, 10.75, new Time(20, 30)));
                eglinton.AddShow(new Show(godzilla, MovieDay.Fri, 8.50, new Time(18, 25)));
                eglinton.AddShow(new Show(avengers, MovieDay.Sun, 10.75, new Time(14, 15)));
                eglinton.PrintShows();                              //displays 27 objects

                eglinton.PrintShows(MovieDay.Sun);                       //displays 8 objects

                eglinton.PrintShows(MovieGenre.Action);                  //displays 19 objects

                eglinton.PrintShows(MovieGenre.Romance);                 //displays 8 objects


                eglinton.PrintShows(MovieGenre.Action | MovieGenre.Romance);  //displays 3 objects

                eglinton.PrintShows("Morgan Freeman");              //displays 5 objects

                Time time = new Time(14, 05, 0);
                eglinton.PrintShows(time);                          //displays 6 objects

                eglinton.PrintShows(MovieDay.Sun, time);                 //displays 3 objects


            }
        }
    }
}
