//        IEnumerable<Tuple<int, string, string>> authors =
            //new[]
            //{
            //  Tuple.Create(1, "Isaac", "Asimov"),
            //  Tuple.Create(2, "Robert", "Heinlein"),
            //  Tuple.Create(3, "Frank", "Herbert"),
            //  Tuple.Create(4, "Aldous", "Huxley"),
            //};

            //        System.Console.WriteLine(authors.ToStringTable(
            //          new[] { "Id", "First Name", "Surname" },
            //          a => a.Item1, a => a.Item2, a => a.Item3));


			string name;

            int age;

            int birthyear;



            System.Console.Write("Please enter your name: ");

            name = System.Console.ReadLine();

            System.Console.Write("Please enter your age: ");

            age = Convert.ToInt32(System.Console.ReadLine());

            System.Console.Write("What year were you born?: ");

            birthyear = Convert.ToInt32(System.Console.ReadLine());



            //Print a blank line

            System.Console.WriteLine();



            //Show the details that the user entered

            System.Console.WriteLine("Name is {0}.", name);

            System.Console.WriteLine("Age is {0}.", age);

            System.Console.WriteLine("Birth year is {0}.", birthyear);
            System.Console.ReadLine();

            InputData();